namespace QuickLinker.Plugin.Menu.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ToolStatusMenuItemAttribute : CustomMenuItemAttribute
    {
        public ToolStatusMenuItemAttribute(string name, int priority = 1000) : base(name, (int)Menu.MenuType.ToolStatus, priority)
        {
        }
    }
}
