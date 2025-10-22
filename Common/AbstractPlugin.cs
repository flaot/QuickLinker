using QFramework;
using QuickLinker.Plugin;

namespace QuickLinker
{
    public abstract class AbstractPlugin : IController, IPlugin
    {
        internal static IArchitecture _architecture;
        public static IArchitecture Architecture => _architecture;

        public virtual void Attach() { }
        public virtual void Detach() { }
        public IArchitecture GetArchitecture() => _architecture;
    }
}
