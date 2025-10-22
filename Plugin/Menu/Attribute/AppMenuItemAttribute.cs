namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AppMenuItemAttribute : CustomMenuItemAttribute
    {
        public AppMenuItemAttribute(string name, int priority = 1000) : base(name, (int)Menu.MenuType.App, priority)
        {
        }
    }
}
