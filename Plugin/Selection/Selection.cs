namespace QuickLinker.Plugin
{
    public class Selection
    {
        /// <summary> 命令行启动 </summary>
        public static bool isBatchMode;
        public static IItem activeEntity;
        public static IItem activeContext;
        public static string[] dropFileOrDirs;
    }
}
