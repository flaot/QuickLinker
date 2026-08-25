using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuickLinker.Mcp.Bridge;

namespace QuickLinker.Mcp
{

    /// <summary>Runs Kestrel + Streamable HTTP MCP in a background task until cancelled.</summary>
    public sealed class QuickLinkerMcpHost : IAsyncDisposable
    {
        /// <summary>Enable or disable MCP without awaiting; <paramref name="completed"/> is invoked on the captured <see cref="SynchronizationContext"/> (e.g. UI thread), or synchronously if none.</summary>
        /// <param name="completed"><c>null</c> on success, or the exception if start/stop failed.</param>
        public void SetEnabled(bool enabled, QuickLinkerMcpStartParameters? parameters = null, Action<Exception?>? completed = null)
        {
            var sync = SynchronizationContext.Current;
            _ = InvokeAsync();

            async Task InvokeAsync()
            {
                Exception? error = null;
                try
                {
                    await SetEnabledAsync(enabled, parameters).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    error = ex;
                }

                if (completed == null)
                    return;

                void Callback() => completed(error);

                if (sync != null)
                    sync.Post(_ => Callback(), null);
                else
                    Callback();
            }
        }

        /// <summary>When <paramref name="enabled"/> is false, stops the host. When true, starts with <paramref name="parameters"/> (required).</summary>
        public Task SetEnabledAsync(bool enabled, QuickLinkerMcpStartParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            if (!enabled)
                return StopAsync();

            ArgumentNullException.ThrowIfNull(parameters);
            return StartAsync(parameters.ToOptions(), parameters.Bridge, cancellationToken);
        }

        public Task StartAsync(int port, string? accessToken, IQuickLinkerMcpBridge bridge, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(bridge);
            return StartAsync(new QuickLinkerMcpOptions { Port = port, AccessToken = accessToken }, bridge, cancellationToken);
        }

        private readonly object _gate = new();
        private CancellationTokenSource? _cts;
        private Task? _runTask;

        public bool IsRunning
        {
            get
            {
                lock (_gate)
                    return _runTask is { IsCompleted: false };
            }
        }

        public async Task StartAsync(QuickLinkerMcpOptions options, IQuickLinkerMcpBridge bridge, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(bridge);
            if (options.Port is < 1 or > 65535)
                throw new ArgumentOutOfRangeException(nameof(options), options.Port, "Port must be 1–65535.");

            await StopAsync().ConfigureAwait(false);

            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Task task;
            lock (_gate)
            {
                _cts = cts;
                _runTask = task = RunWebHostAsync(options, bridge, cts.Token);
            }

            await Task.Yield();
            if (task.IsFaulted)
                await task.ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            Task? task;
            lock (_gate)
            {
                _cts?.Cancel();
                task = _runTask;
                _cts?.Dispose();
                _cts = null;
                _runTask = null;
            }

            if (task != null)
            {
                try
                {
                    await task.ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // expected when stopping
                }
                catch (Exception)
                {
                    // faulted task after failed StartAsync — do not rethrow during shutdown
                }
            }
        }

        private static async Task RunWebHostAsync(QuickLinkerMcpOptions options, IQuickLinkerMcpBridge bridge, CancellationToken ct)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = [],
                ApplicationName = typeof(QuickLinkerMcpHost).Assembly.GetName().Name ?? "QuickLinker.Mcp"
            });

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Warning);

            builder.Services.AddSingleton(bridge);
            builder.Services.AddSingleton<IQuickLinkerMcpBridge>(bridge);
            builder.Services
                .AddMcpServer()
                .WithHttpTransport(o => o.Stateless = true)
                .WithToolsFromAssembly(typeof(QuickLinkerMcpTools).Assembly);

            var app = builder.Build();
            app.Urls.Clear();
            app.Urls.Add($"http://127.0.0.1:{options.Port}");

            var token = options.AccessToken;
            if (!string.IsNullOrWhiteSpace(token))
            {
                app.Use(async (ctx, next) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/mcp", StringComparison.OrdinalIgnoreCase))
                    {
                        var ok = false;
                        if (ctx.Request.Headers.TryGetValue("Authorization", out var auth))
                        {
                            var v = auth.ToString();
                            const string prefix = "Bearer ";
                            if (v.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                            {
                                var t = v.AsSpan(prefix.Length).Trim();
                                ok = t.Equals(token.AsSpan(), StringComparison.Ordinal);
                            }
                        }
                        if (!ok && ctx.Request.Headers.TryGetValue("X-QuickLinker-Mcp-Token", out var h))
                            ok = string.Equals(h.ToString(), token, StringComparison.Ordinal);

                        if (!ok)
                        {
                            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }
                    }

                    await next().ConfigureAwait(false);
                });
            }

            app.MapMcp("/mcp");
            await app.StartAsync(ct).ConfigureAwait(false);
            var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
            using (ct.Register(lifetime.StopApplication))
            {
                try
                {
                    await Task.Delay(Timeout.Infinite, ct).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // stopping
                }
            }

            await app.StopAsync().ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync() => await StopAsync().ConfigureAwait(false);
    }
}