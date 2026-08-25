namespace QuickLinker.Plugin.Menu
{
    /// <summary>
    /// 菜单显示文本多语言接口
    /// </summary>
    public interface IMenuLang
    {
        /// <summary> 先大后小 </summary>
        int Priority { get; }
        string GetLang(string menuPathKey);
    }
}
