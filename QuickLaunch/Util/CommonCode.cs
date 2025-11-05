using IWshRuntimeLibrary;
using QFramework;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;
using System;
using System.Drawing;
using System.IO;
using File = System.IO.File;

namespace QuickLinker.QuickLaunch.Utils
{
    public class CommonCode
    {
        /// <summary>
        /// 根据路径获取文件图标等信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Entity GetIconInfoByPath(string path, bool canParse)
        {
            Entity iconInfo = Entity.Create();
            iconInfo.index = -1;
            string ext = File.Exists(path) ? Path.GetExtension(path) : string.Empty;
            if (ext == ".lnk" && canParse)
                ParseLnk(ref iconInfo, path);
            else if (ext == ".url" && canParse)
                ParseUrl(ref iconInfo, path);
            else
            {
                Bitmap bi = ImageUtil.GetBitmapIconByPath(path);
                iconInfo.ImagePath = path;
                iconInfo.Path = path;
                iconInfo.bitmapImage = bi;
                iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            }

            //是否可以再次解析
            if (!canParse)
            {
                bool parseIcon = ext == ".url";
                if (ext == ".lnk") //有lnk文件没有targetPath
                {
                    WshShell shell = new WshShell();
                    object shortcutObj = shell.CreateShortcut(path);
                    IWshShortcut shortcut = (IWshShortcut)shortcutObj;
                    parseIcon = !string.IsNullOrWhiteSpace(shortcut.TargetPath);
                }
                iconInfo.canParse = parseIcon;
            }

            iconInfo.imageByteArr = AbstractPlugin.Architecture.GetUtility<IImageUtil>().BitmapImageToByte(iconInfo.bitmapImage);
            var mainModule = Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string relativePath = MakeRelativePath(CommonCode.GetFullPath(mainModule), iconInfo.Path);
            if (!string.IsNullOrEmpty(relativePath) && !string.Equals(iconInfo.Path, relativePath))
                iconInfo.RelativePath = relativePath;
            return iconInfo;
        }

        private static void ParseLnk(ref Entity iconInfo, string path)
        {
            // 1.「普通的软链接」
            // 2.「explorer.exe shell:Name」    https://sspai.com/s/pxNm
            // 3.「explorer.exe shell:::GUID」  https://sspai.com/s/k97Q
            WshShell shell = new WshShell();
            object shortcutObj = shell.CreateShortcut(path);
            IWshShortcut shortcut = (IWshShortcut)shortcutObj;
            var locationArray = shortcut.IconLocation.Split(',');
            var iconPath = locationArray[0];
            if (string.IsNullOrEmpty(iconPath))
                iconPath = shortcut.TargetPath;
            if (string.IsNullOrEmpty(iconPath))
                iconPath = shortcut.FullName;
            int iconIndex = 0;
            if (shortcut.IconLocation.Length > 1)
                int.TryParse(locationArray[1], out iconIndex);
            Bitmap bi = ImageUtil.GetBitmapIconByPath(iconPath, iconIndex);
            iconInfo.Path = (string.IsNullOrWhiteSpace(shortcut.TargetPath) ? path : shortcut.TargetPath);
            iconInfo.startArg = shortcut.Arguments;
            iconInfo.bitmapImage = bi;
            iconInfo.desc = shortcut.Description;
            iconInfo.workFolder = shortcut.WorkingDirectory;
            if (string.IsNullOrWhiteSpace(iconInfo.desc))
                iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            iconInfo.ImagePath = iconPath;
            iconInfo.imageIndex = iconIndex;
            switch (shortcut.WindowStyle)
            {
                case 1:
                default:
                    iconInfo.windowStyle = WindowStyle.Normal;
                    break;
                case 7:
                    iconInfo.windowStyle = WindowStyle.Minimized;
                    break;
                case 3:
                    iconInfo.windowStyle = WindowStyle.Maximized;
                    break;
            }
        }
        private static void ParseUrl(ref Entity iconInfo, string path)
        {
            //URI schemes
            //https://www.163.com/dy/article/GK243C9A05119NPR.html
            WshShell shell = new WshShell();
            object shortcutObj = shell.CreateShortcut(path);
            IWshURLShortcut shortcut = (IWshURLShortcut)shortcutObj;
            //可以得到URI schemes的调用者
            //{
            //    var ss = shortcut.TargetPath.Split(':')[0];
            //    var key = Registry.ClassesRoot.OpenSubKey($"{ss}\\Shell\\Open\\Command");
            //    var ssdfsf = key.GetValue(string.Empty);
            //}
            Bitmap bi = ImageUtil.GetBitmapIconByPath(path);
            iconInfo.Path = shortcut.TargetPath;
            iconInfo.bitmapImage = bi;
            iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            iconInfo.iconType = OpenType.URL;
            iconInfo.ImagePath = path;
        }
        public static void CreateShortcut(Entity entity)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutPath = Path.Combine(desktopPath, Path.GetFileNameWithoutExtension(entity.Path) + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = entity.Path;
            shortcut.WorkingDirectory = Path.GetDirectoryName(entity.workFolder);
            shortcut.WindowStyle = 1; // 正常窗口
            shortcut.Description = entity.desc;
            shortcut.Save();
        }

        public static string GetFullPath(string path)
        {
            return Path.Combine(Constants.APP_DIR, path);
        }
        private static string MakeRelativePath(string fromPath, string toPath)
        {
            string relativePath = null;
            try
            {
                if (string.IsNullOrEmpty(toPath) || string.IsNullOrEmpty(fromPath)) return null;
                Uri file = new Uri(@toPath);
                // Must end in a slash to indicate folder
                Uri folder = new Uri(@fromPath);
                relativePath =
                Uri.UnescapeDataString(
                    folder.MakeRelativeUri(file)
                        .ToString()
                        .Replace('/', Path.DirectorySeparatorChar)
                    );
            }
            catch (Exception ex)
            {
                LogKit.E(ex);
            }
            return relativePath;
        }
    }
}
