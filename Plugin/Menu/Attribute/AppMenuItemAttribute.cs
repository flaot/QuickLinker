namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AppMenuItemAttribute : CustomMenuItemAttribute
    {
        public AppMenuItemAttribute(string nameKey, int priority = 1000) : base(nameKey, (int)Menu.MenuType.App, priority)
        {
        }
    }
}
