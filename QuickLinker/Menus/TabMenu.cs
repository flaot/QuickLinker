using QFramework;
using QuickLinker.Model;
using QuickLinker.Utils;
using System;
using System.Linq;
using System.Windows.Forms;
using QuickLinker.Plugin.Menu;
using QuickLinker.Plugin.Menu.Attribute;

namespace QuickLinker.Menus
{
    internal class TabMenu: IController
    {
        private MainForm mainForm;
        public IArchitecture GetArchitecture() => mainForm.GetArchitecture();
        public TabMenu(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        [TabMenuItem("左移标签(&L)", 100)]
        private void TabMenuItem_Left_Click()
        {
            int curIndex = mainForm.tabControl1.SelectedIndex;
            int swapIndex = curIndex - 1;
            mainForm.SwitchPage(curIndex, swapIndex);
            mainForm.tabControl1.SelectedIndex = swapIndex;
        }
        [TabMenuItem("右移标签(&R)", 101)]
        private void TabMenuItem_Right_Click()
        {
            int curIndex = mainForm.tabControl1.SelectedIndex;
            int swapIndex = curIndex + 1;
            mainForm.SwitchPage(curIndex, swapIndex);
            mainForm.tabControl1.SelectedIndex = swapIndex;
        }
        [TabMenuItem("重命名(&E)...", 102)]
        private void TabMenuItem_Rename_Click()
        {
            string pageText = mainForm.tabControl1.TabPages[mainForm.tabControl1.SelectedIndex].Text;
            string groupName = GroupNameForm.Show(pageText);
            if (string.IsNullOrEmpty(groupName))
                return;

            var config = this.GetModel<AppConfig>();
            var groupArray = config.groupArray;
            string[] tempArray = new string[groupArray.Value.Length];
            Array.Copy(groupArray.Value, tempArray, tempArray.Length);
            tempArray[mainForm.tabControl1.SelectedIndex] = groupName;
            config.groupArray.Value = tempArray;
        }
        [TabMenuItem("删除(&D)", 103)]
        private void TabMenuItem_Delete_Click()
        {
            int curIndex = mainForm.tabControl1.SelectedIndex;
            var config = this.GetModel<AppConfig>();
            var groupArray = config.groupArray.Value.ToList();
            groupArray.RemoveAt(curIndex);
            config.groupArray.Value = groupArray.ToArray();
        }
        [TabMenuItem("外观/标准(&N)", 200)]
        private void TabMenuItem_Stand_Click()
        {
            var config = this.GetModel<AppConfig>();
            config.tabAppearance.Value = TabAppearance.Normal;
        }
        [TabMenuItem("外观/按钮(&B)", 201)]
        private void TabMenuItem_Button_Click()
        {
            var config = this.GetModel<AppConfig>();
            config.tabAppearance.Value = TabAppearance.Buttons;
        }
        [TabMenuItem("外观/平面按钮(&F)", 202)]
        private void TabMenuItem_Flot_Click()
        {
            var config = this.GetModel<AppConfig>();
            config.tabAppearance.Value = TabAppearance.FlatButtons;
        }

    }
}
