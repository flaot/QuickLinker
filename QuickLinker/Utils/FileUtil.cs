using QFramework;

namespace QuickLinker.Utils
{
    public interface IFileUtil : IUtility
    {
        int Copy(string path, string dest);
        int MoveTo(string path, string dest);
        int Delete(string path);
        int Rename(string path, string dest);
    }
    internal class FileUtil : IFileUtil
    {
        public int Copy(string path, string dest)
        {
            return Operation(Win32API.WFUNC.FO_COPY, path, dest);
        }

        public int MoveTo(string path, string dest)
        {
            return Operation(Win32API.WFUNC.FO_MOVE, path, dest);
        }

        public int Delete(string path)
        {
            return Operation(Win32API.WFUNC.FO_DELETE, path, null);
        }

        public int Rename(string path, string dest)
        {
            return Operation(Win32API.WFUNC.FO_RENAME, path, dest);
        }

        private int Operation(Win32API.WFUNC func, string path, string dest)
        {
            if (!string.IsNullOrEmpty(dest))
                dest = dest.Replace('/', '\\');
            Win32API.SHFILEOPSTRUCT lpFileOp = new Win32API.SHFILEOPSTRUCT
            {
                wFunc = func,
                pFrom = path + "\0",
                fFlags = Win32API.FILEOP_FLAGS.FOF_ALLOWUNDO,
                fAnyOperationsAborted = false,
            };
            if (!string.IsNullOrEmpty(dest))
            {
                lpFileOp.pTo = dest + "\0";
            }
            return Win32API.SHFileOperation(ref lpFileOp);
        }
    }
}
