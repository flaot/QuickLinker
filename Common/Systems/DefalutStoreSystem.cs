using QFramework;
using QuickLinker.Utils;
using System.IO;

namespace QuickLinker.Systems
{
    public interface IStroeSystem : ISystem
    {
        T Load<T>(T def);
        bool Save(object obj);
    }
    internal class DefalutStoreSystem : AbstractSystem, IStroeSystem
    {
        protected override void OnInit()
        {

        }
        public T Load<T>(T def)
        {
            var configName = typeof(T).Name;
            var basePath = this.GetUtility<IBasePath>();
            var jsonFile = Path.Combine(basePath.ConfigPath, configName + ".json");
            if(!File.Exists(jsonFile))
                return def;
            var serializer = this.GetUtility<IJsonSerializeUtility>();
            return serializer.JsonDeserializeByFile<T>(jsonFile);
        }

        public bool Save(object obj)
        {
            var configName = obj.GetType().Name;
            var basePath = this.GetUtility<IBasePath>();
            var jsonFile = Path.Combine(basePath.ConfigPath, configName + ".json");
            var serializer = this.GetUtility<IJsonSerializeUtility>();
            return serializer.JsonSerializeToFile(jsonFile, obj);
        }
    }
}
