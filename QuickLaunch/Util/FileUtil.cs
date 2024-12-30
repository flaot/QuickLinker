
using System;
using System.IO;

namespace QuickLinker.QuickLaunch.Utils
{
    internal class FileUtil
    {
        public static string MakeRelativePath(string fromPath, string toPath)
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
                LogUtil.WriteErrorLog(ex, "建立相对路径出错:fromPath:" + fromPath + ",toPath:" + toPath);
            }
            return relativePath;
        }
    }
}
