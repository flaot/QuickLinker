namespace QuickLinker.Mcp.Models
{
    public sealed record QuickLinkerHealthInfo(
        string HostAssemblyVersion,
        string McpAssemblyVersion,
        int EntryCount,
        int McpPort,
        bool McpTokenConfigured,
        bool McpEnabled);
}