using QuickLinker.Model;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.QuickLaunch.Utils;
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
            this.RegisterSystem<IFileIconSystem>(new FileIconSystem());
            this.RegisterSystem<IAudioSystem>(new AudioSystem());

            this.RegisterUtility<IProcessUtil>(new ProcessUtil());
            this.RegisterUtility<IImageUtil>(new ImageUtil());
            this.RegisterUtility<IURIUtil>(new URIUtil());
            this.RegisterUtility(new LaunchUtil());
            this.RegisterUtility(new SingleAppUtil());
        }
    }
}
