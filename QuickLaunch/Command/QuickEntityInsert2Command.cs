using QFramework;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntityInsert2Command : AbstractCommand
    {
        public Entity entity;
        public int index = -1;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.Insert(entity, index);
        }
    }
}
