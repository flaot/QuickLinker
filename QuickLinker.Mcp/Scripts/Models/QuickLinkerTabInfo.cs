namespace QuickLinker.Mcp.Models
{
    public sealed record QuickLinkerTabInfo(
        int PageIndex,
        string Title,
        int FirstGlobalIndex,
        int LastGlobalIndexInclusive,
        int CellsPerPage);
}