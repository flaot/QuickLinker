using QFramework;
using QuickLinker;
using QuickLinker.Plugin.Events;
using QuickLinker.Plugin.Menu.Attribute;
using System.Windows.Forms;

namespace CommandPlugin
{
    public class Main : AbstractPlugin
    {
        public override void Attach()
        {
            TypeEventSystem.Global.Register<ShowItemMenuPreEvent>(PreShowItemMenuEvent);
        }

        public override void Detach()
        {
            TypeEventSystem.Global.UnRegister<ShowItemMenuPreEvent>(PreShowItemMenuEvent);
        }

        private void PreShowItemMenuEvent(ShowItemMenuPreEvent data)
        {
            LogKit.E("sdf");
        }

        [PageMenuItem("运行命令", 103)]
        private void Menu()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "命令文件(*.bat)|*.bat|所有文件(*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
        }
    }
}
