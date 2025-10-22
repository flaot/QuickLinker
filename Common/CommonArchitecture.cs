using QFramework;
using QuickLinker.Systems;
using QuickLinker.Utils;

namespace QuickLinker
{
    public class CommonArchitecture<T> : Architecture<T> where T : Architecture<T>, new()
    {
        protected override void Init()
        {
            AbstractPlugin._architecture = this;

            this.RegisterSystem<IStroeSystem>(new StoreSystem());
            this.RegisterSystem<IMenuSystem>(new MenuSystem());
            this.RegisterSystem<IPluginSystem>(new PluginSystem());

            this.RegisterUtility<IBasePath>(new BasePathUtility());
            this.RegisterUtility<IXmlSerializeUtility>(new XmlSerializeUtility());
            this.RegisterUtility<IJsonSerializeUtility>(new JsonSerializeUtility());
        }
    }
}
