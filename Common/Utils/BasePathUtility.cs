using QFramework;
using System.IO;

namespace QuickLinker.Utils
{
    public interface IBasePath : IUtility
    {
        string ConfigPath { get; }
    }
    internal class BasePathUtility : IBasePath
    {
        public string ConfigPath => Directory.GetCurrentDirectory();
    }
}
