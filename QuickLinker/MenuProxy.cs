using QuickLinker.Plugin.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuickLinker
{
    internal class MenuProxy : IMenu
    {
        private int _menuType;
        private ToolStrip _toolStrip;
        private Dictionary<MenuItem, ToolStripMenuItem> _dicCacheByItem = new Dictionary<MenuItem, ToolStripMenuItem>();

        public MenuProxy(int menuType, ToolStrip toolStrip)
        {
            _menuType = menuType;
            _toolStrip = toolStrip;
        }

        private ToolStripItemCollection ChildCollection(MenuItem root) =>
            root == null ? _toolStrip.Items : _dicCacheByItem[root].DropDownItems;

        int IMenu.MenuType => _menuType;
        int IMenu.Count => _toolStrip.Items.Count;

        public List<MenuItem> AllChild => _dicCacheByItem.Keys.ToList();

        void IMenu.AddItem(MenuItem root, MenuItem sub)
        {
            var toolStripItem = new ToolStripMenuItem(sub.name);
            toolStripItem.Name = sub.name;
            toolStripItem.Tag = sub;
            toolStripItem.Click += ToolStripItem_Click;
            _dicCacheByItem[sub] = toolStripItem;
            ChildCollection(root).Add(toolStripItem);
        }
        void IMenu.ReplaceItem(MenuItem item, MenuItem.Info info)
        {
            ToolStripMenuItem toolStripMenuItem = _dicCacheByItem[item];
            ((MenuItem)toolStripMenuItem.Tag).info = info;
        }
        void IMenu.SetCheck(MenuItem item, bool isCheck)
        {
            _dicCacheByItem[item].Checked = isCheck;
        }

        void IMenu.SetEnable(MenuItem item, bool isEnable)
        {
            _dicCacheByItem[item].Enabled = isEnable;
        }
        void IMenu.SetVisible(MenuItem item, bool isVisible)
        {
            _dicCacheByItem[item].Visible = isVisible;
        }
        void IMenu.AddSeparator(MenuItem root)
        {
            ChildCollection(root).Add(new ToolStripSeparator());
        }
        void IMenu.Clear()
        {
            _toolStrip.Items.Clear();
            _dicCacheByItem.Clear();
        }
        MenuItem IMenu.FindItem(MenuItem root, string name)
        {
            var collection = ChildCollection(root);
            if (string.IsNullOrEmpty(name))
                return collection.Count > 0 ? collection[0].Tag as MenuItem : null;
            foreach (ToolStripItem stripItem in collection)
            {
                if (stripItem is ToolStripMenuItem item)
                {
                    var menuItem = (MenuItem)item.Tag;
                    if (menuItem.name == name)
                        return menuItem;
                }
            }
            return null;
        }

        void IMenu.Show(int x, int y)
        {
            if (_toolStrip is ContextMenuStrip menuStrip)
                menuStrip.Show(x, y);
        }

        private void ToolStripItem_Click(object sender, System.EventArgs e)
        {
            var strip = sender as ToolStripMenuItem;
            if (strip == null)
                return;
            var menuItem = strip.Tag as MenuItem;
            menuItem.Func?.Invoke(menuItem.info);
        }

        public ToolStripMenuItem FindStripMenuItem(string menuKey)
        {
            foreach (var dicItem in _dicCacheByItem)
            {
                if (dicItem.Key.info.menuKey == menuKey)
                    return dicItem.Value;
            }
            return null;
        }
    }
}
