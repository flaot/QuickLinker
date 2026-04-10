using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using System;
using System.Drawing;
using System.Collections.Generic;
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
        /// URL 或带协议前缀的路径（非 盘符:\ 与 UNC）若对整串调用 <see cref="Path.GetFullPath"/>，会与当前工作目录拼接成无效路径，导致 IShellLink.SetPath 报 E_INVALIDARG。
        /// </summary>
        private static bool ShouldSkipPathFullNormalization(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;
            string t = path.TrimStart();
            if (t.StartsWith("\\\\", StringComparison.Ordinal))
                return false;
            if (t.Length >= 2 && char.IsLetter(t[0]) && t[1] == ':')
                return false;
            if (t.Contains("://", StringComparison.Ordinal))
                return true;
            int c = t.IndexOf(':');
            return c > 1;
        }

        /// <summary> 桌面 .lnk 文件名：优先沿用源快捷方式文件名，其次条目描述，最后才用目标路径最后一段（避免 URL/协议目标变成 “Client” 等）。 </summary>
        private static string GetDesktopShortcutBaseName(Entity entity)
        {
            string shell = (entity.ShellItemPath ?? "").Trim().Replace('/', Path.DirectorySeparatorChar);
            if (!string.IsNullOrEmpty(shell)
                && string.Equals(Path.GetExtension(shell), ".lnk", StringComparison.OrdinalIgnoreCase))
            {
                string n = SanitizeFileName(Path.GetFileNameWithoutExtension(shell));
                if (!string.IsNullOrEmpty(n))
                    return n;
            }

            if (!string.IsNullOrWhiteSpace(entity.desc))
            {
                string n = SanitizeFileName(entity.desc.Trim());
                if (!string.IsNullOrEmpty(n))
                    return n;
            }

            string pathFs = (entity.Path ?? "").Trim().Replace('/', Path.DirectorySeparatorChar);
            string fromPath = Path.GetFileNameWithoutExtension(pathFs);
            if (string.IsNullOrEmpty(fromPath))
                fromPath = "Shortcut";
            return SanitizeFileName(fromPath);
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "";
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            name = name.Trim().TrimEnd('.');
            return string.IsNullOrEmpty(name) ? "Shortcut" : name;
        }

        /// <summary> 将 Entity 中的图标路径转为 Shell 可用的本地路径字符串（与 ShouldSkipPathFullNormalization 一致）。 </summary>
        private static string GetIconPathForShell(Entity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.ImagePath))
                return null;
            string ip = entity.ImagePath.Trim().Replace('/', Path.DirectorySeparatorChar);
            if (!ShouldSkipPathFullNormalization(ip))
            {
                try
                {
                    ip = Path.GetFullPath(ip);
                }
                catch
                {
                    // keep ip
                }
            }
            return string.IsNullOrEmpty(ip) ? null : ip;
        }

        private static void ApplyShellLinkIcon(IShellLink link, Entity entity)
        {
            string ip = GetIconPathForShell(entity);
            if (ip == null)
                return;
            try
            {
                link.SetIconLocation(ip, entity.imageIndex);
            }
            catch
            {
                // 图标文件不存在或路径无效时保持默认图标
            }
        }

        /// <summary> 比较两路径是否指向同一文件（忽略 / 与 \ 及大小写差异）。 </summary>
        private static bool PathsReferToSameFile(string pathA, string pathB)
        {
            if (string.IsNullOrWhiteSpace(pathA) || string.IsNullOrWhiteSpace(pathB))
                return false;
            try
            {
                var a = Path.GetFullPath(pathA.Replace('/', Path.DirectorySeparatorChar));
                var b = Path.GetFullPath(pathB.Replace('/', Path.DirectorySeparatorChar));
                return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return string.Equals(
                    pathA.Replace('/', '\\'),
                    pathB.Replace('/', '\\'),
                    StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// 桌面快捷方式保存路径：若与目标路径相同（例如目标本身已在桌面且同名），则改用 “名称 (2).lnk” 等避免覆盖自身导致 COM Save 失败。
        /// </summary>
        /// <param name="shortcutBaseName">.lnk 主文件名（不含扩展名），来自 <see cref="GetDesktopShortcutBaseName"/>。</param>
        /// <param name="targetEntityPathForSameFileCheck">用于判断是否与将写入路径为同一文件的 entity.Path。</param>
        private static string ResolveDesktopShortcutSavePath(string desktopPath, string shortcutBaseName, string targetEntityPathForSameFileCheck)
        {
            string baseName = string.IsNullOrEmpty(shortcutBaseName) ? "Shortcut" : shortcutBaseName;
            string shortcutPath = Path.Combine(desktopPath, baseName + ".lnk");
            // 与目标为同一文件时不能 Save 到该路径（COM 失败）；否则保持与原逻辑一致（允许覆盖同名 .lnk）
            if (!PathsReferToSameFile(shortcutPath, targetEntityPathForSameFileCheck))
                return shortcutPath;
            for (int n = 2; n < 10000; n++)
            {
                shortcutPath = Path.Combine(desktopPath, baseName + " (" + n + ").lnk");
                if (!PathsReferToSameFile(shortcutPath, targetEntityPathForSameFileCheck) && !File.Exists(shortcutPath))
                    return shortcutPath;
            }
            return Path.Combine(desktopPath, baseName + " (" + Guid.NewGuid().ToString("N") + ").lnk");
        }

        /// <summary>
        /// 对 .lnk 解析内层目标供快捷方式写入（链式 .lnk 最多 8 层）；非 .lnk 或无法加载时返回规范后的路径。
        /// </summary>
        private static string GetPathForShellShortcutTarget(string path, int depth = 0)
        {
            if (string.IsNullOrWhiteSpace(path) || depth > 8)
                return path?.Trim() ?? "";

            string fs = path.Trim().Replace('/', Path.DirectorySeparatorChar);
            if (!ShouldSkipPathFullNormalization(fs))
            {
                try
                {
                    fs = Path.GetFullPath(fs);
                }
                catch
                {
                    // keep fs as replaced
                }
            }

            if (!string.Equals(Path.GetExtension(fs), ".lnk", StringComparison.OrdinalIgnoreCase) || !File.Exists(fs))
                return fs;

            object comObj = null;
            try
            {
                comObj = new ShellLink();
                var link = (IShellLink)comObj;
                var persistFile = (IPersistFile)comObj;
                persistFile.Load(fs, STGM_READ);
                var sb = new StringBuilder(1024);
                link.GetPath(sb, sb.Capacity, IntPtr.Zero, SLGP_RAWPATH);
                string inner = sb.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(inner))
                    return fs;

                string innerFs = inner.Replace('/', Path.DirectorySeparatorChar);
                if (!ShouldSkipPathFullNormalization(innerFs))
                {
                    try
                    {
                        innerFs = Path.GetFullPath(innerFs);
                    }
                    catch
                    {
                        // keep innerFs
                    }
                }

                if (string.Equals(Path.GetExtension(innerFs), ".lnk", StringComparison.OrdinalIgnoreCase) && File.Exists(innerFs))
                    return GetPathForShellShortcutTarget(innerFs, depth + 1);
                return innerFs;
            }
            catch
            {
                return fs;
            }
            finally
            {
                if (comObj != null)
                    Marshal.FinalReleaseComObject(comObj);
            }
        }

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
                iconInfo.ShellItemPath = path;
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

        /// <summary> 在用户/公共桌面按文件名（Unicode FormC）查找匹配的 .lnk。 </summary>
        private static string TryFindLnkByFileNameOnDesktops(string wantFileName, string desktopPath)
        {
            if (string.IsNullOrEmpty(wantFileName))
                return null;
            if (!string.Equals(Path.GetExtension(wantFileName), ".lnk", StringComparison.OrdinalIgnoreCase))
                return null;

            string wantNorm = wantFileName.Trim().Normalize(NormalizationForm.FormC);
            foreach (string root in new[] { desktopPath, Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) })
            {
                if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
                    continue;
                try
                {
                    foreach (string full in Directory.EnumerateFiles(root, "*.lnk", SearchOption.TopDirectoryOnly))
                    {
                        string fn = Path.GetFileName(full);
                        if (string.Equals(fn.Normalize(NormalizationForm.FormC), wantNorm, StringComparison.OrdinalIgnoreCase))
                            return full;
                    }
                }
                catch
                {
                    // ignore
                }
            }

            return null;
        }

        /// <summary>
        /// 解析磁盘上可复制的源 .lnk：Path、ShellItemPath、pathForShell、桌面组合路径，再枚举桌面按文件名匹配。
        /// </summary>
        private static string TryResolveExistingLnkPath(Entity entity, string desktopPath, string sourceItemFs, string pathForShell)
        {
            void Consider(string raw, List<string> list)
            {
                if (string.IsNullOrWhiteSpace(raw))
                    return;
                string n = raw.Trim().Replace('/', Path.DirectorySeparatorChar);
                if (!ShouldSkipPathFullNormalization(n))
                {
                    try
                    {
                        n = Path.GetFullPath(n);
                    }
                    catch
                    {
                        // keep n
                    }
                }

                if (!string.Equals(Path.GetExtension(n), ".lnk", StringComparison.OrdinalIgnoreCase))
                    return;
                foreach (var x in list)
                {
                    if (string.Equals(x, n, StringComparison.OrdinalIgnoreCase))
                        return;
                }

                list.Add(n);
            }

            var candidates = new List<string>();
            Consider(sourceItemFs, candidates);
            Consider(entity.ShellItemPath, candidates);
            Consider(pathForShell, candidates);

            string fileName = Path.GetFileName(pathForShell ?? sourceItemFs ?? "");
            if (!string.IsNullOrEmpty(fileName) && string.Equals(Path.GetExtension(fileName), ".lnk", StringComparison.OrdinalIgnoreCase))
            {
                Consider(Path.Combine(desktopPath, fileName), candidates);
            }

            foreach (var c in candidates)
            {
                if (File.Exists(c))
                    return c;
                try
                {
                    string normC = c.Normalize(NormalizationForm.FormC);
                    if (!string.Equals(normC, c, StringComparison.Ordinal) && File.Exists(normC))
                        return normC;
                }
                catch
                {
                    // ignore
                }
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                string scanned = TryFindLnkByFileNameOnDesktops(fileName, desktopPath);
                if (scanned != null)
                    return scanned;
            }

            return null;
        }

        private static void ResolveCreateShortcutPaths(Entity entity, out string desktopPath, out string shortcutPath, out string pathForShell, out string sourceItemFs, out string existingLnkPath)
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            shortcutPath = ResolveDesktopShortcutSavePath(desktopPath, GetDesktopShortcutBaseName(entity), entity.Path);
            pathForShell = GetPathForShellShortcutTarget(entity.Path);
            sourceItemFs = (entity.Path ?? "").Trim().Replace('/', Path.DirectorySeparatorChar);
            if (!ShouldSkipPathFullNormalization(sourceItemFs))
            {
                try
                {
                    sourceItemFs = Path.GetFullPath(sourceItemFs);
                }
                catch
                {
                    // keep replaced
                }
            }

            existingLnkPath = TryResolveExistingLnkPath(entity, desktopPath, sourceItemFs, pathForShell);
        }

        /// <summary>
        /// 是否允许创建桌面快捷方式：持久化目标仍为 .lnk 且磁盘上找不到源文件时返回 false（不采用间接启动方式）。
        /// </summary>
        internal static bool CanCreateDesktopShortcut(Entity entity)
        {
            if (entity == null)
                return false;
            ResolveCreateShortcutPaths(entity, out _, out _, out string pathForShell, out _, out string existingLnkPath);
            if (!string.Equals(Path.GetExtension(pathForShell), ".lnk", StringComparison.OrdinalIgnoreCase))
                return true;
            return existingLnkPath != null;
        }

        /// <summary>
        /// 创建快捷方式(适用于WinPE环境)
        /// </summary>
        /// <param name="entity">实体对象</param>
        public static void CreateShortcut(Entity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                if (!CanCreateDesktopShortcut(entity))
                    throw new InvalidOperationException("目标快捷方式在磁盘上不存在，无法创建桌面快捷方式。");

                ResolveCreateShortcutPaths(entity, out _, out string shortcutPath, out string pathForShell, out string sourceItemFs, out string existingLnkPath);

                // 目标仍为 .lnk 且磁盘上能定位源文件时，直接复制字节（shell 类快捷方式等场景下 COM 无法 Persist 指向 .lnk）。
                bool usePhysicalLnkCopy = string.Equals(Path.GetExtension(pathForShell), ".lnk", StringComparison.OrdinalIgnoreCase)
                    && existingLnkPath != null;

                // 协议/URL 类目标没有合法“工作目录”，且 Path.GetDirectoryName 会得到无意义片段；留空并不调用 SetWorkingDirectory，避免 COM 再报 E_INVALIDARG。
                string workDirArg = !string.IsNullOrWhiteSpace(entity.workFolder)
                    ? entity.workFolder
                    : (ShouldSkipPathFullNormalization(pathForShell) ? null : Path.GetDirectoryName(pathForShell));
                string tempLnk = Path.Combine(Path.GetTempPath(), "qlsc-" + Guid.NewGuid().ToString("N") + ".lnk");

                if (usePhysicalLnkCopy)
                {
                    if (File.Exists(shortcutPath))
                        File.Delete(shortcutPath);
                    File.Copy(existingLnkPath, shortcutPath, false);
                    return;
                }

                object comObj = new ShellLink();
                try
                {
                    var link = (IShellLink)comObj;
                    var persistFile = (IPersistFile)comObj;

                    link.SetPath(pathForShell);
                    if (!string.IsNullOrWhiteSpace(workDirArg))
                        link.SetWorkingDirectory(workDirArg);
                    link.SetDescription(entity.desc ?? "");
                    ApplyShellLinkIcon(link, entity);
                    link.SetShowCmd(1);

                    Exception saveTempEx = null;
                    bool savedToTemp = false;
                    try
                    {
                        persistFile.Save(tempLnk, true);
                        savedToTemp = true;
                    }
                    catch (Exception ex)
                    {
                        saveTempEx = ex;
                    }

                    if (savedToTemp)
                    {
                        try
                        {
                            if (File.Exists(shortcutPath))
                                File.Delete(shortcutPath);
                            File.Copy(tempLnk, shortcutPath, false);
                        }
                        finally
                        {
                            try
                            {
                                if (File.Exists(tempLnk))
                                    File.Delete(tempLnk);
                            }
                            catch
                            {
                                // ignore
                            }
                        }
                    }
                    else
                    {
                        // WinPE 等精简环境无 WScript.Shell：仅用 IShellLink/IPersistFile，尝试直接写入桌面路径。
                        try
                        {
                            if (File.Exists(shortcutPath))
                                File.Delete(shortcutPath);
                            persistFile.Save(shortcutPath, true);
                        }
                        catch (Exception exDirect)
                        {
                            throw new InvalidOperationException(
                                "创建快捷方式失败: 无法保存 .lnk（临时路径与目标路径均失败；精简/WinPE 环境请确认 shell32 COM 可用）。",
                                new AggregateException(saveTempEx, exDirect));
                        }
                    }
                }
                finally
                {
                    if (comObj != null)
                        Marshal.FinalReleaseComObject(comObj);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("创建快捷方式失败: " + ex.Message, ex);
            }
        }
    }
}
