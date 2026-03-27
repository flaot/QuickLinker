using QFramework;
using QuickLinker.Plugin.Menu;
using QuickLinker.Plugin.Menu.Attribute;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuickLinker.Systems
{
    public interface IMenuSystem : ISystem
    {
        /// <summary> 注册菜单对象 </summary>
        void RegisterMenu(IMenu defaultVal);
        /// <summary> 注册菜单多语言对象 </summary>
        void RegisterMenuLan(IMenuLang menuLang);
        /// <summary> 初始化内建带对象的菜单(类的实列) </summary>
        void InitSystemMenuItem(object classObj);
        /// <summary> 请求刷新指定菜单 </summary>
        void RequestReset(int menuType);
        /// <summary> 请求刷新所有菜单 </summary>
        void RequestResetAll();
        /// <summary> 显示一个菜单 </summary>
        void Show(int menuType, int x, int y);
        IMenu GetMenu(int menuType);

        void SetChecked(int menuType, string menuKey, bool isChecked);
        bool GetChecked(int menuType, string menuKey);
        void SetEnable(int menuType, string menuKey, bool isEnable);
        bool GetEnable(int menuType, string menuKey);
        void SetVisible(int menuType, string menuKey, bool isVisible);
        bool GetVisible(int menuType, string menuKey);
    }

    internal class MenuSystem : AbstractSystem, IMenuSystem
    {
        /// <summary> 存储菜单对象 </summary>
        private Dictionary<int, IMenu> _dicMenuByType;
        /// <summary> 除插件外的菜单 </summary>
        private Dictionary<int, List<MenuItem.Info>> _dicSystemMenuByType;
        /// <summary> 初始化完成的菜单 </summary>
        private List<int> _initFinishType;

        private List<IMenuLang> _menuLangs;
        private Dictionary<int, Dictionary<string, SwitchData>> _dicSwitchByPath;
        private class SwitchData
        {
            public bool isCheck;
            public bool isEnable = true;
            public bool isVisible = true;
            public override string ToString()
            {
                return $"isCheck:{isCheck} isEnable:{isEnable}";
            }
        }

        protected override void OnInit()
        {
            _dicMenuByType = new Dictionary<int, IMenu>();
            _dicSystemMenuByType = new Dictionary<int, List<MenuItem.Info>>();
            _initFinishType = new List<int>();
            _dicSwitchByPath = new Dictionary<int, Dictionary<string, SwitchData>>();
        }
        public void RegisterMenu(IMenu defaultVal)
        {
            _dicMenuByType[defaultVal.MenuType] = defaultVal;
        }
        public void RegisterMenuLan(IMenuLang menuLang)
        {
            if (_menuLangs == null)
                _menuLangs = new List<IMenuLang>();
            _menuLangs.Add(menuLang);
            _menuLangs.Sort((l,r) => r.Priority.CompareTo(l.Priority));
        }
        /// <summary> 请求刷新所有菜单 </summary>
        public void RequestResetAll()
        {
            _initFinishType.Clear();
            foreach (var item in _dicMenuByType)
                InitMenuItems(item.Key);
        }
        /// <summary> 请求刷新指定菜单 </summary>
        public void RequestReset(int menuType)
        {
            _initFinishType.Remove(menuType);
            InitMenuItems(menuType);
        }

        /// <summary> 显示一个菜单 </summary>
        public void Show(int menuType, int x, int y)
        {
            if (!_dicMenuByType.TryGetValue(menuType, out var menu))
                return;
            if (_dicSwitchByPath.TryGetValue(menuType, out var dic))
            {
                foreach (var item in menu.AllChild)
                {
                    if (item.info == null)
                        continue;
                    if (!dic.TryGetValue(item.info.menuKey, out var switchPath))
                        continue;
                    menu.SetCheck(item, switchPath.isCheck);
                    menu.SetEnable(item, switchPath.isEnable);
                    menu.SetVisible(item, switchPath.isVisible);
                }
            }
            menu.Show(x, y);
        }

        /// <summary> 初始化一个菜单项 </summary>
        private IMenu InitMenuItems(int menuType)
        {
            if (!_dicMenuByType.TryGetValue(menuType, out var menu))
                return null;

            if (!_initFinishType.Contains(menuType))
            {
                //得到内建菜单项
                if (!_dicSystemMenuByType.TryGetValue(menuType, out var menuItemList))
                    menuItemList = new List<MenuItem.Info>();

                //得到插件菜单项
                var pluginSystem = this.GetSystem<IPluginSystem>();
                if (pluginSystem != null)
                {
                    var menuList = pluginSystem.GetMenuInfos(menuType);
                    menuItemList.AddRange(menuList);
                }

                ResetMenu(menu, menuItemList);
                _initFinishType.Add(menuType);
            }
            return menu;
        }
        /// <summary> 初始化内建带对象的菜单(类的实列) </summary>
        public void InitSystemMenuItem(object classObj)
        {
            var menuItems = new List<MenuItem.Info>();
            var t = classObj.GetType();
            foreach (var methodInfo in t.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = methodInfo.GetCustomAttribute<CustomMenuItemAttribute>();
                if (attr == null)
                    continue;
                MenuItem.Info menuInfo = new MenuItem.Info();
                menuInfo.menuKey = attr.Key;
                menuInfo.namePath = GetLangPath(attr.Key);
                menuInfo.priority = attr.Priority;
                menuInfo.MethodInfo = methodInfo;
                menuInfo.type = attr.MenuType;
                menuInfo.classObj = classObj;
                menuItems.Add(menuInfo);
            }
            foreach (var menuItem in menuItems)
            {
                var type = menuItem.type;
                if (!_dicSystemMenuByType.TryGetValue(type, out var infos))
                    _dicSystemMenuByType.Add(type, infos = new List<MenuItem.Info>());
                infos.Add(menuItem);
            }
        }

        /// <summary> 重置菜单项 </summary>
        private void ResetMenu(IMenu menu, List<MenuItem.Info> systemMenuItem)
        {
            systemMenuItem.Sort();
            menu.Clear();
            int oldPriority = int.MinValue;
            int oldSplitLen = int.MaxValue;
            foreach (var item in systemMenuItem)
            {
                var nameSplit = item.namePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                //从低层级升到高层级时,添加分隔符
                if (oldSplitLen != nameSplit.Length && oldSplitLen < nameSplit.Length)
                {
                    menu.AddSeparator(null);
                }
              
                //先确保子菜单之前的路径是存在的
                MenuItem root = null;
                for (int i = 0; i < nameSplit.Length - 1; i++)
                {
                    var name = nameSplit[i];
                    var findMenu = menu.FindItem(root, name);
                    if (findMenu == null)
                    {
                        var newMenuItem = new MenuItem() { name = name };
                        menu.AddItem(root, newMenuItem);
                        root = newMenuItem;
                    }
                    else
                    {
                        root = findMenu;
                    }
                }
                //首项不添加分割符
                if (menu.FindItem(root, null) != null)
                {
                    //相邻菜单项优先级大于10则添加分割符
                    if (item.priority - oldPriority > 10)
                    {
                        if (nameSplit.Length > 0)
                            menu.AddSeparator(root);
                    }
                }
                //对最后一级菜单做处理
                {
                    var lastIndex = nameSplit.Length - 1;
                    var name = nameSplit[lastIndex];
                    var findMenu = menu.FindItem(root, nameSplit[lastIndex]);
                    if (findMenu == null)
                    {
                        menu.AddItem(root, new MenuItem()
                        {
                            name = name,
                            info = item,
                            Func = MenuItem_OnClick
                        });
                    }
                    else
                    {
                        menu.ReplaceItem(findMenu, item); //后面的覆盖前面的
                        //Logger.LogError($"菜单{type}的路径冲突{item.name}, 无法重复添加菜单");
                    }
                }
                oldPriority = item.priority;
                oldSplitLen = nameSplit.Length;
            }
        }

        /// <summary> 菜单回调 </summary>
        private void MenuItem_OnClick(MenuItem.Info menuItemInfo)
        {
            if (menuItemInfo.MethodInfo.GetParameters().Length == 0) //支持无参
                menuItemInfo.MethodInfo.Invoke(menuItemInfo.classObj, null);
            else
                menuItemInfo.MethodInfo.Invoke(menuItemInfo.classObj, new object[] { menuItemInfo });
        }
        IMenu IMenuSystem.GetMenu(int menuType)
        {
            if (_dicMenuByType.TryGetValue(menuType, out var menu))
                return menu;
            return null;
        }

        void IMenuSystem.SetChecked(int menuType, string menuKey, bool isChecked)
        {
            if (!_dicSwitchByPath.TryGetValue(menuType, out var dic))
                _dicSwitchByPath.Add(menuType, dic = new Dictionary<string, SwitchData>());
            if (!dic.TryGetValue(menuKey, out var switchData))
                dic.Add(menuKey, switchData = new SwitchData());
            switchData.isCheck = isChecked;
        }
        bool IMenuSystem.GetChecked(int menuType, string menuKey)
        {
            if (!_dicSwitchByPath.TryGetValue(menuType, out var dic))
                return false;
            if (dic.TryGetValue(menuKey, out var switchData))
                return switchData.isCheck;
            else
                return false;
        }
        void IMenuSystem.SetEnable(int menuType, string menuKey, bool isEnable)
        {
            if (!_dicSwitchByPath.TryGetValue(menuType, out var dic))
                _dicSwitchByPath.Add(menuType, dic = new Dictionary<string, SwitchData>());
            if (!dic.TryGetValue(menuKey, out var switchData))
                dic.Add(menuKey, switchData = new SwitchData());
            switchData.isEnable = isEnable;
        }
        bool IMenuSystem.GetEnable(int menuType, string menuKey)
        {
            if (!_dicSwitchByPath.TryGetValue(menuType, out var dic))
                return true;
            if (dic.TryGetValue(menuKey, out var switchData))
                return switchData.isEnable;
            else
                return true;
        }
        void IMenuSystem.SetVisible(int menuType, string menuKey, bool isVisible)
        {
            if (!_dicSwitchByPath.TryGetValue(menuType, out var dic))
                _dicSwitchByPath.Add(menuType, dic = new Dictionary<string, SwitchData>());
            if (!dic.TryGetValue(menuKey, out var switchData))
                dic.Add(menuKey, switchData = new SwitchData());
            switchData.isVisible = isVisible;
        }
        bool IMenuSystem.GetVisible(int menuType, string menuKey)
        {
            if (!_dicSwitchByPath.TryGetValue(menuType, out var dic))
                return true;
            if (dic.TryGetValue(menuKey, out var switchData))
                return switchData.isVisible;
            else
                return true;
        }
        private string GetLangPath(string menuKey)
        {
            if (_menuLangs == null)
                return menuKey;

            string displayPath = string.Empty;
            for (int i = 0; i < _menuLangs.Count && string.IsNullOrEmpty(displayPath); i++)
            {
                displayPath = _menuLangs[i].GetLang(menuKey);
            }

            if (string.IsNullOrEmpty(displayPath))
                return menuKey;

            return displayPath;
        }
    }
}
