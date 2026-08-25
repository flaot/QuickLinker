using QFramework;
using QuickLinker.QuickLaunch.Systems;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntityOptCommand : AbstractCommand
    {
        public enum OptType
        {
            None,
            /// <summary> 拷贝 arg1:fromIndex arg1:index </summary>
            Copy,
            /// <summary> 交换 arg1:fromIndex arg1:index </summary>
            Switch,
            /// <summary> 排列 arg1:fromIndex arg1:index </summary>
            Align,
        }
        public OptType optType;
        public string filePath;
        public int fromIndex;
        public int index = -1;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            switch (optType)
            {
                case OptType.Copy:
                    entitySystem.Copy(fromIndex, index);
                    break;
                case OptType.Switch:
                    entitySystem.Switch(fromIndex, index);
                    break;
                case OptType.Align:
                    entitySystem.Align(fromIndex, index);
                    break;
                case OptType.None:
                default:
                    break;
            }
        }
    }
}
