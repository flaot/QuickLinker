using QFramework;

namespace QuickLinker.Systems
{
    public interface IFileIconSystem : ISystem
    {
        object GetImage(string filePath, int index);
    }
}
