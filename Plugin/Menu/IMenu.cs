namespace QuickLinker.Plugin.Menu
{
    /// <summary>
    /// 菜单对象的接口
    /// </summary>
    public interface IMenu
    {
        int MenuType { get; }
        int Count { get; }
        List<MenuItem> AllChild { get; }
        void Show(int x, int y);
        void Clear();
        void AddSeparator(MenuItem root);
        MenuItem FindItem(MenuItem root, string name);
        void AddItem(MenuItem root, MenuItem sub);
        void ReplaceItem(MenuItem item, MenuItem.Info info);
        void SetCheck(MenuItem item, bool isCheck);
        void SetEnable(MenuItem item, bool isEnable);
        void SetVisible(MenuItem item, bool isVisible);
    }
}
