using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QuickLinker.Utils
{
    /// <summary> 在原始图标上叠加红叉；无图标时回退为「未设置」占位 + 红叉（单例缓存）。 </summary>
    internal static class TargetMissingIndicatorBitmap
    {
        private static Bitmap _fallbackTemplateWithCross;

        /// <summary> 在 <paramref name="original"/> 的副本上画红叉；<paramref name="original"/> 为 null 时用占位图 + 红叉。调用方拥有返回的 Bitmap 并负责 Dispose。 </summary>
        internal static Bitmap CreateOverOriginal(Image original, IImageUtil imageUtil)
        {
            if (original != null)
            {
                var surface = new Bitmap(original.Width, original.Height);
                using (var g = Graphics.FromImage(surface))
                {
                    g.DrawImage(original, 0, 0, original.Width, original.Height);
                    DrawRedCross(g, surface.Width, surface.Height);
                }
                return surface;
            }
            return GetOrCreateFallbackWithCross(imageUtil);
        }

        private static Bitmap GetOrCreateFallbackWithCross(IImageUtil imageUtil)
        {
            if (_fallbackTemplateWithCross != null)
                return CloneBitmap(_fallbackTemplateWithCross);
            if (imageUtil == null)
                return null;
            lock (typeof(TargetMissingIndicatorBitmap))
            {
                if (_fallbackTemplateWithCross != null)
                    return CloneBitmap(_fallbackTemplateWithCross);
                using (Bitmap template = imageUtil.Base64ToBitmapImage(Constants.DEFAULT_ICON_NULL_BASE64))
                {
                    var bmp = new Bitmap(template.Width, template.Height);
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(template, 0, 0);
                        DrawRedCross(g, bmp.Width, bmp.Height);
                    }
                    _fallbackTemplateWithCross = bmp;
                }
                return CloneBitmap(_fallbackTemplateWithCross);
            }
        }

        private static Bitmap CloneBitmap(Bitmap src)
        {
            return new Bitmap(src);
        }

        private static void DrawRedCross(Graphics g, int width, int height)
        {
            float thick = Math.Max(width * 0.07f, 6f);
            int m = (int)(Math.Min(width, height) * 0.22f);
            int cx = width / 2;
            int cy = height / 2;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.FromArgb(198, 40, 40), thick))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, cx - m, cy - m, cx + m, cy + m);
                g.DrawLine(pen, cx - m, cy + m, cx + m, cy - m);
            }
        }
    }
}
