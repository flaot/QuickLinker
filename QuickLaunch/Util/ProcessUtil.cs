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
            StartIconApp(iconInfo, Array.Empty<string>(), IconStartType.SHOW_IN_EXPLORE);
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
                    if (type != IconStartType.SHOW_IN_EXPLORE)
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
                        //Debug.Error("程序启动失败(文件路径不存在或已删除)!");
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
                        case IconStartType.SHOW_IN_EXPLORE:
                            FileExplorerHelper.OpenFileInExplorer(p.StartInfo.FileName);
                            return;
                    }
                    if (p.Start())
                    {
                        //以正确启动应用的路径为准
                        if (type != IconStartType.SHOW_IN_EXPLORE
                            && !string.Equals(p.StartInfo.FileName, icon.Path))
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
