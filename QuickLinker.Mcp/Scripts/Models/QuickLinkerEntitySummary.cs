namespace QuickLinker.Mcp.Models
{
    /// <summary>Launch entry summary for MCP (no binary icon payload).</summary>
    public sealed record QuickLinkerEntitySummary(
        int Index,
        Guid Guid,
        string Desc,
        string Path,
        int IconType,
        IReadOnlyList<string> Flags);
}
