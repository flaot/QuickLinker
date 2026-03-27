namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class FolderMenuItemAttribute : CustomMenuItemAttribute
    {
        public FolderMenuItemAttribute(string menuKey, int priority = 1000) : base(menuKey, (int)Menu.MenuType.Folder, priority)
        {
        }
    }
}
