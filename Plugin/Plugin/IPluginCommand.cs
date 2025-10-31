namespace QuickLinker.Plugin
{
    /// <summary>
    /// 自定义命令 支持URI执行
    /// </summary>
    public interface IPluginCommand
    {
        string Name { get; }
        string Description { get; }
        string Icon { get; }
        void Action();
    }
}
