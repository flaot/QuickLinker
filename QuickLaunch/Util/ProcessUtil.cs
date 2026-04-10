using QFramework;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace QuickLinker.QuickLaunch.Utils
{
    public interface IProcessUtil : IUtility
    {
        void RunEntity(Entity iconInfo, string[] dropFileOrDirs);
        void ShowInExplore(Entity iconInfo);

        /// <summary>仅对 <see cref="OpenType.OTHER"/> 校验磁盘路径是否仍存在；其它类型由原有启动逻辑处理。</summary>
        bool CanResolveLaunchTarget(Entity iconInfo);

        /// <summary>是否存在可用于资源管理器定位 / 系统右键的磁盘项。</summary>
        bool CanOpenInExplorer(Entity iconInfo);

        /// <summary>解析资源管理器应使用的本地路径（.lnk/.url 文件或普通文件/目录）。</summary>
        bool TryGetShellItemPath(Entity iconInfo, out string fullPath);
    }

    public class ProcessUtil : IProcessUtil
    {
        public void RunEntity(Entity iconInfo, string[] dropFileOrDirs)
        {
            StartIconApp(iconInfo, dropFileOrDirs, iconInfo.adminStartUp ?
                IconStartType.ADMIN_STARTUP : IconStartType.DEFAULT_STARTUP);
        }

        public void ShowInExplore(Entity iconInfo)
        {
            if (!TryGetShellItemPath(iconInfo, out string fullPath))
                return;
            try
            {
                FileExplorerHelper.OpenFileInExplorer(fullPath);
            }
            catch (Exception e)
            {
                LogKit.E(e);
            }
        }

        public bool CanOpenInExplorer(Entity iconInfo) =>
            iconInfo != null && TryGetShellItemPath(iconInfo, out _);

        public bool TryGetShellItemPath(Entity iconInfo, out string fullPath)
        {
            fullPath = null;
            if (iconInfo == null)
                return false;

            if (TryResolveShellCandidate(iconInfo.ShellItemPath, out fullPath))
                return true;
            if (iconInfo.iconType == OpenType.URL && TryResolveShellCandidate(iconInfo.ImagePath, out fullPath))
                return true;
            if (TryResolveShellCandidate(iconInfo.Path, out fullPath))
                return true;
            if (!string.IsNullOrWhiteSpace(iconInfo.RelativePath))
            {
                var combined = Path.Combine(Constants.APP_DIR, OsPath(iconInfo.RelativePath));
                if (TryResolveShellCandidate(combined, out fullPath))
                    return true;
            }
            return false;
        }

        private static string OsPath(string s) =>
            string.IsNullOrWhiteSpace(s) ? s : s.Replace('/', Path.DirectorySeparatorChar).Trim();

        private static bool TryResolveShellCandidate(string candidate, out string fullPath)
        {
            fullPath = null;
            if (string.IsNullOrWhiteSpace(candidate))
                return false;
            var os = OsPath(candidate);
            try
            {
                if (File.Exists(os))
                {
                    fullPath = Path.GetFullPath(os);
                    return true;
                }
                if (Directory.Exists(os))
                {
                    fullPath = Path.GetFullPath(os);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogKit.E(ex);
            }
            return false;
        }

        public bool CanResolveLaunchTarget(Entity iconInfo)
        {
            if (iconInfo == null)
                return false;
            if (iconInfo.iconType != OpenType.OTHER)
                return true;
            return !string.IsNullOrEmpty(GetFullPath(iconInfo));
        }

        private void StartIconApp(Entity icon, string[] dropFileOrDirs, IconStartType type)
        {
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = true;

                    p.StartInfo.FileName = icon.Path;
                    if (!string.IsNullOrWhiteSpace(icon.startArg))
                        p.StartInfo.Arguments = icon.startArg;
                    if (dropFileOrDirs.Length > 0)
                    {
                        if (p.StartInfo.Arguments.Length > 0)
                            p.StartInfo.Arguments += ' ';
                        p.StartInfo.Arguments += string.Join(' ', Array.ConvertAll(dropFileOrDirs, str => str.Contains(' ') ? '"' + str + '"' : str));
                    }

                    if (icon.iconType != OpenType.OTHER)
                    {
                        if (p.Start())
                            p.PriorityClass = PriorityClass2Process(icon.priorityClass);
                        return;
                    }
                    string fileOrFolder = GetFullPath(icon);
                    if (string.IsNullOrEmpty(fileOrFolder))
                    {
                        return;
                    }
                    p.StartInfo.FileName = fileOrFolder;
                    p.StartInfo.WindowStyle = WindowsStyle2Process(icon.windowStyle);
                    if (!string.IsNullOrEmpty(icon.workFolder) && Directory.Exists(icon.workFolder))
                        p.StartInfo.WorkingDirectory = icon.workFolder;
                    else
                        p.StartInfo.WorkingDirectory = WorkFolder(p.StartInfo.FileName);
                    switch (type)
                    {
                        case IconStartType.ADMIN_STARTUP:
                            p.StartInfo.Verb = "runas";
                            break;
                    }
                    if (p.Start())
                    {
                        if (!string.Equals(p.StartInfo.FileName, icon.Path))
                        {
                            icon.Path = p.StartInfo.FileName;
                            icon.needSave.Value = true;
                        }
                        p.PriorityClass = PriorityClass2Process(icon.priorityClass);
                    }
                }
            }
            catch (Exception e)
            {
                LogKit.E(e);
            }
        }

        /// <summary>
        /// 获取文件或目录的绝对路径 优先级:绝对路径 > 相对路径
        /// </summary>
        private string GetFullPath(Entity icon)
        {
            if (File.Exists(icon.Path) || Directory.Exists(icon.Path))
                return Path.GetFullPath(icon.Path);
            if (string.IsNullOrWhiteSpace(icon.RelativePath))
                return string.Empty;
            if (File.Exists(icon.RelativePath) || Directory.Exists(icon.RelativePath))
                return Path.GetFullPath(Path.Combine(Constants.APP_DIR, icon.RelativePath));
            return string.Empty;
        }

        /// <summary>
        /// 获取文件或目录的工作目录
        /// </summary>
        private string WorkFolder(string fullPath)
        {
            if (File.Exists(fullPath))
                return Path.GetDirectoryName(fullPath);
            string filePath = fullPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
            if (filePath.EndsWith(Path.DirectorySeparatorChar))
                filePath = filePath.Substring(0, filePath.Length - 1);
            return filePath;
        }

        private ProcessWindowStyle WindowsStyle2Process(WindowStyle windowStyle)
        {
            switch (windowStyle)
            {
                case WindowStyle.Normal: return ProcessWindowStyle.Normal;
                case WindowStyle.Minimized: return ProcessWindowStyle.Minimized;
                case WindowStyle.Maximized: return ProcessWindowStyle.Maximized;
                case WindowStyle.Hidden: return ProcessWindowStyle.Hidden;
                default: throw new NotImplementedException();
            }
        }

        private ProcessPriorityClass PriorityClass2Process(PriorityClass priorityClass)
        {
            switch (priorityClass)
            {
                case PriorityClass.RealTime: return ProcessPriorityClass.RealTime;
                case PriorityClass.High: return ProcessPriorityClass.High;
                case PriorityClass.AboveNormal: return ProcessPriorityClass.AboveNormal;
                case PriorityClass.Normal: return ProcessPriorityClass.Normal;
                case PriorityClass.BelowNormal: return ProcessPriorityClass.BelowNormal;
                case PriorityClass.Idle: return ProcessPriorityClass.Idle;
                default: throw new NotImplementedException();
            }
        }
    }
}
