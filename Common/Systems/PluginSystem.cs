using QFramework;
using QuickLinker.Plugin;
using QuickLinker.Plugin.Menu;
using QuickLinker.Utils;
using System.Collections.Generic;
using System.IO;

namespace QuickLinker.Systems
{
    public interface IPluginSystem : ISystem
    {
        void LoadAll();
        void UnLoadAll();
        /// <summary> 获取插件附带的菜单 </summary>
        List<MenuItem.Info> GetMenuInfos(int menuType);
        List<PluginObj> Plugins { get; }
    }
    internal class PluginSystem : AbstractSystem, IPluginSystem
    {
        public const string MAIN_CONFIG_FILE = "package.json";

        public List<PluginObj> Plugins { get; private set; }
        private Dictionary<int, List<MenuItem.Info>> _dicPluginMenuByType;
        protected override void OnInit()
        {
            Plugins = new List<PluginObj>();
            _dicPluginMenuByType = new Dictionary<int, List<MenuItem.Info>>();
            InitPackage();
        }
        List<MenuItem.Info> IPluginSystem.GetMenuInfos(int menuType)
        {
            if (_dicPluginMenuByType.TryGetValue(menuType, out var menuInfos))
                return menuInfos;
            else
                return new List<MenuItem.Info>();
        }

        private void InitPackage()
        {
            string pluginPath = this.GetUtility<IBasePath>().PluginPath;
            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);
            foreach (var pluginDir in Directory.GetDirectories(pluginPath))
            {
                var packageFile = Path.Combine(pluginDir, MAIN_CONFIG_FILE);
                var model = this.GetUtility<IJsonSerializeUtility>().JsonDeserializeByFile<PluginModel>(packageFile);
                if (model == null)
                    continue;
                PluginObj pluginObj = new PluginObj();
                pluginObj.menuItems = new List<MenuItem.Info>();
                pluginObj.commands = new List<IPluginCommand>();
                pluginObj.model = model;
                pluginObj.dllPath = Path.Combine(pluginDir, Path.GetFileName(pluginDir) + ".dll");
                if (pluginObj != null)
                    Plugins.Add(pluginObj);
            }
        }

        public void LoadAll()
        {
            foreach (var plugin in Plugins)
            {
                plugin.Attach();
                if (plugin.menuItems.Count <= 0)
                    continue;
                foreach (var menuItem in plugin.menuItems)
                {
                    if (!_dicPluginMenuByType.TryGetValue(menuItem.type, out var items))
                        _dicPluginMenuByType.Add(menuItem.type, items = new List<MenuItem.Info>());
                    items.Add(menuItem);
                }
            }
        }
        public void UnLoadAll()
        {
            foreach (var plugin in Plugins)
            {
                if (plugin.menuItems.Count <= 0)
                {
                    plugin.Detach();
                    continue;
                }
                foreach (var menuItem in plugin.menuItems)
                {
                    if (_dicPluginMenuByType.TryGetValue(menuItem.type, out var items))
                        items.Remove(menuItem);
                }
                plugin.Detach();
            }
        }
    }
}
