namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class FolderMenuItemAttribute : CustomMenuItemAttribute
    {
        public FolderMenuItemAttribute(string name, int priority = 1000) : base(name, (int)Menu.MenuType.Folder, priority)
        {
        }
    }
}
