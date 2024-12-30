using QFramework;
using QuickLinker.Model;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.Utils;

namespace QuickLinker
{
    internal class AppArchitecture : Architecture<AppArchitecture>
    {
        protected override void Init()
        {
            this.RegisterModel(new AppConfig());

            this.RegisterSystem(new QuickEntitySystem());
            this.RegisterSystem(new HotKeyManager());

            this.RegisterUtility(new LaunchUtil());
            this.RegisterUtility(new SingleAppUtil());
        }
    }
}
