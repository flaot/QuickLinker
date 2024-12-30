using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuickLinker
{
    internal class SystemMenu
    {
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0X0;
        private const int MF_SEPARATOR = 0X800;
        private const int MF_BYCOMMAND = 0X0;
        private const int MF_CHECKED = 0x8;
        private enum SystemMenuItem : int
        {
            Separator,
            TopMost,
            RemoveTitle,
            Setting,
            About,
        }
        private readonly static List<Tuple<SystemMenuItem, string, Action<MainForm>>> _sysMenuTuple = new List<Tuple<SystemMenuItem, string, Action<MainForm>>>()
        {
            new(SystemMenuItem.Separator, string.Empty, null),
            new(SystemMenuItem.TopMost, Resources.MainForm_SysMenu_TopMost, (f)=> {var top = f.GetModel<AppConfig>().topWindow;top.Value = !top.Value;}),
            new(SystemMenuItem.RemoveTitle, Resources.MainForm_SysMenu_RemoveSysMenu, (f)=> f.GetModel<AppConfig>().titleStyle.Value = TitleStyle.None),
            new(SystemMenuItem.Separator, string.Empty, null),
            new(SystemMenuItem.Setting, Resources.MainForm_SysMenu_Setting, (f)=> f.OpenSettingWindow()),
        };
        public static void OnHandleCreated(EventArgs e, MainForm arg)
        {
            var hSysMenu = GetSystemMenu(arg.Handle, false);
            foreach (var tuple in _sysMenuTuple)
            {
                if (tuple.Item1 == SystemMenuItem.Separator)
                    AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty);
                else
                    AppendMenu(hSysMenu, MF_STRING, (int)tuple.Item1, tuple.Item2);
            }
            arg.GetModel<AppConfig>().topWindow.RegisterWithInitValue(b => EnableMenu(arg.Handle, (int)SystemMenuItem.TopMost, b));
        }
        public static void WndProc(ref Message m, MainForm arg)
        {
            if (m.Msg != WM_SYSCOMMAND)
                return;

            var wParam = (int)m.WParam;
            var findItem = _sysMenuTuple.Find(item => (int)item.Item1 == wParam);
            if (findItem != null)
                findItem.Item3?.Invoke(arg);
        }

        private static void EnableMenu(nint hanlde, int menuId, bool enable)
        {
            var hSysMenu = GetSystemMenu(hanlde, false);
            var flag = MF_BYCOMMAND | MF_STRING;
            if (enable)
                flag |= MF_CHECKED;
            var findItem = _sysMenuTuple.Find(item => (int)item.Item1 == menuId);
            if (findItem != null)
                ModifyMenu(hSysMenu, menuId, flag, menuId, findItem.Item2);
        }

        /// <summary> 获取系统菜单 </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        /// <summary> 追加菜单项 </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ModifyMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);
    }
}
