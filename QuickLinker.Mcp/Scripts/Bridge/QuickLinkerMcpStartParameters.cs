namespace QuickLinker.Mcp.Bridge
{
    /// <summary>Parameters required to enable MCP; maps to <see cref="QuickLinkerMcpOptions"/> plus the host bridge.</summary>
    public sealed class QuickLinkerMcpStartParameters
    {
        public QuickLinkerMcpStartParameters(int port, string? accessToken, IQuickLinkerMcpBridge bridge)
        {
            Bridge = bridge ?? throw new ArgumentNullException(nameof(bridge));
            Port = port;
            AccessToken = accessToken;
        }

        public int Port { get; }

        public string? AccessToken { get; }

        public IQuickLinkerMcpBridge Bridge { get; }

        public QuickLinkerMcpOptions ToOptions() => new() { Port = Port, AccessToken = AccessToken };
    }

}