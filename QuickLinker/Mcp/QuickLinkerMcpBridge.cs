using QFramework;
using QuickLinker.Mcp.Bridge;
using QuickLinker.Mcp.Models;
using QuickLinker.Model;
using QuickLinker.Plugin;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

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

        public Task<IReadOnlyList<QuickLinkerEntitySummary>> ListEntitiesAsync(CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var raw = system.QueryDataWithAnyFlag(Array.Empty<string>());
                    raw.Sort((a, b) => a.index.CompareTo(b.index));
                    var list = new List<QuickLinkerEntitySummary>(raw.Count);
                    foreach (var entity in raw)
                        list.Add(ToSummary(entity));
                    return (IReadOnlyList<QuickLinkerEntitySummary>)list;
                },
                cancellationToken,
                Array.Empty<QuickLinkerEntitySummary>());

        public Task<string> LaunchByGuidAsync(Guid guid, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                        return "Entry not found for GUID.";
                    Selection.activeContext = null;
                    Selection.activeEntity = entity;
                    _form.GetArchitecture().SendCommand(new QuickEntityOpenCommand { index = entity.index });
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<IReadOnlyList<QuickLinkerEntitySummary>> SearchEntitiesAsync(
            string? query,
            bool searchDesc,
            bool searchPath,
            bool ignoreCase,
            CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var raw = system.QueryDataWithAnyFlag(Array.Empty<string>());
                    raw.Sort((a, b) => a.index.CompareTo(b.index));
                    if (string.IsNullOrWhiteSpace(query))
                    {
                        var all = new List<QuickLinkerEntitySummary>(raw.Count);
                        foreach (var e in raw)
                            all.Add(ToSummary(e));
                        return (IReadOnlyList<QuickLinkerEntitySummary>)all;
                    }

                    var q = query.Trim();
                    var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                    var filtered = new List<QuickLinkerEntitySummary>();
                    foreach (var entity in raw)
                    {
                        var match = false;
                        if (searchDesc && (entity.desc ?? string.Empty).IndexOf(q, comparison) >= 0)
                            match = true;
                        if (searchPath && (entity.Path ?? string.Empty).IndexOf(q, comparison) >= 0)
                            match = true;
                        if (match)
                            filtered.Add(ToSummary(entity));
                    }
                    return (IReadOnlyList<QuickLinkerEntitySummary>)filtered;
                },
                cancellationToken,
                Array.Empty<QuickLinkerEntitySummary>());

        public Task<QuickLinkerEntityDetail?> GetEntityDetailAsync(Guid guid, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    return entity == null ? null : ToDetail(entity);
                },
                cancellationToken,
                null);

        public Task<string> ShowInExplorerAsync(Guid guid, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                        return "Entry not found for GUID.";
                    _form.GetArchitecture().SendCommand(new QuickEntityShowInExploreCommand { index = entity.index });
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> FocusMainWindowAsync(CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    _form.ShowMainWindow();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<IReadOnlyList<QuickLinkerEntitySummary>> ListEntitiesByFlagsAsync(
            string[]? matchAnyFlags,
            string[]? matchAllFlags,
            CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    List<Entity> raw;
                    var all = matchAllFlags != null && matchAllFlags.Length > 0;
                    var any = matchAnyFlags != null && matchAnyFlags.Length > 0;
                    if (all)
                        raw = system.QueryDataWithAllFlags(matchAllFlags);
                    else if (any)
                        raw = system.QueryDataWithAnyFlag(matchAnyFlags!);
                    else
                        raw = system.QueryDataWithAnyFlag(Array.Empty<string>());
                    raw.Sort((a, b) => a.index.CompareTo(b.index));
                    var list = new List<QuickLinkerEntitySummary>(raw.Count);
                    foreach (var entity in raw)
                        list.Add(ToSummary(entity));
                    return (IReadOnlyList<QuickLinkerEntitySummary>)list;
                },
                cancellationToken,
                Array.Empty<QuickLinkerEntitySummary>());

        public Task<QuickLinkerHealthInfo> GetHealthAsync(CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var config = _form.GetModel<AppConfig>();
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var count = system.QueryDataWithAnyFlag(Array.Empty<string>()).Count;
                    var hostVer = typeof(MainForm).Assembly.GetName().Version?.ToString() ?? "?";
                    var mcpVer = typeof(IQuickLinkerMcpBridge).Assembly.GetName().Version?.ToString() ?? "?";
                    var token = config.mcpToken.Value;
                    return new QuickLinkerHealthInfo(
                        hostVer,
                        mcpVer,
                        count,
                        config.mcpPort.Value,
                        !string.IsNullOrWhiteSpace(token),
                        config.mcpEnabled.Value);
                },
                cancellationToken,
                new QuickLinkerHealthInfo("?", "?", 0, 0, false, false));

        public Task<string> LaunchByGuidWithFilesAsync(Guid guid, string[] files, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                        return "Entry not found for GUID.";
                    Selection.activeContext = null;
                    Selection.activeEntity = entity;
                    _form.GetArchitecture().SendCommand(new QuickEntityOpenCommand
                    {
                        index = entity.index,
                        dropFileOrDirs = files ?? Array.Empty<string>()
                    });
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> AddEntryFromPathAsync(string path, int index, bool canParse, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(path))
                        return "Path is empty.";
                    _form.GetArchitecture().SendCommand(new QuickEntityInsertCommand
                    {
                        filePath = path.Trim(),
                        index = index,
                        canParse = canParse
                    });
                    ReloadQuickActionsHotKeys();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> RemoveEntryByGuidAsync(Guid guid, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                        return "Entry not found for GUID.";
                    _form.GetArchitecture().SendCommand(new QuickEntityRemoveCommand { index = entity.index });
                    ReloadQuickActionsHotKeys();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> UpdateEntryByGuidAsync(Guid guid, QuickLinkerEntryPatch patch, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    if (patch == null)
                        return "Patch is null.";
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                        return "Entry not found for GUID.";
                    var index = entity.index;
                    var cmd = new QuickEntitySetCommand { index = index };
                    if (patch.Desc != null)
                        cmd.desc = patch.Desc;
                    if (patch.Path != null)
                        cmd.filePath = patch.Path;
                    if (patch.StartArg != null)
                        cmd.startArg = patch.StartArg;
                    if (patch.WorkFolder != null)
                        cmd.workFolder = patch.WorkFolder;
                    if (patch.ActionHotKey != null)
                        cmd.actionHotKey = patch.ActionHotKey;
                    if (patch.AdminStartUp.HasValue)
                        cmd.adminStartUp = patch.AdminStartUp;
                    if (patch.DropNLaunch.HasValue)
                        cmd.dropNLaunch = patch.DropNLaunch;
                    if (patch.LaunchOnStartup.HasValue)
                        cmd.launchOnStartup = patch.LaunchOnStartup;
                    if (patch.CloseSoft.HasValue)
                        cmd.closeSoft = patch.CloseSoft;
                    if (patch.WindowStyle.HasValue)
                    {
                        var ws = patch.WindowStyle.Value;
                        if (ws < 0 || ws > 3)
                            return "windowStyle must be 0–3 (Normal, Minimized, Maximized, Hidden).";
                        cmd.windowStyle = (WindowStyle)ws;
                    }
                    if (patch.PriorityClass.HasValue)
                    {
                        var pc = patch.PriorityClass.Value;
                        if (pc < 0 || pc > 5)
                            return "priorityClass must be 0–5 (RealTime..Idle).";
                        cmd.priorityClass = (PriorityClass)pc;
                    }
                    var hasScalar = patch.Desc != null || patch.Path != null || patch.StartArg != null || patch.WorkFolder != null
                        || patch.ActionHotKey != null || patch.AdminStartUp.HasValue || patch.DropNLaunch.HasValue
                        || patch.LaunchOnStartup.HasValue || patch.CloseSoft.HasValue || patch.WindowStyle.HasValue
                        || patch.PriorityClass.HasValue;
                    if (hasScalar)
                        _form.GetArchitecture().SendCommand(cmd);
                    if (patch.Flags != null)
                    {
                        _form.GetArchitecture().SendCommand(new QuickEntitySetFlagCommand
                        {
                            index = index,
                            flags = patch.Flags
                        });
                    }
                    if (!hasScalar && patch.Flags == null)
                        return "No patch fields set.";
                    ReloadQuickActionsHotKeys();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<IReadOnlyList<QuickLinkerTabInfo>> ListTabsAsync(CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var config = _form.GetModel<AppConfig>();
                    var cells = config.gridRow.Value * config.gridColumn.Value;
                    var names = config.groupArray.Value;
                    var list = new List<QuickLinkerTabInfo>(names.Length);
                    for (var i = 0; i < names.Length; i++)
                    {
                        var first = i * cells;
                        list.Add(new QuickLinkerTabInfo(i, names[i] ?? string.Empty, first, first + cells - 1, cells));
                    }
                    return (IReadOnlyList<QuickLinkerTabInfo>)list;
                },
                cancellationToken,
                Array.Empty<QuickLinkerTabInfo>());

        public Task<string> AddTabAsync(string title, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(title))
                        return "Title is empty.";
                    var config = _form.GetModel<AppConfig>();
                    var list = config.groupArray.Value.ToList();
                    list.Add(title.Trim());
                    config.gridGroup.SetValueWithoutEvent(list.Count);
                    config.groupArray.Value = list.ToArray();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> RenameTabAsync(int pageIndex, string newTitle, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(newTitle))
                        return "Title is empty.";
                    var config = _form.GetModel<AppConfig>();
                    var arr = config.groupArray.Value;
                    if (pageIndex < 0 || pageIndex >= arr.Length)
                        return "Invalid pageIndex.";
                    var temp = new string[arr.Length];
                    Array.Copy(arr, temp, arr.Length);
                    temp[pageIndex] = newTitle.Trim();
                    config.groupArray.Value = temp;
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> RemoveTabAsync(int pageIndex, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var config = _form.GetModel<AppConfig>();
                    var arr = config.groupArray.Value;
                    if (pageIndex < 0 || pageIndex >= arr.Length)
                        return "Invalid pageIndex.";
                    if (arr.Length <= 1)
                        return "Cannot remove the last tab.";
                    var list = arr.ToList();
                    list.RemoveAt(pageIndex);
                    config.gridGroup.SetValueWithoutEvent(list.Count);
                    config.groupArray.Value = list.ToArray();
                    ReloadQuickActionsHotKeys();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> AddEntriesOnPageAsync(int pageIndex, IReadOnlyList<string> paths, bool canParse, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    if (paths == null || paths.Count == 0)
                        return "No paths.";
                    var config = _form.GetModel<AppConfig>();
                    var cells = config.gridRow.Value * config.gridColumn.Value;
                    if (pageIndex < 0 || pageIndex >= config.groupArray.Value.Length)
                        return "Invalid pageIndex.";
                    if (paths.Count > cells)
                        return $"At most {cells} paths per page.";
                    var arch = _form.GetArchitecture();
                    var start = pageIndex * cells;
                    for (var i = 0; i < paths.Count; i++)
                    {
                        var p = paths[i];
                        if (string.IsNullOrWhiteSpace(p))
                            return $"Path at index {i} is empty.";
                        arch.SendCommand(new QuickEntityInsertCommand
                        {
                            filePath = p.Trim(),
                            index = start + i,
                            canParse = canParse
                        });
                    }
                    ReloadQuickActionsHotKeys();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        public Task<string> MoveEntryToIndexAsync(Guid guid, int targetGlobalIndex, CancellationToken cancellationToken = default) =>
            InvokeAsync(
                () =>
                {
                    var system = _form.GetSystem<QuickEntitySystem>();
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                        return "Entry not found for GUID.";
                    if (targetGlobalIndex < 0)
                        return "Invalid target index.";
                    if (entity.index == targetGlobalIndex)
                        return string.Empty;
                    _form.GetArchitecture().SendCommand(new QuickEntitySwitchCommand
                    {
                        srcIndex = entity.index,
                        desIndex = targetGlobalIndex
                    });
                    ReloadQuickActionsHotKeys();
                    return string.Empty;
                },
                cancellationToken,
                string.Empty,
                "Application is closing.");

        /// <summary>Rebind global and per-entry hotkeys after entity list or <see cref="Entity.actionHotKey"/> changes.</summary>
        private void ReloadQuickActionsHotKeys() =>
            _form.GetSystem<HotKeyManager>().ReloadQuickActionsHotKeys();

        private Task<T> InvokeAsync<T>(Func<T> work, CancellationToken cancellationToken, T disposedOrClosingDefault)
        {
            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            void Work()
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (_form.IsDisposed)
                    {
                        tcs.TrySetResult(disposedOrClosingDefault);
                        return;
                    }
                    tcs.TrySetResult(work());
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

        private Task<string> InvokeAsync(Func<string> work, CancellationToken cancellationToken, string disposedOk, string closingMessage)
        {
            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            void Work()
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (_form.IsDisposed)
                    {
                        tcs.TrySetResult(string.IsNullOrEmpty(closingMessage) ? disposedOk : closingMessage);
                        return;
                    }
                    tcs.TrySetResult(work());
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

        private static QuickLinkerEntityDetail ToDetail(Entity entity)
        {
            var flags = entity.flags == null || entity.flags.Length == 0
                ? Array.Empty<string>()
                : (IReadOnlyList<string>)Array.AsReadOnly((string[])entity.flags.Clone());
            return new QuickLinkerEntityDetail(
                entity.index,
                entity.guid,
                entity.desc ?? string.Empty,
                entity.Path ?? string.Empty,
                (int)entity.iconType,
                flags,
                entity.adminStartUp,
                entity.launchOnStartup,
                entity.dropNLaunch,
                entity.closeSoft,
                entity.startArg ?? string.Empty,
                entity.workFolder ?? string.Empty,
                entity.actionHotKey ?? string.Empty,
                (int)entity.windowStyle,
                (int)entity.priorityClass,
                entity.RelativePath ?? string.Empty,
                entity.ShellItemPath ?? string.Empty,
                entity.canParse);
        }
    }
}
