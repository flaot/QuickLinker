using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace QuickLinker.Mcp;

[McpServerToolType]
public sealed class QuickLinkerMcpTools(IQuickLinkerMcpBridge bridge)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
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
}
