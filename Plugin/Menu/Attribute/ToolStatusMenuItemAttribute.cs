namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ToolStatusMenuItemAttribute : CustomMenuItemAttribute
    {
        public ToolStatusMenuItemAttribute(string menuKey, int priority = 1000) : base(menuKey, (int)Menu.MenuType.ToolStatus, priority)
        {
        }
    }
}
