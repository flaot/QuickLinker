using QFramework;
using QuickLinker.Systems;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace IconFromImage
{
    internal class ImageFileIconSystem : AbstractSystem, IFileIconSystem
    {
        private IFileIconSystem _parent;
        private string[] _suffixArr;
        public ImageFileIconSystem(IFileIconSystem parent)
        {
            _parent = parent;
        }
        protected override void OnInit()
        {
            string suffixs = "bmp,jpg,jpeg,png,tif,gif,pcx,tga,exif,fpx,svg,psd,cdr,pcd,dxf,ufo,eps,ai,raw,WMF,webp,avif";
            _suffixArr = suffixs.Split(',');
        }

        public object GetImage(string filePath, int index)
        {
            object result = null;
            if (IsImage(filePath))
            { 
                try
                {
                    result = GetThumbnailByFile(filePath, 256, 256);
                }
                catch
                {
                    result = null;
                }
            }
            if (result == null)
            { 
                result = _parent.GetImage(filePath, index);
            }
            return result;
        }

        public bool IsImage(string path)
        {
            string ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
                return false;
            string strExt = ext.Substring(1);
            for (int i = 0; i < _suffixArr.Length; i++)
            {
                string suffix = _suffixArr[i];
                if (suffix.Equals(strExt, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        public Bitmap GetThumbnailByFile(string filePath, int tWidth, int tHeight)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
                return null;
            if (file.Length <= 0)
                return null;
            if (Path.GetExtension(filePath).Contains("psd"))
                return null;
            Image img = Image.FromFile(filePath);
            if (img.Width <= tWidth && img.Height <= tHeight)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    return new Bitmap(fs);
                }
            }
            else
            {
                Bitmap loBMP = new Bitmap(filePath);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth;
                int lnNewHeight;
                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)tWidth / loBMP.Width;
                    lnNewWidth = tWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)tHeight / loBMP.Height;
                    lnNewHeight = tHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                Bitmap bmpOut = new Bitmap(loBMP, lnNewWidth, lnNewHeight);
                using (Graphics g = Graphics.FromImage(bmpOut))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                    g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                }
                loBMP.Dispose();
                using (MemoryStream ms = new MemoryStream())
                {
                    bmpOut.Save(ms, loFormat);
                    ms.Position = 0;
                    return new Bitmap(ms);
                }
            }
        }
    }
}
