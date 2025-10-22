namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class PageMenuItemAttribute : CustomMenuItemAttribute
    {
        public PageMenuItemAttribute(string name, int priority = 1000) : base(name, (int)Menu.MenuType.Page, priority)
        {
        }
    }
}
