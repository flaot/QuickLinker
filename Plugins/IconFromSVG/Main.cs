using QFramework;
using QuickLinker;
using QuickLinker.Systems;

namespace IconFromSVG
{
    public class Main : AbstractPlugin
    {
        private IFileIconSystem _oldSystem;
        public override void Attach()
        {
            _oldSystem = this.GetSystem<IFileIconSystem>();
            this.GetArchitecture().RegisterSystem<IFileIconSystem>(new SVGFileIconSystem(_oldSystem));
        }

        public override void Detach()
        {
            this.GetArchitecture().RegisterSystem<IFileIconSystem>(_oldSystem);
        }
    }
}
