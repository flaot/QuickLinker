namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class TabMenuItemAttribute : CustomMenuItemAttribute
    {
        public TabMenuItemAttribute(string name, int priority = 1000) : base(name, (int)Menu.MenuType.Tab, priority)
        {
        }
    }
}
