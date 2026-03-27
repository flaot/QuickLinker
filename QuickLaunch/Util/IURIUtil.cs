using QFramework;

namespace QuickLinker.QuickLaunch.Utils
{
    public interface IURIUtil : IUtility
    {
        string Protocol { get; }
        void Set(bool enable);
    }
}
