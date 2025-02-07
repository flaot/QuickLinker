using QFramework;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Utils;
using System;
using System.IO;
using System.Windows.Forms;
using AppConfig = QuickLinker.Model.AppConfig;

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

        public static DialogResult Show(TPanel tPanel)
        {
            var config = AppArchitecture.Interface.GetModel<AppConfig>();
            using (var properties = new BtnPropertiesFrom())
            {
                properties.TopMost = config.topWindow.Value;
                properties.SetTPanel(tPanel);
                return properties.ShowDialog();
            }
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
                CheckBox_AutoRun.Checked = _inputEntity.autoRun;
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
                _tempEntity = new Entity();
                _tempEntity.index = -1;
                Btn_Ok.Enabled = false;
                Btn_Clear.Enabled = false;
            }
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
            _tempEntity.autoRun = CheckBox_AutoRun.Checked;

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
    }
}
