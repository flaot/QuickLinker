using System.Windows.Forms;
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
  
        [AppMenuItem("显示(&S)", 108)]
        private void AppMenu_Show_Click()
        {
            mainForm.NotifyIcon.Visible = true;
            mainForm.Show();
            mainForm.WindowState = FormWindowState.Normal;
            mainForm.Focus();
        }
        [AppMenuItem("首选项(&P)...", 109)]
        private void AppMenu_Setting_Click()
        {
            AppMenu_Show_Click();
            PreferencesFrom.ShowSetting();
        }
        [AppMenuItem("关闭 QuickLinker", 200)]
        private void AppMenu_Quit_Click()
        {
            mainForm.NotifyIcon.Visible = false;
            mainForm.Close();
            mainForm.Dispose();
            Application.Exit();
        }
    }
}
