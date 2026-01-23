using QFramework;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace QuickLinker.QuickLaunch.Utils
{
    internal class FileExplorerHelper
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cild, IntPtr[] apidl, uint dwFlags);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern void ILFree(IntPtr pidl);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ILCreateFromPath(string path);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHParseDisplayName(string name, IntPtr bindingContext, out IntPtr pidl, uint sfgaoIn, out uint psfgaoOut);

        public static bool OpenFileInExplorer(string filePath)
        {
            try
            {
                // 获取文件所在目录
                string directory = System.IO.Path.GetDirectoryName(filePath);

                // 创建目录的 ITEMIDLIST
                IntPtr pidlDirectory = ILCreateFromPath(directory);

                if (pidlDirectory != IntPtr.Zero)
                {
                    // 创建文件的 ITEMIDLIST
                    IntPtr pidlFile = ILCreateFromPath(filePath);

                    if (pidlFile != IntPtr.Zero)
                    {
                        // 打开文件夹并选中文件
                        IntPtr[] pidlArray = { pidlFile };
                        SHOpenFolderAndSelectItems(pidlDirectory, 1, pidlArray, 0);

                        // 清理资源
                        ILFree(pidlFile);
                    }

                    // 清理目录资源
                    ILFree(pidlDirectory);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogKit.E(ex);
                return false;
            }
        }
    }
}
