using QFramework;

namespace QuickLinker.Systems
{
    public interface IStroeSystem : ISystem
    {
        T Load<T>();
        void Save(object obj);
    }
    internal class DefalutStoreSystem : AbstractSystem, IStroeSystem
    {
        protected override void OnInit()
        {

        }
        public T Load<T>()
        {
            var configName = typeof(T).Name;
            return default;
        }

        public void Save(object obj)
        {

        }
    }
}
