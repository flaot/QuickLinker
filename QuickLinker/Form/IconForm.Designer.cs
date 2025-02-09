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
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)MumericUpDown_CurIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(10, 14);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "图标(&F)：";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Location = new Point(10, 40);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 1;
            label2.Text = "索引(&I)：";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // Txt_IconPath
            // 
            Txt_IconPath.AllowDrop = true;
            Txt_IconPath.Location = new Point(84, 11);
            Txt_IconPath.Name = "Txt_IconPath";
            Txt_IconPath.Size = new Size(194, 23);
            Txt_IconPath.TabIndex = 2;
            Txt_IconPath.TextChanged += Txt_IconPath_TextChanged;
            Txt_IconPath.DragDrop += Txt_FolderOrFile_DragDrop;
            Txt_IconPath.DragEnter += Txt_FolderOrFile_DragEnter;
            // 
            // Btn_Browse
            // 
            Btn_Browse.Location = new Point(284, 11);
            Btn_Browse.Name = "Btn_Browse";
            Btn_Browse.Size = new Size(30, 25);
            Btn_Browse.TabIndex = 3;
            Btn_Browse.Text = "button1";
            Btn_Browse.Click += Btn_Browse_Click;
            // 
            // MumericUpDown_CurIndex
            // 
            MumericUpDown_CurIndex.Location = new Point(84, 40);
            MumericUpDown_CurIndex.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MumericUpDown_CurIndex.Name = "MumericUpDown_CurIndex";
            MumericUpDown_CurIndex.Size = new Size(68, 23);
            MumericUpDown_CurIndex.TabIndex = 4;
            MumericUpDown_CurIndex.Value = new decimal(new int[] { 1, 0, 0, 0 });
            MumericUpDown_CurIndex.ValueChanged += MumericUpDown_CurIndex_ValueChanged;
            // 
            // Txt_MaxIndex
            // 
            Txt_MaxIndex.AutoSize = true;
            Txt_MaxIndex.Location = new Point(158, 42);
            Txt_MaxIndex.Name = "Txt_MaxIndex";
            Txt_MaxIndex.Size = new Size(20, 17);
            Txt_MaxIndex.TabIndex = 5;
            Txt_MaxIndex.Text = "/1";
            // 
            // Btn_OK
            // 
            Btn_OK.Location = new Point(183, 117);
            Btn_OK.Name = "Btn_OK";
            Btn_OK.Size = new Size(75, 25);
            Btn_OK.TabIndex = 8;
            Btn_OK.Text = "确定";
            Btn_OK.UseVisualStyleBackColor = true;
            Btn_OK.Click += Btn_OK_Click;
            // 
            // Btn_Cancel
            // 
            Btn_Cancel.DialogResult = DialogResult.Cancel;
            Btn_Cancel.Location = new Point(264, 117);
            Btn_Cancel.Name = "Btn_Cancel";
            Btn_Cancel.Size = new Size(75, 25);
            Btn_Cancel.TabIndex = 9;
            Btn_Cancel.Text = "取消";
            Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(269, 42);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(Txt_IconPath);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(MumericUpDown_CurIndex);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(Txt_MaxIndex);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(Btn_Browse);
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(329, 91);
            panel1.TabIndex = 12;
            // 
            // IconForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 154);
            Controls.Add(panel1);
            Controls.Add(Btn_Cancel);
            Controls.Add(Btn_OK);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IconForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "按钮图标";
            Load += IconForm_Load;
            ((System.ComponentModel.ISupportInitialize)MumericUpDown_CurIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private PictureBox pictureBox1;
        private Panel panel1;
    }
}