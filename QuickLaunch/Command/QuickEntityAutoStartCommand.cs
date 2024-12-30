using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntityAutoStartCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.AutoStart();
        }
    }
}
