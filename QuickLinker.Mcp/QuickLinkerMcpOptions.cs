namespace QuickLinker.Mcp;

public sealed class QuickLinkerMcpOptions
{
    /// <summary>TCP port; listener uses 127.0.0.1 only.</summary>
    public int Port { get; init; } = 37842;

    /// <summary>If set, require Authorization: Bearer &lt;token&gt; or X-QuickLinker-Mcp-Token header for /mcp.</summary>
    public string? AccessToken { get; init; }
}
