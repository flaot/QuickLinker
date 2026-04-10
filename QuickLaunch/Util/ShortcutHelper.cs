using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace QuickLinker.QuickLaunch.Util
{
    internal static class ShortcutHelper
    {
        private const int STGM_READ = 0;
        private const int SLGP_RAWPATH = 0x4;

        #region COM接口定义
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            // pwfd 可为 NULL；使用 IntPtr 以便传入 IntPtr.Zero
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, IntPtr pwfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("0000010B-0000-0000-C000-000000000046")]
        internal interface IPersistFile
        {
            void GetCurFile([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFileName);
            void IsDirty();
            void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);
            void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, bool fRemember);
            void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
        }
        #endregion

        /// <summary>
        /// 判断 .lnk 是否含有可解析的目标路径（不依赖 WScript）
        /// </summary>
        internal static bool LnkHasTarget(string path)
        {
            object comObj = null;
            try
            {
                comObj = new ShellLink();
                var link = (IShellLink)comObj;
                var persistFile = (IPersistFile)comObj;
                persistFile.Load(path, STGM_READ);
                var sb = new StringBuilder(1024);
                link.GetPath(sb, sb.Capacity, IntPtr.Zero, SLGP_RAWPATH);
                return !string.IsNullOrWhiteSpace(sb.ToString());
            }
            catch
            {
                return false;
            }
            finally
            {
                if (comObj != null)
                    Marshal.FinalReleaseComObject(comObj);
            }
        }

        /// <summary>
        /// 解析 .lnk 填入 Entity（IShellLink，无 WScript）
        /// </summary>
        internal static void ParseLnk(string path, Entity iconInfo)
        {
            object comObj = null;
            try
            {
                comObj = new ShellLink();
                var link = (IShellLink)comObj;
                var persistFile = (IPersistFile)comObj;
                persistFile.Load(path, STGM_READ);

                var targetSb = new StringBuilder(1024);
                link.GetPath(targetSb, targetSb.Capacity, IntPtr.Zero, SLGP_RAWPATH);
                string targetPath = targetSb.ToString();

                var iconSb = new StringBuilder(260);
                link.GetIconLocation(iconSb, iconSb.Capacity, out int iconIndex);
                string iconPath = iconSb.ToString();
                if (string.IsNullOrEmpty(iconPath))
                    iconPath = targetPath;
                if (string.IsNullOrEmpty(iconPath))
                    iconPath = path;

                var argSb = new StringBuilder(2048);
                link.GetArguments(argSb, argSb.Capacity);

                var workSb = new StringBuilder(260);
                link.GetWorkingDirectory(workSb, workSb.Capacity);

                var descSb = new StringBuilder(1024);
                link.GetDescription(descSb, descSb.Capacity);

                link.GetShowCmd(out int showCmd);

                Bitmap bi = ImageUtil.GetBitmapIconByPath(iconPath, iconIndex);
                iconInfo.Path = string.IsNullOrWhiteSpace(targetPath) ? path : targetPath;
                iconInfo.startArg = argSb.ToString();
                iconInfo.bitmapImage = bi;
                iconInfo.desc = descSb.ToString();
                iconInfo.workFolder = workSb.ToString();
                if (string.IsNullOrWhiteSpace(iconInfo.desc))
                    iconInfo.desc = Path.GetFileNameWithoutExtension(path);
                iconInfo.ImagePath = iconPath;
                iconInfo.imageIndex = iconIndex;
                switch (showCmd)
                {
                    case 7:
                        iconInfo.windowStyle = WindowStyle.Minimized;
                        break;
                    case 3:
                        iconInfo.windowStyle = WindowStyle.Maximized;
                        break;
                    case 1:
                    default:
                        iconInfo.windowStyle = WindowStyle.Normal;
                        break;
                }
            }
            finally
            {
                if (comObj != null)
                    Marshal.FinalReleaseComObject(comObj);
            }
        }

        /// <summary>
        /// 创建快捷方式(适用于WinPE环境)
        /// </summary>
        /// <param name="entity">实体对象</param>
        public static void CreateShortcut(Entity entity)
        {
            try
            {
                // 获取桌面路径
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktopPath, Path.GetFileNameWithoutExtension(entity.Path) + ".lnk");

                // 创建ShellLink对象
                var shellLink = new ShellLink();
                var link = (IShellLink)shellLink;
                var persistFile = (IPersistFile)shellLink;

                // 设置快捷方式属性
                link.SetPath(entity.Path);
                link.SetWorkingDirectory(string.IsNullOrWhiteSpace(entity.workFolder)
                    ? Path.GetDirectoryName(entity.Path)
                    : entity.workFolder);
                link.SetDescription(entity.desc ?? "");
                link.SetShowCmd(1); // 正常窗口

                // 保存快捷方式
                persistFile.Save(shortcutPath, true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("创建快捷方式失败: " + ex.Message, ex);
            }
        }
    }
}
