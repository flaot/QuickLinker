using Microsoft.Win32;
using QFramework;
using QuickLinker.Menus;
using QuickLinker.Model;
using QuickLinker.Plugin;
using QuickLinker.Plugin.Events;
using QuickLinker.Plugin.Menu;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WK.Libraries.HotkeyListenerNS;
using AppConfig = QuickLinker.Model.AppConfig;
using OptType = QuickLinker.QuickLaunch.Command.QuickEntityOptCommand.OptType;
using Timer = System.Windows.Forms.Timer;

namespace QuickLinker
{
    public partial class MainForm : Form, IController
    {
        private BindableProperty<OptType> _optType = new BindableProperty<OptType>();
        private TPanel _optFirstTemp; //临时数据：如交换、排列
        private Timer _dateTimer;
        private int _lastRow;
        private int _lastColumn;

        private bool formMove = false;//窗体是否移动
        private Point formPoint;//记录窗体的位置
        private int _oldGroupCount;
        private Size _offsetSize = Size.Empty;
        ToolTip _toolTip = new ToolTip();
        public static int ignoreDeactivate = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        public IArchitecture GetArchitecture() => AppArchitecture.Interface;
        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            ToolStatus_Txt.Text = string.Empty;

            var config = this.GetModel<AppConfig>();
            if (config.fontStates.Value.Invalid) config.fontStates.Value = new FontInfo(StatusStrip.Font);
            if (config.fontGroupTitle.Value.Invalid) config.fontGroupTitle.Value = new FontInfo(tabControl1.Font);
            if (config.fontBtnTitile.Value.Invalid) config.fontBtnTitile.Value = new FontInfo(tabControl1.Font);
            config.gridGroup.SetValueWithoutEvent(config.groupArray.Value.Length);

            _lastRow = config.gridRow.Value;
            _lastColumn = config.gridColumn.Value;
            _oldGroupCount = config.gridGroup.Value;

            this.MouseDown += TabControl1_MouseDragDown;
            this.MouseMove += TabControl1_MouseDragMove;
            this.MouseUp += TabControl1_MouseDragUp;

            config.groupArray.RegisterWithInitValue(Event_GroupChange);
            AutoWindowSize();
            config.grid.Register(Event_RefreshTabSizeControl);
            config.gridSize.Register(Event_RefreshTabSizeControl);
            config.gridColumn.Register(Event_RefreshTabPageControl);
            config.gridRow.Register(Event_RefreshTabPageControl);

            config.disableMinClose.RegisterWithInitValue(b => MinimizeBox = !b);
            config.disableMaxClose.RegisterWithInitValue(b => MaximizeBox = !b);
            config.titleStyle.RegisterWithInitValue(Event_TitleStyleChange);
            config.topWindow.RegisterWithInitValue(b => TopMost = b);
            _optType.Register(Event_ChengOptType);
            config.dateTimeType.RegisterWithInitValue(Event_RefreshShowMenu);
            config.dateTimeType.RegisterWithInitValue(t => Event_RefreshShowTime(null, null));
            config.windowAlpha.RegisterWithInitValue(t => Opacity = t * 1f / 100);
            config.tabAppearance.RegisterWithInitValue(t => tabControl1.Appearance = t);
            config.disableAffinity.RegisterWithInitValue(b => Win32API.SetWindowDisplayAffinity(Handle, (uint)(b ? 0x11 : 0)));
            config.showInTray.RegisterWithInitValue(b => { ShowInTaskbar = b; NotifyIcon.Visible = !b; });
            config.launch.RegisterWithInitValue(this.GetUtility<LaunchUtil>().Set);
            config.registerURI.RegisterWithInitValue(this.GetUtility<IURIUtil>().Set);
            config.fontStates.RegisterWithInitValue(t => StatusStrip.Font = new Font(t.familyName, t.pointSize, GraphicsUnit.Point));
            config.fontGroupTitle.RegisterWithInitValue(t => tabControl1.Font = new Font(t.familyName, t.pointSize, GraphicsUnit.Point));
            TypeEventSystem.Global.Register<RefreshStateTextEvent>(Event_RefreshStateText);
            TypeEventSystem.Global.Register<ShowToolTipEvent>(Event_ShowToolTip);
            TypeEventSystem.Global.Register<ClickTPanelEvent>(TPanel_OnClick);
            TypeEventSystem.Global.Register<ClickMenuTPanelEvent>(TPanel_OnClickMenu);
            TypeEventSystem.Global.Register<NoSettingStratEvent>(NoSettingStartEvent);
            TypeEventSystem.Global.Register<CloseSoftwareEvent>(CloseSoftwareEvent);

