using QFramework;
using QuickLinker.Plugin;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Utils;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static QuickLinker.QuickLaunch.Command.QuickEntityOptCommand;
using QuickLinker.Plugin.Menu;
using QuickLinker.Plugin.Menu.Attribute;

namespace QuickLinker
{
    public partial class MainForm
    {
        [PageMenuItem("(未配置)", 100)]
        private void MenuStrip_Null_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            CommonCode.CreateShortcut(tPanel.Entity);
        }
        [PageMenuItem("创建快捷方式(&R)", 200)]
        private void MenuStrip_CreateQuick_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            CommonCode.CreateShortcut(tPanel.Entity);
        }
        [PageMenuItem("资源管理器(&X)", 201)]
        private void MenuStrip_SystemContextMenu_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            DirectoryInfo[] folders = new DirectoryInfo[1];
            folders[0] = new DirectoryInfo(tPanel.Entity.Path);
            ShellContextMenu scm = new ShellContextMenu();
            Point p = Cursor.Position;
            p.X -= 80;
            p.Y -= 80;
            scm.ShowContextMenu(folders, p);
        }
      
        [PageMenuItem("复制(&D)", 300)]
        private void MenuStrip_Copy_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            _optFirstTemp = tPanel;
            _optType.Value = OptType.Copy;
        }
        [PageMenuItem("交换(&S)", 301)]
        private void MenuStrip_Switch_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            _optFirstTemp = tPanel;
            _optType.Value = OptType.Switch;
        }
        [PageMenuItem("排列(&A)", 302)]
        private void MenuStrip_Align_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            _optFirstTemp = tPanel;
            _optType.Value = OptType.Align;
        }
        [PageMenuItem("清除(&C)", 303)]
        private void MenuStrip_Clear_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            DialogResult dialogResult = MessageBox.Show(string.Format(Resources.MainForm_Remove, tPanel.Title), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                this.SendCommand(new QuickEntityRemoveCommand() { index = tPanel.Index });
        }
        [PageMenuItem("属性(&P)", 400)]
        private void MenuStrip_Attr_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            tPanel.Invert(true);
            BtnPropertiesFrom.Show(tPanel);
            tPanel.Invert(false);
        }
    }
}
