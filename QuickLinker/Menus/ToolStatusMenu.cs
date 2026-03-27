using QFramework;
using QuickLinker.Model;
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

        [ToolStatusMenuItem(MenuKey.ToolStatusMenu_Time, 100)]
        private void ToolStatusMenu_Time()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.Time;
        }
        [ToolStatusMenuItem(MenuKey.ToolStatusMenu_Date, 101)]
        private void ToolStatusMenu_Date()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.Date;
        }
        [ToolStatusMenuItem(MenuKey.ToolStatusMenu_DateTime, 102)]
        private void ToolStatusMenu_DateTime()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.DateTime;
        }
        [ToolStatusMenuItem(MenuKey.ToolStatusMenu_None, 103)]
        private void ToolStatusMenu_None()
        {
            var config = this.GetModel<AppConfig>();
            config.dateTimeType.Value = DateTimeType.None;
        }
    }
}
