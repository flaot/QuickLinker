using IWshRuntimeLibrary;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using File = System.IO.File;

namespace QuickLinker.QuickLaunch.Utils
{
    public class CommonCode
    {

        /// <summary>
        /// 获取app 数据
        /// </summary>
        /// <returns></returns>
        internal static AppData GetAppDataByFile()
        {
            AppData appData = new AppData();
            if (!File.Exists(Constants.DATA_FILE_PATH))
            {
                using (FileStream fs = File.Create(Constants.DATA_FILE_PATH)) { }
                appData = new AppData();
                SaveAppData(appData, Constants.DATA_FILE_PATH);
            }
            else
            {
                try
                {
                    using (FileStream fs = new FileStream(Constants.DATA_FILE_PATH, FileMode.Open))
                    {
                        appData = JsonSerializer.Deserialize<AppData>(fs);

                        //将菜单密码写入文件
                        if (!string.IsNullOrEmpty(appData.AppConfig.MenuPassword))
                        {
                            SavePassword(appData.AppConfig.MenuPassword);
                        }
                    }
                }
                catch
                {
                    LogUtil.WriteErrorLog("不幸的是, GeekDesk当前的数据文件已经损坏\n如果你有备份, 请将备份文件重命名为:Data 然后将Data覆盖到GeekDesk的根目录即可!");
                }
            }
            return appData;
        }

        private readonly static object _MyLock = new object();

        /// <summary>
        /// 保存app 数据
        /// </summary>
        /// <param name="appData"></param>
        public static void SaveAppData(AppData appData, string filePath)
        {
            lock (_MyLock)
            {
                if (filePath.Equals(Constants.DATA_FILE_BAK_PATH))
                {
                    appData.AppConfig.SysBakTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (!Directory.Exists(filePath.Substring(0, filePath.LastIndexOf("\\"))))
                {
                    Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf("\\")));
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    JsonSerializer.Serialize(fs, appData);
                }
            }
        }

        public static void SavePassword(string password)
        {
            using (StreamWriter sw = new StreamWriter(Constants.PW_FILE_BAK_PATH))
            {
                sw.Write(password);
            }
        }

        private static string GeneraterUUID()
        {
            try
            {
                if (!File.Exists(Constants.UUID_FILE_BAK_PATH) || string.IsNullOrEmpty(GetUniqueUUID()))
                {
                    using (StreamWriter sw = new StreamWriter(Constants.UUID_FILE_BAK_PATH))
                    {
                        string uuid = Guid.NewGuid().ToString() + "-" + Constants.MY_UUID;
                        sw.Write(uuid);
                        return uuid;
                    }
                }
            }
            catch (Exception) { }
            return "ERROR_UUID_GeneraterUUID_" + Constants.MY_UUID;
        }

        public static string GetUniqueUUID()
        {
            try
            {
                if (File.Exists(Constants.UUID_FILE_BAK_PATH))
                {
                    using (StreamReader reader = new StreamReader(Constants.UUID_FILE_BAK_PATH))
                    {
                        return reader.ReadToEnd().Trim();
                    }
                }
                else
                {
                    return GeneraterUUID();
                }
            }
            catch (Exception) { }
            return "ERROR_UUID_GetUniqueUUID_" + Constants.MY_UUID;
        }

        /// <summary>
        /// 根据路径获取文件图标等信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Entity GetIconInfoByPath(string path, bool canParse)
        {
            Entity iconInfo = new Entity();
            iconInfo.index = -1;
            string ext = File.Exists(path) ? Path.GetExtension(path) : string.Empty;
            if (ext == ".lnk" && canParse)
                ParseLnk(ref iconInfo, path);
            else if (ext == ".url" && canParse)
                ParseUrl(ref iconInfo, path);
            else
            {
                Bitmap bi = ImageUtil.GetBitmapIconByPath(path);
                iconInfo.imagePath = path;
                iconInfo.path = path;
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

            iconInfo.imageByteArr = ImageUtil.BitmapImageToByte(iconInfo.bitmapImage);
            var mainModule = Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string relativePath = FileUtil.MakeRelativePath(CommonCode.GetFullPath(mainModule), iconInfo.path);
            if (!string.IsNullOrEmpty(relativePath) && !string.Equals(iconInfo.path, relativePath))
                iconInfo.relativePath = relativePath;
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
            iconInfo.path = string.IsNullOrWhiteSpace(shortcut.TargetPath) ? path : shortcut.TargetPath;
            iconInfo.startArg = shortcut.Arguments;
            iconInfo.bitmapImage = bi;
            iconInfo.desc = shortcut.Description;
            iconInfo.workFolder = shortcut.WorkingDirectory;
            if (string.IsNullOrWhiteSpace(iconInfo.desc))
                iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            iconInfo.imagePath = iconPath;
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
            iconInfo.path = shortcut.TargetPath;
            iconInfo.bitmapImage = bi;
            iconInfo.desc = Path.GetFileNameWithoutExtension(path);
            iconInfo.iconType = OpenType.URL;
            iconInfo.imagePath = path;
        }
        public static void CreateShortcut(Entity entity)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutPath = Path.Combine(desktopPath, Path.GetFileNameWithoutExtension(entity.path) + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = entity.path;
            shortcut.WorkingDirectory = Path.GetDirectoryName(entity.workFolder);
            shortcut.WindowStyle = 1; // 正常窗口
            shortcut.Description = entity.desc;
            shortcut.Save();
        }

        public static string GetFullPath(string path)
        {
            return Path.Combine(Constants.APP_DIR, path);
        }
    }
}
