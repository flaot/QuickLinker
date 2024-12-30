using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntityRemoveCommand : AbstractCommand
    {
        public int index;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.Remove(index, true);
        }
    }
}
