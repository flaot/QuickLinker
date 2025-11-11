using QFramework;
using QuickLinker.Plugin;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Channels;
using System.Windows.Forms;
using AppConfig = QuickLinker.Model.AppConfig;
using Constants = QuickLinker.QuickLaunch.Constant.Constants;

namespace QuickLinker
{
    public partial class BtnPropertiesFrom : Form, IController
    {
        private Entity _inputEntity;
        private Entity _tempEntity;
        private int _OptIndex;
        public IArchitecture GetArchitecture() => AppArchitecture.Interface;
        public BtnPropertiesFrom()
        {
            InitializeComponent();
        }
        private void BtnPropertiesFrom_Load(object sender, EventArgs e)
        {
            string shellFile = Path.Combine(Environment.SystemDirectory, "SHELL32.dll");
            var imageUtil = this.GetUtility<IImageUtil>();
            Btn_Parse.Image = imageUtil.ScaleBitmap(ImageUtil.GetBitmapIconByPath(shellFile, 263), Btn_Parse.Width, Btn_Parse.Height);
            Btn_BrowsePath.Image = imageUtil.ScaleBitmap(imageUtil.Base64ToBitmapImage(Constants.DEFAULT_DIR_IMAGE_BASE64), Btn_BrowsePath.Width, Btn_BrowsePath.Height);
            Btn_BrowseArgFile.Image = imageUtil.ScaleBitmap(imageUtil.Base64ToBitmapImage(Constants.DEFAULT_DIR_IMAGE_BASE64), Btn_BrowseArgFile.Width, Btn_BrowseArgFile.Height);
            Btn_BrowseFolder.Image = imageUtil.ScaleBitmap(imageUtil.Base64ToBitmapImage(Constants.DEFAULT_DIR_IMAGE_BASE64), Btn_BrowseFolder.Width, Btn_BrowseFolder.Height);
            Txt_HotKey.KeyUp += HotKeyUtil.Control_KeyUp;
            Txt_HotKey.KeyDown += HotKeyUtil.Control_KeyDown;
            var commandSystem = this.GetSystem<ICommandSystem>();
            var allCommand = commandSystem.AllCommand;
            DataGridView_Opt.RowCount = allCommand.Length;
            for (int row = 0; row < DataGridView_Opt.RowCount; row++)
            {
                var rowObj = DataGridView_Opt.Rows[row];
                rowObj.Tag = commandSystem.CommandInfo(allCommand[row]);
                RefreshDataGridViewByRow(rowObj);
            }
        }
        private void RefreshDataGridViewByRow(DataGridViewRow rowObj)
        {
            var commandObj = (IPluginCommand)rowObj.Tag;
            rowObj.Cells[Head_Name.Icon].Value = ImageUtil.GetBitmapIconByPath(commandObj.Icon);
            rowObj.Cells[Head_Name.Name].Value = commandObj.Name;
            rowObj.Cells[Head_Name.Desc].Value = commandObj.Description;
        }

        public static DialogResult Show(TPanel tPanel)
        {
            var config = AppArchitecture.Interface.GetModel<AppConfig>();
            if (!config.persistConfigureMenu.Value && !PasswordForm.ShowForm())
                return DialogResult.Cancel;

            DialogResult dialogResult;
            var hotKeySys = AppArchitecture.Interface.GetSystem<HotKeyManager>();
            hotKeySys.RemoveAllQuickActions();
            try
            {
                MainForm.ignoreDeactivate++;
                using (var properties = new BtnPropertiesFrom())
                {
                    properties.TopMost = config.topWindow.Value;
                    properties.SetTPanel(tPanel);
                    dialogResult = properties.ShowDialog();
                }
            }
            finally
            {
                MainForm.ignoreDeactivate--;
                hotKeySys.InitializeQuickActionsHotKeys();
            }
            return dialogResult;
        }
        private void SetTPanel(TPanel tPanel)
        {
            var appConfig = this.GetModel<AppConfig>();
            _OptIndex = tPanel.Index;
            int pageGridCount = appConfig.gridColumn.Value * appConfig.gridRow.Value;
            var groupNum = _OptIndex / pageGridCount;
            var num = _OptIndex % pageGridCount;
            Text = string.Format(Resources.BtnPropertiesFrom_Title, groupNum + 1, num + 1);
            _inputEntity = tPanel.Entity;
            SetEntitiy(tPanel.Entity, true);
            if (_inputEntity != null)
            {
                CheckBox_AutoRun.Checked = _inputEntity.launchOnStartup;
                CheckBox_DropLaunch.Checked = _inputEntity.dropNLaunch;
            }
        }
        private void SetEntitiy(Entity entity, bool setExtInfo)
        {
            if (entity != null)
            {
                _tempEntity = entity;
                Btn_Ok.Enabled = true;
                if (_inputEntity != null)
                    Btn_Clear.Enabled = true;
            }
            else
            {
                _tempEntity = Entity.Create();
                _tempEntity.index = -1;
                Btn_Ok.Enabled = false;
                Btn_Clear.Enabled = false;
            }
            Txt_HotKey.Text = _tempEntity.actionHotKey;
            PictureBox_Icon.Image = _tempEntity.bitmapImage;
            Btn_Parse.Enabled = _tempEntity.canParse;
            ComboBox_WindowStyle.SelectedIndex = (int)_tempEntity.windowStyle;
            ComboBox_PriorityClass.SelectedIndex = (int)_tempEntity.priorityClass;
            if (setExtInfo)
            {
                Txt_TargetPostion.TextChanged -= Txt_TargetPostion_TextChanged;
                Txt_TargetPostion.Text = _tempEntity.Path;
                Txt_Args.Text = _tempEntity.startArg;
                Txt_WorkFolder.Text = _tempEntity.workFolder;
                Txt_Desc.Text = _tempEntity.desc;
                Txt_TargetPostion.TextChanged += Txt_TargetPostion_TextChanged;
            }
        }

