namespace QuickLinker.Mcp;

/// <summary>Implemented by QuickLinker host; MCP tools call this and never touch EntityCache.json.</summary>
public interface IQuickLinkerMcpBridge
{
    Task<IReadOnlyList<QuickLinkerEntitySummary>> ListEntitiesAsync(CancellationToken cancellationToken = default);

    /// <summary>Launches an entry by GUID. Empty string on success, otherwise an error message.</summary>
    Task<string> LaunchByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
}
