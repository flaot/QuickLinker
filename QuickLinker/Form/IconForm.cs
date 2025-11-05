using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using Constants = QuickLinker.QuickLaunch.Constant.Constants;

namespace QuickLinker
{
    public partial class IconForm : Form, IController
    {
        private Entity _entity;
        public IArchitecture GetArchitecture() => AppArchitecture.Interface;
        public IconForm(Entity entity)
        {
            _entity = entity;
            InitializeComponent();
        }

        private void IconForm_Load(object sender, EventArgs e)
        {
            var imageUtil = this.GetUtility<IImageUtil>();
            Btn_Browse.Image = imageUtil.ScaleBitmap(imageUtil.Base64ToBitmapImage(Constants.DEFAULT_DIR_IMAGE_BASE64), Btn_Browse.Width, Btn_Browse.Height);

            Txt_IconPath.TextChanged -= Txt_IconPath_TextChanged;
            MumericUpDown_CurIndex.ValueChanged -= MumericUpDown_CurIndex_ValueChanged;
            Txt_IconPath.Text = _entity.ImagePath;
            pictureBox1.Image = _entity.bitmapImage;
            var iconTotalCount = Win32API.PrivateExtractIcons(_entity.ImagePath, 0, 0, 0, null, null, 0, 0);
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
            var fileIconSystem = this.GetSystem<IFileIconSystem>();
            pictureBox1.Image = fileIconSystem.GetImage(Txt_IconPath.Text, ((int)MumericUpDown_CurIndex.Value) - 1) as Bitmap;
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
            var iconTotalCount = Win32API.PrivateExtractIcons(Txt_IconPath.Text, 0, 0, 0, null, null, 0, 0);
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
            try
            {
                MainForm.ignoreDeactivate++;
                using (IconForm iconForm = new IconForm(entity))
                {
                    iconForm.TopMost = config.topWindow.Value;
                    if (iconForm.ShowDialog() != DialogResult.OK)
                        return false;
                    return true;
                }
            }
            finally
            {
                MainForm.ignoreDeactivate--;
            }
        }
    }
}
