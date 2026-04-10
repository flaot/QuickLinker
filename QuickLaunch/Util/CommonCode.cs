using QFramework;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Util;
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
                iconInfo.ShellItemPath = path;
                iconInfo.bitmapImage = bi;
                iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            }

            //是否可以再次解析
            if (!canParse)
            {
                bool parseIcon = ext == ".url";
                if (ext == ".lnk") //有lnk文件没有targetPath
                    parseIcon = ShortcutHelper.LnkHasTarget(path);
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
            ShortcutHelper.ParseLnk(path, iconInfo);
        }

        /// <summary> 从 .url（InternetShortcut）读取 URL=，不依赖 WScript </summary>
        private static string ReadInternetShortcutUrl(string path)
        {
            foreach (string raw in File.ReadAllLines(path))
            {
                string line = raw.Trim();
                if (line.StartsWith("URL=", StringComparison.OrdinalIgnoreCase))
                    return line.Substring(4).Trim();
            }
            return string.Empty;
        }

        private static void ParseUrl(ref Entity iconInfo, string path)
        {
            //URI schemes
            //https://www.163.com/dy/article/GK243C9A05119NPR.html
            Bitmap bi = ImageUtil.GetBitmapIconByPath(path);
            iconInfo.Path = ReadInternetShortcutUrl(path);
            iconInfo.bitmapImage = bi;
            iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            iconInfo.iconType = OpenType.URL;
            iconInfo.ImagePath = path;
            iconInfo.ShellItemPath = path;
        }

        public static void CreateShortcut(Entity entity) => ShortcutHelper.CreateShortcut(entity);

        /// <summary> 是否允许将该项创建到桌面的快捷方式（目标 .lnk 在磁盘不存在时为 false）。 </summary>
        public static bool CanCreateDesktopShortcut(Entity entity) => ShortcutHelper.CanCreateDesktopShortcut(entity);

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
