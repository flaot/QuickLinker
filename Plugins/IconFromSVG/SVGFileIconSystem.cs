using QFramework;
using QuickLinker.Systems;
using Svg;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace IconFromSVG
{
    internal class SVGFileIconSystem : AbstractSystem, IFileIconSystem
    {
        private IFileIconSystem _parent;
        public SVGFileIconSystem(IFileIconSystem parent)
        {
            _parent = parent;
        }
        protected override void OnInit()
        {
        }

        public object GetImage(string filePath, int index)
        {
            if (filePath.StartsWith("<svg"))
            {
                try
                {
                    return SvgToImage(256, 256, filePath);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return _parent.GetImage(filePath, index);
        }

        /// <summary>
        /// 把Svg文件按指定宽度和高度转为Image对象
        /// </summary>
        /// <param name="svgFile">Svg文件完整路径</param>
        /// <param name="width">转换后的图像宽度</param>
        /// <param name="height">转换后的图像高度</param>
        /// <returns>返回转换后的Image对象</returns>
        private Bitmap SvgToImage(int width, int height, string svgCode)
        {
            Bitmap imgResult = null;
            using (Stream svgMs = new MemoryStream(Encoding.Default.GetBytes(svgCode)))
            {
                SvgDocument sdoc = SvgDocument.Open<SvgDocument>(svgMs);

                //解析Svg文件中的viewBox值
                string xml = sdoc.ToString();
                string beginStr = "viewBox=";
                string endStr = "\" ";
                int begin = xml.IndexOf(beginStr);
                if (begin > 0)
                {
                    begin = begin + beginStr.Length;
                    int end = xml.IndexOf(endStr, begin);
                    string viewBox = xml.Substring(begin, end - begin);
                    viewBox = viewBox.Replace("\"", String.Empty).Replace("'", String.Empty);
                    if (!String.IsNullOrEmpty(viewBox))
                    {
                        string[] vbs = viewBox.Split(new char[] { ' ' });
                        if (vbs.Length == 4)
                        {
                            float vbx = 0.0f;
                            float vby = 0.0f;
                            float vbw = 0.0f;
                            float vbh = 0.0f;
                            float.TryParse(vbs[0], out vbx);
                            float.TryParse(vbs[1], out vby);
                            float.TryParse(vbs[2], out vbw);
                            float.TryParse(vbs[3], out vbh);
                            sdoc.ViewBox = new SvgViewBox(vbx, vby, vbw, vbh);
                        }
                    }
                }

                sdoc.Width = width;
                sdoc.Height = height;
                Bitmap bitmap = sdoc.Draw();
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);                    //把svg按照指定宽度和高度转为png后放入内存流中
                    imgResult = new Bitmap(ms);
                    //bitmap.Save("e:\\test.png", System.Drawing.Imaging.ImageFormat.Png);      //保存png图片至磁盘
                }
            }

            return imgResult;
        }
    }
}
