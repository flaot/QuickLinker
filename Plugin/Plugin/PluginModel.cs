namespace QuickLinker.Plugin
{
    public class PluginModel
    {
        /// <summary> 插件名称 </summary>
        public string name;
        /// <summary> 描述 </summary>
        public string description;
        /// <summary> 版本号 </summary>
        public string version;
        /// <summary> 图标 </summary>
        public string icon;
        /// <summary> 作者信息 </summary>
        public PluginAuthor author;

        public class PluginAuthor
        {
            /// <summary> 作者名称 </summary>
            public string name;
        }
    }
}
