using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker
{
    partial class IconForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconForm));
            label1 = new Label();
            label2 = new Label();
            Txt_IconPath = new TextBox();
            Btn_Browse = new TButton();
            MumericUpDown_CurIndex = new NumericUpDown();
            Txt_MaxIndex = new Label();
            Btn_OK = new Button();
            Btn_Cancel = new Button();
            panel1 = new Panel();
            pictureBox1 = new TButton();
            ((System.ComponentModel.ISupportInitialize)MumericUpDown_CurIndex).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(11, 13);
            label1.Name = "label1";
            label1.Size = new Size(72, 16);
            label1.TabIndex = 0;
            label1.Text = "图标(&F)：";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Location = new Point(11, 38);
            label2.Name = "label2";
            label2.Size = new Size(72, 16);
            label2.TabIndex = 3;
            label2.Text = "索引(&I)：";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // Txt_IconPath
            // 
            Txt_IconPath.AllowDrop = true;
            Txt_IconPath.Location = new Point(89, 10);
            Txt_IconPath.Name = "Txt_IconPath";
            Txt_IconPath.Size = new Size(206, 23);
            Txt_IconPath.TabIndex = 1;
            Txt_IconPath.TextChanged += Txt_IconPath_TextChanged;
            Txt_IconPath.DragDrop += Txt_FolderOrFile_DragDrop;
            Txt_IconPath.DragEnter += Txt_FolderOrFile_DragEnter;
            // 
            // Btn_Browse
            // 
            Btn_Browse.allowStyle = false;
            Btn_Browse.BackColor = Color.FromArgb(240, 240, 240);
            Btn_Browse.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Browse.Image = null;
            Btn_Browse.Location = new Point(301, 10);
            Btn_Browse.Margin = new Padding(6, 5, 6, 5);
            Btn_Browse.Name = "Btn_Browse";
            Btn_Browse.Size = new Size(32, 23);
            Btn_Browse.TabIndex = 2;
            Btn_Browse.Click += Btn_Browse_Click;
            // 
            // MumericUpDown_CurIndex
            // 
            MumericUpDown_CurIndex.Location = new Point(89, 38);
            MumericUpDown_CurIndex.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MumericUpDown_CurIndex.Name = "MumericUpDown_CurIndex";
            MumericUpDown_CurIndex.Size = new Size(72, 23);
            MumericUpDown_CurIndex.TabIndex = 4;
            MumericUpDown_CurIndex.Value = new decimal(new int[] { 1, 0, 0, 0 });
            MumericUpDown_CurIndex.ValueChanged += MumericUpDown_CurIndex_ValueChanged;
            // 
            // Txt_MaxIndex
            // 
            Txt_MaxIndex.AutoSize = true;
            Txt_MaxIndex.Location = new Point(167, 39);
            Txt_MaxIndex.Name = "Txt_MaxIndex";
            Txt_MaxIndex.Size = new Size(20, 17);
            Txt_MaxIndex.TabIndex = 5;
            Txt_MaxIndex.Text = "/1";
            // 
            // Btn_OK
            // 
            Btn_OK.Location = new Point(194, 110);
            Btn_OK.Name = "Btn_OK";
            Btn_OK.Size = new Size(79, 23);
            Btn_OK.TabIndex = 1;
            Btn_OK.Text = "确定";
            Btn_OK.UseVisualStyleBackColor = true;
            Btn_OK.Click += Btn_OK_Click;
            // 
            // Btn_Cancel
            // 
            Btn_Cancel.DialogResult = DialogResult.Cancel;
            Btn_Cancel.Location = new Point(280, 110);
            Btn_Cancel.Name = "Btn_Cancel";
            Btn_Cancel.Size = new Size(79, 23);
            Btn_Cancel.TabIndex = 2;
            Btn_Cancel.Text = "取消";
            Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(Txt_IconPath);
            panel1.Controls.Add(MumericUpDown_CurIndex);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(Txt_MaxIndex);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(Btn_Browse);
            panel1.Location = new Point(11, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(349, 86);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.allowStyle = false;
            pictureBox1.BackColor = Color.FromArgb(240, 240, 240);
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = null;
            pictureBox1.Location = new Point(286, 39);
            pictureBox1.Margin = new Padding(6, 5, 6, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(34, 30);
            pictureBox1.TabIndex = 6;
            // 
            // IconForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(375, 145);
            Controls.Add(panel1);
            Controls.Add(Btn_Cancel);
            Controls.Add(Btn_OK);
            Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IconForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "按钮图标";
            Load += IconForm_Load;
            ((System.ComponentModel.ISupportInitialize)MumericUpDown_CurIndex).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox Txt_IconPath;
        private TButton Btn_Browse;
        private NumericUpDown MumericUpDown_CurIndex;
        private Label Txt_MaxIndex;
        private Button Btn_OK;
        private Button Btn_Cancel;
        private Panel panel1;
        private TButton pictureBox1;
    }
}