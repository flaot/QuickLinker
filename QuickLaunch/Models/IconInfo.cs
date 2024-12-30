using QFramework;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Utils;
using System;
using System.Drawing;

namespace QuickLinker.QuickLaunch.Models
{
    [Serializable]
    public class IconInfo : AbstractModel
    {
        private string path; //路径
        private string name; //文件名
        private int count = 0; //打开次数
        [field: NonSerialized]
        private BindableProperty<Bitmap> bitmapImage; //位图
        private byte[] imageByteArr; //图片 byte数组
        private string content; //显示信息
        private bool adminStartUp = false; //始终管理员方式启动  默认否
        private byte[] defaultImage; //默认图标
        private string startArg; //启动参数
        private string lnkPath;

        private string relativePath; //相对路径

        private OpenType iconType = OpenType.OTHER;

        protected override void OnInit()
        {
            bitmapImage = new BindableProperty<Bitmap>();
            //bitmapImage.Register(SaveAppData);
        }
        public string RelativePath_NoWrite
        {
            get
            {
                return relativePath;
            }
            set
            {
                relativePath = value;
            }
        }

        public string RelativePath
        {
            get
            {
                return relativePath;
            }
            set
            {
                relativePath = value;
                //OnPropertyChanged("RelativePath");
            }
        }

        public string LnkPath_NoWrite
        {
            get
            {
                return lnkPath;
            }
            set
            {
                lnkPath = value;
            }
        }
        public string LnkPath
        {
            get
            {
                return lnkPath;
            }
            set
            {
                lnkPath = value;
                //OnPropertyChanged("LnkPath");
            }
        }

        public string StartArg
        {
            get
            {
                return startArg;
            }
            set
            {
                startArg = value;
                //OnPropertyChanged("StartArg");
            }
        }
        public string StartArg_NoWrite
        {
            get
            {
                return startArg;
            }
            set
            {
                startArg = value;
            }
        }

        public OpenType IconType
        {
            get
            {
                if (iconType == 0) return OpenType.OTHER;
                return iconType;
            }
            set
            {
                iconType = value;
                //OnPropertyChanged("IconType");
            }
        }

        public byte[] DefaultImage
        {
            get
            {
                return defaultImage;
            }
            set
            {
                defaultImage = value;
                //OnPropertyChanged("DefaultImage");
            }
        }

        public byte[] DefaultImage_NoWrite
        {
            get
            {
                return defaultImage;
            }
            set
            {
                defaultImage = value;
            }
        }

        public bool AdminStartUp
        {
            get
            {
                return adminStartUp;
            }
            set
            {
                adminStartUp = value;
                //OnPropertyChanged("AdminStartUp");
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                if (string.IsNullOrWhiteSpace(Path))
                {
                    Content = Name + "\n使用次数: " + Count;
                }
                else
                {
                    Content = Path + "\n" + Name + "\n使用次数: " + Count;
                }
                //OnPropertyChanged("Count");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (string.IsNullOrWhiteSpace(Path))
                {
                    Content = Name + "\n使用次数: " + Count;
                }
                else
                {
                    Content = Path + "\n" + Name + "\n使用次数: " + Count;
                }
                //OnPropertyChanged("Name");
            }
        }

        public string Name_NoWrite
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (string.IsNullOrWhiteSpace(Path))
                {
                    content = Name + "\n使用次数: " + Count;
                }
                else
                {
                    content = Path + "\n" + Name + "\n使用次数: " + Count;
                }
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                if (string.IsNullOrWhiteSpace(Path))
                {
                    Content = Name + "\n使用次数: " + Count;
                }
                else
                {
                    Content = Path + "\n" + Name + "\n使用次数: " + Count;
                }
                //OnPropertyChanged("Path");
            }
        }

        public string Path_NoWrite
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                if (string.IsNullOrWhiteSpace(Path))
                {
                    content = Name + "\n使用次数: " + Count;
                }
                else
                {
                    content = Path + "\n" + Name + "\n使用次数: " + Count;
                }
            }
        }


        public Bitmap BitmapImage
        {
            get
            {

                return ImageUtil.ByteArrToImage(ImageByteArr);
            }
            set
            {
                bitmapImage.Value = value;
                ImageByteArr = ImageUtil.BitmapImageToByte(bitmapImage.Value);
                //OnPropertyChanged("BitmapImage");
            }
        }

        public Bitmap BitmapImage_NoWrite
        {
            get
            {
                return ImageUtil.ByteArrToImage(ImageByteArr_NoWrite);
            }
            set
            {
                bitmapImage.Value = value;
                ImageByteArr_NoWrite = ImageUtil.BitmapImageToByte(bitmapImage.Value);
                //OnPropertyChanged("BitmapImage_NoWrite");
            }
        }


        public byte[] ImageByteArr
        {
            get
            {

                return imageByteArr;
            }
            set
            {
                imageByteArr = value;
                //OnPropertyChanged("ImageByteArr");
            }
        }

        public byte[] ImageByteArr_NoWrite
        {
            get
            {

                return imageByteArr;
            }
            set
            {
                imageByteArr = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                //OnPropertyChanged("Content");
            }
        }


        public string Content_NoWrite
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
    }
}
