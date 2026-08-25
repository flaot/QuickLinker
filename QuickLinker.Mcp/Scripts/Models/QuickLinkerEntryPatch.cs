namespace QuickLinker.Mcp.Models
{
    /// <summary>Optional fields for UpdateQuickLinkerEntry; JSON keys camelCase.</summary>
    public sealed class QuickLinkerEntryPatch
    {
        public string? Desc { get; set; }
        public string? Path { get; set; }
        public string? StartArg { get; set; }
        public string? WorkFolder { get; set; }
        public string? ActionHotKey { get; set; }
        public bool? AdminStartUp { get; set; }
        public bool? DropNLaunch { get; set; }
        public bool? LaunchOnStartup { get; set; }
        public bool? CloseSoft { get; set; }
        public int? WindowStyle { get; set; }
        public int? PriorityClass { get; set; }
        public string[]? Flags { get; set; }
    }
}