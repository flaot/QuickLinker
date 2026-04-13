using ModelContextProtocol.Server;
using QuickLinker.Mcp.Bridge;
using QuickLinker.Mcp.Models;
using System.ComponentModel;
using System.Text.Json;

namespace QuickLinker.Mcp
{
    [McpServerToolType]
    public sealed class QuickLinkerMcpTools(IQuickLinkerMcpBridge bridge)
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static readonly JsonSerializerOptions PatchJsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        [McpServerTool]
        [Description(
            "Lists QuickLinker launch entries (index, guid, desc, path, iconType, flags). Does not include icon bytes. " +
            "The path field is for identification only: to start an entry you MUST call LaunchQuickLinkerEntry with that entry's guid. " +
            "Never launch by executing path via shell, terminal, run_command, or any external process.")]
        public async Task<string> ListQuickLinkerEntities(CancellationToken cancellationToken)
        {
            var list = await bridge.ListEntitiesAsync(cancellationToken).ConfigureAwait(false);
            return JsonSerializer.Serialize(list, JsonOptions);
        }

        [McpServerTool]
        [Description(
            "Starts a QuickLinker entry. This is the only supported MCP way to launch; use the guid from ListQuickLinkerEntities. " +
            "Same behavior as the app URI handler (working directory, args, protocol paths). Do not bypass this by running path directly.")]
        public async Task<string> LaunchQuickLinkerEntry(
            [Description("Entry guid string exactly as returned by ListQuickLinkerEntities (guid field).")] string guid,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            var err = await bridge.LaunchByGuidAsync(g, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "Launched." : err;
        }

        [McpServerTool]
        [Description("Filters entries by substring in description and/or path. Empty query returns all entries.")]
        public async Task<string> SearchQuickLinkerEntries(
            [Description("Substring to match; empty returns all.")] string? query,
            [Description("Match against description text.")] bool searchDesc = true,
            [Description("Match against path text.")] bool searchPath = true,
            [Description("Use case-insensitive matching.")] bool ignoreCase = true,
            CancellationToken cancellationToken = default)
        {
            var list = await bridge.SearchEntitiesAsync(query, searchDesc, searchPath, ignoreCase, cancellationToken)
                .ConfigureAwait(false);
            return JsonSerializer.Serialize(list, JsonOptions);
        }

        [McpServerTool]
        [Description("Returns full metadata for one entry (no icon bytes). Use guid from ListQuickLinkerEntities.")]
        public async Task<string> GetQuickLinkerEntryDetail(
            [Description("Entry guid.")] string guid,
            CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            var detail = await bridge.GetEntityDetailAsync(g, cancellationToken).ConfigureAwait(false);
            return detail == null ? "Entry not found for GUID." : JsonSerializer.Serialize(detail, JsonOptions);
        }

        [McpServerTool]
        [Description("Opens File Explorer at the entry target (same as in-app Show in Explorer).")]
        public async Task<string> ShowQuickLinkerEntryInExplorer(
            [Description("Entry guid.")] string guid,
            CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            var err = await bridge.ShowInExplorerAsync(g, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description("Shows and activates the QuickLinker main window (e.g. after tray minimize).")]
        public async Task<string> FocusQuickLinkerWindow(CancellationToken cancellationToken = default)
        {
            var err = await bridge.FocusMainWindowAsync(cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description(
            "Lists entries filtered by flags. If matchAllFlags is non-empty, every listed flag must be present. " +
            "Else if matchAnyFlags is non-empty, at least one must match. If both empty, returns all entries.")]
        public async Task<string> ListQuickLinkerEntriesByFlags(
            [Description("Optional: entry must contain all of these flags.")] string[]? matchAllFlags = null,
            [Description("Optional: entry must contain at least one of these flags (used when matchAllFlags is empty).")]
        string[]? matchAnyFlags = null,
            CancellationToken cancellationToken = default)
        {
            var list = await bridge.ListEntitiesByFlagsAsync(matchAnyFlags, matchAllFlags, cancellationToken)
                .ConfigureAwait(false);
            return JsonSerializer.Serialize(list, JsonOptions);
        }

        [McpServerTool]
        [Description("Health probe: host version, entry count, MCP port, token configured, MCP enabled flag.")]
        public async Task<string> GetQuickLinkerHealth(CancellationToken cancellationToken = default)
        {
            var h = await bridge.GetHealthAsync(cancellationToken).ConfigureAwait(false);
            return JsonSerializer.Serialize(h, JsonOptions);
        }

        [McpServerTool]
        [Description(
            "Launches an entry with dropped file/folder paths (for drop-aware shortcuts). " +
            "Otherwise same as LaunchQuickLinkerEntry; never run path via external shell.")]
        public async Task<string> LaunchQuickLinkerEntryWithFiles(
            [Description("Entry guid.")] string guid,
            [Description("File or folder paths passed as drop targets.")] string[] files,
            CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            var err = await bridge.LaunchByGuidWithFilesAsync(g, files ?? Array.Empty<string>(), cancellationToken)
                .ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "Launched." : err;
        }

        [McpServerTool]
        [Description(
            "Adds a new launch entry from a file path (exe, lnk, etc.). Writes configuration. " +
            "index: global grid index; use ListQuickLinkerTabs for page ranges, or -1 for first free slot.")]
        public async Task<string> AddQuickLinkerEntryFromPath(
            [Description("Path to executable or shortcut.")] string path,
            [Description("Global cell index, or -1 for auto.")] int index = -1,
            [Description("Parse .lnk/.url targets when true.")] bool canParse = true,
            CancellationToken cancellationToken = default)
        {
            var err = await bridge.AddEntryFromPathAsync(path ?? string.Empty, index, canParse, cancellationToken)
                .ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "Added." : err;
        }

        [McpServerTool]
        [Description("Removes the entry with the given guid. Destructive; cannot be undone via MCP.")]
        public async Task<string> RemoveQuickLinkerEntry(
            [Description("Entry guid.")] string guid,
            CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            var err = await bridge.RemoveEntryByGuidAsync(g, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "Removed." : err;
        }

        [McpServerTool]
        [Description(
            "Updates an entry. patchJson is a JSON object with optional camelCase keys: " +
            "desc, path, startArg, workFolder, actionHotKey, adminStartUp, dropNLaunch, launchOnStartup, closeSoft, " +
            "windowStyle (0-3), priorityClass (0-5), flags (string array). Omitted keys are left unchanged.")]
        public async Task<string> UpdateQuickLinkerEntry(
            [Description("Entry guid.")] string guid,
            [Description("JSON object with fields to change.")] string patchJson,
            CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            if (string.IsNullOrWhiteSpace(patchJson))
                return "patchJson is empty.";
            QuickLinkerEntryPatch? patch;
            try
            {
                patch = JsonSerializer.Deserialize<QuickLinkerEntryPatch>(patchJson, PatchJsonOptions);
            }
            catch (JsonException ex)
            {
                return "Invalid JSON: " + ex.Message;
            }
            if (patch == null)
                return "Patch deserialized to null.";
            var err = await bridge.UpdateEntryByGuidAsync(g, patch, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "Updated." : err;
        }

        [McpServerTool]
        [Description("Lists tab titles and each page global index range (cellsPerPage = gridRow * gridColumn).")]
        public async Task<string> ListQuickLinkerTabs(CancellationToken cancellationToken = default)
        {
            var tabs = await bridge.ListTabsAsync(cancellationToken).ConfigureAwait(false);
            return JsonSerializer.Serialize(tabs, JsonOptions);
        }

        [McpServerTool]
        [Description("Appends a new tab (page) with the given title. Syncs grid group count.")]
        public async Task<string> AddQuickLinkerTab(
            [Description("Tab title shown in UI.")] string title,
            CancellationToken cancellationToken = default)
        {
            var err = await bridge.AddTabAsync(title ?? string.Empty, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description("Renames an existing tab by zero-based pageIndex (see ListQuickLinkerTabs).")]
        public async Task<string> RenameQuickLinkerTab(
            [Description("Zero-based page index.")] int pageIndex,
            [Description("New tab title.")] string newTitle,
            CancellationToken cancellationToken = default)
        {
            var err = await bridge.RenameTabAsync(pageIndex, newTitle ?? string.Empty, cancellationToken)
                .ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description(
            "Removes a tab by pageIndex (same semantics as in-app tab delete: may remove last physical page from UI). " +
            "Cannot remove the last remaining tab.")]
        public async Task<string> RemoveQuickLinkerTab(
            [Description("Zero-based page index.")] int pageIndex,
            CancellationToken cancellationToken = default)
        {
            var err = await bridge.RemoveTabAsync(pageIndex, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description(
            "Inserts entries from paths into consecutive cells starting at the top-left of pageIndex. " +
            "At most cellsPerPage paths; overwrites existing cells like the UI insert.")]
        public async Task<string> AddQuickLinkerEntriesOnPage(
            [Description("Zero-based page index.")] int pageIndex,
            [Description("Paths to add in order (row-major).")] string[] paths,
            [Description("Parse shortcuts when true.")] bool canParse = true,
            CancellationToken cancellationToken = default)
        {
            var err = await bridge
                .AddEntriesOnPageAsync(pageIndex, paths ?? Array.Empty<string>(), canParse, cancellationToken)
                .ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description(
            "Swaps the entry identified by guid with whatever occupies targetGlobalIndex (same as UI switch two cells).")]
        public async Task<string> MoveQuickLinkerEntryToIndex(
            [Description("Entry guid.")] string guid,
            [Description("Target global grid index.")] int targetGlobalIndex,
            CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(guid, out var g))
                return "Invalid GUID.";
            var err = await bridge.MoveEntryToIndexAsync(g, targetGlobalIndex, cancellationToken).ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }

        [McpServerTool]
        [Description(
            "If a tab with the given title does not exist, creates it; then adds paths starting at the top-left of that page. " +
            "If the tab already exists, only adds/replaces cells on that page.")]
        public async Task<string> EnsureQuickLinkerTabWithPaths(
            [Description("Tab title to find or create.")] string tabTitle,
            [Description("Paths to place on that page in order.")] string[] paths,
            [Description("When matching an existing tab title, ignore case.")] bool ignoreCase = true,
            [Description("Parse shortcuts when true.")] bool canParse = true,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(tabTitle))
                return "tabTitle is empty.";
            if (paths == null || paths.Length == 0)
                return "paths is empty.";
            var tabs = await bridge.ListTabsAsync(cancellationToken).ConfigureAwait(false);
            var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var match = tabs.FirstOrDefault(t => string.Equals(t.Title, tabTitle.Trim(), comparison));
            if (match == null)
            {
                var addErr = await bridge.AddTabAsync(tabTitle.Trim(), cancellationToken).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(addErr))
                    return addErr;
                tabs = await bridge.ListTabsAsync(cancellationToken).ConfigureAwait(false);
                match = tabs.FirstOrDefault(t => string.Equals(t.Title, tabTitle.Trim(), comparison));
                if (match == null)
                    return "Tab not found after AddQuickLinkerTab.";
            }
            var err = await bridge.AddEntriesOnPageAsync(match.PageIndex, paths, canParse, cancellationToken)
                .ConfigureAwait(false);
            return string.IsNullOrEmpty(err) ? "OK." : err;
        }
    }
}
