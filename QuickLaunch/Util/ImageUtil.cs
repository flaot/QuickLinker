using QFramework;
using QuickLinker.Systems;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace QuickLinker.QuickLaunch.Utils
{
    public interface IImageUtil : IUtility
    {
        /// <summary> 图片base64 转 BitmapImage </summary>
        Bitmap Base64ToBitmapImage(string base64);
        /// <summary> 图片文件转base64 </summary>
        string FileImageToBase64(string Imagefilename, ImageFormat format);
        /// <summary> 图片数组转 BitmapImage </summary>
        Bitmap ByteArrToImage(byte[] array);
        /// <summary> BitmapImage 转数组 </summary>
        byte[] BitmapImageToByte(Bitmap bi);
        /// <summary> 缩放图片 </summary>
        Bitmap ScaleBitmap(Bitmap originalImage, int newWidth, int newHeight);
    }
    /// <summary>
    /// 文件的icon获取
    /// </summary>
    public class ImageUtil : IImageUtil
    {
        /// <summary>
        /// 图片数组转 BitmapImage
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public Bitmap ByteArrToImage(byte[] array)
        {
            if (array == null) return null;
            using (var ms = new MemoryStream(array))
            {
                return new Bitmap(ms);
            }
        }

        /// <summary>
        /// BitmapImage 转数组
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public byte[] BitmapImageToByte(Bitmap bi)
        {
            if (bi == null) return null;
            using (Bitmap bitmap = new Bitmap(bi))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, bi.RawFormat);
                    return stream.GetBuffer();
                }
            }
        }

        /// <summary>
        /// 图片base64 转 BitmapImage
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public Bitmap Base64ToBitmapImage(string base64)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64);
            return ByteArrToImage(byteBuffer);
        }

        /// <summary>
        /// 获取文件 icon
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static Bitmap GetBitmapIconByPath(string filePath, int index = 0)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;
            if (filePath.Contains("%windir%"))
            {
                filePath = filePath.Replace("%windir%", Environment.GetEnvironmentVariable("windir"));
            }
            var fileIconSystem = AbstractPlugin.Architecture.GetSystem<IFileIconSystem>();
            return fileIconSystem.GetImage(filePath, index) as Bitmap;
        }

        
        /// <summary>
        /// 缩放图片
        /// </summary>
        public Bitmap ScaleBitmap(Bitmap originalImage, int newWidth, int newHeight)
        {
            Bitmap bmpOut = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(bmpOut))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }
            return bmpOut;
        }

        /// <summary>
        /// 图片文件转base64
        /// </summary>
        /// <param name="Imagefilename"></param>
        /// <returns></returns>
        public string FileImageToBase64(string Imagefilename, ImageFormat format)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, format);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception e)
            {
                LogUtil.WriteErrorLog(e, "图片文件转base64失败!Imagefilename=" + Imagefilename + ",ImageFormat=" + format);
                return null;
            }
        }
    }
}
