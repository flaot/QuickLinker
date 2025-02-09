using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuickLinker.Model;

namespace QuickLinker
{
    public partial class IconForm : Form
    {
        private Entity _entity;
        public IconForm(Entity entity)
        {
            _entity = entity;
            InitializeComponent();
        }

        private void IconForm_Load(object sender, EventArgs e)
        {
            Txt_IconPath.TextChanged -= Txt_IconPath_TextChanged;
            MumericUpDown_CurIndex.ValueChanged -= MumericUpDown_CurIndex_ValueChanged;
            Txt_IconPath.Text = _entity.ImagePath;
            pictureBox1.Image = _entity.bitmapImage;
            var iconTotalCount = FileIcon.PrivateExtractIcons(_entity.ImagePath, 0, 0, 0, null, null, 0, 0);
            if (iconTotalCount > 0)
            {
                MumericUpDown_CurIndex.Value = _entity.imageIndex + 1;
                MumericUpDown_CurIndex.Maximum = iconTotalCount;
            }
            else
            {
                MumericUpDown_CurIndex.Value = 1;
                MumericUpDown_CurIndex.Maximum = 1;
            }
            Txt_MaxIndex.Text = "/" + MumericUpDown_CurIndex.Maximum;
            Btn_OK.Enabled = !string.IsNullOrWhiteSpace(Txt_IconPath.Text);
            Txt_IconPath.TextChanged += Txt_IconPath_TextChanged;
            MumericUpDown_CurIndex.ValueChanged += MumericUpDown_CurIndex_ValueChanged;
        }

        private void MumericUpDown_CurIndex_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = FileIcon.GetBitmapImage(Txt_IconPath.Text, ((int)MumericUpDown_CurIndex.Value) - 1);
        }
        private void Txt_FolderOrFile_DragEnter(object sender, DragEventArgs e) => FromUtil.TextBoxFileFolderOrFile_DragEnter(sender, e);
        private void Txt_FolderOrFile_DragDrop(object sender, DragEventArgs e) => FromUtil.TextBoxFileFolderOrFile_DragDrop(sender, e);

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Resources.IconFrom_OpenFile;
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            Txt_IconPath.Text = openFileDialog.FileName;
        }

        private void Txt_IconPath_TextChanged(object sender, EventArgs e)
        {
            MumericUpDown_CurIndex.ValueChanged -= MumericUpDown_CurIndex_ValueChanged;
            var iconTotalCount = FileIcon.PrivateExtractIcons(Txt_IconPath.Text, 0, 0, 0, null, null, 0, 0);
            if (iconTotalCount > 0)
            {
                MumericUpDown_CurIndex.Value = 1;
                MumericUpDown_CurIndex.Maximum = iconTotalCount;
            }
            else
            {
                MumericUpDown_CurIndex.Value = 1;
                MumericUpDown_CurIndex.Maximum = 1;
            }
            Txt_MaxIndex.Text = "/" + MumericUpDown_CurIndex.Maximum;
            var newIconPath = Txt_IconPath.Text.Trim();
            Btn_OK.Enabled = !string.IsNullOrEmpty(newIconPath);
            pictureBox1.Image = ImageUtil.GetBitmapIconByPath(newIconPath, ((int)MumericUpDown_CurIndex.Value) - 1);
            MumericUpDown_CurIndex.ValueChanged += MumericUpDown_CurIndex_ValueChanged;
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            _entity.ImagePath = Txt_IconPath.Text.Trim();
            _entity.bitmapImage = (Bitmap)pictureBox1.Image;
            _entity.imageIndex = ((int)MumericUpDown_CurIndex.Value) - 1;
            DialogResult = DialogResult.OK;
            Close();
        }

        public static bool Show(Entity entity)
        {
            var config = AppArchitecture.Interface.GetModel<AppConfig>();
            using (IconForm iconForm = new IconForm(entity))
            {
                iconForm.TopMost = config.topWindow.Value;
                if (iconForm.ShowDialog() != DialogResult.OK)
                    return false;
                return true;
            }
        }
    }
}
