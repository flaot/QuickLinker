using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntityInsertCommand : AbstractCommand
    {
        public string filePath;
        /// <summary> 解析快捷方式 </summary>
        public bool canParse;
        public int index = -1;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.Insert(filePath, index, canParse);
        }
    }
}
