using QFramework;
using QuickLinker.Model;
using QuickLinker.QuickLaunch.Command;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker.Utils
{
    public static class GroupUtil
    {
        public static void AddPage(this MainForm mainForm, string pageName)
        {
            var config = mainForm.GetModel<AppConfig>();
            var tabPageTemp = new TabPage(pageName);
            mainForm.tabControl1.SuspendLayout();
            tabPageTemp.SuspendLayout();
            mainForm.SuspendLayout();
            mainForm.tabControl1.Controls.Add(tabPageTemp);
            int pageStart = (mainForm.tabControl1.TabCount - 1) * config.gridColumn.Value * config.gridRow.Value;
            for (int row = 0; row < config.gridRow.Value; row++)
            {
                for (int column = 0; column < config.gridColumn.Value; column++)
                {
                    var defalutIndex = row * config.gridColumn.Value + column + pageStart;
                    var tPanel = new TPanel(defalutIndex);
                    tPanel.Size = new Size(config.gridSize.Value, config.gridSize.Value);
                    var columnSize = column * config.gridSize.Value;
                    var rowSize = row * config.gridSize.Value;
                    columnSize += config.grid.Value * column;
                    rowSize += config.grid.Value * row;
                    tPanel.Location = new Point(columnSize, rowSize);
                    tabPageTemp.Controls.Add(tPanel);
                }
            }
            mainForm.tabControl1.ResumeLayout(false);
            tabPageTemp.ResumeLayout(false);
            mainForm.ResumeLayout();
        }
        public static void RemovePage(this MainForm mainForm, int pageIndex)
        {
            TabPage findPage = null;
            int i = 0;
            foreach (TabPage page in mainForm.tabControl1.TabPages)
            {
                if (i == pageIndex)
                {
                    findPage = page;
                    break;
                }
                ++i;
            }
            if (findPage == null)
                return;
            mainForm.tabControl1.TabPages.Remove(findPage);
            //移除entity
            var config = mainForm.GetModel<AppConfig>();
            int curStartIndex = pageIndex * config.gridRow.Value * config.gridColumn.Value;
            for (int index = 0; index < config.gridRow.Value * config.gridColumn.Value; index++)
            {
                int rmIndex = curStartIndex + index;
                mainForm.SendCommand(new QuickEntityRemoveCommand() { index = rmIndex });
            }
            int toIndex = (mainForm.tabControl1.TabPages.Count + 1) * config.gridRow.Value * config.gridColumn.Value;
            for (int index = 0; index < config.gridRow.Value * config.gridColumn.Value; index++)
            {
                int rmIndex = curStartIndex + index;
                mainForm.SendCommand(new QuickEntityOptCommand() { optType = QuickEntityOptCommand.OptType.Align, fromIndex = rmIndex, index = toIndex });
            }
        }
        public static void SwitchPage(this IController mainForm, int leftIndex, int rightIndex)
        {
            var config = mainForm.GetModel<AppConfig>();
            //交换group文本
            var groupArray = config.groupArray;
            if (!string.Equals(groupArray.Value[rightIndex], groupArray.Value[leftIndex]))
            {
                string[] tempArray = new string[groupArray.Value.Length];
                Array.Copy(groupArray.Value, tempArray, tempArray.Length);
                string tempText = tempArray[leftIndex];
                tempArray[leftIndex] = tempArray[rightIndex];
                tempArray[rightIndex] = tempText;
                config.groupArray.Value = tempArray;
            }
            //交换entity位置
            int curStartIndex = leftIndex * config.gridRow.Value * config.gridColumn.Value;
            int swapStartIndex = rightIndex * config.gridRow.Value * config.gridColumn.Value;
            for (int i = 0; i < config.gridRow.Value * config.gridColumn.Value; i++)
            {
                int left = curStartIndex + i;
                int right = swapStartIndex + i;
                mainForm.SendCommand(new QuickEntitySwitchCommand() { srcIndex = left, desIndex = right });
            }
        }
    }
}
