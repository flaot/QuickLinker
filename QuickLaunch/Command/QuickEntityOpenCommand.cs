using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    /// <summary>
    /// 打开一个入口实体
    /// </summary>
    public class QuickEntityOpenCommand : AbstractCommand
    {
        public int index;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.Open(index);
        }
    }
}
