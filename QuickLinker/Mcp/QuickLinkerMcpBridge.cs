using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QFramework;
using QuickLinker.Mcp;
using QuickLinker.Plugin;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker
{
    /// <summary>Marshals MCP tool calls to the WinForms UI thread and QuickEntitySystem.</summary>
    internal sealed class QuickLinkerMcpBridge : IQuickLinkerMcpBridge
    {
        private readonly MainForm _form;

        public QuickLinkerMcpBridge(MainForm form)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
        }

        public Task<IReadOnlyList<QuickLinkerEntitySummary>> ListEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<IReadOnlyList<QuickLinkerEntitySummary>>();
            void Work()
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (_form.IsDisposed)
                    {
                        tcs.TrySetResult(Array.Empty<QuickLinkerEntitySummary>());
                        return;
                    }

                    var system = _form.GetSystem<QuickEntitySystem>();
                    var raw = system.QueryDataWithAnyFlag(Array.Empty<string>());
                    raw.Sort((a, b) => a.index.CompareTo(b.index));
                    var list = new List<QuickLinkerEntitySummary>(raw.Count);
                    foreach (var entity in raw)
                        list.Add(ToSummary(entity));
                    tcs.TrySetResult(list);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }

            if (_form.InvokeRequired)
                _form.BeginInvoke((Action)Work);
            else
                Work();
            return tcs.Task;
        }

        public Task<string> LaunchByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<string>();
            void Work()
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (_form.IsDisposed)
                    {
                        tcs.TrySetResult("Application is closing.");
                        return;
                    }

                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                    {
                        tcs.TrySetResult("Entry not found for GUID.");
                        return;
                    }

                    Selection.activeContext = null;
                    Selection.activeEntity = entity;
                    _form.GetArchitecture().SendCommand(new QuickEntityOpenCommand { index = entity.index });
                    tcs.TrySetResult(string.Empty);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }

            if (_form.InvokeRequired)
                _form.BeginInvoke((Action)Work);
            else
                Work();
            return tcs.Task;
        }

        private static QuickLinkerEntitySummary ToSummary(Entity entity)
        {
            var flags = entity.flags == null || entity.flags.Length == 0
                ? Array.Empty<string>()
                : (IReadOnlyList<string>)Array.AsReadOnly((string[])entity.flags.Clone());
            return new QuickLinkerEntitySummary(
                entity.index,
                entity.guid,
                entity.desc ?? string.Empty,
                entity.Path ?? string.Empty,
                (int)entity.iconType,
                flags);
        }
    }
}
