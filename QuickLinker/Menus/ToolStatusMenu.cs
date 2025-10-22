using QFramework;
using QuickLinker.Model;
using QuickLinker.Plugin.Menu;
using QuickLinker.Plugin.Menu.Attribute;

namespace QuickLinker.Menus
{
    internal class ToolStatusMenu : IController
    {
        private MainForm mainForm;
        public IArchitecture GetArchitecture() => mainForm.GetArchitecture();
        public ToolStatusMenu(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        [ToolStatusMenuItem("时间(&T)", 108)]
        private void ToolStatusMenu_Time()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.Time;
        }
        [ToolStatusMenuItem("日期(&D)", 109)]
        private void ToolStatusMenu_Date()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.Date;
        }
        [ToolStatusMenuItem("时间与日期(&A)", 110)]
        private void ToolStatusMenu_DateTime()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.DateTime;
        }
        [ToolStatusMenuItem("无(&N)", 111)]
        private void ToolStatusMenu_None()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.None;
        }
    }
}
