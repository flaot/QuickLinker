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

        [FolderMenuItem(MenuKey.FolderMenu_Copy, 100)]
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
        [FolderMenuItem(MenuKey.FolderMenu_Move, 101)]
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
        [FolderMenuItem(MenuKey.FolderMenu_Delate, 102)]
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
        [FolderMenuItem(MenuKey.FolderMenu_Cancel, 200)]
        private void FolderMenu_Cancel_Click()
        {
        }
    }
}