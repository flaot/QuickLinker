using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabPage1 = new TabPage();
            tableLayoutPanel = new TableLayoutPanel();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            BottomToolStripPanel = new ToolStripPanel();
            StatusStrip = new StatusStrip();
            ToolStatus_Txt = new ToolStripStatusLabel();
            ToolStatus_DateTime = new ToolStripStatusLabel();
            TopToolStripPanel = new ToolStripPanel();
            RightToolStripPanel = new ToolStripPanel();
            LeftToolStripPanel = new ToolStripPanel();
            ContentPanel = new ToolStripContentPanel();
            NotifyIcon = new NotifyIcon(components);
            tabPage1.SuspendLayout();
            tabControl1.SuspendLayout();
            StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(240, 240, 240);
            tabPage1.Controls.Add(tableLayoutPanel);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(0);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(362, 220);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel.ColumnCount = 6;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 43F));
            tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel.Location = new Point(21, 11);
            tableLayoutPanel.Margin = new Padding(0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.Size = new Size(219, 195);
            tableLayoutPanel.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(0, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(370, 250);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            tabControl1.Click += TabControl1_Click;
            tabControl1.Enter += TabControl1_Enter;
            tabControl1.MouseUp += TabControl1_MouseUp;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(362, 220);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // BottomToolStripPanel
            // 
            BottomToolStripPanel.Location = new Point(0, 0);
            BottomToolStripPanel.Name = "BottomToolStripPanel";
            BottomToolStripPanel.Orientation = Orientation.Horizontal;
            BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            BottomToolStripPanel.Size = new Size(0, 0);
            // 
            // StatusStrip
            // 
            StatusStrip.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            StatusStrip.ImageScalingSize = new Size(28, 28);
            StatusStrip.Items.AddRange(new ToolStripItem[] { ToolStatus_Txt, ToolStatus_DateTime });
            StatusStrip.Location = new Point(0, 249);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Padding = new Padding(1, 0, 15, 0);
            StatusStrip.Size = new Size(370, 26);
            StatusStrip.SizingGrip = false;
            StatusStrip.TabIndex = 1;
            StatusStrip.Text = "statusStrip1";
            StatusStrip.MouseUp += StatusStrip_MouseUp;
            // 
            // ToolStatus_Txt
            // 
            ToolStatus_Txt.BorderSides = ToolStripStatusLabelBorderSides.Top;
            ToolStatus_Txt.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStatus_Txt.ImageAlign = ContentAlignment.MiddleLeft;
            ToolStatus_Txt.Name = "ToolStatus_Txt";
            ToolStatus_Txt.Size = new Size(283, 21);
            ToolStatus_Txt.Spring = true;
            ToolStatus_Txt.Text = "toolStripStatusLabel1";
            ToolStatus_Txt.TextAlign = ContentAlignment.MiddleLeft;
            ToolStatus_Txt.MouseUp += StatusStrip_MouseUp;
            // 
            // ToolStatus_DateTime
            // 
            ToolStatus_DateTime.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top;
            ToolStatus_DateTime.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStatus_DateTime.Name = "ToolStatus_DateTime";
            ToolStatus_DateTime.Size = new Size(71, 21);
            ToolStatus_DateTime.Text = "2024/8/22";
            ToolStatus_DateTime.MouseUp += StatusStrip_MouseUp;
            // 
            // TopToolStripPanel
            // 
            TopToolStripPanel.Location = new Point(0, 0);
            TopToolStripPanel.Name = "TopToolStripPanel";
            TopToolStripPanel.Orientation = Orientation.Horizontal;
            TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            TopToolStripPanel.Size = new Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            RightToolStripPanel.Location = new Point(0, 0);
            RightToolStripPanel.Name = "RightToolStripPanel";
            RightToolStripPanel.Orientation = Orientation.Horizontal;
            RightToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            RightToolStripPanel.Size = new Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            LeftToolStripPanel.Location = new Point(0, 0);
            LeftToolStripPanel.Name = "LeftToolStripPanel";
            LeftToolStripPanel.Orientation = Orientation.Horizontal;
            LeftToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            LeftToolStripPanel.Size = new Size(0, 0);
            // 
            // ContentPanel
            // 
            ContentPanel.Size = new Size(150, 128);
            // 
            // NotifyIcon
            // 
            NotifyIcon.Icon = (Icon)resources.GetObject("NotifyIcon.Icon");
            NotifyIcon.Text = "QuickLinker";
            NotifyIcon.Visible = true;
            NotifyIcon.MouseClick += NotifyIcon_MouseClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(370, 275);
            Controls.Add(StatusStrip);
            Controls.Add(tabControl1);
            DoubleBuffered = true;
            Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "QuickLinker";
            Deactivate += MainForm_Deactivate;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tabPage1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel;
        private ToolStripMenuItem quickLinkerTToolStripMenuItem;
        private ToolStripMenuItem 帮助HToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripPanel BottomToolStripPanel;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel ToolStatus_Txt;
        private ToolStripStatusLabel ToolStatus_DateTime;
        private ToolStripPanel TopToolStripPanel;
        private ToolStripPanel RightToolStripPanel;
        private ToolStripPanel LeftToolStripPanel;
        private ToolStripContentPanel ContentPanel;
        private TabPage tabPage2;
        public TabControl tabControl1;
        public NotifyIcon NotifyIcon;
    }
}
