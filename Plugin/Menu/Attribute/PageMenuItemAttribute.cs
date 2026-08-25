namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class PageMenuItemAttribute : CustomMenuItemAttribute
    {
        public PageMenuItemAttribute(string menuKey, int priority = 1000) : base(menuKey, (int)Menu.MenuType.Page, priority)
        {
        }
    }
}
