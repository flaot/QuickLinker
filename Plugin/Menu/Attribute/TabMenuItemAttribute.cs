namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class TabMenuItemAttribute : CustomMenuItemAttribute
    {
        public TabMenuItemAttribute(string menuKey, int priority = 1000) : base(menuKey, (int)Menu.MenuType.Tab, priority)
        {
        }
    }
}
