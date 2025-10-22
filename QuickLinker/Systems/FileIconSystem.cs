using QFramework;
using QuickLinker.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace QuickLinker.Systems
{
    internal class FileIconSystem : AbstractSystem, IFileIconSystem
    {
        const string IID_IImageList = "46EB5926-582E-4017-9FDF-E8998DAA0950";
        const string IID_IImageList2 = "192B9D83-50FC-457B-90A0-2B82A8B5DAE1";

        private List<string> blurExtsList;
        private List<int[]> pixelList;
        protected override void OnInit()
        {
            blurExtsList = new List<string>
            {
                ".exe",
                ".cer",
                ".lnk",
                ".chm"
            };
            pixelList = new List<int[]>
            {
                new int[]{ 256 / 4, 256 / 4 },
                new int[]{ 256 / 2, 256 / 4 },
                new int[]{ 256 / 4 * 3, 256 / 4 },
                new int[]{ 256 / 4, 256 / 2 },
                new int[]{ 256 / 4, 256 / 4 * 3 },
                new int[]{ 256 / 4 * 3, 256 / 2 },
                new int[]{ 256 / 4 * 3, 256 / 4 * 3 },
                new int[]{ 256 / 2, 256 / 4 * 3 },
            };
        }
        public object GetImage(string filePath, int index)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;
            Icon ico;
            //选中文件中的图标总数
            var iconTotalCount = Win32API.PrivateExtractIcons(filePath, 0, 0, 0, null, null, 0, 0);
            //用于接收获取到的图标指针
            IntPtr[] hIcons = new IntPtr[iconTotalCount];
            //对应的图标id
            int[] ids = new int[iconTotalCount];
            //成功获取到的图标个数
            var successCount = Win32API.PrivateExtractIcons(filePath, 0, 256, 256, hIcons, ids, iconTotalCount, 0);
            string ext = Path.GetExtension(filePath).ToLower();
            IntPtr ip = IntPtr.Zero;
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
            else if (blurExtsList.Contains(ext))
            {
                ico = Icon.ExtractAssociatedIcon(filePath);
            }
            else
            {
                ip = GetJumboIcon(GetIconIndex(filePath));
                ico = Icon.FromHandle(ip);
                if (CheckIsSmallIco(ico.ToBitmap()))
                {
                    try
                    {
                        ico = Icon.ExtractAssociatedIcon(filePath);
                    }
                    catch (Exception)
                    {
                        ico = GetFileIcon(filePath);
                    }
                }
            }


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
        private bool CheckIsSmallIco(Bitmap bm)
        {
            Color color;
            int count = 0;
            foreach (int[] arr in pixelList)
            {
                color = bm.GetPixel(arr[0], arr[1]);
                if (color.A == 0)
                {
                    count++;
                }
            }
            Win32API.DeleteObject(bm.GetHbitmap());
            if (count >= 6)
            {
                return true;
            }
            return false;
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
        private int GetIconIndex(string pszFile)
        {
            Win32API.SHFILEINFO sfi = new Win32API.SHFILEINFO();
            Win32API.SHGetFileInfo(pszFile
                , 0
                , ref sfi
                , (uint)Marshal.SizeOf(sfi)
                , (uint)(Win32API.SHGFI.SysIconIndex | Win32API.SHGFI.LargeIcon | Win32API.SHGFI.UseFileAttributes));
            return sfi.iIcon;
        }

        // 256*256
        private IntPtr GetJumboIcon(int iImage)
        {
            Win32API.IImageList spiml = null;
            Guid guil = new Guid(IID_IImageList);//or IID_IImageList

            Win32API.SHGetImageList(Win32API.SHIL_JUMBO, ref guil, ref spiml);
            IntPtr hIcon = IntPtr.Zero;
            spiml.GetIcon(iImage, Win32API.ILD_TRANSPARENT | Win32API.ILD_IMAGE, ref hIcon);

            return hIcon;
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
                 , (uint)(Win32API.SHGFI.Icon | Win32API.SHGFI.LargeIcon | Win32API.SHGFI.UseFileAttributes));
            if (IconIntPtr.Equals(IntPtr.Zero))
                return null;
            return Icon.FromHandle(sfi.hIcon);
        }
    }
}