            _dateTimer = new Timer();
            _dateTimer.Tick += Event_RefreshShowTime;
            _dateTimer.Interval = 120;
            _dateTimer.Start();

            var system = this.GetSystem<QuickEntitySystem>();
            var hotKeyMgr = this.GetSystem<HotKeyManager>();
            hotKeyMgr.HotKeyListener.HotkeyPressed += HotkeyListener_HotkeyPressed;
            hotKeyMgr.InitializeQuickActionsHotKeys();
            TypeEventSystem.Global.Send(new NoSettingStratEvent());

            var appMenu = new ContextMenuStrip();
            NotifyIcon.ContextMenuStrip = appMenu;
            var menuSystem = this.GetSystem<IMenuSystem>();
            menuSystem.RegisterMenu(new MenuProxy((int)MenuType.App, appMenu));
            menuSystem.RegisterMenu(new MenuProxy((int)MenuType.Tab, new ContextMenuStrip()));
            menuSystem.RegisterMenu(new MenuProxy((int)MenuType.Page, new ContextMenuStrip()));
            menuSystem.RegisterMenu(new MenuProxy((int)MenuType.ToolStatus, new ContextMenuStrip()));
            menuSystem.RegisterMenu(new MenuProxy((int)MenuType.Folder, new ContextMenuStrip()));
            menuSystem.InitSystemMenuItem(new AppMenu(this));
            menuSystem.InitSystemMenuItem(new TabMenu(this));
            menuSystem.InitSystemMenuItem(this);
            menuSystem.InitSystemMenuItem(new ToolStatusMenu(this));
            menuSystem.InitSystemMenuItem(new FolderMenu(this));
            var pluginSystem = this.GetSystem<IPluginSystem>();
            pluginSystem.LoadAll();
            menuSystem.RequestResetAll();
            this.GetSystem<ICommandSystem>().RequestResetAll();

            if (config.appHideType.Value == AppHideType.AutoMinimize)
                Hide();

