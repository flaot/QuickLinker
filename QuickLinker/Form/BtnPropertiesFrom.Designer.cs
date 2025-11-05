using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker
{
    partial class BtnPropertiesFrom
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BtnPropertiesFrom));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            PictureBox_Icon = new TButton();
            Btn_ChangeIcon = new Button();
            label11 = new Label();
            label12 = new Label();
            checkBox3 = new CheckBox();
            CheckBox_AutoRun = new CheckBox();
            CheckBox1 = new CheckBox();
            label9 = new Label();
            label8 = new Label();
            label10 = new Label();
            ComboBox_PriorityClass = new ComboBox();
            label7 = new Label();
            ComboBox_WindowStyle = new ComboBox();
            label6 = new Label();
            Txt_HotKey = new TextBox();
            label5 = new Label();
            Txt_Desc = new TextBox();
            label4 = new Label();
            Btn_BrowseFolder = new TButton();
            Txt_WorkFolder = new TextBox();
            label3 = new Label();
            Btn_BrowseArgFile = new TButton();
            Btn_BrowsePath = new TButton();
            Txt_Args = new TextBox();
            label2 = new Label();
            Btn_Parse = new TButton();
            Txt_TargetPostion = new TextBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            DataGridView_Opt = new DataGridView();
            icon = new DataGridViewImageColumn();
            name = new DataGridViewTextBoxColumn();
            desc = new DataGridViewTextBoxColumn();
            splitContainer1 = new SplitContainer();
            Btn_Cancel = new Button();
            Btn_Ok = new Button();
            Btn_Clear = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridView_Opt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(518, 230);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(240, 240, 240);
            tabPage1.Controls.Add(PictureBox_Icon);
            tabPage1.Controls.Add(Btn_ChangeIcon);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(label12);
            tabPage1.Controls.Add(checkBox3);
            tabPage1.Controls.Add(CheckBox_AutoRun);
            tabPage1.Controls.Add(CheckBox1);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(ComboBox_PriorityClass);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(ComboBox_WindowStyle);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(Txt_HotKey);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(Txt_Desc);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(Btn_BrowseFolder);
            tabPage1.Controls.Add(Txt_WorkFolder);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(Btn_BrowseArgFile);
            tabPage1.Controls.Add(Btn_BrowsePath);
            tabPage1.Controls.Add(Txt_Args);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(Btn_Parse);
            tabPage1.Controls.Add(Txt_TargetPostion);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(510, 200);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "运行一个程序";
            // 
            // PictureBox_Icon
            // 
            PictureBox_Icon.allowStyle = false;
            PictureBox_Icon.BackColor = Color.FromArgb(240, 240, 240);
            PictureBox_Icon.BackgroundImageLayout = ImageLayout.Stretch;
            PictureBox_Icon.Image = null;
            PictureBox_Icon.Location = new Point(380, 154);
            PictureBox_Icon.Margin = new Padding(0);
            PictureBox_Icon.Name = "PictureBox_Icon";
            PictureBox_Icon.Size = new Size(32, 32);
            PictureBox_Icon.TabIndex = 26;
            // 
            // Btn_ChangeIcon
            // 
            Btn_ChangeIcon.Location = new Point(424, 159);
            Btn_ChangeIcon.Name = "Btn_ChangeIcon";
            Btn_ChangeIcon.Size = new Size(65, 25);
            Btn_ChangeIcon.TabIndex = 27;
            Btn_ChangeIcon.Text = "更改(&A)...";
            Btn_ChangeIcon.UseVisualStyleBackColor = true;
            Btn_ChangeIcon.Click += Btn_ChangeIcon_Click;
            // 
            // label11
            // 
            label11.BorderStyle = BorderStyle.Fixed3D;
            label11.Location = new Point(399, 135);
            label11.Name = "label11";
            label11.Size = new Size(100, 2);
            label11.TabIndex = 25;
            label11.Text = "label11";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(361, 126);
            label12.Name = "label12";
            label12.Size = new Size(32, 17);
            label12.TabIndex = 24;
            label12.Text = "图标";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Enabled = false;
            checkBox3.Location = new Point(366, 91);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(134, 21);
            checkBox3.TabIndex = 23;
            checkBox3.Text = "关闭QuickLinker(&T)";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // CheckBox_AutoRun
            // 
            CheckBox_AutoRun.AutoSize = true;
            CheckBox_AutoRun.Location = new Point(366, 64);
            CheckBox_AutoRun.Name = "CheckBox_AutoRun";
            CheckBox_AutoRun.Size = new Size(128, 21);
            CheckBox_AutoRun.TabIndex = 22;
            CheckBox_AutoRun.Text = "系统启动时运行(&U)";
            CheckBox_AutoRun.UseVisualStyleBackColor = true;
            // 
            // CheckBox1
            // 
            CheckBox1.AutoSize = true;
            CheckBox1.Enabled = false;
            CheckBox1.Location = new Point(366, 37);
            CheckBox1.Name = "CheckBox1";
            CheckBox1.Size = new Size(91, 21);
            CheckBox1.TabIndex = 21;
            CheckBox1.Text = "拖放启动(&R)";
            CheckBox1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.BorderStyle = BorderStyle.Fixed3D;
            label9.Location = new Point(399, 19);
            label9.Name = "label9";
            label9.Size = new Size(100, 2);
            label9.TabIndex = 20;
            label9.Text = "label9";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(361, 10);
            label8.Name = "label8";
            label8.Size = new Size(32, 17);
            label8.TabIndex = 19;
            label8.Text = "选项";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.BorderStyle = BorderStyle.Fixed3D;
            label10.Location = new Point(353, 14);
            label10.Name = "label10";
            label10.Size = new Size(2, 175);
            label10.TabIndex = 18;
            label10.Text = "label10";
            // 
            // ComboBox_PriorityClass
            // 
            ComboBox_PriorityClass.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_PriorityClass.FormattingEnabled = true;
            ComboBox_PriorityClass.Items.AddRange(new object[] { "实时", "高", "高于正常", "正常", "低于正常", "低" });
            ComboBox_PriorityClass.Location = new Point(265, 161);
            ComboBox_PriorityClass.Name = "ComboBox_PriorityClass";
            ComboBox_PriorityClass.Size = new Size(81, 25);
            ComboBox_PriorityClass.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(191, 164);
            label7.Name = "label7";
            label7.Size = new Size(74, 17);
            label7.TabIndex = 16;
            label7.Text = "优先级(&O)：";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ComboBox_WindowStyle
            // 
            ComboBox_WindowStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_WindowStyle.FormattingEnabled = true;
            ComboBox_WindowStyle.Items.AddRange(new object[] { "标准", "最小化", "最大化", "隐藏" });
            ComboBox_WindowStyle.Location = new Point(101, 161);
            ComboBox_WindowStyle.Name = "ComboBox_WindowStyle";
            ComboBox_WindowStyle.Size = new Size(89, 25);
            ComboBox_WindowStyle.TabIndex = 15;
            // 
            // label6
            // 
            label6.Location = new Point(5, 164);
            label6.Name = "label6";
            label6.Size = new Size(93, 17);
            label6.TabIndex = 14;
            label6.Text = "窗口样式(&W)：";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Txt_HotKey
            // 
            Txt_HotKey.Location = new Point(101, 130);
            Txt_HotKey.Name = "Txt_HotKey";
            Txt_HotKey.Size = new Size(239, 23);
            Txt_HotKey.TabIndex = 13;
            // 
            // label5
            // 
            label5.Location = new Point(11, 133);
            label5.Name = "label5";
            label5.Size = new Size(84, 17);
            label5.TabIndex = 12;
            label5.Text = "快捷键(&K)：";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Txt_Desc
            // 
            Txt_Desc.Location = new Point(101, 99);
            Txt_Desc.Name = "Txt_Desc";
            Txt_Desc.Size = new Size(239, 23);
            Txt_Desc.TabIndex = 11;
            Txt_Desc.Leave += Txt_TextBox_Leave;
            // 
            // label4
            // 
            label4.Location = new Point(11, 102);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 10;
            label4.Text = "描述(&D)：";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Btn_BrowseFolder
            // 
            Btn_BrowseFolder.allowStyle = false;
            Btn_BrowseFolder.BackColor = Color.FromArgb(240, 240, 240);
            Btn_BrowseFolder.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_BrowseFolder.Image = null;
            Btn_BrowseFolder.Location = new Point(322, 68);
            Btn_BrowseFolder.Margin = new Padding(0);
            Btn_BrowseFolder.Name = "Btn_BrowseFolder";
            Btn_BrowseFolder.Size = new Size(24, 24);
            Btn_BrowseFolder.TabIndex = 9;
            Btn_BrowseFolder.Click += Btn_BrowseFolder_Click;
            // 
            // Txt_WorkFolder
            // 
            Txt_WorkFolder.AllowDrop = true;
            Txt_WorkFolder.Location = new Point(101, 70);
            Txt_WorkFolder.Name = "Txt_WorkFolder";
            Txt_WorkFolder.Size = new Size(215, 23);
            Txt_WorkFolder.TabIndex = 8;
            Txt_WorkFolder.DragDrop += Txt_Folder_DragDrop;
            Txt_WorkFolder.DragEnter += Txt_Folder_DragEnter;
            Txt_WorkFolder.Leave += Txt_TextBox_Leave;
            // 
            // label3
            // 
            label3.Location = new Point(11, 73);
            label3.Name = "label3";
            label3.Size = new Size(84, 17);
            label3.TabIndex = 7;
            label3.Text = "起始位置(&S)：";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Btn_BrowseArgFile
            // 
            Btn_BrowseArgFile.allowStyle = false;
            Btn_BrowseArgFile.BackColor = Color.FromArgb(240, 240, 240);
            Btn_BrowseArgFile.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_BrowseArgFile.Image = null;
            Btn_BrowseArgFile.Location = new Point(322, 40);
            Btn_BrowseArgFile.Margin = new Padding(0);
            Btn_BrowseArgFile.Name = "Btn_BrowseArgFile";
            Btn_BrowseArgFile.Size = new Size(24, 24);
            Btn_BrowseArgFile.TabIndex = 6;
            Btn_BrowseArgFile.Click += Btn_BrowseArgFile_Click;
            // 
            // Btn_BrowsePath
            // 
            Btn_BrowsePath.allowStyle = false;
            Btn_BrowsePath.BackColor = Color.FromArgb(240, 240, 240);
            Btn_BrowsePath.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_BrowsePath.Image = null;
            Btn_BrowsePath.Location = new Point(322, 12);
            Btn_BrowsePath.Margin = new Padding(0);
            Btn_BrowsePath.Name = "Btn_BrowsePath";
            Btn_BrowsePath.Size = new Size(24, 24);
            Btn_BrowsePath.TabIndex = 3;
            Btn_BrowsePath.Click += Btn_BrowsePath_Click;
            // 
            // Txt_Args
            // 
            Txt_Args.AllowDrop = true;
            Txt_Args.Location = new Point(101, 41);
            Txt_Args.Name = "Txt_Args";
            Txt_Args.Size = new Size(215, 23);
            Txt_Args.TabIndex = 5;
            Txt_Args.DragDrop += Txt_FolderOrFile_DragDrop;
            Txt_Args.DragEnter += Txt_FolderOrFile_DragEnter;
            Txt_Args.Leave += Txt_TextBox_Leave;
            // 
            // label2
            // 
            label2.Location = new Point(11, 41);
            label2.Name = "label2";
            label2.Size = new Size(84, 17);
            label2.TabIndex = 4;
            label2.Text = "参数(&P)：";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Btn_Parse
            // 
            Btn_Parse.allowStyle = false;
            Btn_Parse.BackColor = Color.FromArgb(240, 240, 240);
            Btn_Parse.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Parse.Image = null;
            Btn_Parse.Location = new Point(293, 12);
            Btn_Parse.Margin = new Padding(0);
            Btn_Parse.Name = "Btn_Parse";
            Btn_Parse.Size = new Size(24, 24);
            Btn_Parse.TabIndex = 2;
            Btn_Parse.Click += Btn_Parse_Click;
            // 
            // Txt_TargetPostion
            // 
            Txt_TargetPostion.AllowDrop = true;
            Txt_TargetPostion.Location = new Point(101, 11);
            Txt_TargetPostion.Name = "Txt_TargetPostion";
            Txt_TargetPostion.Size = new Size(187, 23);
            Txt_TargetPostion.TabIndex = 1;
            Txt_TargetPostion.TextChanged += Txt_TargetPostion_TextChanged;
            Txt_TargetPostion.DragDrop += Txt_FolderOrFile_DragDrop;
            Txt_TargetPostion.DragEnter += Txt_FolderOrFile_DragEnter;
            Txt_TargetPostion.Leave += Txt_TextBox_Leave;
            // 
            // label1
            // 
            label1.Location = new Point(8, 15);
            label1.Name = "label1";
            label1.Size = new Size(87, 17);
            label1.TabIndex = 0;
            label1.Text = "目标位置(&C)：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.ButtonFace;
            tabPage2.Controls.Add(DataGridView_Opt);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(510, 200);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "执行一个命令";
            // 
            // DataGridView_Opt
            // 
            DataGridView_Opt.AllowUserToAddRows = false;
            DataGridView_Opt.AllowUserToDeleteRows = false;
            DataGridView_Opt.AllowUserToResizeRows = false;
            DataGridView_Opt.BackgroundColor = Color.White;
            DataGridView_Opt.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DataGridView_Opt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridView_Opt.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView_Opt.Columns.AddRange(new DataGridViewColumn[] { icon, name, desc });
            DataGridView_Opt.Dock = DockStyle.Fill;
            DataGridView_Opt.Location = new Point(0, 0);
            DataGridView_Opt.MultiSelect = false;
            DataGridView_Opt.Name = "DataGridView_Opt";
            DataGridView_Opt.ReadOnly = true;
            DataGridView_Opt.RowHeadersVisible = false;
            DataGridView_Opt.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Opt.Size = new Size(510, 200);
            DataGridView_Opt.TabIndex = 0;
            DataGridView_Opt.CellContentDoubleClick += DataGridView_Opt_CellContentDoubleClick;
            // 
            // icon
            // 
            icon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = resources.GetObject("dataGridViewCellStyle2.NullValue");
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            icon.DefaultCellStyle = dataGridViewCellStyle2;
            icon.HeaderText = "";
            icon.ImageLayout = DataGridViewImageCellLayout.Stretch;
            icon.MinimumWidth = 25;
            icon.Name = "icon";
            icon.ReadOnly = true;
            icon.Width = 25;
            // 
            // name
            // 
            name.HeaderText = "名称";
            name.Name = "name";
            name.ReadOnly = true;
            // 
            // desc
            // 
            desc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            desc.HeaderText = "描述";
            desc.Name = "desc";
            desc.ReadOnly = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(Btn_Cancel);
            splitContainer1.Panel2.Controls.Add(Btn_Ok);
            splitContainer1.Panel2.Controls.Add(Btn_Clear);
            splitContainer1.Size = new Size(518, 272);
            splitContainer1.SplitterDistance = 230;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 1;
            // 
            // Btn_Cancel
            // 
            Btn_Cancel.DialogResult = DialogResult.Cancel;
            Btn_Cancel.Location = new Point(438, 7);
            Btn_Cancel.Name = "Btn_Cancel";
            Btn_Cancel.Size = new Size(75, 25);
            Btn_Cancel.TabIndex = 2;
            Btn_Cancel.Text = "取消";
            Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // Btn_Ok
            // 
            Btn_Ok.Location = new Point(357, 7);
            Btn_Ok.Name = "Btn_Ok";
            Btn_Ok.Size = new Size(75, 25);
            Btn_Ok.TabIndex = 1;
            Btn_Ok.Text = "确定";
            Btn_Ok.UseVisualStyleBackColor = true;
            Btn_Ok.Click += Btn_Ok_Click;
            // 
            // Btn_Clear
            // 
            Btn_Clear.Location = new Point(245, 7);
            Btn_Clear.Name = "Btn_Clear";
            Btn_Clear.Size = new Size(75, 25);
            Btn_Clear.TabIndex = 0;
            Btn_Clear.Text = "清除";
            Btn_Clear.UseVisualStyleBackColor = true;
            Btn_Clear.Click += Btn_Clear_Click;
            // 
            // BtnPropertiesFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(518, 272);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BtnPropertiesFrom";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "BtnPropertiesFrom";
            Load += BtnPropertiesFrom_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridView_Opt).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox Txt_TargetPostion;
        private Label label1;
        private ComboBox ComboBox_PriorityClass;
        private Label label7;
        private ComboBox ComboBox_WindowStyle;
        private Label label6;
        private TextBox Txt_HotKey;
        private Label label5;
        private TextBox Txt_Desc;
        private Label label4;
        private TButton Btn_BrowseFolder;
        private TextBox Txt_WorkFolder;
        private Label label3;
        private TButton Btn_BrowseArgFile;
        private TButton Btn_BrowsePath;
        private TextBox Txt_Args;
        private Label label2;
        private TButton Btn_Parse;
        private Label label9;
        private Label label8;
        private Label label10;
        private Button Btn_ChangeIcon;
        private Label label11;
        private Label label12;
        private CheckBox checkBox3;
        private CheckBox CheckBox_AutoRun;
        private CheckBox CheckBox1;
        private SplitContainer splitContainer1;
        private Button Btn_Cancel;
        private Button Btn_Ok;
        private Button Btn_Clear;
        private TabPage tabPage2;
        private DataGridView DataGridView_Opt;
        private DataGridViewImageColumn icon;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn desc;
        private TButton PictureBox_Icon;
    }
}