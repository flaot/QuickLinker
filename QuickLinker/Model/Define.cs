using System.Windows.Forms;

namespace QuickLinker.Model
{
    public enum DateTimeType
    {
        Time = 0,
        Date = 1,
        DateTime = 2,
        None = 3,
    }
    public enum AppHideType
    {
        None = 0,
        /// <summary> 启动后最小化 </summary>
        AutoMinimize = 1,
        /// <summary> 不活动时最小化 </summary>
        LoseFocusMinimize = 2,
    }
    /// <summary> 标题栏外观 </summary>
    public enum TitleStyle
    {
        /// <summary> 标准 </summary>
        Stand,
        /// <summary> 最小 </summary>
        Min,
        /// <summary> 无 </summary>
        None,
    }

    /// <summary> 刷新状态栏文本 </summary>
    public struct RefreshStateTextEvent
    {
        public string text;
        public RefreshStateTextEvent(string txt) => text = txt;
    }
    /// <summary> 显示tooltip </summary>
    public struct ShowToolTipEvent
    {
        public Control control;
        public string text;
        public ShowToolTipEvent(Control control, string txt)
        {
            this.control = control;
            text = txt;
        }
    }
    /// <summary> 鼠标左键单击入口 </summary>
    public struct ClickTPanelEvent
    {
    }
    /// <summary> 鼠标右键单击入口 </summary>
    public struct ClickMenuTPanelEvent
    {
    }
    /// <summary> 关闭软件 </summary>
    public struct CloseSoftwareEvent { }

    /// <summary> 无配置情况下首次启动 </summary>
    public struct NoSettingStratEvent { }
}
