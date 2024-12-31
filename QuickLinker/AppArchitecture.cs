using QFramework;
using QuickLinker.Model;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.Systems;
using QuickLinker.Utils;

namespace QuickLinker
{
    internal class AppArchitecture : CommonArchitecture<AppArchitecture>
    {
        protected override void Init()
        {
            base.Init();

            var appConfig = GetSystem<IStroeSystem>().Load<AppConfig>();
            if (appConfig == null)
                appConfig = new AppConfig();
            this.RegisterModel(appConfig);

            this.RegisterSystem(new QuickEntitySystem());
            this.RegisterSystem(new HotKeyManager());

            this.RegisterUtility(new LaunchUtil());
            this.RegisterUtility(new SingleAppUtil());
        }
    }
}
