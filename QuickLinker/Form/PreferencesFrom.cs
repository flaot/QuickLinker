using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using QuickLinker.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WK.Libraries.HotkeyListenerNS;

namespace QuickLinker
{
    public partial class PreferencesFrom : Form, IController
    {
        private List<IUnRegister> _unRegisters = new List<IUnRegister>();
        private readonly HotkeySelector _hotkeySelector = new();

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
            RegisterBool(appConfig.startbutton, checkBox10);
            RegisterCombox(appConfig.tabAppearance, comboBox4);
            RegisterBool(appConfig.disableAffinity, checkBox18);
            var hotKeySys = this.GetSystem<HotKeyManager>();
            hotKeySys.HotKeyListener?.RemoveAll();
            _hotkeySelector.EmptyHotkeyText = string.Empty;
            if (!string.IsNullOrEmpty(appConfig.actionHotKey.Value))
                _hotkeySelector.Enable(textBox1, new Hotkey(appConfig.actionHotKey.Value));
            else
                _hotkeySelector.Enable(textBox1);
            textBox1.KeyUp += TextBox1_KeyUp;
            textBox1.KeyDown += TextBox1_KeyUp;

            //外观
            RegisterCombox(appConfig.titleStyle, comboBox1);
            RegisterBool(appConfig.showToolTip, checkBox11);
            RegisterBool(appConfig.showStateTip, checkBox12);
            RegisterBool(appConfig.showButtonTip, checkBox13);
            RegisterCombox(appConfig.appHideType, comboBox2);
            RegisterCombox(appConfig.dateTimeType, comboBox3);
            RegisterBool(appConfig.showLongFormatTime, checkBox14);
            RegisterBool(appConfig.showLongFormatDate, checkBox15);
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
        }
        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var appConfig = this.GetModel<AppConfig>();
            appConfig.actionHotKey.Value = textBox1.Text;
            _hotkeySelector?.Disable(textBox1);
            _hotkeySelector?.Dispose();
            if (_unRegisters != null)
            {
                _unRegisters.ForEach(item => item.UnRegister());
                _unRegisters = null;
            }
            var hotKeySys = this.GetSystem<HotKeyManager>();
            hotKeySys.InitializeQuickActionsHotKeys();
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                _hotkeySelector.Clear(sender as Control);
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
    }
}
