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
            TabMenuStrip = new ContextMenuStrip(components);
            TabMenuItem_Left = new ToolStripMenuItem();
            TabMenuItem_Right = new ToolStripMenuItem();
            TabMenuItem_Rename = new ToolStripMenuItem();
            TabMenuItem_Delete = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            外观ToolStripMenuItem = new ToolStripMenuItem();
            TabMenuItem_Stand = new ToolStripMenuItem();
            TabMenuItem_Button = new ToolStripMenuItem();
            TabMenuItem_Flot = new ToolStripMenuItem();
            PageMenuStrip = new ContextMenuStrip(components);
            MenuStrip_FullPath = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            MenuStrip_CreateQuick = new ToolStripMenuItem();
            MenuStrip_SystemContextMenu = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            MenuStrip_Copy = new ToolStripMenuItem();
            MenuStrip_Switch = new ToolStripMenuItem();
            MenuStrip_Align = new ToolStripMenuItem();
            MenuStrip_Clear = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            MenuStrip_Attr = new ToolStripMenuItem();
            BottomToolStripPanel = new ToolStripPanel();
            StatusStrip = new StatusStrip();
            ToolStatusContextMenu = new ContextMenuStrip(components);
            ToolStatusMenu_Time = new ToolStripMenuItem();
            ToolStatusMenu_Date = new ToolStripMenuItem();
            ToolStatusMenu_DateTime = new ToolStripMenuItem();
            ToolStatusMenu_None = new ToolStripMenuItem();
            ToolStatus_Txt = new ToolStripStatusLabel();
            ToolStatus_DateTime = new ToolStripStatusLabel();
            TopToolStripPanel = new ToolStripPanel();
            RightToolStripPanel = new ToolStripPanel();
            LeftToolStripPanel = new ToolStripPanel();
            ContentPanel = new ToolStripContentPanel();
            NotifyIcon = new NotifyIcon(components);
            AppMenu = new ContextMenuStrip(components);
            AppMenu_Show = new ToolStripMenuItem();
            AppMenu_Setting = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripSeparator();
            AppMenu_Quit = new ToolStripMenuItem();
            tabPage1.SuspendLayout();
            tabControl1.SuspendLayout();
            TabMenuStrip.SuspendLayout();
            PageMenuStrip.SuspendLayout();
            StatusStrip.SuspendLayout();
            ToolStatusContextMenu.SuspendLayout();
            AppMenu.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(240, 240, 240);
            tabPage1.Controls.Add(tableLayoutPanel);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(0);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(341, 236);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel.ColumnCount = 6;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel.Location = new Point(63, 20);
            tableLayoutPanel.Margin = new Padding(0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.Size = new Size(206, 207);
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
            tabControl1.Size = new Size(349, 266);
            tabControl1.TabIndex = 0;
            tabControl1.MouseDown += tabControl1_MouseDown_1;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(341, 236);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // TabMenuStrip
            // 
            TabMenuStrip.Items.AddRange(new ToolStripItem[] { TabMenuItem_Left, TabMenuItem_Right, TabMenuItem_Rename, TabMenuItem_Delete, toolStripMenuItem4, 外观ToolStripMenuItem });
            TabMenuStrip.Name = "TabMenuStrip";
            TabMenuStrip.Size = new Size(141, 120);
            // 
            // TabMenuItem_Left
            // 
            TabMenuItem_Left.Name = "TabMenuItem_Left";
            TabMenuItem_Left.Size = new Size(140, 22);
            TabMenuItem_Left.Text = "左移标签(&L)";
            TabMenuItem_Left.Click += TabMenuItem_Left_Click;
            // 
            // TabMenuItem_Right
            // 
            TabMenuItem_Right.Name = "TabMenuItem_Right";
            TabMenuItem_Right.Size = new Size(140, 22);
            TabMenuItem_Right.Text = "右移标签(&R)";
            TabMenuItem_Right.Click += TabMenuItem_Right_Click;
            // 
            // TabMenuItem_Rename
            // 
            TabMenuItem_Rename.Name = "TabMenuItem_Rename";
            TabMenuItem_Rename.Size = new Size(140, 22);
            TabMenuItem_Rename.Text = "重命名(&E)...";
            TabMenuItem_Rename.Click += TabMenuItem_Rename_Click;
            // 
            // TabMenuItem_Delete
            // 
            TabMenuItem_Delete.Name = "TabMenuItem_Delete";
            TabMenuItem_Delete.Size = new Size(140, 22);
            TabMenuItem_Delete.Text = "删除(&D)";
            TabMenuItem_Delete.Click += TabMenuItem_Delete_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(137, 6);
            // 
            // 外观ToolStripMenuItem
            // 
            外观ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { TabMenuItem_Stand, TabMenuItem_Button, TabMenuItem_Flot });
            外观ToolStripMenuItem.Name = "外观ToolStripMenuItem";
            外观ToolStripMenuItem.Size = new Size(140, 22);
            外观ToolStripMenuItem.Text = "外观";
            // 
            // TabMenuItem_Stand
            // 
            TabMenuItem_Stand.Name = "TabMenuItem_Stand";
            TabMenuItem_Stand.Size = new Size(138, 22);
            TabMenuItem_Stand.Text = "标准(&N)";
            TabMenuItem_Stand.Click += TabMenuItem_Stand_Click;
            // 
            // TabMenuItem_Button
            // 
            TabMenuItem_Button.Name = "TabMenuItem_Button";
            TabMenuItem_Button.Size = new Size(138, 22);
            TabMenuItem_Button.Text = "按钮(&B)";
            TabMenuItem_Button.Click += TabMenuItem_Button_Click;
            // 
            // TabMenuItem_Flot
            // 
            TabMenuItem_Flot.Name = "TabMenuItem_Flot";
            TabMenuItem_Flot.Size = new Size(138, 22);
            TabMenuItem_Flot.Text = "平面按钮(&F)";
            TabMenuItem_Flot.Click += TabMenuItem_Flot_Click;
            // 
            // PageMenuStrip
            // 
            PageMenuStrip.Items.AddRange(new ToolStripItem[] { MenuStrip_FullPath, toolStripMenuItem3, MenuStrip_CreateQuick, MenuStrip_SystemContextMenu, toolStripMenuItem5, MenuStrip_Copy, MenuStrip_Switch, MenuStrip_Align, MenuStrip_Clear, toolStripMenuItem1, MenuStrip_Attr });
            PageMenuStrip.Name = "MenuStrip";
            PageMenuStrip.Size = new Size(181, 220);
            // 
            // MenuStrip_FullPath
            // 
            MenuStrip_FullPath.Name = "MenuStrip_FullPath";
            MenuStrip_FullPath.Size = new Size(180, 22);
            MenuStrip_FullPath.Text = "(未配置)";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(177, 6);
            // 
            // MenuStrip_CreateQuick
            // 
            MenuStrip_CreateQuick.Name = "MenuStrip_CreateQuick";
            MenuStrip_CreateQuick.Size = new Size(180, 22);
            MenuStrip_CreateQuick.Text = "创建快捷方式(&R)";
            MenuStrip_CreateQuick.Click += MenuStrip_CreateQuick_Click;
            // 
            // MenuStrip_SystemContextMenu
            // 
            MenuStrip_SystemContextMenu.Name = "MenuStrip_SystemContextMenu";
            MenuStrip_SystemContextMenu.Size = new Size(180, 22);
            MenuStrip_SystemContextMenu.Text = "资源管理器(&X)";
            MenuStrip_SystemContextMenu.Click += MenuStrip_SystemContextMenu_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(177, 6);
            // 
            // MenuStrip_Copy
            // 
            MenuStrip_Copy.Name = "MenuStrip_Copy";
            MenuStrip_Copy.Size = new Size(180, 22);
            MenuStrip_Copy.Text = "复制(&D)";
            MenuStrip_Copy.Click += MenuStrip_Copy_Click;
            // 
            // MenuStrip_Switch
            // 
            MenuStrip_Switch.Name = "MenuStrip_Switch";
            MenuStrip_Switch.Size = new Size(180, 22);
            MenuStrip_Switch.Text = "交换(&S)";
            MenuStrip_Switch.Click += MenuStrip_Switch_Click;
            // 
            // MenuStrip_Align
            // 
            MenuStrip_Align.Name = "MenuStrip_Align";
            MenuStrip_Align.Size = new Size(180, 22);
            MenuStrip_Align.Text = "排列(&A)";
            MenuStrip_Align.Click += MenuStrip_Align_Click;
            // 
            // MenuStrip_Clear
            // 
            MenuStrip_Clear.Name = "MenuStrip_Clear";
            MenuStrip_Clear.Size = new Size(180, 22);
            MenuStrip_Clear.Text = "清除(&C)";
            MenuStrip_Clear.Click += MenuStrip_Clear_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // MenuStrip_Attr
            // 
            MenuStrip_Attr.Name = "MenuStrip_Attr";
            MenuStrip_Attr.Size = new Size(180, 22);
            MenuStrip_Attr.Text = "属性(&P)";
            MenuStrip_Attr.Click += MenuStrip_Attr_Click;
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
            StatusStrip.ContextMenuStrip = ToolStatusContextMenu;
            StatusStrip.Items.AddRange(new ToolStripItem[] { ToolStatus_Txt, ToolStatus_DateTime });
            StatusStrip.Location = new Point(0, 266);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(349, 26);
            StatusStrip.SizingGrip = false;
            StatusStrip.TabIndex = 1;
            StatusStrip.Text = "statusStrip1";
            // 
            // ToolStatusContextMenu
            // 
            ToolStatusContextMenu.Items.AddRange(new ToolStripItem[] { ToolStatusMenu_Time, ToolStatusMenu_Date, ToolStatusMenu_DateTime, ToolStatusMenu_None });
            ToolStatusContextMenu.Name = "StatusContextMenu";
            ToolStatusContextMenu.Size = new Size(153, 92);
            // 
            // ToolStatusMenu_Time
            // 
            ToolStatusMenu_Time.Name = "ToolStatusMenu_Time";
            ToolStatusMenu_Time.Size = new Size(152, 22);
            ToolStatusMenu_Time.Text = "时间(&T)";
            ToolStatusMenu_Time.Click += ToolStatusMenu_Item_Click;
            // 
            // ToolStatusMenu_Date
            // 
            ToolStatusMenu_Date.Name = "ToolStatusMenu_Date";
            ToolStatusMenu_Date.Size = new Size(152, 22);
            ToolStatusMenu_Date.Text = "日期(&D)";
            ToolStatusMenu_Date.Click += ToolStatusMenu_Item_Click;
            // 
            // ToolStatusMenu_DateTime
            // 
            ToolStatusMenu_DateTime.Name = "ToolStatusMenu_DateTime";
            ToolStatusMenu_DateTime.Size = new Size(152, 22);
            ToolStatusMenu_DateTime.Text = "时间与日期(&A)";
            ToolStatusMenu_DateTime.Click += ToolStatusMenu_Item_Click;
            // 
            // ToolStatusMenu_None
            // 
            ToolStatusMenu_None.Name = "ToolStatusMenu_None";
            ToolStatusMenu_None.Size = new Size(152, 22);
            ToolStatusMenu_None.Text = "无(&N)";
            ToolStatusMenu_None.Click += ToolStatusMenu_Item_Click;
            // 
            // ToolStatus_Txt
            // 
            ToolStatus_Txt.BorderSides = ToolStripStatusLabelBorderSides.Top;
            ToolStatus_Txt.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStatus_Txt.ImageAlign = ContentAlignment.MiddleLeft;
            ToolStatus_Txt.Name = "ToolStatus_Txt";
            ToolStatus_Txt.Size = new Size(263, 21);
            ToolStatus_Txt.Spring = true;
            ToolStatus_Txt.Text = "toolStripStatusLabel1";
            ToolStatus_Txt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ToolStatus_DateTime
            // 
            ToolStatus_DateTime.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top;
            ToolStatus_DateTime.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolStatus_DateTime.Name = "ToolStatus_DateTime";
            ToolStatus_DateTime.Size = new Size(71, 21);
            ToolStatus_DateTime.Text = "2024/8/22";
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
            NotifyIcon.ContextMenuStrip = AppMenu;
            NotifyIcon.Icon = (Icon)resources.GetObject("NotifyIcon.Icon");
            NotifyIcon.Text = "WinAssistPro";
            NotifyIcon.Visible = true;
            NotifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            // 
            // AppMenu
            // 
            AppMenu.Items.AddRange(new ToolStripItem[] { AppMenu_Show, AppMenu_Setting, toolStripMenuItem6, AppMenu_Quit });
            AppMenu.Name = "AppMenu";
            AppMenu.Size = new Size(173, 76);
            // 
            // AppMenu_Show
            // 
            AppMenu_Show.Name = "AppMenu_Show";
            AppMenu_Show.Size = new Size(172, 22);
            AppMenu_Show.Text = "显示(&S)";
            AppMenu_Show.Click += AppMenu_Show_Click;
            // 
            // AppMenu_Setting
            // 
            AppMenu_Setting.Name = "AppMenu_Setting";
            AppMenu_Setting.Size = new Size(172, 22);
            AppMenu_Setting.Text = "首选项(&P)...";
            AppMenu_Setting.Click += AppMenu_Setting_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(169, 6);
            // 
            // AppMenu_Quit
            // 
            AppMenu_Quit.Name = "AppMenu_Quit";
            AppMenu_Quit.Size = new Size(172, 22);
            AppMenu_Quit.Text = "关闭 QuickLinker";
            AppMenu_Quit.Click += AppMenu_Quit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(349, 292);
            Controls.Add(StatusStrip);
            Controls.Add(tabControl1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "QuickLinker";
            Deactivate += MainForm_Deactivate;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tabPage1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            TabMenuStrip.ResumeLayout(false);
            PageMenuStrip.ResumeLayout(false);
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            ToolStatusContextMenu.ResumeLayout(false);
            AppMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel;
        private ContextMenuStrip PageMenuStrip;
        private ToolStripMenuItem MenuStrip_FullPath;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem MenuStrip_Copy;
        private ToolStripMenuItem MenuStrip_Switch;
        private ToolStripMenuItem MenuStrip_Align;
        private ToolStripMenuItem MenuStrip_CreateQuick;
        private ToolStripMenuItem MenuStrip_Clear;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem quickLinkerTToolStripMenuItem;
        private ToolStripMenuItem 帮助HToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem MenuStrip_Attr;
        private ToolStripPanel BottomToolStripPanel;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel ToolStatus_Txt;
        private ToolStripStatusLabel ToolStatus_DateTime;
        private ToolStripPanel TopToolStripPanel;
        private ToolStripPanel RightToolStripPanel;
        private ToolStripPanel LeftToolStripPanel;
        private ToolStripContentPanel ContentPanel;
        private ContextMenuStrip ToolStatusContextMenu;
        private ToolStripMenuItem ToolStatusMenu_Time;
        private ToolStripMenuItem ToolStatusMenu_Date;
        private ToolStripMenuItem ToolStatusMenu_DateTime;
        private ToolStripMenuItem ToolStatusMenu_None;
        private ContextMenuStrip TabMenuStrip;
        private ToolStripMenuItem TabMenuItem_Left;
        private ToolStripMenuItem TabMenuItem_Right;
        private ToolStripMenuItem TabMenuItem_Rename;
        private ToolStripMenuItem TabMenuItem_Delete;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem 外观ToolStripMenuItem;
        private ToolStripMenuItem TabMenuItem_Stand;
        private ToolStripMenuItem TabMenuItem_Button;
        private ToolStripMenuItem TabMenuItem_Flot;
        private TabPage tabPage2;
        public TabControl tabControl1;
        private ToolStripMenuItem MenuStrip_SystemContextMenu;
        private ToolStripSeparator toolStripMenuItem5;
        private NotifyIcon NotifyIcon;
        private ContextMenuStrip AppMenu;
        private ToolStripMenuItem AppMenu_Quit;
        private ToolStripMenuItem AppMenu_Show;
        private ToolStripMenuItem AppMenu_Setting;
        private ToolStripSeparator toolStripMenuItem6;
    }
}
