using Microsoft.Win32;

namespace QuickLinker
{
    internal static class RegistryKeyExt
    {
        public static int ReadDword(this RegistryKey registryKey, string name, int defValue = 0)
        {
            var readRows = registryKey.GetValue(name);
            if (readRows == null)
                return defValue;
            return (int)readRows;
        }
        public static string ReadSz(this RegistryKey registryKey, string name, string defValue = "")
        {
            var readRows = registryKey.GetValue(name);
            if (readRows == null)
                return defValue;
            string str = readRows.ToString();
            str = str.Replace("\0", string.Empty);
            if (string.IsNullOrWhiteSpace(str))
                return defValue;
            return str.Trim();
        }
    }
}
