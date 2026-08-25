namespace QuickLinker.Plugin.Events
{
    /// <summary>
    /// 解析文件图标
    /// </summary>
    public struct PaseIconEvent
    {
        public string filePath;

        public PaseIconEvent(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
