using QFramework;
using QuickLinker.Plugin;
using QuickLinker.Plugin.Menu.Attribute;
using QuickLinker.Properties;
using QuickLinker.Utils;
using System.Windows.Forms;

namespace QuickLinker.Menus
{
    internal class FolderMenu : IController
    {
        private MainForm mainForm;
        public IArchitecture GetArchitecture() => mainForm.GetArchitecture();
        public FolderMenu(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        [FolderMenuItem("复制文件到此处(&C)", 100)]
        private void FolderMenu_Copy_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            for (int i = 0; i < Selection.dropFileOrDirs.Length; i++)
            {
                var path = Selection.dropFileOrDirs[i];
                this.GetUtility<IFileUtil>().Copy(path, tPanel.Entity.Path);
            }
        }
        [FolderMenuItem("移动文件到此处(&C)", 101)]
        private void FolderMenu_Move_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            for (int i = 0; i < Selection.dropFileOrDirs.Length; i++)
            {
                var path = Selection.dropFileOrDirs[i];
                this.GetUtility<IFileUtil>().MoveTo(path, tPanel.Entity.Path);
            }
        }
        [FolderMenuItem("删除文件(&D)", 102)]
        private void FolderMenu_Delate_Click()
        {
            var tPanel = Selection.activeContext as TPanel;
            if (tPanel.Entity == null)
                return;
            DialogResult dialogResult = MessageBox.Show(Resources.MainForm_ChekDeletaDropFileOrDirs,
                         Resources.MSGBox_Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
                return;
            for (int i = 0; i < Selection.dropFileOrDirs.Length; i++)
            {
                var path = Selection.dropFileOrDirs[i];
                this.GetUtility<IFileUtil>().Delete(path);
            }
        }
        [FolderMenuItem("取消", 120)]
        private void FolderMenu_Cancel_Click()
        {
        }
    }
}