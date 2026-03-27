using QFramework;
using QuickLinker.Systems;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuickLinker.Model
{
    public class AppConfig : AbstractModel
    {
        /// <summary> 首次创建配置标识 </summary>
        public BindableProperty<bool> firstCreate = new BindableProperty<bool>(true);
        /// <summary> 置顶 </summary>
        public BindableProperty<bool> topWindow = new BindableProperty<bool>(false);
        /// <summary> 解析拖放的快捷方式 </summary>
        public BindableProperty<bool> analyzeDrapLink = new BindableProperty<bool>(true);
        /// <summary> 禁用关闭按钮:关闭按钮无效化 </summary>
        public BindableProperty<bool> disableClose = new BindableProperty<bool>();
        /// <summary> 禁止最小化按钮:最小化按钮无效化 </summary>
        public BindableProperty<bool> disableMinClose = new BindableProperty<bool>(true);
        /// <summary> 禁止最大化按钮:最大化按钮无效化 </summary>
        public BindableProperty<bool> disableMaxClose = new BindableProperty<bool>(true);
        /// <summary> 禁止移动:不可移动窗口 </summary>
        public BindableProperty<bool> disableMove = new BindableProperty<bool>();
        /// <summary> 忽略空白按钮上的点击 </summary>
        public BindableProperty<bool> ignoreZeroButton = new BindableProperty<bool>(true);
        /// <summary> 防止重复运行 </summary>
        public BindableProperty<bool> blockRepeatRun = new BindableProperty<bool>(true);
        /// <summary> 在截图或录屏中不可见，对于远程桌面也有效;这个常见于视频录制软件，不希望捕获录制软件(WIN10_2004_OR_NEW) </summary>
        public BindableProperty<bool> disableAffinity = new BindableProperty<bool>();

        /// <summary> 显示在鼠标位置 </summary>
        public BindableProperty<bool> showMouse = new BindableProperty<bool>(true);
        /// <summary> 开机启动 </summary>
        public BindableProperty<bool> launch = new BindableProperty<bool>();
        /// <summary> 注册URI </summary>
        public BindableProperty<bool> registerURI = new BindableProperty<bool>();
        /// <summary> 处理启动按钮 </summary>
        public BindableProperty<bool> startbutton = new BindableProperty<bool>();

        /// <summary> 标题栏外观 1-标准 2-最小 0-无 </summary>
        public BindableProperty<TitleStyle> titleStyle = new BindableProperty<TitleStyle>();
        /// <summary> 唤醒快捷键 </summary>
        public BindableProperty<string> actionHotKey = new BindableProperty<string>(string.Empty);



        /// <summary> 行 </summary>
        public BindableProperty<int> gridRow = new BindableProperty<int>(5);
        /// <summary> 列 </summary>
        public BindableProperty<int> gridColumn = new BindableProperty<int>(10);
        /// <summary> 组 </summary>
        public BindableProperty<int> gridGroup = new BindableProperty<int>(1);
        /// <summary> 格子大小 </summary>
        public BindableProperty<int> gridSize = new BindableProperty<int>(32);
        /// <summary> 格子之间的间距 </summary>
        public BindableProperty<int> grid = new BindableProperty<int>(0);
        /// <summary> 平面按钮 </summary>
        public BindableProperty<bool> flatButton = new BindableProperty<bool>();


        /// <summary> 作为工具提示 </summary>
        public BindableProperty<bool> showToolTip = new BindableProperty<bool>();
        /// <summary> 在状态栏提示 </summary>
        public BindableProperty<bool> showStateTip = new BindableProperty<bool>(true);
        /// <summary> 在按钮表面显示 </summary>
        public BindableProperty<bool> showButtonTip = new BindableProperty<bool>(false);
        /// <summary> 显示时间 </summary>
        public BindableProperty<DateTimeType> dateTimeType = new BindableProperty<DateTimeType>(DateTimeType.DateTime);
        /// <summary> 显示长格式时间 </summary>
        public BindableProperty<bool> useLongTime = new BindableProperty<bool>(true);
        /// <summary> 显示长格式日期 </summary>
        public BindableProperty<bool> useLongDate = new BindableProperty<bool>();
        /// <summary> 显示在任务栏而不是托盘 </summary>
        public BindableProperty<bool> showInTray = new BindableProperty<bool>();
        /// <summary> 窗口透明度 </summary>
        public BindableProperty<int> windowAlpha = new BindableProperty<int>(100);
        /// <summary> 索引标签 </summary>
        public BindableProperty<TabAppearance> tabAppearance = new BindableProperty<TabAppearance>(TabAppearance.Normal);
        /// <summary> 组描述 </summary>
        public BindableProperty<string[]> groupArray = new BindableProperty<string[]>(new string[] { "flaot", "flaot1" });
        /// <summary> 自动最小化或上卷 </summary>
        public BindableProperty<AppHideType> appHideType = new BindableProperty<AppHideType>();

        public BindableProperty<AudioInfo> audioClick = new BindableProperty<AudioInfo>(new AudioInfo());
        public BindableProperty<AudioInfo> audioButton = new BindableProperty<AudioInfo>(new AudioInfo());
        public BindableProperty<AudioInfo> audioDrop = new BindableProperty<AudioInfo>(new AudioInfo());
        public BindableProperty<AudioInfo> audioGroup = new BindableProperty<AudioInfo>(new AudioInfo());

        public BindableProperty<FontInfo> fontStates = new BindableProperty<FontInfo>(new FontInfo());
        public BindableProperty<FontInfo> fontBtnTitile = new BindableProperty<FontInfo>(new FontInfo());
        public BindableProperty<FontInfo> fontGroupTitle = new BindableProperty<FontInfo>(new FontInfo());

        /// <summary> 密码 </summary>
        public BindableProperty<string> password = new BindableProperty<string>(string.Empty);
        /// <summary> 防止更改配置 </summary>
        public BindableProperty<bool> disableChangeSetting = new BindableProperty<bool>();
        /// <summary> 保留配置菜单项目 </summary>
        public BindableProperty<bool> persistConfigureMenu = new BindableProperty<bool>();
        /// <summary> 保留拖放支持 </summary>
        public BindableProperty<bool> persistDragMenu = new BindableProperty<bool>();
        /// <summary> 防止关闭本软件 </summary>
        public BindableProperty<bool> disableCloseSoftware = new BindableProperty<bool>();

        [JsonIgnore, XmlIgnore]
        public EasyEvent TirggerSaveEvent = new EasyEvent();
        protected override void OnInit()
        {
            foreach (var fieldInfo in typeof(AppConfig).GetFields())
            {
                if (!fieldInfo.FieldType.IsGenericType)
                    continue;
                if (fieldInfo.FieldType.GetGenericTypeDefinition() != typeof(BindableProperty<>))
                    continue;
                var easyEvent = (IEasyEvent)fieldInfo.GetValue(this);
                easyEvent.Register(Event_TirggerSave);
            }
        }
        private void Event_TirggerSave()
        {
            TirggerSaveEvent.Trigger();
        }
    }
    public class FontInfo
    {
        public string familyName;
        public float size;

        public FontInfo() { }
        public FontInfo(Font font)
        {
            this.familyName = font.FontFamily.Name;
            this.size = font.Size;
        }

        public override string ToString() =>
            string.Format("{0}, {1}", familyName == null ? string.Empty : familyName, size);
        public bool Invalid => string.IsNullOrWhiteSpace(familyName) || size <= 0;
    }
}
