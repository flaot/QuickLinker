using QFramework;
using QuickLinker.Systems;
using QuickLinker.Utils;

namespace QuickLinker
{
    public class CommonArchitecture<T> : Architecture<T>
        where T : Architecture<T>, new()
    {
        protected override void Init()
        {
            this.RegisterSystem<IStroeSystem>(new DefalutStoreSystem());

            this.RegisterUtility<IBasePath>(new BasePathUtility());
            this.RegisterUtility<IXmlSerializeUtility>(new XmlSerializeUtility());
            this.RegisterUtility<IJsonSerializeUtility>(new JsonSerializeUtility());
        }
    }
}
