using System.Windows.Forms;
using QFramework;
using QuickLinker.Model;
using QuickLinker.Plugin.Menu;
using QuickLinker.Plugin.Menu.Attribute;

namespace QuickLinker.Menus
{
    internal class AppMenu
    {
        private MainForm mainForm;
        public AppMenu(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }
  
        [AppMenuItem("显示(&S)", 100)]
        private void AppMenu_Show_Click()
        {
            mainForm.ShowMainWindow();
        }
        [AppMenuItem("首选项(&P)...", 101)]
        private void AppMenu_Setting_Click()
        {
            AppMenu_Show_Click();
            PreferencesFrom.ShowSetting();
        }
        [AppMenuItem("关闭 QuickLinker", 200)]
        private void AppMenu_Quit_Click()
        {
            TypeEventSystem.Global.Send(new CloseSoftwareEvent());
        }
    }
}
