using QuickLinker.Plugin.Menu;
using QuickLinker.Plugin.Menu.Attribute;
using System.Reflection;

namespace QuickLinker.Plugin
{
    public class PluginObj : IPlugin
    {
        /// <summary> 插件信息 </summary>
        public PluginModel model;
        /// <summary> 插件全路径 </summary>
        public string dllPath;
        /// <summary> dll程序集 </summary>
        public Assembly assembly;
        /// <summary> 插件的类型 </summary>
        public Type pluginType;
        /// <summary> 插件的对象 </summary>
        public IPlugin pluginObj;
        /// <summary> 是否已附加 </summary>
        public bool isAttach;
        /// <summary> 加载插件失败文本 </summary>
        public string error;
        /// <summary> 插件支持的菜单 </summary>
        public List<MenuItem.Info> menuItems;
        /// <summary> 插件支持的命令 </summary>
        public List<IPluginCommand> commands;

        public void Attach()
        {
            if (isAttach)
                return;
            if (!File.Exists(dllPath))
            {
                error = "dll不存在:" + dllPath;
                return;
            }
            Assembly assembly = null;
            if (dllPath == Assembly.GetExecutingAssembly().Location)
                assembly = Assembly.GetExecutingAssembly();
            if (assembly == null)
                assembly = Assembly.LoadFrom(dllPath);
            if (assembly == null)
            {
                error = "assembly加载失败:" + dllPath;
                return;
            }
            string mainClass = Path.GetFileNameWithoutExtension(dllPath) + ".Main";
            var type = assembly.GetType(mainClass);
            if (type == null)
            {
                error = "找不到主入口:" + mainClass;
                return;
            }

            if (!typeof(IPlugin).IsAssignableFrom(type))
            {
                error = "主入口未继承IPlugin:" + mainClass;
                return;
            }

            isAttach = true;
            this.assembly = assembly;
            pluginType = type;
            pluginObj = (IPlugin)Activator.CreateInstance(type);
            pluginObj.Attach();
            //加载菜单
            foreach (var methodInfo in pluginType.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                var attr = methodInfo.GetCustomAttribute<CustomMenuItemAttribute>();
                if (attr == null)
                    continue;
                MenuItem.Info menuInfo = new MenuItem.Info();
                menuInfo.namePath = attr.Key;
                menuInfo.priority = attr.Priority;
                menuInfo.MethodInfo = methodInfo;
                menuInfo.type = attr.MenuType;
                menuInfo.classObj = pluginObj;
                menuItems.Add(menuInfo);
            }
            menuItems.Sort();
            //加载命令
            foreach (Type assType in assembly.GetTypes())
            {
                if (assType.IsInterface || assType.IsAbstract)
                    continue;
                if (!typeof(IPluginCommand).IsAssignableFrom(assType))
                    continue;
                var pluginCommand = (IPluginCommand)Activator.CreateInstance(assType);
                commands.Add(pluginCommand);
            }
            commands.Sort((l, r) => l.GetType().FullName.CompareTo(r.GetType().FullName));
        }

        public void Detach()
        {
            if (!isAttach)
                return;
            isAttach = false;
            pluginObj.Detach();
            menuItems.Clear();
            commands.Clear();
            pluginObj = null;
            pluginType = null;
            assembly = null;
        }
    }
}
