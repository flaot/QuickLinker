namespace QuickLinker.Mcp.Models
{
    /// <summary>Full entry metadata for MCP (no icon bytes).</summary>
    public sealed record QuickLinkerEntityDetail(
        int Index,
        Guid Guid,
        string Desc,
        string Path,
        int IconType,
        IReadOnlyList<string> Flags,
        bool AdminStartUp,
        bool LaunchOnStartup,
        bool DropNLaunch,
        bool CloseSoft,
        string StartArg,
        string WorkFolder,
        string ActionHotKey,
        int WindowStyle,
        int PriorityClass,
        string RelativePath,
        string ShellItemPath,
        bool CanParse);
}
