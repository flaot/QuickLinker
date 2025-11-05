using QFramework;
using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace QuickLinker.Systems
{
    internal class FileIconSystem : AbstractSystem, IFileIconSystem
    {
        protected override void OnInit()
        {
        }
        public object GetImage(string filePath, int index)
        {
            Icon ico = null;
            IntPtr ip = IntPtr.Zero;
            //选中文件中的图标总数
            var iconTotalCount = Win32API.PrivateExtractIcons(filePath, 0, 0, 0, null, null, 0, 0);
            if (iconTotalCount > 0)
            {
                //用于接收获取到的图标指针
                IntPtr[] hIcons = new IntPtr[iconTotalCount];
                //对应的图标id
                int[] ids = new int[iconTotalCount];
                //成功获取到的图标个数
                var successCount = Win32API.PrivateExtractIcons(filePath, 0, 256, 256, hIcons, ids, iconTotalCount, 0);
                if (successCount > 0)
                {
                    int selIndex = Math.Clamp(index, 0, successCount - 1);
                    ip = hIcons[selIndex];
                    ico = Icon.FromHandle(ip);
                    for (int i = 0; i < successCount; i++)
                    {
                        if (i != selIndex)
                            Win32API.DestroyIcon(hIcons[i]);
                    }
                }
            }
            if (ico == null)
                ico = GetFileIcon(filePath);
            if (ico == null)
                return this.GetUtility<IImageUtil>().Base64ToBitmapImage(Constants.DEFAULT_ICON_NULL_BASE64);
            Bitmap bmp = ico.ToBitmap();
            MemoryStream strm = new MemoryStream();

            ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/png");
            Encoder myEncoder = Encoder.Quality;
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp.Save(strm, myImageCodecInfo, myEncoderParameters);

            strm.Seek(0, SeekOrigin.Begin);
            Bitmap bmpImage = new Bitmap(strm);
            if (ip != IntPtr.Zero)
            {
                Win32API.DestroyIcon(ip);
            }
            Win32API.DeleteObject(bmp.GetHbitmap());
            return bmpImage;
        }

        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 根据文件扩展名得到系统扩展名的图标
        /// </summary>
        /// <param name="fileName">文件名(如：win.rar;setup.exe;temp.txt)</param>
        /// <param name="largeIcon">图标的大小</param>
        /// <returns></returns>
        private Icon GetFileIcon(string pszFile)
        {
            Win32API.SHFILEINFO sfi = new Win32API.SHFILEINFO();
            IntPtr IconIntPtr = Win32API.SHGetFileInfo(pszFile
                 , 256
                 , ref sfi
                 , (uint)Marshal.SizeOf(sfi)
                 , (uint)(Win32API.SHGFI.Icon | Win32API.SHGFI.LargeIcon | Win32API.SHGFI.OverlayIndex));
            if (IconIntPtr.Equals(IntPtr.Zero))
                return null;
            return Icon.FromHandle(sfi.hIcon);
        }
    }
}
