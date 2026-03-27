using QFramework;
using QuickLinker.Model;
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

        [AppMenuItem(MenuKey.AppMenu_Show, 100)]
        private void AppMenu_Show_Click()
        {
            mainForm.ShowMainWindow();
        }
        [AppMenuItem(MenuKey.AppMenu_ShowSetting, 101)]
        private void AppMenu_Setting_Click()
        {
            AppMenu_Show_Click();
            PreferencesFrom.ShowSetting();
        }
        [AppMenuItem(MenuKey.AppMenu_Quit, 200)]
        private void AppMenu_Quit_Click()
        {
            TypeEventSystem.Global.Send(new CloseSoftwareEvent());
        }
    }
}