            if (config.startbutton.Value)
                this.SendCommand(new QuickEntityAutoStartCommand());
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var config = this.GetModel<AppConfig>();
                if (config.disableClose.Value)
                {
                    e.Cancel = true;
                    return;
                }
                if (!config.showInTray.Value)
                {
                    e.Cancel = true;
                    Hide();
                    return;
                }
            }
            ApplicationExit();
        }
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (ignoreDeactivate <= 0)
            {
                var config = this.GetModel<AppConfig>();
                if (config.appHideType.Value == AppHideType.LoseFocusMinimize)
                    Hide();
                //else if (config.appHideType.Value == AppHideType.RollUp)
                //    WindowState = FormWindowState.
            }
            this.SendCommand(new QuickEntitySaveCommand());
        }

        private void HotkeyListener_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            var hotKeyMgr = this.GetSystem<HotKeyManager>();
            var config = this.GetModel<AppConfig>();
            if (e.Hotkey.ToString() == HotKeyUtil.Convert(config.actionHotKey.Value).ToString())
            {
                WindowState = FormWindowState.Normal;
                Show();
                Activate();
                if (config.showMouse.Value)
                {
                    var showPos = MousePosition;
                    showPos.X -= Width / 2;
                    showPos.Y -= Height / 2;
                    this.Location = showPos;
                }
            }
            else
                hotKeyMgr.ProcessQuickActionHotKey(e.Hotkey);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //上下键切换焦点逻辑,与原生左右键一致
            if (keyData == Keys.Up || keyData == Keys.Down)
            {
                var tPanel = this.ActiveControl as TPanel;
                if (tPanel == null)
                    return base.ProcessCmdKey(ref msg, keyData);
                var appConfig = this.GetModel<AppConfig>();
                int pageGridCount = appConfig.gridColumn.Value * appConfig.gridRow.Value;
                var num = tPanel.Index % pageGridCount;
                var row = num / appConfig.gridColumn.Value;
                var column = num % appConfig.gridColumn.Value;
                var newRow = Math.Clamp(keyData == Keys.Up ? row - 1 : row + 1, 0, appConfig.gridRow.Value - 1);
                if (newRow == row)
                {
                    newRow = keyData == Keys.Up ? appConfig.gridRow.Value - 1 : 0;
                    var newColumn = Math.Clamp(keyData == Keys.Up ? column - 1 : column + 1, 0, appConfig.gridColumn.Value - 1);
                    if (newColumn == column)
                        newColumn = keyData == Keys.Up ? appConfig.gridColumn.Value - 1 : 0;
                    column = newColumn;
                }
                var controlIndex = newRow * appConfig.gridColumn.Value + column;
                var nextSelect = tabControl1.SelectedTab.Controls[controlIndex] as TPanel;
                nextSelect.Focus();
                return true;
            }
            if (keyData == Keys.Apps)
            {
                var tPanel = ActiveControl as TPanel;
                Cursor.Position = ActiveControl.PointToScreen((Point)(ActiveControl.Size / 2));
                if (tPanel == null)
                {
                    ShowTabMenu();
                }
                else
                { 
                    Selection.activeContext = tPanel;
                    TypeEventSystem.Global.Send(new ClickMenuTPanelEvent());
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TabControl1_MouseDragDown(object sender, MouseEventArgs e)
        {
            if (this.GetModel<AppConfig>().disableMove.Value)
                return;
            formPoint = new Point();
            if (e.Button == MouseButtons.Left)
            {
                formPoint = e.Location;//获取鼠标在窗口上的坐标
                formMove = true;//开始移动
            }
        }
        private void TabControl1_MouseDragMove(object sender, MouseEventArgs e)
        {
            if (formMove == true)
            {
                Point point = new Point(e.Location.X - formPoint.X, e.Location.Y - formPoint.Y);
                point.X += Location.X;
                point.Y += Location.Y;
                this.Location = point;
            }
        }
        private void TabControl1_MouseDragUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//按下的是鼠标左键
            {
                formMove = false;//停止移动
            }
        }
        private void TabControl1_MouseUp(object _, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            ShowTabMenu();
        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSystem<IAudioSystem>().PlayAudio(AudioType.Group);
        }
        private void TabControl1_Enter(object sender, EventArgs e)
        {
            (sender as TabControl).SelectedTab?.Focus();
        }
        private void TabControl1_Click(object sender, EventArgs e)
        {
            (sender as TabControl).SelectedTab?.Focus();
        }
        public void ShowTabMenu()
        {
            var config = this.GetModel<AppConfig>();
            var menuSystem = this.GetSystem<IMenuSystem>();
            var menuType = (int)MenuType.Tab;
            menuSystem.SetEnable(menuType, "左移标签(&L)", tabControl1.SelectedIndex != 0);
            menuSystem.SetEnable(menuType, "右移标签(&R)", tabControl1.SelectedIndex != tabControl1.TabPages.Count - 1);
            menuSystem.SetEnable(menuType, "删除(&D)", tabControl1.TabPages.Count > 1);
            menuSystem.SetChecked(menuType, "外观/标准(&N)", config.tabAppearance.Value == TabAppearance.Normal);
            menuSystem.SetChecked(menuType, "外观/按钮(&B)", config.tabAppearance.Value == TabAppearance.Buttons);
            menuSystem.SetChecked(menuType, "外观/平面按钮(&F)", config.tabAppearance.Value == TabAppearance.FlatButtons);
            this.GetSystem<IMenuSystem>().Show(menuType, MousePosition.X, MousePosition.Y);
        }

        private void TPanel_OnClick(ClickTPanelEvent info)
        {
            var panel = Selection.activeContext as TPanel;
            if (_optType.Value != OptType.None)
            {
                _optFirstTemp.Invert(false);
                do
                {
                    if (_optFirstTemp.Index == panel.Index)
                        break;
                    if (_optType.Value == OptType.Copy && panel.Entity != null)
                    {
                        DialogResult dialogResult = MessageBox.Show(string.Format(Resources.MainForm_Copy, _optFirstTemp.Title, panel.Title), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult != DialogResult.Yes)
                            break;
                    }
                    this.SendCommand(new QuickEntityOptCommand() { optType = _optType.Value, fromIndex = _optFirstTemp.Index, index = panel.Index });
                } while (false);
                _optType.Value = OptType.None;
                return;
            }
            if ((ModifierKeys & Keys.Control) != 0) //Ctrl+左键 打开所在目录
            {
                if (panel.Entity != null)
                    this.SendCommand(new QuickEntityShowInExploreCommand() { index = panel.Index });
            }
            else //只有鼠标左键 打开应用
            {
                if (panel.Entity == null)
                {
                    if (!this.GetModel<AppConfig>().ignoreZeroButton.Value)
                    {
                        panel.Invert(true);
                        BtnPropertiesFrom.Show(panel);
                        panel.Invert(false);
                    }
                    return;
                }
                Selection.activeEntity = panel.Entity;
                bool closeSoft = panel.Entity.closeSoft;
                this.GetSystem<IAudioSystem>().PlayAudio(AudioType.Click);
                this.SendCommand(new QuickEntityOpenCommand() { index = panel.Index, dropFileOrDirs = Selection.dropFileOrDirs });
                if (closeSoft)
                    TypeEventSystem.Global.Send(new CloseSoftwareEvent());
            }
        }
        private void TPanel_OnClickMenu(ClickMenuTPanelEvent info)
        {
            var panel = Selection.activeContext as TPanel;
            if (_optType.Value != OptType.None)
                return;
            Selection.activeEntity = panel.Entity;
            TypeEventSystem.Global.Send(new ShowItemMenuPreEvent());
            if (Selection.activeContext == null)
                return;
            var menuSystem = this.GetSystem<IMenuSystem>();
            var menuType = (int)MenuType.Page;
            menuSystem.SetEnable(menuType, "(未配置)", panel.Entity != null);
            var menuProxy = menuSystem.GetMenu(menuType) as MenuProxy;
            var menuFullPath = menuProxy.FindStripMenuItem("(未配置)");
            menuFullPath.Text = panel.Title;
            menuFullPath.Font = new Font(menuFullPath.Font, panel.Entity != null ? FontStyle.Bold : FontStyle.Regular);
            var menuAttr = menuProxy.FindStripMenuItem("属性(&P)");
            menuAttr.Font = new Font(menuAttr.Font, panel.Entity == null ? FontStyle.Bold : FontStyle.Regular);
            menuSystem.Show(menuType, MousePosition.X, MousePosition.Y);
            TypeEventSystem.Global.Send(new ShowItemMenuPostEvent());
        }
        private void Event_ChengOptType(OptType type)
        {
            if (type != OptType.None)
            {
                switch (type)
                {
                    case OptType.Copy:
                        _optFirstTemp.Invert(true);
                        ToolStatus_Txt.Text = Resources.MainForm_CopyStats;
                        break;
                    case OptType.Switch:
                        _optFirstTemp.Invert(true);
                        ToolStatus_Txt.Text = Resources.MainForm_SwitchStats;
                        break;
                    case OptType.Align:
                        _optFirstTemp.Invert(true);
                        ToolStatus_Txt.Text = Resources.MainForm_AlignStats;
                        break;
                }
                using (MemoryStream ms = new MemoryStream(Resources.CUR_COPY))
                    tabControl1.Cursor = new Cursor(ms);
            }
            else
            {
                ToolStatus_Txt.Text = string.Empty;
                tabControl1.Cursor = Cursors.Default;
                this.GetSystem<IAudioSystem>().PlayAudio(AudioType.Button);
            }
        }

        //MenuStrip
        private void MenuStrip_CreateQuick_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            if (tPanel.Entity == null)
                return;
            CommonCode.CreateShortcut(tPanel.Entity);
        }
        private void MenuStrip_SystemContextMenu_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            if (tPanel.Entity == null)
                return;
            DirectoryInfo[] folders = new DirectoryInfo[1];
            folders[0] = new DirectoryInfo(tPanel.Entity.Path);
            ShellContextMenu scm = new ShellContextMenu();
            Point p = Cursor.Position;
            p.X -= 80;
            p.Y -= 80;
            scm.ShowContextMenu(folders, p);
        }
        private void MenuStrip_Clear_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            if (tPanel.Entity == null)
                return;
            DialogResult dialogResult = MessageBox.Show(string.Format(Resources.MainForm_Remove, tPanel.Title), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                this.SendCommand(new QuickEntityRemoveCommand() { index = tPanel.Index });
        }
        private void MenuStrip_Copy_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            _optFirstTemp = tPanel;
            _optType.Value = OptType.Copy;
        }
        private void MenuStrip_Switch_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            _optFirstTemp = tPanel;
            _optType.Value = OptType.Switch;
        }
        private void MenuStrip_Align_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            _optFirstTemp = tPanel;
            _optType.Value = OptType.Align;
        }
        private void MenuStrip_Attr_Click(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            var tPanel = stripMenuItem.Owner.Tag as TPanel;
            tPanel.Invert(true);
            BtnPropertiesFrom.Show(tPanel);
            tPanel.Invert(false);
        }

        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MOVE = 0xF012;
        protected override void WndProc(ref Message m)
        {
            SystemMenu.WndProc(ref m, this);
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_MOVE)
            {
                if (this.GetModel<AppConfig>().disableMove.Value)
                    return;
            }
            base.WndProc(ref m);
        }

        public bool ApplicationExit()
        {
            var config = AppArchitecture.Interface.GetModel<AppConfig>();
            if (config.disableCloseSoftware.Value && !PasswordForm.ShowForm())
                return false;
            _dateTimer?.Stop();
            var hotKeyMgr = this.GetSystem<HotKeyManager>();
            hotKeyMgr.HotKeyListener?.RemoveAll();
            hotKeyMgr.HotKeyListener?.Dispose();
            return true;
        }
        private void Event_TitleStyleChange(TitleStyle b)
        {
            switch (b)
            {
                case TitleStyle.Stand:
                    FormBorderStyle = FormBorderStyle.FixedSingle;
                    break;
                case TitleStyle.Min:
                    FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    break;
                case TitleStyle.None:
                default:
                    FormBorderStyle = FormBorderStyle.None;
                    break;
            }
        }
        private void Event_RefreshShowMenu(DateTimeType dateTime)
        {
            var menuSystem = this.GetSystem<IMenuSystem>();
            var menuType = (int)MenuType.ToolStatus;
            menuSystem.SetChecked(menuType, "时间(&T)", dateTime == DateTimeType.Time);
            menuSystem.SetChecked(menuType, "无(&N)", dateTime == DateTimeType.None);
            menuSystem.SetChecked(menuType, "日期(&D)", dateTime == DateTimeType.Date);
            menuSystem.SetChecked(menuType, "时间与日期(&A)", dateTime == DateTimeType.DateTime);
        }
        private void Event_RefreshShowTime(object sender, EventArgs e)
        {
            var config = this.GetModel<AppConfig>();
            DateTime dateTime = DateTime.Now;
            string showDateOrTime = string.Empty;
            switch (config.dateTimeType.Value)
            {
                case DateTimeType.Time:
                    showDateOrTime = config.useLongTime.Value ?
                        dateTime.ToLongTimeString() : dateTime.ToShortTimeString();
                    break;
                case DateTimeType.Date:
                    showDateOrTime = config.useLongDate.Value ?
                        dateTime.ToLongDateString() : dateTime.ToShortDateString();
                    break;
                case DateTimeType.DateTime:
                    var time = config.useLongTime.Value ?
                    dateTime.ToLongTimeString() : dateTime.ToShortTimeString();
                    var date = config.useLongDate.Value ?
                    dateTime.ToLongDateString() : dateTime.ToShortDateString();
                    showDateOrTime = time + " - " + date;
                    break;
                case DateTimeType.None:
                default:
                    break;
            }
            if (string.IsNullOrEmpty(showDateOrTime))
                ToolStatus_DateTime.Text = string.Empty;
            else
                ToolStatus_DateTime.Text = showDateOrTime;
        }
        private void StatusStrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            this.GetSystem<IMenuSystem>().Show((int)MenuType.ToolStatus, MousePosition.X, MousePosition.Y);
        }
        private void Event_RefreshStateText(RefreshStateTextEvent info)
        {
            this.Invoke(() =>
            {
                ToolStatus_Txt.Text = info.text;
            });
        }
        private void Event_ShowToolTip(ShowToolTipEvent info)
        {
            _toolTip.SetToolTip(info.control, info.text);
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SystemMenu.OnHandleCreated(e, this);
        }

        private void AutoWindowSize()
        {
            var config = this.GetModel<AppConfig>();
            if (_offsetSize == Size.Empty)
                _offsetSize = tabControl1.Size - tabControl1.TabPages[0].Size;
            tabControl1.Size = new Size(config.gridColumn.Value * config.gridSize.Value + (config.gridColumn.Value - 1) * config.grid.Value,
                                        config.gridSize.Value * config.gridRow.Value + (config.gridRow.Value - 1) * config.grid.Value)
                                       + _offsetSize;
            var titelHeight = Height - ClientRectangle.Height;
            Size = new Size(tabControl1.Size.Width, tabControl1.Size.Height + titelHeight + StatusStrip.Height);
        }
        private void Event_RefreshTabSizeControl(int _)
        {
            var config = this.GetModel<AppConfig>();
            tabControl1.SuspendLayout();
            SuspendLayout();
            foreach (TabPage page in tabControl1.TabPages)
            {
                page.SuspendLayout();
                List<Control> controls = new List<Control>(page.Controls.Count);
                foreach (Control control in page.Controls)
                    controls.Add(control);
                for (int row = 0; row < config.gridRow.Value; row++)
                {
                    for (int column = 0; column < config.gridColumn.Value; column++)
                    {
                        var defalutIndex = row * config.gridColumn.Value + column;
                        var tPanel = controls[defalutIndex];
                        tPanel.Size = new Size(config.gridSize.Value, config.gridSize.Value);
                        var columnSize = column * config.gridSize.Value;
                        var rowSize = row * config.gridSize.Value;
                        columnSize += config.grid.Value * column;
                        rowSize += config.grid.Value * row;
                        tPanel.Location = new Point(columnSize, rowSize);
                    }
                }
                page.ResumeLayout(false);
            }
            tabControl1.ResumeLayout(true);
            ResumeLayout();
            AutoWindowSize();
        }
        private void Event_RefreshTabPageControl(int _)
        {
            var config = this.GetModel<AppConfig>();
            tabControl1.SuspendLayout();
            int lastPageGridCount = tabControl1.TabPages.Count * _lastColumn * _lastRow;
            int pageGridCount = tabControl1.TabPages.Count * config.gridColumn.Value * config.gridRow.Value;
            List<TPanel> controls = new List<TPanel>(Math.Max(lastPageGridCount, pageGridCount));
            foreach (TabPage tabPage in tabControl1.TabPages)
                foreach (TPanel control in tabPage.Controls)
                    controls.Add(control);
            //增加
            for (int i = lastPageGridCount; i < pageGridCount; i++)
            {
                var tPanel = new TPanel(i);
                tPanel.Size = new Size(config.gridSize.Value, config.gridSize.Value);
                controls.Add(tPanel);
            }
            //减少
            for (int i = lastPageGridCount - 1; i >= pageGridCount; i--)
            {
                var tPanel = controls[i];
                tPanel.UnLoad();
                var tabPage = tPanel.Parent as TabPage;
                tabPage.Controls.Remove(tPanel);
            }
            //调整page中的格子所属容器
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                int startIndex = i * config.gridColumn.Value * config.gridRow.Value;
                int endIndex = (i + 1) * config.gridColumn.Value * config.gridRow.Value;
                TabPage tabPage = tabControl1.TabPages[i];
                tabPage.Controls.Clear();
                for (int index = startIndex; index < endIndex; index++)
                {
                    var tPanel = controls[index];
                    tabPage.Controls.Add(tPanel);
                }
            }
            tabControl1.ResumeLayout(false);
            //对数据层进行更改
            for (int pageIndex = tabControl1.TabPages.Count - 1; pageIndex >= 0; pageIndex--)
            {
                int lastPageCount = pageIndex * _lastColumn * _lastRow;
                int newPageCount = pageIndex * config.gridColumn.Value * config.gridRow.Value;
                //减少行
                for (int row = _lastRow - 1; row >= config.gridRow.Value; row--)
                {
                    for (int column = config.gridColumn.Value - 1; column >= 0; column--)
                    {
                        var defalutIndex = row * _lastColumn + column + lastPageCount;
                        this.SendCommand(new QuickEntityRemoveCommand() { index = defalutIndex });
                        if (pageIndex != tabControl1.TabPages.Count - 1)
                        {
                            this.SendCommand(new QuickEntityOptCommand() { optType = OptType.Align, fromIndex = defalutIndex, index = lastPageGridCount - 1 });
                        }
                    }
                }
                //减少列(必须先遍历行)
                for (int row = config.gridRow.Value - 1; row >= 0; row--)
                {
                    for (int column = _lastColumn - 1; column >= config.gridColumn.Value; column--)
                    {
                        var defalutIndex = row * _lastColumn + column + lastPageCount;
                        this.SendCommand(new QuickEntityRemoveCommand() { index = defalutIndex });
                        //在减少列时，需要进行一次排列，把其中的图标挤到指定位置
                        if (defalutIndex != lastPageGridCount - 1)
                            this.SendCommand(new QuickEntityOptCommand() { optType = OptType.Align, fromIndex = defalutIndex, index = lastPageGridCount - 1 });
                    }
                }
                //增加行
                for (int row = _lastRow; row < config.gridRow.Value; row++)
                {
                    for (int column = 0; column < config.gridColumn.Value; column++)
                    {
                        var defalutIndex = row * _lastColumn + column + lastPageCount;
                        if (pageIndex != tabControl1.TabPages.Count - 1)
                        {
                            this.SendCommand(new QuickEntityOptCommand() { optType = OptType.Align, fromIndex = pageGridCount - 1, index = defalutIndex });
                        }
                    }
                }
                //LogPage(new Point(_lastRow, _lastColumn), new Point(_lastRow, _lastColumn));
                //增加列
                for (int column = _lastColumn; column < config.gridColumn.Value; column++)
                {
                    for (int row = config.gridRow.Value - 1; row >= 0; row--)
                    {
                        var defalutIndex = row * _lastColumn + column + lastPageCount;
                        //在增加列时，需要进行一次排列，把其中的图标挤到指定位置
                        if (defalutIndex < lastPageGridCount)
                            this.SendCommand(new QuickEntityOptCommand() { optType = OptType.Align, fromIndex = pageGridCount, index = defalutIndex });
                    }
                }
                //LogPage(new Point(config.gridRow.Value, config.gridColumn.Value), new Point(config.gridRow.Value, config.gridColumn.Value));
            }
            _lastRow = config.gridRow.Value;
            _lastColumn = config.gridColumn.Value;
            Event_RefreshTabSizeControl(0);
        }
        private void LogPage(params Point[] points)
        {
            StringBuilder sb = new StringBuilder();
            var config = this.GetModel<AppConfig>();
            var system = this.GetSystem<QuickEntitySystem>();
            var entytys = system.QueryDataWithAnyFlag(Array.Empty<string>());
            //string.Join(",", entytys).LogInfo();
            for (int tagPag = 0; tagPag < config.groupArray.Value.Length; tagPag++)
            {
                sb.AppendLine($"---- pag:{tagPag} -----");
                Point pos;
                if (tagPag < points.Length)
                    pos = points[tagPag];
                else
                    pos = points[points.Length - 1];
                var pagCount = tagPag * pos.X * pos.Y;
                for (int row = 0; row < pos.X; row++)
                {
                    for (int column = 0; column < pos.Y; column++)
                    {
                        var findIndex = row * pos.Y + column + pagCount;
                        var entity = system.Find(findIndex);
                        sb.Append(entity != null ? "{" + entity.index + "}" : findIndex.ToString());
                        sb.Append('\t');
                    }
                    sb.AppendLine();
                }
            }
            sb.LogInfo();
        }
        private void Event_GroupChange(string[] strs)
        {
            var config = this.GetModel<AppConfig>();
            var groupArray = config.groupArray;
            for (int i = tabControl1.TabCount; i < groupArray.Value.Length; i++)
            {
                this.AddPage(groupArray.Value[i]);
            }
            for (int i = tabControl1.TabCount; i > groupArray.Value.Length; i--)
            {
                this.RemovePage(i - 1);
            }
            for (int i = 0; i < groupArray.Value.Length; i++)
            {
                tabControl1.TabPages[i].Text = groupArray.Value[i];
            }
        }

        //NotifyIcon
        public void ShowMainWindow()
        {
            NotifyIcon.Visible = true;
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
            if(SystemInformation.TerminalServerSession)
                this.Location = new Point(0, 0);
        }
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if ((ModifierKeys & Keys.Control) != 0) //Ctrl+左键 打开所在目录
            {
                Process.Start("explorer.exe", "/e,/select," + Application.ExecutablePath);
            }
            else
            {
                ShowMainWindow();
            }
        }

        private void NoSettingStartEvent(NoSettingStratEvent _)
        {
            var appConfig = this.GetModel<AppConfig>();
            if (!appConfig.firstCreate.Value)
                return;
            appConfig.firstCreate.Value = false;
            var config = this.GetModel<AppConfig>();
            var system = this.GetSystem<QuickEntitySystem>();

            var turboLaunchRoot = Registry.CurrentUser.OpenSubKey("Software\\TurboLaunch");
            if (turboLaunchRoot == null)
                return;
            if (MessageBox.Show(Resources.FromTurboLaunch_Switch, Resources.MSGBox_Tip, MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            var entytys = system.QueryDataWithAnyFlag(Array.Empty<string>());
            foreach (var item in entytys)
                this.SendCommand(new QuickEntityRemoveCommand() { index = item.index });

            config.gridRow.Value = turboLaunchRoot.ReadDword("Rows", config.gridRow.Value);
            config.gridColumn.Value = turboLaunchRoot.ReadDword("Columns", config.gridColumn.Value);
            config.gridGroup.Value = turboLaunchRoot.ReadDword("Groups", config.gridGroup.Value);
            config.gridSize.Value = turboLaunchRoot.ReadDword("IconSize", config.gridSize.Value);
            config.topWindow.Value = turboLaunchRoot.ReadDword("AlwaysOnTop", config.topWindow.Value ? 1 : 0) == 1;
            config.showToolTip.Value = turboLaunchRoot.ReadDword("ShowToolTips", config.showToolTip.Value ? 1 : 0) == 1;
            config.ignoreZeroButton.Value = turboLaunchRoot.ReadDword("IgnoreBlankClicks", config.ignoreZeroButton.Value ? 1 : 0) == 1;
            config.showInTray.Value = turboLaunchRoot.ReadDword("ShowInTaskBar", config.showInTray.Value ? 1 : 0) == 1;
            config.showStateTip.Value = turboLaunchRoot.ReadDword("ShowStatusBar", config.showStateTip.Value ? 1 : 0) == 1;
            config.showToolTip.Value = turboLaunchRoot.ReadDword("ShowToolTips", config.showToolTip.Value ? 1 : 0) == 1;
            config.showButtonTip.Value = turboLaunchRoot.ReadDword("ShowButtonCaptions", config.showButtonTip.Value ? 1 : 0) == 1;
            config.titleStyle.Value = (TitleStyle)turboLaunchRoot.ReadDword("TitleBar", 0);
            config.windowAlpha.Value = turboLaunchRoot.ReadDword("Transparency", 256) / 256 * 100;
            config.useLongDate.Value = turboLaunchRoot.ReadDword("UseLongDate", config.useLongDate.Value ? 1 : 0) == 1;
            config.useLongTime.Value = turboLaunchRoot.ReadDword("UseLongTime", config.useLongTime.Value ? 1 : 0) == 1;
            var groupsKey = turboLaunchRoot.OpenSubKey("GroupNames");
            List<string> names = new List<string>();
            for (int i = 0; i < config.gridGroup.Value; i++)
            {
                string name = groupsKey.ReadSz(string.Format("{0:000}", i + 1), string.Format(Resources.BtnPropertiesFrom_GroupDefName, i + 1));
                names.Add(name);
            }
            config.groupArray.Value = names.ToArray();
            var configsKey = turboLaunchRoot.OpenSubKey("ButtonConfigs");
            foreach (var subKeyName in configsKey.GetSubKeyNames())
            {
                if (!int.TryParse(subKeyName, out var inIndex))
                    continue;
                var subKey = configsKey.OpenSubKey(subKeyName);
                if (subKey.ReadDword("Initialized") == 0)
                    continue;
                string exeFile = subKey.ReadSz("Command", string.Empty);
                if (string.IsNullOrEmpty(exeFile) || !(File.Exists(exeFile) || Directory.Exists(exeFile)))
                    continue;
                this.SendCommand(new QuickEntityInsertCommand() { filePath = exeFile, index = inIndex - 1, canParse = false });
                string description = subKey.ReadSz("Description", string.Empty);
                if (!string.IsNullOrEmpty(description))
                    this.SendCommand(new QuickEntitySetCommand() { index = inIndex - 1, desc = description });
                string workingDir = subKey.ReadSz("WorkingDir", string.Empty);
                if (!string.IsNullOrEmpty(workingDir))
                    this.SendCommand(new QuickEntitySetCommand() { index = inIndex - 1, workFolder = workingDir });
                string hotKey = subKey.ReadSz("HotKey", string.Empty);
                if (!string.IsNullOrEmpty(hotKey))
                    this.SendCommand(new QuickEntitySetCommand() { index = inIndex - 1, actionHotKey = hotKey });
                bool dropNLaunch = subKey.ReadDword("DropNLaunch", 0) == 1;
                if (dropNLaunch)
                    this.SendCommand(new QuickEntitySetCommand() { index = inIndex - 1, dropNLaunch = dropNLaunch });
                bool launchOnStartup = subKey.ReadDword("LaunchOnStartup", 0) == 1;
                if (dropNLaunch)
                    this.SendCommand(new QuickEntitySetCommand() { index = inIndex - 1, launchOnStartup = launchOnStartup });
                string parameters = subKey.ReadSz("Parameters", string.Empty);
                if (!string.IsNullOrEmpty(parameters))
                    this.SendCommand(new QuickEntitySetCommand() { index = inIndex - 1, startArg = parameters });
            }
        }
        private void CloseSoftwareEvent(CloseSoftwareEvent _)
        {
            if (!ApplicationExit())
                return;
            NotifyIcon.Visible = false;
            Close();
            Dispose();
            Application.Exit();
        }
    }
}
