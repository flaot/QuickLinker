using QFramework;
using System.IO;

namespace QuickLinker.Utils
{
    public interface IBasePath : IUtility
    {
        /// <summary> 配置存放目录 </summary>
        string ConfigPath { get; }
        string PluginPath { get; }
    }
    internal class BasePathUtility : IBasePath
    {
        public string ConfigPath => Directory.GetCurrentDirectory();
        public string PluginPath => Path.GetFullPath("PlugIns");
    }
}
