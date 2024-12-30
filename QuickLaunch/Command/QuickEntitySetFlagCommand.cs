using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntitySetFlagCommand : AbstractCommand
    {
        public int index;
        public string[] flags;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            var entity = entitySystem.Find(index);
            if (entity == null)
                return;
            entity.flags = flags;
            entity.needSave.Value = true;
        }
    }
}
