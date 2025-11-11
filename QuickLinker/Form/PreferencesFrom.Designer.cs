using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker
{
    partial class PreferencesFrom
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            TreeNode treeNode1 = new TreeNode("状态栏");
            TreeNode treeNode2 = new TreeNode("按钮标题");
            TreeNode treeNode3 = new TreeNode("组标签");
            TreeNode treeNode4 = new TreeNode("QuickLinker 字体设置", new TreeNode[] { treeNode1, treeNode2, treeNode3 });
            TreeNode treeNode5 = new TreeNode("启动按钮");
            TreeNode treeNode6 = new TreeNode("切换组");
            TreeNode treeNode7 = new TreeNode("拖放对象");
            TreeNode treeNode8 = new TreeNode("按钮操作 （交换、排列、复制）");
            TreeNode treeNode9 = new TreeNode("QuickLinker 声音事件", new TreeNode[] { treeNode5, treeNode6, treeNode7, treeNode8 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesFrom));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            checkBox19 = new CheckBox();
            label28 = new Label();
            checkBox18 = new CheckBox();
            checkBox10 = new CheckBox();
            checkBox9 = new CheckBox();
            label2 = new Label();
            checkBox8 = new CheckBox();
            textBox1 = new TextBox();
            label1 = new Label();
            checkBox7 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            tabPage2 = new TabPage();
            label27 = new Label();
            label26 = new Label();
            trackBar1 = new TrackBar();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            checkBox16 = new CheckBox();
            label7 = new Label();
            checkBox15 = new CheckBox();
            checkBox14 = new CheckBox();
            comboBox3 = new ComboBox();
            label6 = new Label();
            comboBox2 = new ComboBox();
            label5 = new Label();
            checkBox13 = new CheckBox();
            checkBox12 = new CheckBox();
            checkBox11 = new CheckBox();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label3 = new Label();
            tabPage3 = new TabPage();
            label24 = new Label();
            dataGridView1 = new DataGridView();
            Number = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            comboBox4 = new ComboBox();
            radioButton1 = new RadioButton();
            label22 = new Label();
            Btn_GroupDown = new Button();
            Btn_GroupUp = new Button();
            label21 = new Label();
            numericUpDown5 = new NumericUpDown();
            label20 = new Label();
            numericUpDown4 = new NumericUpDown();
            label19 = new Label();
            label17 = new Label();
            label18 = new Label();
            numericUpDown3 = new NumericUpDown();
            label16 = new Label();
            numericUpDown2 = new NumericUpDown();
            label15 = new Label();
            numericUpDown1 = new NumericUpDown();
            label14 = new Label();
            checkBox17 = new CheckBox();
            label12 = new Label();
            label13 = new Label();
            tabPage4 = new TabPage();
            Txt_FontChangeTip = new Label();
            Btn_FontDefault = new Button();
            Btn_FontChange = new Button();
            TreeView_Font = new TreeView();
            label31 = new Label();
            label32 = new Label();
            tabPage5 = new TabPage();
            Btn_AudioBrowse = new TButton();
            Btn_TestAudio = new Button();
            TextBox_AudioFilePath = new TextBox();
            RadioBtn_Custom = new RadioButton();
            RadioBtn_Default = new RadioButton();
            RadioBtn_Null = new RadioButton();
            TreeView_Audio = new TreeView();
            Txt_AudioChangeTip = new Label();
            label29 = new Label();
            label30 = new Label();
            tabPage6 = new TabPage();
            linkLabel1 = new LinkLabel();
            label25 = new Label();
            label23 = new Label();
            tabPage7 = new TabPage();
            checkBox23 = new CheckBox();
            label39 = new Label();
            label40 = new Label();
            checkBox22 = new CheckBox();
            checkBox21 = new CheckBox();
            checkBox20 = new CheckBox();
            label37 = new Label();
            label38 = new Label();
            Txt_RPassword = new TextBox();
            label36 = new Label();
            Txt_Password = new TextBox();
            label35 = new Label();
            label33 = new Label();
            label34 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage7.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(0, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(448, 271);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Transparent;
            tabPage1.Controls.Add(checkBox19);
            tabPage1.Controls.Add(label28);
            tabPage1.Controls.Add(checkBox18);
            tabPage1.Controls.Add(checkBox10);
            tabPage1.Controls.Add(checkBox9);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(checkBox8);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(checkBox7);
            tabPage1.Controls.Add(checkBox6);
            tabPage1.Controls.Add(checkBox5);
            tabPage1.Controls.Add(checkBox4);
            tabPage1.Controls.Add(checkBox3);
            tabPage1.Controls.Add(checkBox2);
            tabPage1.Controls.Add(checkBox1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(440, 241);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "常规";
            // 
            // checkBox19
            // 
            checkBox19.AutoSize = true;
            checkBox19.Location = new Point(8, 203);
            checkBox19.Name = "checkBox19";
            checkBox19.Size = new Size(89, 21);
            checkBox19.TabIndex = 8;
            checkBox19.Text = "注册URI(&U)";
            checkBox19.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            label28.BorderStyle = BorderStyle.Fixed3D;
            label28.Location = new Point(284, 118);
            label28.Name = "label28";
            label28.Size = new Size(100, 2);
            label28.TabIndex = 13;
            label28.Text = "label28";
            // 
            // checkBox18
            // 
            checkBox18.AutoSize = true;
            checkBox18.Location = new Point(8, 179);
            checkBox18.Name = "checkBox18";
            checkBox18.Size = new Size(162, 21);
            checkBox18.TabIndex = 7;
            checkBox18.Text = "在截屏或录屏中不可见(&S)";
            checkBox18.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(236, 155);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(115, 21);
            checkBox10.TabIndex = 15;
            checkBox10.Text = "处理启动按钮(&C)";
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(236, 132);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(175, 21);
            checkBox9.TabIndex = 14;
            checkBox9.Text = "启动时运行 QuickLinker(&R)";
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(222, 109);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 12;
            label2.Text = "启动选项";
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(236, 70);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(126, 21);
            checkBox8.TabIndex = 11;
            checkBox8.Text = "显示在鼠标位置(&P)";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(236, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(155, 23);
            textBox1.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(222, 21);
            label1.Name = "label1";
            label1.Size = new Size(132, 17);
            label1.TabIndex = 9;
            label1.Text = "QuickLinker 快捷键(&K)";
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(8, 155);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(116, 21);
            checkBox7.TabIndex = 6;
            checkBox7.Text = "防止重复运行(&D)";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(8, 132);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(165, 21);
            checkBox6.TabIndex = 5;
            checkBox6.Text = "忽略空白按钮上的点击(&O)";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(8, 109);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(93, 21);
            checkBox5.TabIndex = 4;
            checkBox5.Text = "禁止移动(&N)";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(8, 86);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(102, 21);
            checkBox4.TabIndex = 3;
            checkBox4.Text = "禁止最小化(&Z)";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(8, 63);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(111, 21);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "禁用关闭按钮(&I)";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(8, 40);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(151, 21);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "解析拖放的快捷方式(&V)";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(8, 17);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(102, 21);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "总在最前面(&T)";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(240, 240, 240);
            tabPage2.Controls.Add(label27);
            tabPage2.Controls.Add(label26);
            tabPage2.Controls.Add(trackBar1);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(label10);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(checkBox16);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(checkBox15);
            tabPage2.Controls.Add(checkBox14);
            tabPage2.Controls.Add(comboBox3);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(checkBox13);
            tabPage2.Controls.Add(checkBox12);
            tabPage2.Controls.Add(checkBox11);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(440, 241);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "外观";
            // 
            // label27
            // 
            label27.BorderStyle = BorderStyle.Fixed3D;
            label27.Location = new Point(272, 141);
            label27.Name = "label27";
            label27.Size = new Size(120, 2);
            label27.TabIndex = 16;
            label27.Text = "label27";
            // 
            // label26
            // 
            label26.BorderStyle = BorderStyle.Fixed3D;
            label26.Location = new Point(319, 22);
            label26.Name = "label26";
            label26.Size = new Size(100, 2);
            label26.TabIndex = 11;
            label26.Text = "label26";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(235, 198);
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 12;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(184, 45);
            trackBar1.TabIndex = 20;
            trackBar1.TickFrequency = 7;
            trackBar1.Value = 12;
            // 
            // label11
            // 
            label11.BorderStyle = BorderStyle.Fixed3D;
            label11.Location = new Point(70, 77);
            label11.Name = "label11";
            label11.Size = new Size(100, 2);
            label11.TabIndex = 4;
            label11.Text = "label11";
            // 
            // label10
            // 
            label10.BorderStyle = BorderStyle.Fixed3D;
            label10.Location = new Point(70, 22);
            label10.Name = "label10";
            label10.Size = new Size(100, 2);
            label10.TabIndex = 1;
            label10.Text = "label10";
            // 
            // label9
            // 
            label9.ImageAlign = ContentAlignment.MiddleRight;
            label9.Location = new Point(330, 178);
            label9.Name = "label9";
            label9.Size = new Size(89, 17);
            label9.TabIndex = 19;
            label9.Tag = "";
            label9.Text = "100%";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(235, 177);
            label8.Name = "label8";
            label8.Size = new Size(89, 17);
            label8.TabIndex = 18;
            label8.Text = "不透明级别(&N):";
            // 
            // checkBox16
            // 
            checkBox16.AutoSize = true;
            checkBox16.Location = new Point(252, 153);
            checkBox16.Name = "checkBox16";
            checkBox16.Size = new Size(179, 21);
            checkBox16.TabIndex = 17;
            checkBox16.Text = "显示在任务栏而不是托盘(&W)";
            checkBox16.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(235, 133);
            label7.Name = "label7";
            label7.Size = new Size(32, 17);
            label7.TabIndex = 15;
            label7.Text = "其他";
            // 
            // checkBox15
            // 
            checkBox15.AutoSize = true;
            checkBox15.Location = new Point(256, 96);
            checkBox15.Name = "checkBox15";
            checkBox15.Size = new Size(129, 21);
            checkBox15.TabIndex = 14;
            checkBox15.Text = "显示长格式日期(&N)";
            checkBox15.UseVisualStyleBackColor = true;
            // 
            // checkBox14
            // 
            checkBox14.AutoSize = true;
            checkBox14.Location = new Point(256, 69);
            checkBox14.Name = "checkBox14";
            checkBox14.Size = new Size(125, 21);
            checkBox14.TabIndex = 13;
            checkBox14.Text = "显示长格式时间(&L)";
            checkBox14.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "时间", "日期", "时间与日期", "无" });
            comboBox3.Location = new Point(256, 33);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(100, 25);
            comboBox3.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(235, 13);
            label6.Name = "label6";
            label6.Size = new Size(83, 17);
            label6.TabIndex = 10;
            label6.Text = "状态栏时钟(&S)";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "无", "启动后最小化", "不活动时最小化" });
            comboBox2.Location = new Point(20, 198);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(116, 25);
            comboBox2.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 178);
            label5.Name = "label5";
            label5.Size = new Size(120, 17);
            label5.TabIndex = 8;
            label5.Text = "自动最小化或上卷(&A)";
            // 
            // checkBox13
            // 
            checkBox13.AutoSize = true;
            checkBox13.Location = new Point(20, 144);
            checkBox13.Name = "checkBox13";
            checkBox13.Size = new Size(116, 21);
            checkBox13.TabIndex = 7;
            checkBox13.Text = "按钮表面显示(&U)";
            checkBox13.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            checkBox12.AutoSize = true;
            checkBox12.Location = new Point(20, 117);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new Size(115, 21);
            checkBox12.TabIndex = 6;
            checkBox12.Text = "在状态栏显示(&B)";
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(20, 90);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(114, 21);
            checkBox11.TabIndex = 5;
            checkBox11.Text = "作为工具提示(&P)";
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 70);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 3;
            label4.Text = "按钮描述";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "标准", "最小", "无" });
            comboBox1.Location = new Point(20, 33);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(100, 25);
            comboBox1.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 13);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 0;
            label3.Text = "标题栏(&T)";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = SystemColors.Control;
            tabPage3.Controls.Add(label24);
            tabPage3.Controls.Add(dataGridView1);
            tabPage3.Controls.Add(comboBox4);
            tabPage3.Controls.Add(radioButton1);
            tabPage3.Controls.Add(label22);
            tabPage3.Controls.Add(Btn_GroupDown);
            tabPage3.Controls.Add(Btn_GroupUp);
            tabPage3.Controls.Add(label21);
            tabPage3.Controls.Add(numericUpDown5);
            tabPage3.Controls.Add(label20);
            tabPage3.Controls.Add(numericUpDown4);
            tabPage3.Controls.Add(label19);
            tabPage3.Controls.Add(label17);
            tabPage3.Controls.Add(label18);
            tabPage3.Controls.Add(numericUpDown3);
            tabPage3.Controls.Add(label16);
            tabPage3.Controls.Add(numericUpDown2);
            tabPage3.Controls.Add(label15);
            tabPage3.Controls.Add(numericUpDown1);
            tabPage3.Controls.Add(label14);
            tabPage3.Controls.Add(checkBox17);
            tabPage3.Controls.Add(label12);
            tabPage3.Controls.Add(label13);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(440, 241);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "按钮与组";
            // 
            // label24
            // 
            label24.BorderStyle = BorderStyle.Fixed3D;
            label24.Location = new Point(307, 18);
            label24.Name = "label24";
            label24.Size = new Size(100, 2);
            label24.TabIndex = 16;
            label24.Text = "label24";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 20;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Number, Column1 });
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(252, 29);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle3.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowTemplate.Height = 18;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(155, 103);
            dataGridView1.TabIndex = 17;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // Number
            // 
            Number.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            Number.DefaultCellStyle = dataGridViewCellStyle2;
            Number.HeaderText = "Num";
            Number.Name = "Number";
            Number.ReadOnly = true;
            Number.Resizable = DataGridViewTriState.False;
            Number.SortMode = DataGridViewColumnSortMode.NotSortable;
            Number.Width = 5;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            // 
            // comboBox4
            // 
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "标准", "按钮", "平面按钮" });
            comboBox4.Location = new Point(359, 162);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(72, 25);
            comboBox4.TabIndex = 22;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(261, 163);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(92, 21);
            radioButton1.TabIndex = 21;
            radioButton1.TabStop = true;
            radioButton1.Text = "索引标签(&N)";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(241, 143);
            label22.Name = "label22";
            label22.Size = new Size(44, 17);
            label22.TabIndex = 20;
            label22.Text = "组导航";
            // 
            // Btn_GroupDown
            // 
            Btn_GroupDown.Location = new Point(413, 68);
            Btn_GroupDown.Name = "Btn_GroupDown";
            Btn_GroupDown.Size = new Size(23, 23);
            Btn_GroupDown.TabIndex = 19;
            Btn_GroupDown.Text = "v";
            Btn_GroupDown.UseVisualStyleBackColor = true;
            Btn_GroupDown.Click += Btn_GroupDown_Click;
            // 
            // Btn_GroupUp
            // 
            Btn_GroupUp.Location = new Point(413, 39);
            Btn_GroupUp.Name = "Btn_GroupUp";
            Btn_GroupUp.Size = new Size(23, 23);
            Btn_GroupUp.TabIndex = 18;
            Btn_GroupUp.Text = "^";
            Btn_GroupUp.UseVisualStyleBackColor = true;
            Btn_GroupUp.Click += Btn_GroupUp_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(241, 9);
            label21.Name = "label21";
            label21.Size = new Size(61, 17);
            label21.TabIndex = 15;
            label21.Text = "组描述(&D)";
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(90, 192);
            numericUpDown5.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(55, 23);
            numericUpDown5.TabIndex = 14;
            // 
            // label20
            // 
            label20.ImageAlign = ContentAlignment.MiddleRight;
            label20.Location = new Point(13, 194);
            label20.Name = "label20";
            label20.Size = new Size(71, 17);
            label20.TabIndex = 13;
            label20.Text = "间距(&A):";
            label20.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Increment = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDown4.Location = new Point(90, 163);
            numericUpDown4.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(55, 23);
            numericUpDown4.TabIndex = 12;
            numericUpDown4.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(13, 165);
            label19.Name = "label19";
            label19.Size = new Size(71, 17);
            label19.TabIndex = 11;
            label19.Text = "图标大小(&I):";
            // 
            // label17
            // 
            label17.BorderStyle = BorderStyle.Fixed3D;
            label17.Location = new Point(78, 152);
            label17.Name = "label17";
            label17.Size = new Size(100, 2);
            label17.TabIndex = 10;
            label17.Text = "label17";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(8, 143);
            label18.Name = "label18";
            label18.Size = new Size(68, 17);
            label18.TabIndex = 9;
            label18.Text = "大小与间距";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(90, 109);
            numericUpDown3.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(55, 23);
            numericUpDown3.TabIndex = 8;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(45, 111);
            label16.Name = "label16";
            label16.Size = new Size(40, 17);
            label16.TabIndex = 7;
            label16.Text = "组(&G):";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(90, 80);
            numericUpDown2.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(55, 23);
            numericUpDown2.TabIndex = 6;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(45, 82);
            label15.Name = "label15";
            label15.Size = new Size(39, 17);
            label15.TabIndex = 5;
            label15.Text = "列(&C):";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(90, 51);
            numericUpDown1.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(55, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(45, 53);
            label14.Name = "label14";
            label14.Size = new Size(39, 17);
            label14.TabIndex = 3;
            label14.Text = "行(&R):";
            // 
            // checkBox17
            // 
            checkBox17.AutoSize = true;
            checkBox17.Location = new Point(20, 29);
            checkBox17.Name = "checkBox17";
            checkBox17.Size = new Size(89, 21);
            checkBox17.TabIndex = 2;
            checkBox17.Text = "平面按钮(&F)";
            checkBox17.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.BorderStyle = BorderStyle.Fixed3D;
            label12.Location = new Point(45, 18);
            label12.Name = "label12";
            label12.Size = new Size(100, 2);
            label12.TabIndex = 1;
            label12.Text = "label12";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(8, 9);
            label13.Name = "label13";
            label13.Size = new Size(32, 17);
            label13.TabIndex = 0;
            label13.Text = "布局";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(Txt_FontChangeTip);
            tabPage4.Controls.Add(Btn_FontDefault);
            tabPage4.Controls.Add(Btn_FontChange);
            tabPage4.Controls.Add(TreeView_Font);
            tabPage4.Controls.Add(label31);
            tabPage4.Controls.Add(label32);
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(440, 241);
            tabPage4.TabIndex = 6;
            tabPage4.Text = "字体";
            // 
            // Txt_FontChangeTip
            // 
            Txt_FontChangeTip.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Txt_FontChangeTip.AutoSize = true;
            Txt_FontChangeTip.Location = new Point(147, 179);
            Txt_FontChangeTip.Name = "Txt_FontChangeTip";
            Txt_FontChangeTip.Size = new Size(128, 17);
            Txt_FontChangeTip.TabIndex = 3;
            Txt_FontChangeTip.Text = "请选择要修改的字体。";
            // 
            // Btn_FontDefault
            // 
            Btn_FontDefault.Location = new Point(357, 175);
            Btn_FontDefault.Name = "Btn_FontDefault";
            Btn_FontDefault.Size = new Size(76, 26);
            Btn_FontDefault.TabIndex = 5;
            Btn_FontDefault.Text = "默认(&D)";
            Btn_FontDefault.UseVisualStyleBackColor = true;
            Btn_FontDefault.Click += Btn_FontDefault_Click;
            // 
            // Btn_FontChange
            // 
            Btn_FontChange.Location = new Point(357, 143);
            Btn_FontChange.Name = "Btn_FontChange";
            Btn_FontChange.Size = new Size(76, 26);
            Btn_FontChange.TabIndex = 4;
            Btn_FontChange.Text = "更改(&C)...";
            Btn_FontChange.UseVisualStyleBackColor = true;
            Btn_FontChange.Click += Btn_FontChange_Click;
            // 
            // TreeView_Font
            // 
            TreeView_Font.FullRowSelect = true;
            TreeView_Font.HideSelection = false;
            TreeView_Font.Location = new Point(9, 0);
            TreeView_Font.Name = "TreeView_Font";
            treeNode1.Name = "节点1";
            treeNode1.Tag = "1";
            treeNode1.Text = "状态栏";
            treeNode2.Name = "节点2";
            treeNode2.Tag = "2";
            treeNode2.Text = "按钮标题";
            treeNode3.Name = "节点3";
            treeNode3.Tag = "3";
            treeNode3.Text = "组标签";
            treeNode4.Name = "节点0";
            treeNode4.Text = "QuickLinker 字体设置";
            TreeView_Font.Nodes.AddRange(new TreeNode[] { treeNode4 });
            TreeView_Font.Size = new Size(424, 118);
            TreeView_Font.TabIndex = 0;
            TreeView_Font.AfterSelect += TreeView_Font_AfterSelect;
            // 
            // label31
            // 
            label31.BorderStyle = BorderStyle.Fixed3D;
            label31.Location = new Point(72, 132);
            label31.Name = "label31";
            label31.Size = new Size(362, 2);
            label31.TabIndex = 2;
            label31.Text = "label31";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(7, 124);
            label32.Name = "label32";
            label32.Size = new Size(56, 17);
            label32.TabIndex = 1;
            label32.Text = "字体样式";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(Btn_AudioBrowse);
            tabPage5.Controls.Add(Btn_TestAudio);
            tabPage5.Controls.Add(TextBox_AudioFilePath);
            tabPage5.Controls.Add(RadioBtn_Custom);
            tabPage5.Controls.Add(RadioBtn_Default);
            tabPage5.Controls.Add(RadioBtn_Null);
            tabPage5.Controls.Add(TreeView_Audio);
            tabPage5.Controls.Add(Txt_AudioChangeTip);
            tabPage5.Controls.Add(label29);
            tabPage5.Controls.Add(label30);
            tabPage5.Location = new Point(4, 26);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(440, 241);
            tabPage5.TabIndex = 5;
            tabPage5.Text = "声音";
            // 
            // Btn_AudioBrowse
            // 
            Btn_AudioBrowse.allowStyle = false;
            Btn_AudioBrowse.BackColor = Color.FromArgb(240, 240, 240);
            Btn_AudioBrowse.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_AudioBrowse.Enabled = false;
            Btn_AudioBrowse.Image = null;
            Btn_AudioBrowse.Location = new Point(334, 204);
            Btn_AudioBrowse.Margin = new Padding(0);
            Btn_AudioBrowse.Name = "Btn_AudioBrowse";
            Btn_AudioBrowse.Size = new Size(24, 24);
            Btn_AudioBrowse.TabIndex = 8;
            // 
            // Btn_TestAudio
            // 
            Btn_TestAudio.Location = new Point(357, 143);
            Btn_TestAudio.Name = "Btn_TestAudio";
            Btn_TestAudio.Size = new Size(76, 26);
            Btn_TestAudio.TabIndex = 9;
            Btn_TestAudio.Text = "测试(&T)";
            Btn_TestAudio.UseVisualStyleBackColor = true;
            Btn_TestAudio.Click += Btn_TestAudio_Click;
            // 
            // TextBox_AudioFilePath
            // 
            TextBox_AudioFilePath.AllowDrop = true;
            TextBox_AudioFilePath.Enabled = false;
            TextBox_AudioFilePath.Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            TextBox_AudioFilePath.Location = new Point(103, 206);
            TextBox_AudioFilePath.Name = "TextBox_AudioFilePath";
            TextBox_AudioFilePath.Size = new Size(226, 21);
            TextBox_AudioFilePath.TabIndex = 7;
            TextBox_AudioFilePath.DragDrop += Txt_Path_DragDrop;
            TextBox_AudioFilePath.DragEnter += Txt_Path_DragEnter;
            // 
            // RadioBtn_Custom
            // 
            RadioBtn_Custom.AutoSize = true;
            RadioBtn_Custom.Location = new Point(19, 206);
            RadioBtn_Custom.Name = "RadioBtn_Custom";
            RadioBtn_Custom.Size = new Size(90, 21);
            RadioBtn_Custom.TabIndex = 6;
            RadioBtn_Custom.Tag = "3";
            RadioBtn_Custom.Text = "自定义(&C)：";
            RadioBtn_Custom.UseVisualStyleBackColor = true;
            RadioBtn_Custom.CheckedChanged += RadioBtn_Custom_CheckedChanged;
            // 
            // RadioBtn_Default
            // 
            RadioBtn_Default.AutoSize = true;
            RadioBtn_Default.Checked = true;
            RadioBtn_Default.Location = new Point(19, 179);
            RadioBtn_Default.Name = "RadioBtn_Default";
            RadioBtn_Default.Size = new Size(67, 21);
            RadioBtn_Default.TabIndex = 4;
            RadioBtn_Default.TabStop = true;
            RadioBtn_Default.Tag = "2";
            RadioBtn_Default.Text = "默认(&D)";
            RadioBtn_Default.UseVisualStyleBackColor = true;
            // 
            // RadioBtn_Null
            // 
            RadioBtn_Null.AutoSize = true;
            RadioBtn_Null.Location = new Point(19, 152);
            RadioBtn_Null.Name = "RadioBtn_Null";
            RadioBtn_Null.Size = new Size(56, 21);
            RadioBtn_Null.TabIndex = 3;
            RadioBtn_Null.Tag = "1";
            RadioBtn_Null.Text = "无(&N)";
            RadioBtn_Null.UseVisualStyleBackColor = true;
            // 
            // TreeView_Audio
            // 
            TreeView_Audio.FullRowSelect = true;
            TreeView_Audio.HideSelection = false;
            TreeView_Audio.Location = new Point(9, 0);
            TreeView_Audio.Name = "TreeView_Audio";
            treeNode5.Name = "节点1";
            treeNode5.Tag = "1";
            treeNode5.Text = "启动按钮";
            treeNode6.Name = "节点2";
            treeNode6.Tag = "2";
            treeNode6.Text = "切换组";
            treeNode7.Name = "节点3";
            treeNode7.Tag = "3";
            treeNode7.Text = "拖放对象";
            treeNode8.Name = "节点4";
            treeNode8.Tag = "4";
            treeNode8.Text = "按钮操作 （交换、排列、复制）";
            treeNode9.Name = "节点0";
            treeNode9.Text = "QuickLinker 声音事件";
            TreeView_Audio.Nodes.AddRange(new TreeNode[] { treeNode9 });
            TreeView_Audio.Size = new Size(424, 118);
            TreeView_Audio.TabIndex = 0;
            TreeView_Audio.AfterSelect += TreeView_Audio_AfterSelect;
            // 
            // Txt_AudioChangeTip
            // 
            Txt_AudioChangeTip.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Txt_AudioChangeTip.AutoSize = true;
            Txt_AudioChangeTip.Location = new Point(147, 179);
            Txt_AudioChangeTip.Name = "Txt_AudioChangeTip";
            Txt_AudioChangeTip.Size = new Size(152, 17);
            Txt_AudioChangeTip.TabIndex = 5;
            Txt_AudioChangeTip.Text = "请选择要修改的声音事件。";
            // 
            // label29
            // 
            label29.BorderStyle = BorderStyle.Fixed3D;
            label29.Location = new Point(72, 132);
            label29.Name = "label29";
            label29.Size = new Size(362, 2);
            label29.TabIndex = 2;
            label29.Text = "label29";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(7, 124);
            label30.Name = "label30";
            label30.Size = new Size(56, 17);
            label30.TabIndex = 1;
            label30.Text = "声音类型";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = Color.FromArgb(240, 240, 240);
            tabPage6.Controls.Add(linkLabel1);
            tabPage6.Controls.Add(label25);
            tabPage6.Controls.Add(label23);
            tabPage6.Location = new Point(4, 26);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(440, 241);
            tabPage6.TabIndex = 4;
            tabPage6.Text = "联系我";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(20, 32);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(134, 17);
            linkLabel1.TabIndex = 2;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "1436485479@qq.com";
            // 
            // label25
            // 
            label25.BorderStyle = BorderStyle.Fixed3D;
            label25.Location = new Point(43, 21);
            label25.Name = "label25";
            label25.Size = new Size(106, 2);
            label25.TabIndex = 1;
            label25.Text = "label25";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(8, 13);
            label23.Name = "label23";
            label23.Size = new Size(28, 17);
            label23.TabIndex = 0;
            label23.Text = "QQ";
            // 
            // tabPage7
            // 
            tabPage7.Controls.Add(checkBox23);
            tabPage7.Controls.Add(label39);
            tabPage7.Controls.Add(label40);
            tabPage7.Controls.Add(checkBox22);
            tabPage7.Controls.Add(checkBox21);
            tabPage7.Controls.Add(checkBox20);
            tabPage7.Controls.Add(label37);
            tabPage7.Controls.Add(label38);
            tabPage7.Controls.Add(Txt_RPassword);
            tabPage7.Controls.Add(label36);
            tabPage7.Controls.Add(Txt_Password);
            tabPage7.Controls.Add(label35);
            tabPage7.Controls.Add(label33);
            tabPage7.Controls.Add(label34);
            tabPage7.Location = new Point(4, 26);
            tabPage7.Name = "tabPage7";
            tabPage7.Size = new Size(440, 241);
            tabPage7.TabIndex = 7;
            tabPage7.Text = "保护";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // checkBox23
            // 
            checkBox23.AutoSize = true;
            checkBox23.Location = new Point(31, 198);
            checkBox23.Name = "checkBox23";
            checkBox23.Size = new Size(163, 21);
            checkBox23.TabIndex = 13;
            checkBox23.Text = "防止关闭 QuickLinker(&R)";
            checkBox23.UseVisualStyleBackColor = true;
            // 
            // label39
            // 
            label39.BorderStyle = BorderStyle.Fixed3D;
            label39.Location = new Point(43, 185);
            label39.Name = "label39";
            label39.Size = new Size(380, 2);
            label39.TabIndex = 12;
            label39.Text = "label39";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new Point(8, 177);
            label40.Name = "label40";
            label40.Size = new Size(32, 17);
            label40.TabIndex = 11;
            label40.Text = "操作";
            // 
            // checkBox22
            // 
            checkBox22.AutoSize = true;
            checkBox22.Location = new Point(31, 153);
            checkBox22.Name = "checkBox22";
            checkBox22.Size = new Size(116, 21);
            checkBox22.TabIndex = 10;
            checkBox22.Text = "保留拖放支持(&D)";
            checkBox22.UseVisualStyleBackColor = true;
            // 
            // checkBox21
            // 
            checkBox21.AutoSize = true;
            checkBox21.Location = new Point(31, 130);
            checkBox21.Name = "checkBox21";
            checkBox21.Size = new Size(139, 21);
            checkBox21.TabIndex = 9;
            checkBox21.Text = "保留配置菜单项目(&C)";
            checkBox21.UseVisualStyleBackColor = true;
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new Point(31, 109);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new Size(114, 21);
            checkBox20.TabIndex = 8;
            checkBox20.Text = "防止更改配置(&P)";
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            label37.BorderStyle = BorderStyle.Fixed3D;
            label37.Location = new Point(43, 98);
            label37.Name = "label37";
            label37.Size = new Size(380, 2);
            label37.TabIndex = 7;
            label37.Text = "label37";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Location = new Point(8, 90);
            label38.Name = "label38";
            label38.Size = new Size(32, 17);
            label38.TabIndex = 6;
            label38.Text = "配置";
            // 
            // Txt_RPassword
            // 
            Txt_RPassword.AllowDrop = true;
            Txt_RPassword.Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Txt_RPassword.Location = new Point(120, 62);
            Txt_RPassword.MaxLength = 16;
            Txt_RPassword.Name = "Txt_RPassword";
            Txt_RPassword.Size = new Size(170, 21);
            Txt_RPassword.TabIndex = 5;
            Txt_RPassword.UseSystemPasswordChar = true;
            Txt_RPassword.WordWrap = false;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new Point(31, 64);
            label36.Name = "label36";
            label36.Size = new Size(84, 17);
            label36.TabIndex = 4;
            label36.Text = "重输确认(&V)：";
            // 
            // Txt_Password
            // 
            Txt_Password.AllowDrop = true;
            Txt_Password.Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Txt_Password.Location = new Point(120, 35);
            Txt_Password.MaxLength = 16;
            Txt_Password.Name = "Txt_Password";
            Txt_Password.Size = new Size(170, 21);
            Txt_Password.TabIndex = 3;
            Txt_Password.UseSystemPasswordChar = true;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(31, 37);
            label35.Name = "label35";
            label35.Size = new Size(83, 17);
            label35.TabIndex = 2;
            label35.Text = "输入密码(&T)：";
            // 
            // label33
            // 
            label33.BorderStyle = BorderStyle.Fixed3D;
            label33.Location = new Point(43, 18);
            label33.Name = "label33";
            label33.Size = new Size(380, 2);
            label33.TabIndex = 1;
            label33.Text = "label33";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(8, 10);
            label34.Name = "label34";
            label34.Size = new Size(32, 17);
            label34.TabIndex = 0;
            label34.Text = "密码";
            // 
            // PreferencesFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(448, 271);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PreferencesFrom";
            StartPosition = FormStartPosition.CenterParent;
            Text = "首选项";
            FormClosing += SettingForm_FormClosing;
            Load += SettingForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            tabPage7.ResumeLayout(false);
            tabPage7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage6;
        private CheckBox checkBox7;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox10;
        private CheckBox checkBox9;
        private Label label2;
        private CheckBox checkBox8;
        private TextBox textBox1;
        private Label label1;
        private Label label6;
        private ComboBox comboBox2;
        private Label label5;
        private CheckBox checkBox13;
        private CheckBox checkBox12;
        private CheckBox checkBox11;
        private Label label4;
        private ComboBox comboBox1;
        private Label label3;
        private Label label9;
        private Label label8;
        private CheckBox checkBox16;
        private Label label7;
        private CheckBox checkBox15;
        private CheckBox checkBox14;
        private ComboBox comboBox3;
        private Label label11;
        private Label label10;
        private NumericUpDown numericUpDown2;
        private Label label15;
        private NumericUpDown numericUpDown1;
        private Label label14;
        private CheckBox checkBox17;
        private Label label12;
        private Label label13;
        private Label label21;
        private NumericUpDown numericUpDown5;
        private Label label20;
        private NumericUpDown numericUpDown4;
        private Label label19;
        private Label label17;
        private Label label18;
        private NumericUpDown numericUpDown3;
        private Label label16;
        private ComboBox comboBox4;
        private RadioButton radioButton1;
        private Label label22;
        private Button Btn_GroupDown;
        private Button Btn_GroupUp;
        private ComboBox comboBox5;
        private RadioButton RadioBtn_Null;
        private TrackBar trackBar1;
        private CheckBox checkBox18;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn Column1;
        private Label label24;
        private LinkLabel linkLabel1;
        private Label label25;
        private Label label23;
        private Label label26;
        private Label label28;
        private Label label27;
        private TabPage tabPage5;
        private Label Txt_AudioChangeTip;
        private Label label29;
        private Label label30;
        private TreeView TreeView_Audio;
        private RadioButton RadioBtn_Custom;
        private RadioButton RadioBtn_Default;
        private Button Btn_TestAudio;
        private TextBox TextBox_AudioFilePath;
        private TabPage tabPage4;
        private TButton Btn_AudioBrowse;
        private Button Btn_FontChange;
        private TreeView TreeView_Font;
        private Label label31;
        private Label label32;
        private Button Btn_FontDefault;
        private Label Txt_FontChangeTip;
        private CheckBox checkBox19;
        private TabPage tabPage7;
        private Label label35;
        private Label label33;
        private Label label34;
        private CheckBox checkBox21;
        private CheckBox checkBox20;
        private Label label37;
        private Label label38;
        private TextBox Txt_RPassword;
        private Label label36;
        private TextBox Txt_Password;
        private CheckBox checkBox23;
        private Label label39;
        private Label label40;
        private CheckBox checkBox22;
    }
}