        private void Txt_Path_DragEnter(object sender, DragEventArgs e) => FromUtil.TextBoxFilePath_DragEnter(sender, e);
        private void Txt_Path_DragDrop(object sender, DragEventArgs e) => FromUtil.TextBoxFilePath_DragDrop(sender, e);
        private void Txt_Folder_DragEnter(object sender, DragEventArgs e) => FromUtil.TextBoxFileFolder_DragEnter(sender, e);
        private void Txt_Folder_DragDrop(object sender, DragEventArgs e) => FromUtil.TextBoxFileFolder_DragDrop(sender, e);
        private void Txt_FolderOrFile_DragEnter(object sender, DragEventArgs e) => FromUtil.TextBoxFileFolderOrFile_DragEnter(sender, e);
        private void Txt_FolderOrFile_DragDrop(object sender, DragEventArgs e) => FromUtil.TextBoxFileFolderOrFile_DragDrop(sender, e);

        private void Txt_TextBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }
        private void Txt_TargetPostion_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            string newPath = textBox.Text.Trim();
            if (string.IsNullOrEmpty(newPath))
            {
                Btn_Ok.Enabled = false;
                PictureBox_Icon.Image = null;
                return;
            }
            Entity entity = CommonCode.GetIconInfoByPath(newPath, false);
            if (entity == null)
            {
                Btn_Ok.Enabled = false;
                PictureBox_Icon.Image = null;
                return;
            }
            SetEntitiy(entity, false);
        }
        private void Btn_BrowsePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Resources.BtnPropertiesFrom_OpenFile;
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            Txt_TargetPostion.Text = openFileDialog.FileName;
        }
        private void Btn_BrowseArgFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Resources.BtnPropertiesFrom_OpenFile;
            openFileDialog.FilterIndex = 2;
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            Txt_Args.Text = openFileDialog.FileName;
        }
        private void Btn_BrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            Txt_WorkFolder.Text = folderBrowserDialog.SelectedPath;
        }
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            string filePath = Txt_TargetPostion.Text.Trim();
            _tempEntity.Path = filePath;
            _tempEntity.workFolder = Txt_WorkFolder.Text.Trim();
            _tempEntity.desc = Txt_Desc.Text.Trim();
            _tempEntity.startArg = Txt_Args.Text.Trim();
            _tempEntity.windowStyle = (WindowStyle)ComboBox_WindowStyle.SelectedIndex;
            _tempEntity.priorityClass = (PriorityClass)ComboBox_PriorityClass.SelectedIndex;
            _tempEntity.launchOnStartup = CheckBox_AutoRun.Checked;
            _tempEntity.actionHotKey = Txt_HotKey.Text;
            _tempEntity.dropNLaunch = CheckBox_DropLaunch.Checked;

            if (_inputEntity != null)
                this.SendCommand(new QuickEntityRemoveCommand() { index = _inputEntity.index });
            this.SendCommand(new QuickEntityInsert2Command() { entity = _tempEntity, index = _OptIndex });
            DialogResult = DialogResult.OK;
            Close();
        }
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(Resources.BtnPropertiesFrom_Clear, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
                return;
            this.SendCommand(new QuickEntityRemoveCommand() { index = _inputEntity.index });
            _inputEntity = null;
            SetEntitiy(null, true);
        }
        private void Btn_Parse_Click(object sender, EventArgs e)
        {
            string filePath = Txt_TargetPostion.Text.Trim();
            Entity entity = CommonCode.GetIconInfoByPath(filePath, true);
            SetEntitiy(entity, true);
        }

        private void Btn_ChangeIcon_Click(object sender, EventArgs e)
        {
            if (IconForm.Show(_tempEntity))
                SetEntitiy(_tempEntity, false);
        }

        private class Head_Name
        {
            public const string Icon = "icon";
            public const string Name = "name";
            public const string Desc = "desc";
        }

        private void DataGridView_Opt_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_Opt.SelectedRows.Count <= 0)
                return;
            var rowObj = DataGridView_Opt.SelectedRows[0];
            var commandObj = (IPluginCommand)rowObj.Tag;
            string protocol = this.GetUtility<IURIUtil>().Protocol;
            Txt_TargetPostion.Text = protocol + ':' + commandObj.GetType().FullName;
            Txt_Args.Text = string.Empty;
            Txt_WorkFolder.Text = string.Empty;
            Txt_Desc.Text = commandObj.Description;
            _tempEntity.ImagePath = commandObj.Icon;
            PictureBox_Icon.Image = ImageUtil.GetBitmapIconByPath(commandObj.Icon);
            _tempEntity.iconType = OpenType.URL;
            tabControl1.SelectedIndex = 0;
        }
    }
}
