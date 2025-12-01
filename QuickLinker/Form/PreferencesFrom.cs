using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace QuickLinker
{
    public partial class PreferencesFrom : Form, IController
    {
        private List<IUnRegister> _unRegisters = new List<IUnRegister>();

        private int _oldGroupNumber = -1;
        public PreferencesFrom()
        {
            InitializeComponent();
        }

        public IArchitecture GetArchitecture() => AppArchitecture.Interface;
        private void SettingForm_Load(object sender, EventArgs e)
        {
            var appConfig = this.GetModel<AppConfig>();
            this.TopMost = appConfig.topWindow.Value;
            //常规
            RegisterBool(appConfig.topWindow, checkBox1);
            RegisterBool(appConfig.analyzeDrapLink, checkBox2);
            RegisterBool(appConfig.disableClose, checkBox3);
            RegisterBool(appConfig.disableMinClose, checkBox4);
            RegisterBool(appConfig.disableMove, checkBox5);
            RegisterBool(appConfig.ignoreZeroButton, checkBox6);
            RegisterBool(appConfig.blockRepeatRun, checkBox7);
            RegisterBool(appConfig.showMouse, checkBox8);
            RegisterBool(appConfig.launch, checkBox9);
            RegisterBool(appConfig.registerURI, checkBox19);
            RegisterBool(appConfig.startbutton, checkBox10);
            RegisterCombox(appConfig.tabAppearance, comboBox4);
            RegisterBool(appConfig.disableAffinity, checkBox18);
            var hotKeySys = this.GetSystem<HotKeyManager>();
            hotKeySys.RemoveAllQuickActions();
            textBox1.Text = appConfig.actionHotKey.Value;
            textBox1.KeyUp += HotKeyUtil.Control_KeyUp;
            textBox1.KeyDown += HotKeyUtil.Control_KeyDown;

            //外观
            RegisterCombox(appConfig.titleStyle, comboBox1);
            RegisterBool(appConfig.showToolTip, checkBox11);
            RegisterBool(appConfig.showStateTip, checkBox12);
            RegisterBool(appConfig.showButtonTip, checkBox13);
            RegisterCombox(appConfig.appHideType, comboBox2);
            RegisterCombox(appConfig.dateTimeType, comboBox3);
            RegisterBool(appConfig.useLongTime, checkBox14);
            RegisterBool(appConfig.useLongDate, checkBox15);
            RegisterBool(appConfig.showInTray, checkBox16);
            RegisterTrackBar(appConfig.windowAlpha, trackBar1);
            _unRegisters.Add(appConfig.windowAlpha.RegisterWithInitValue(Event_ChangeAlphaLabel));

            //按钮与组
            RegisterBool(appConfig.flatButton, checkBox17);
            RegisterNumericUpDown(appConfig.gridRow, numericUpDown1);
            RegisterNumericUpDown(appConfig.gridColumn, numericUpDown2);
            RegisterNumericUpDown(appConfig.gridGroup, numericUpDown3);
            RegisterNumericUpDown(appConfig.gridSize, numericUpDown4);
            RegisterNumericUpDown(appConfig.grid, numericUpDown5);
            _unRegisters.Add(appConfig.gridGroup.RegisterWithInitValue(Event_GroupNumberChange));

            //声音
            TreeView_Audio.ExpandAll();
            TreeView_Audio.SelectedNode = TreeView_Audio.Nodes[0];
            var imageUtil = this.GetUtility<IImageUtil>();
            Btn_AudioBrowse.Image = imageUtil.ScaleBitmap(imageUtil.Base64ToBitmapImage(Constants.DEFAULT_DIR_IMAGE_BASE64), Btn_AudioBrowse.Width, Btn_AudioBrowse.Height);

            //字体
            TreeView_Font.ExpandAll();
            TreeView_Font.SelectedNode = TreeView_Font.Nodes[0];

            //保护
            Txt_Password.Text = appConfig.password.Value;
            Txt_RPassword.Text = appConfig.password.Value;
            RegisterBool(appConfig.disableChangeSetting, checkBox20);
            RegisterBool(appConfig.persistConfigureMenu, checkBox21);
            RegisterBool(appConfig.persistDragMenu, checkBox22);
            RegisterBool(appConfig.disableCloseSoftware, checkBox23);
            _unRegisters.Add(appConfig.disableChangeSetting.RegisterWithInitValue(Event_DispableChangeSetting));
            _unRegisters.Add(appConfig.password.RegisterWithInitValue(Event_Password));
            Txt_Password.TextChanged += Txt_Password_TextChanged;
            Txt_RPassword.TextChanged += Txt_Password_TextChanged;
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var appConfig = this.GetModel<AppConfig>();
            appConfig.actionHotKey.Value = textBox1.Text;
            if (_unRegisters != null)
            {
                _unRegisters.ForEach(item => item.UnRegister());
                _unRegisters = null;
            }
            var hotKeySys = this.GetSystem<HotKeyManager>();
            hotKeySys.InitializeQuickActionsHotKeys();
        }
        private void RegisterBool(BindableProperty<bool> bindable, CheckBox checkBox)
        {
            _unRegisters.Add(bindable.RegisterWithInitValue(v => checkBox.Checked = v));
            checkBox.Tag = bindable;
            checkBox.CheckedChanged += CheckBox_CheckedChanged;
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            var bindable = checkBox.Tag as BindableProperty<bool>;
            bindable.Value = checkBox.Checked;
        }
        private void RegisterCombox<T>(BindableProperty<T> bindable, ComboBox comboBox)
        {
            _unRegisters.Add(bindable.RegisterWithInitValue(v => comboBox.SelectedIndex = (int)(object)v));
            comboBox.Tag = bindable;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        }
        private void RegisterComboxEnum<T>(BindableProperty<T> bindable, ComboBox comboBox)
            where T : Enum
        {
            comboBox.Items.Clear();
            foreach (var enumName in Enum.GetNames(typeof(T)))
            {
                comboBox.Items.Add(enumName);
            }
            _unRegisters.Add(bindable.RegisterWithInitValue(v => comboBox.SelectedIndex = (int)(object)v));
            comboBox.Tag = bindable;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            var valueProperty = comboBox.Tag.GetType().GetProperty("Value");
            object setValue = null;
            if (valueProperty.PropertyType.IsEnum)
                setValue = Enum.ToObject(valueProperty.PropertyType, comboBox.SelectedIndex);
            else
                setValue = Convert.ChangeType(comboBox.SelectedIndex, valueProperty.PropertyType);
            valueProperty.SetValue(comboBox.Tag, setValue);
        }

        private void RegisterNumericUpDown(BindableProperty<int> bindable, NumericUpDown numberic)
        {
            _unRegisters.Add(bindable.RegisterWithInitValue(v => numberic.Value = v));
            numberic.Tag = bindable;
            numberic.ValueChanged += Numberic_ValueChanged;
        }
        private void Numberic_ValueChanged(object sender, EventArgs e)
        {
            var numberic = sender as NumericUpDown;
            var bindable = numberic.Tag as BindableProperty<int>;
            bindable.Value = (int)numberic.Value;
        }

        private void RegisterTrackBar(BindableProperty<int> bindable, TrackBar trackBar)
        {
            _unRegisters.Add(bindable.RegisterWithInitValue(v => trackBar.Value = v));
            trackBar.Tag = bindable;
            trackBar.ValueChanged += TrackBar_ValueChanged;
        }
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBar;
            var bindable = trackBar.Tag as BindableProperty<int>;
            bindable.Value = trackBar.Value;
        }
        private void Event_ChangeAlphaLabel(int alpha)
        {
            label9.Text = alpha.ToString() + '%';
        }
        private void Event_GroupNumberChange(int groupCount)
        {
            var appConfig = this.GetModel<AppConfig>();
            var tempGroupList = appConfig.groupArray.Value.ToList();
            var newGroupCount = appConfig.gridGroup.Value;
            //初始化
            if (_oldGroupNumber < 0)
                _oldGroupNumber = appConfig.gridGroup.Value;

            //增加组
            for (int i = _oldGroupNumber; i < newGroupCount; i++)
            {
                string groupName = string.Format(Resources.BtnPropertiesFrom_GroupDefName, i + 1);
                tempGroupList.Add(groupName);
            }
            //减少组
            for (int i = _oldGroupNumber; i > newGroupCount; i--)
            {
                tempGroupList.RemoveAt(i - 1);
            }
            //显示至界面
            dataGridView1.Rows.Clear();
            for (int i = 0; i < tempGroupList.Count; i++)
            {
                dataGridView1.Rows.Add(i + 1, tempGroupList[i]);
            }
            //保存配置
            if (!tempGroupList.All(appConfig.groupArray.Value.Contains) ||
                appConfig.groupArray.Value.Length != tempGroupList.Count)
                appConfig.groupArray.Value = tempGroupList.ToArray();
            _oldGroupNumber = newGroupCount;
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            var appConfig = this.GetModel<AppConfig>();
            DataGridView dataGridView = sender as DataGridView;
            string changeText = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            var groupArray = appConfig.groupArray;
            string oldeText = groupArray.Value[e.RowIndex];
            if (!string.Equals(oldeText, changeText))
            {
                string[] tempArray = new string[groupArray.Value.Length];
                Array.Copy(groupArray.Value, tempArray, tempArray.Length);
                tempArray[e.RowIndex] = changeText;
                appConfig.groupArray.Value = tempArray;
            }
        }

        private void Btn_GroupUp_Click(object sender, EventArgs e)
        {
            int curIndex = dataGridView1.CurrentRow.Index;
            int swapIndex = curIndex - 1;
            if (swapIndex < 0)
                return;
            this.SwitchPage(curIndex, swapIndex);
            Event_GroupNumberChange(_oldGroupNumber);
            dataGridView1.Rows[swapIndex].Cells[1].Selected = true;
        }
        private void Btn_GroupDown_Click(object sender, EventArgs e)
        {
            int curIndex = dataGridView1.CurrentRow.Index;
            int swapIndex = curIndex + 1;
            if (swapIndex >= dataGridView1.RowCount)
                return;
            this.SwitchPage(curIndex, swapIndex);
            Event_GroupNumberChange(_oldGroupNumber);
            dataGridView1.Rows[swapIndex].Cells[1].Selected = true;
        }
        public static bool ShowSetting()
        {
            try
            {
                MainForm.ignoreDeactivate++;
                if (!PasswordForm.ShowForm())
                    return false;
                using (var settingForm = new PreferencesFrom())
                {
                    settingForm.ShowDialog();
                }
                return true;
            }
            finally
            {

                MainForm.ignoreDeactivate--;
            }
        }

        private void Txt_Path_DragEnter(object sender, DragEventArgs e) => FromUtil.TextBoxFilePath_DragEnter(sender, e);
        private void Txt_Path_DragDrop(object sender, DragEventArgs e) => FromUtil.TextBoxFilePath_DragDrop(sender, e);
        #region 声音
        private void Btn_TestAudio_Click(object sender, EventArgs e)
        {
            var node = TreeView_Audio.SelectedNode;
            var audioType = (AudioType)Enum.Parse(typeof(AudioType), (string)node.Tag);
            this.GetSystem<IAudioSystem>().PlayAudio(audioType);
        }
        private void Btn_AudioBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Resources.BtnPropertiesForm_OpenFileTitle;
            openFileDialog.Filter = Resources.BtnPropertiesForm_OpenFileFilter;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            TextBox_AudioFilePath.Text = openFileDialog.FileName;
        }
        private void TreeView_Audio_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RadioBtn_Null.CheckedChanged -= RadioBtn_AudioModoCheckedChanged;
            RadioBtn_Default.CheckedChanged -= RadioBtn_AudioModoCheckedChanged;
            RadioBtn_Custom.CheckedChanged -= RadioBtn_AudioModoCheckedChanged;
            TextBox_AudioFilePath.TextChanged -= TextBox_AudioFilePath_TextChanged;
            var node = TreeView_Audio.SelectedNode;
            if (node.Tag == null)
            {
                Txt_AudioChangeTip.Visible = true;
                RadioBtn_Null.Visible = false;
                RadioBtn_Default.Visible = false;
                RadioBtn_Custom.Visible = false;
                Btn_TestAudio.Visible = false;
                TextBox_AudioFilePath.Visible = false;
                Btn_AudioBrowse.Visible = false;
            }
            else
            {
                var audioType = (AudioType)Enum.Parse(typeof(AudioType), (string)node.Tag);
                var audioInfo = AudioConfig(audioType);
                Txt_AudioChangeTip.Visible = false;
                RadioBtn_Null.Visible = true;
                RadioBtn_Default.Visible = true;
                RadioBtn_Custom.Visible = true;
                Btn_TestAudio.Visible = true;
                TextBox_AudioFilePath.Visible = true;
                Btn_AudioBrowse.Visible = true;
                RadioBtn_Null.Checked = audioInfo.Value.mode == 1;
                RadioBtn_Default.Checked = audioInfo.Value.mode == 2;
                RadioBtn_Custom.Checked = audioInfo.Value.mode == 3;
                RadioBtn_Null.CheckedChanged += RadioBtn_AudioModoCheckedChanged;
                RadioBtn_Default.CheckedChanged += RadioBtn_AudioModoCheckedChanged;
                RadioBtn_Custom.CheckedChanged += RadioBtn_AudioModoCheckedChanged;
                TextBox_AudioFilePath.TextChanged += TextBox_AudioFilePath_TextChanged;
            }
        }
        private BindableProperty<AudioInfo> AudioConfig(AudioType audioType)
        {
            var appConfig = this.GetModel<AppConfig>();
            switch (audioType)
            {
                case AudioType.Click: return appConfig.audioClick;
                case AudioType.Group: return appConfig.audioGroup;
                case AudioType.Drop: return appConfig.audioDrop;
                case AudioType.Button: return appConfig.audioButton;
                case AudioType.None:
                default: return null;
            }
        }
        private void RadioBtn_Custom_CheckedChanged(object sender, EventArgs e)
        {
            var senderObj = (RadioButton)sender;
            TextBox_AudioFilePath.Enabled = senderObj.Checked;
            Btn_AudioBrowse.Enabled = senderObj.Checked;
        }
        private void RadioBtn_AudioModoCheckedChanged(object sender, EventArgs e)
        {
            var node = TreeView_Audio.SelectedNode;
            var audioType = (AudioType)Enum.Parse(typeof(AudioType), (string)node.Tag);
            var audioInfo = AudioConfig(audioType);

            var senderObj = (RadioButton)sender;
            int mode = int.Parse((string)senderObj.Tag);
            audioInfo.Value.mode = mode;
        }
        private void TextBox_AudioFilePath_TextChanged(object sender, EventArgs e)
        {
            var node = TreeView_Audio.SelectedNode;
            var audioType = (AudioType)Enum.Parse(typeof(AudioType), (string)node.Tag);
            var audioInfo = AudioConfig(audioType);

            var senderObj = (TextBox)sender;
            audioInfo.Value.file = senderObj.Text;
        }
        #endregion

        #region 字体
        private void TreeView_Font_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = TreeView_Font.SelectedNode;
            if (node.Tag == null)
            {
                Txt_FontChangeTip.Text = Resources.BtnPropertiesForm_ChangeFontTip;
                Btn_FontChange.Visible = false;
                Btn_FontDefault.Visible = false;
            }
            else
            {
                var appConfig = this.GetModel<AppConfig>();
                var bindFontInfo = FontConfig((string)node.Tag);
                FontInfo fontInfo = bindFontInfo.Value.Invalid ? new FontInfo(Font) : bindFontInfo.Value;
                Txt_FontChangeTip.Text = fontInfo.ToString();
                Btn_FontChange.Visible = true;
                Btn_FontDefault.Visible = true;
            }
        }
        private BindableProperty<FontInfo> FontConfig(string mode)
        {
            var appConfig = this.GetModel<AppConfig>();
            switch (mode)
            {
                case "1": return appConfig.fontStates;
                case "2": return appConfig.fontBtnTitile;
                case "3": return appConfig.fontGroupTitle;
                default: return null;
            }
        }
        private void Btn_FontChange_Click(object sender, EventArgs e)
        {
            var node = TreeView_Font.SelectedNode;
            var bindFontInfo = FontConfig((string)node.Tag);
            FontDialog fontDialog = new FontDialog();
            FontInfo fontInfo = bindFontInfo.Value.Invalid ? new FontInfo(Font) : bindFontInfo.Value;
            fontDialog.Font = new Font(fontInfo.familyName, fontInfo.size, GraphicsUnit.Pixel);
            fontDialog.ShowEffects = false;
            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;
            bindFontInfo.Value = new FontInfo(fontDialog.Font);
            TreeView_Font_AfterSelect(sender, null);
        }
        private void Btn_FontDefault_Click(object sender, EventArgs e)
        {
            var node = TreeView_Font.SelectedNode;
            var bindFontInfo = FontConfig((string)node.Tag);
            FontInfo fontInfo = bindFontInfo.Value.Invalid ? new FontInfo(Font) : bindFontInfo.Value;
            if (fontInfo.familyName == Font.FontFamily.Name &&
               fontInfo.size == Font.Size)
                return;
            bindFontInfo.Value = new FontInfo(Font);
            TreeView_Font_AfterSelect(sender, null);
        }
        #endregion

        #region 保护
        private void Event_DispableChangeSetting(bool dcSetting)
        {
            checkBox21.Enabled = dcSetting;
            checkBox22.Enabled = dcSetting;
            if (!dcSetting)
            { 
                checkBox21.Checked = false;
                checkBox22.Checked = false;
            }
        }
        private void Event_Password(string password)
        {
            bool enable = !string.IsNullOrEmpty(password);
            checkBox20.Enabled = enable;
            checkBox23.Enabled = enable;
            if (enable)
            {
                checkBox20.Checked = true;
            }
            else
            {
                checkBox20.Checked = false;
                checkBox23.Checked = false;
            }
        }
        private void Txt_Password_TextChanged(object sender, EventArgs e)
        {
            var appConfig = this.GetModel<AppConfig>();
            if (string.Equals(Txt_Password.Text, Txt_RPassword.Text))
                appConfig.password.Value = Txt_Password.Text;
            else
                appConfig.password.Value = string.Empty;
        }
        #endregion
    }
}
