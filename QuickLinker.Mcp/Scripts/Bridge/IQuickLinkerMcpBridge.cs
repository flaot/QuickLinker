using QuickLinker.Mcp.Models;

namespace QuickLinker.Mcp.Bridge
{
    /// <summary>Implemented by QuickLinker host; MCP tools call this and never touch EntityCache.json directly.</summary>
    public interface IQuickLinkerMcpBridge
    {
        Task<IReadOnlyList<QuickLinkerEntitySummary>> ListEntitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>Launches an entry by GUID. Empty string on success, otherwise an error message.</summary>
        Task<string> LaunchByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<QuickLinkerEntitySummary>> SearchEntitiesAsync(
            string? query,
            bool searchDesc,
            bool searchPath,
            bool ignoreCase,
            CancellationToken cancellationToken = default);

        Task<QuickLinkerEntityDetail?> GetEntityDetailAsync(Guid guid, CancellationToken cancellationToken = default);

        /// <summary>Show entry target in Explorer. Empty string on success.</summary>
        Task<string> ShowInExplorerAsync(Guid guid, CancellationToken cancellationToken = default);

        /// <summary>Brings QuickLinker to foreground. Empty string on success.</summary>
        Task<string> FocusMainWindowAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<QuickLinkerEntitySummary>> ListEntitiesByFlagsAsync(
            string[]? matchAnyFlags,
            string[]? matchAllFlags,
            CancellationToken cancellationToken = default);

        Task<QuickLinkerHealthInfo> GetHealthAsync(CancellationToken cancellationToken = default);

        Task<string> LaunchByGuidWithFilesAsync(Guid guid, string[] files, CancellationToken cancellationToken = default);

        Task<string> AddEntryFromPathAsync(string path, int index, bool canParse, CancellationToken cancellationToken = default);

        Task<string> RemoveEntryByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

        Task<string> UpdateEntryByGuidAsync(Guid guid, QuickLinkerEntryPatch patch, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<QuickLinkerTabInfo>> ListTabsAsync(CancellationToken cancellationToken = default);

        Task<string> AddTabAsync(string title, CancellationToken cancellationToken = default);

        Task<string> RenameTabAsync(int pageIndex, string newTitle, CancellationToken cancellationToken = default);

        Task<string> RemoveTabAsync(int pageIndex, CancellationToken cancellationToken = default);

        Task<string> AddEntriesOnPageAsync(int pageIndex, IReadOnlyList<string> paths, bool canParse, CancellationToken cancellationToken = default);

        /// <summary>Swaps the entry at guid's cell with the entry at targetGlobalIndex (same as UI switch).</summary>
        Task<string> MoveEntryToIndexAsync(Guid guid, int targetGlobalIndex, CancellationToken cancellationToken = default);
    }
}
