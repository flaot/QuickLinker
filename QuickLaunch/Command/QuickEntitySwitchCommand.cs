using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntitySwitchCommand : AbstractCommand
    {
        public int srcIndex;
        public int desIndex;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.Switch(srcIndex, desIndex);
        }
    }
}
