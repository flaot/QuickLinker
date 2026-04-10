using QFramework;
using QuickLinker.Menus;
using QuickLinker.Plugin;
using QuickLinker.Plugin.Menu.Attribute;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Utils;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static QuickLinker.QuickLaunch.Command.QuickEntityOptCommand;

namespace QuickLinker
{
    internal class PageMenu : IController
    {
        private MainForm mainForm;
        public IArchitecture GetArchitecture() => mainForm.GetArchitecture();
        public PageMenu(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        [PageMenuItem(MenuKey.PageMenu_Null, 100)]
        private void MenuStrip_Null_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            CommonCode.CreateShortcut(tPanel.Entity);
        }
        [PageMenuItem(MenuKey.PageMenu_CreateQuick, 200)]
        private void MenuStrip_CreateQuick_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            CommonCode.CreateShortcut(tPanel.Entity);
        }
        [PageMenuItem(MenuKey.PageMenu_SystemContext, 201)]
        private void MenuStrip_SystemContextMenu_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            var processUtil = this.GetUtility<IProcessUtil>();
            if (!processUtil.TryGetShellItemPath(tPanel.Entity, out string fsPath))
                return;
            ShellContextMenu scm = new ShellContextMenu();
            Point p = Cursor.Position;
            p.X -= 80;
            p.Y -= 80;
            if (Directory.Exists(fsPath))
                scm.ShowContextMenu(new[] { new DirectoryInfo(fsPath) }, p);
            else
                scm.ShowContextMenu(new[] { new FileInfo(fsPath) }, p);
        }

        [PageMenuItem(MenuKey.PageMenu_Copy, 300)]
        private void MenuStrip_Copy_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            mainForm.OperateGrid = tPanel;
            mainForm.OperateType.Value = OptType.Copy;
        }
        [PageMenuItem(MenuKey.PageMenu_Switch, 301)]
        private void MenuStrip_Switch_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            mainForm.OperateGrid = tPanel;
            mainForm.OperateType.Value = OptType.Switch;
        }
        [PageMenuItem(MenuKey.PageMenu_Align, 302)]
        private void MenuStrip_Align_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            mainForm.OperateGrid = tPanel;
            mainForm.OperateType.Value = OptType.Align;
        }
        [PageMenuItem(MenuKey.PageMenu_Clear, 303)]
        private void MenuStrip_Clear_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            DialogResult dialogResult = MessageBox.Show(string.Format(Resources.MainForm_Remove, tPanel.Title), mainForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                this.SendCommand(new QuickEntityRemoveCommand() { index = tPanel.Index });
        }
        [PageMenuItem(MenuKey.PageMenu_Attr, 400)]
        private void MenuStrip_Attr_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            tPanel.Invert(true);
            BtnPropertiesFrom.Show(tPanel);
            tPanel.Invert(false);
        }
    }
}
