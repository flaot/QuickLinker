using QFramework;
using QuickLinker.QuickLaunch.Constant;
using System;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace QuickLinker.QuickLaunch.Models
{
    public class Entity : ICloneable
    {
        /// <summary> 下标(位置) </summary>
        public int index;
        /// <summary> 标识 </summary>
        public string[] flags = Array.Empty<string>();
        /// <summary> 始终管理员方式启动 </summary>
        public bool adminStartUp = false;
        /// <summary> 跟随启动则触发运行 </summary>
        public bool autoRun = false;

        /// <summary> 打开方式 </summary>
        public OpenType iconType = OpenType.OTHER;
        [JsonInclude , XmlAttribute]
        private string path = string.Empty;
        /// <summary> 路径 </summary>
        [JsonIgnore, XmlIgnore]
        public string Path { get => path; set => path = value.Replace(System.IO.Path.DirectorySeparatorChar, '/'); }
        [JsonInclude, XmlAttribute]
        private string relativePath = string.Empty;
        /// <summary> 相对路径 </summary>
        [JsonIgnore, XmlIgnore]
        public string RelativePath { get => relativePath; set => relativePath = value.Replace(System.IO.Path.DirectorySeparatorChar, '/'); }
        /// <summary> 启动参数 </summary>
        public string startArg = string.Empty;
        /// <summary> 工作目录 </summary>
        public string workFolder = string.Empty;
        /// <summary> 描述 </summary>
        public string desc = string.Empty;
        /// <summary> 可以再次解析 </summary>
        public bool canParse;
        /// <summary> 窗口样式 </summary>
        public WindowStyle windowStyle = WindowStyle.Normal;
        /// <summary> 优先级 </summary>
        public PriorityClass priorityClass = PriorityClass.Normal;

        /// <summary> 图标 </summary>
        [JsonIgnore, XmlIgnore]
        public Bitmap bitmapImage;
        /// <summary> 图标 byte数组(存) </summary>
        public byte[] imageByteArr;
        [JsonInclude, XmlAttribute]
        private string imagePath = string.Empty;
        /// <summary> 图标的来源路径 </summary>
        [JsonIgnore, XmlIgnore]
        public string ImagePath { get => imagePath; set => imagePath = value.Replace(System.IO.Path.DirectorySeparatorChar, '/'); }
        /// <summary> 图标的下标 </summary>
        public int imageIndex;

        /// <summary> 需要保存 </summary>
        [JsonIgnore, XmlIgnore]
        public BindableProperty<bool> needSave = new BindableProperty<bool>();

        public object Clone()
        {
            var newObj = (Entity)MemberwiseClone();
            newObj.needSave = new BindableProperty<bool>();
            return newObj;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", desc, index);
        }
    }

    /// <summary> 创建的进程的窗口样式 </summary>
    public enum WindowStyle
    {
        Normal = 0,
        Minimized = 1,
        Maximized = 2,
        Hidden = 3,
    }
    /// <summary> 进程优先级 </summary>
    public enum PriorityClass
    {
        /// <summary> 实时 </summary>
        RealTime,
        /// <summary> 高 </summary>
        High,
        /// <summary> 高于正常 </summary>
        AboveNormal,
        /// <summary> 正常 </summary>
        Normal,
        /// <summary> 低于正常 </summary>
        BelowNormal,
        /// <summary> 低 </summary>
        Idle,
    }
}
