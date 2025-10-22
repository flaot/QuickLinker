namespace QuickLinker.Plugin.Menu.Attribute
{
    /// <summary>
    /// 自定义菜单项 优先级越高越后面
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public abstract class CustomMenuItemAttribute : System.Attribute
    {
        public string Name { get; }
        public int Priority { get; set; }
        public int MenuType { get; set; }
        public CustomMenuItemAttribute(string name, int menuType, int priority = 1000)
        {
            MenuType = menuType;
            Name = name;
            Priority = priority;
        }
    }
}
