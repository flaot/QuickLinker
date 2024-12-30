using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace QuickLinker.Utils
{
    public class GDI32
    {
        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="g">位图</param>
        /// <param name="imagePtr">图像</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="panX">X偏移量</param>
        /// <param name="panY">Y偏移量</param>
        /// <param name="zoom">缩放比例</param>
        public static void DrawImage(Graphics g, IntPtr imagePtr, int width, int height, int panX, int panY, float zoom)
        {
            IntPtr hdc = g.GetHdc();
            DrawImage(hdc, imagePtr, width, height, panX, panY, zoom);
            g.ReleaseHdc(hdc);
        }
        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="hdc">设备上下文的句柄</param>
        /// <param name="imagePtr">图像</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="panX">X偏移量</param>
        /// <param name="panY">Y偏移量</param>
        /// <param name="zoom">缩放比例</param>
        public static void DrawImage(IntPtr hdc, IntPtr imagePtr, int width, int height, int panX, int panY, float zoom)
        {
            SetStretchBltMode(hdc, STRETCH_DELETESCANS);
            IntPtr hMemDC = CreateCompatibleDC(hdc);
            IntPtr hOldBitmap = SelectObject(hMemDC, imagePtr);
            int w = (int)(width * zoom);
            int h = (int)(height * zoom);
            StretchBlt(hdc, panX, panY, w, h, hMemDC, 0, 0, width, height, SRCCOPY);
            SelectObject(hdc, hMemDC);
            DeleteDC(hOldBitmap);
            DeleteDC(hMemDC);
        }
        /// <summary>
        /// 铺满
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="imagePtr"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        public static void DrawImage2(IntPtr hdc, IntPtr imagePtr, int x, int y, int srcWidth, int srcHeight, int destWidth, int destHeight)
        {
            SetStretchBltMode(hdc, STRETCH_DELETESCANS);
            IntPtr hMemDC = CreateCompatibleDC(hdc);
            IntPtr hOldBitmap = SelectObject(hMemDC, imagePtr);
            StretchBlt(hdc, x, y, destWidth, destHeight, hMemDC, 0, 0, srcWidth, srcHeight, SRCCOPY);
            SelectObject(hdc, hMemDC);
            DeleteDC(hOldBitmap);
            DeleteDC(hMemDC);
        }
        static BITMAPINFO _bmi = null;
        IntPtr hBitmap = IntPtr.Zero;
        public static void DrawImageForBuffer(IntPtr hdc, byte[] buffer, int x, int y, int srcWidth, int srcHeight, int destWidth, int destHeight, int colorDepth)
        {
            if (_bmi == null)
                _bmi = new BITMAPINFO(srcWidth, srcHeight, colorDepth);

            DrawImageForBuffer(hdc, buffer, x, y, srcWidth, srcHeight, destWidth, destHeight, _bmi);
        }
        public static void DrawImageForBuffer(IntPtr hdc, byte[] buffer, int x, int y, int srcWidth, int srcHeight, int destWidth, int destHeight, BITMAPINFO bmi)
        {
            SetStretchBltMode(hdc, STRETCH_DELETESCANS);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr bits = IntPtr.Zero;
            hBitmap = CreateDIBSection(hdc, bmi, 0, out bits, IntPtr.Zero, 0);

            // 选择位图到内存设备上下文
            IntPtr hdcMem = CreateCompatibleDC(hdc);
            IntPtr hOldBitmap = SelectObject(hdcMem, hBitmap);

            Marshal.Copy(buffer, 0, bits, buffer.Length);
            // 将图像绘制到目标设备上下文
            StretchBlt(hdc, x, y, destWidth, destHeight, hdcMem, 0, 0, srcWidth, srcHeight, SRCCOPY);
            //BitBlt(hdc, x, y, destWidth, destHeight, hdcMem, 0, 0, SRCCOPY);
            IntPtr ptr = SelectObject(hdcMem, hOldBitmap);
            DeleteDC(ptr);
            DeleteDC(hOldBitmap);
            DeleteDC(hdcMem);
            DeleteObject(hBitmap);
        }
        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="g">位图</param>
        /// <param name="imagePtr">图像</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="destWidth">操作控件宽</param>
        /// <param name="destHeight">操作控件高</param>
        public static void DrawImage(Graphics g, IntPtr imagePtr, int width, int height, int destWidth, int destHeight)
        {
            IntPtr hdc = g.GetHdc();
            DrawImage(hdc, imagePtr, width, height, destWidth, destHeight);
            g.ReleaseHdc(hdc);

        }
        public static void DrawImage(IntPtr hdc, IntPtr imagePtr, int width, int height, int destWidth, int destHeight)
        {
            float scaleX = (float)destWidth / width;
            float scaleY = (float)destHeight / height;
            float scale = Math.Min(scaleX, scaleY);
            int w = (int)(width * scale);
            int h = (int)(height * scale);
            int x = (destWidth - w) >> 1;
            int y = (destHeight - h) >> 1;
            DrawImage(hdc, imagePtr, width, height, x, y, scale);
        }
        /// <summary>
        /// 背景透明
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="imagePtr"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        public static void DrawImageTransparenc(IntPtr hdc, IntPtr imagePtr, int x, int y, int srcWidth, int srcHeight, int destWidth, int destHeight)
        {
            SetStretchBltMode(hdc, STRETCH_DELETESCANS);
            IntPtr hMemDC = CreateCompatibleDC(hdc);
            IntPtr hOldBitmap = SelectObject(hMemDC, imagePtr);
            AlphaBlend(hdc, x, y, destWidth, destHeight, hMemDC, 0, 0, srcWidth, srcHeight, blendFunction);
            SelectObject(hMemDC, hOldBitmap);
            DeleteDC(hOldBitmap);
            DeleteDC(hMemDC);
        }
        static readonly GDI32.BLENDFUNCTION blendFunction = new GDI32.BLENDFUNCTION
        {
            BlendOp = AC_SRC_OVER,
            BlendFlags = 0,
            SourceConstantAlpha = 255, // 255 表示不透明
            AlphaFormat = AC_SRC_ALPHA
        };
        public const int AC_SRC_OVER = 0x00;
        public const int AC_SRC_ALPHA = 0x01;
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        public static void DrawLine(Graphics g, IntPtr pen, int startX, int startY, int endX, int endY)
        {
            IntPtr hdc = g.GetHdc();
            DrawLine(hdc, pen, startX, startY, endX, endY);
            g.ReleaseHdc(hdc);
        }
        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="pen"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        public static void DrawLine(IntPtr hdc, IntPtr pen, int startX, int startY, int endX, int endY)
        {
            IntPtr oldPen = SelectObject(hdc, pen);// 将画笔选入设备环境
            DrawLine(hdc, startX, startY, endX, endY);
            SelectObject(hdc, oldPen); // 恢复原来的画笔
        }
        public static void DrawLine(IntPtr hdc, int startX, int startY, int endX, int endY)
        {
            GDI32.MoveToEx(hdc, startX, startY, IntPtr.Zero);
            GDI32.LineTo(hdc, endX, endY);
        }
        /// <summary>
        /// 绘制字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="hdc"></param>
        /// <param name="fontPtr"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public static void DrawString(string text, IntPtr hdc, IntPtr fontPtr, int startX, int startY, uint fontColor)
        {
            IntPtr hOldFont = SelectObject(hdc, fontPtr);
            SetBkMode(hdc, 1);
            SetTextColor(hdc, fontColor);
            int charCount = Encoding.Unicode.GetByteCount(text);
            TextOut(hdc, startX, startY, text, charCount);
            SelectObject(hdc, hOldFont);
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称</param>
        /// <returns></returns>
        public static IntPtr GetFont(int fontSize, string fontName)
        {
            return CreateFont(fontSize, 0, 0, 0, FW_BOLD, 0, 0, 0, ANSI_CHARSET, OUT_DEFAULT_PRECIS,
           CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, DEFAULT_PITCH | FF_DONTCARE, fontName);
        }
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="hdc"></param>
        public static void DrawRectangle(IntPtr hdc, IntPtr pen, int x, int y, int width, int height)
        {
            IntPtr oldPen = SelectObject(hdc, pen);// 将画笔选入设备环境
            DrawRectangle(hdc, x, y, width, height);
            SelectObject(hdc, oldPen); // 恢复原来的画笔
        }
        public static void DrawRectangle(IntPtr hdc, int x, int y, int width, int height)
        {
            int x1 = x + width;
            int y1 = y + height;
            DrawLine(hdc, x, y, x1, y);
            DrawLine(hdc, x, y, x, y1);
            DrawLine(hdc, x1, y, x1, y1);
            DrawLine(hdc, x, y1, x1, y1);
        }
        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="color"></param>
        public static void FillRectangle(IntPtr hdc, int left, int top, int right, int bottom, uint color)
        {
            IntPtr brush = CreateSolidBrush(color);
            FillRectangle(hdc, brush, left, top, right, bottom);
            DeleteObject(brush);
        }
        public static void FillRectangle(IntPtr hdc, IntPtr brush, int x, int y, int width, int height)
        {
            RECT rect = CreateRect(x, y, width, height);
            FillRect(hdc, ref rect, brush);
            DeleteObject(brush);
        }
        // 创建TRIVERTEX数组
        static readonly TRIVERTEX[] vertices = new TRIVERTEX[2];
        // 创建GRADIENT_RECT数组
        static readonly GRADIENT_RECT[] mesh = new GRADIENT_RECT[1];
        public static void FillGradientRectangle(IntPtr hdc, int left, int top, int right, int bottom, uint startColor, uint endColor)
        {
            vertices[0] = new TRIVERTEX { X = left, Y = top, Red = (startColor & 0xFF0000) >> 16, Green = (startColor & 0x00FF00) >> 8, Blue = startColor & 0x0000FF, Alpha = 0xFF };
            vertices[1] = new TRIVERTEX { X = right, Y = bottom, Red = (endColor & 0xFF0000) >> 16, Green = (endColor & 0x00FF00) >> 8, Blue = endColor & 0x0000FF, Alpha = 0xFF };

            mesh[0] = new GRADIENT_RECT { UpperLeft = 0, LowerRight = 1 };
            GradientFill(hdc, vertices, vertices.Length, mesh, mesh.Length, 0);
        }
        static RECT CreateRect(int x, int y, int width, int height)
        {
            return new RECT
            {
                Left = x,
                Top = y,
                Right = x + width, // 计算右边界
                Bottom = y + height // 计算下边界
            };
        }
        /// <summary>
        /// 绘制圆
        /// </summary>
        /// <param name="hdc"></param>
        public static void DrawEllipse(IntPtr hdc, IntPtr hPen, int centerX, int centerY, int radius)
        {
            IntPtr oldPen = SelectObject(hdc, hPen);
            Ellipse(hdc, centerX - radius, centerY - radius, centerX + radius, centerY + radius);
            SelectObject(hdc, oldPen);
        }
        /// <summary>
        /// 填充圆
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        public static void FillEllipse(IntPtr hdc, int centerX, int centerY, int radius, uint penColor, uint brushcolor)
        {
            IntPtr hPen = CreatePen(0, 1, penColor);
            IntPtr hBrush = CreateSolidBrush(brushcolor);
            SetBkMode(hdc, 2);
            FillEllipse(hdc, hPen, hBrush, centerX, centerY, radius);
            DeleteObject(hPen);
            DeleteObject(hBrush);
        }
        public static void FillEllipse(IntPtr hdc, IntPtr hPen, IntPtr hBrush, int centerX, int centerY, int radius)
        {
            IntPtr oldPen = SelectObject(hdc, hPen);
            IntPtr oldBrush = SelectObject(hdc, hBrush);
            Ellipse(hdc, centerX - radius, centerY - radius, centerX + radius, centerY + radius);
            SelectObject(hdc, oldPen);
            SelectObject(hdc, oldBrush);
        }
        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a">默认Alpha值为0</param>
        /// <returns></returns>
        public static uint GetColor(byte r, byte g, byte b, byte a = 0)
        {
            return (uint)((a << 24) | ((b << 16) | (g << 8) | r));
        }
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int SetBkColor(IntPtr hdc, uint rgb);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool ExtTextOut(IntPtr hdc, int x, int y, uint options, IntPtr lprect, string text);
        /// <summary>
        /// 创建笔
        /// </summary>
        /// <param name="fnPenStyle"></param>
        /// <param name="nWidth"></param>
        /// <param name="crColor"></param>
        /// <returns></returns>
        public static IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, uint crColor)
        {
            return CreatePen((int)fnPenStyle, nWidth, crColor);
        }

        internal const int SRCCOPY = 0x00CC0020;
        internal const int STRETCH_ANDSCANS = 1;// 使用位与运算来处理像素的拉伸。
        internal const int STRETCH_ORSCANS = 2;// 使用位或运算来处理像素的拉伸。
        internal const int STRETCH_DELETESCANS = 3;// 使用位异或运算来处理像素的拉伸。
        internal const int STRETCH_HALFTONE = 4;// 使用半色调算法来处理像素的拉伸，以获得更平滑的结果。
        internal const int FW_BOLD = 700;
        internal const uint ANSI_CHARSET = 0;
        internal const uint OUT_DEFAULT_PRECIS = 0;
        internal const uint CLIP_DEFAULT_PRECIS = 0;
        internal const uint DEFAULT_QUALITY = 0;
        internal const uint DEFAULT_PITCH = 0;
        internal const uint FF_DONTCARE = (0 << 4);

        [DllImport("user32.dll")]
        internal static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr hwnd);
        /// <summary>
        /// 创建与当前窗口兼容的HDC2（内存设备上下文）
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// 每个设备场景都可能有选入其中的图形对象
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);
        /// <summary>
        /// 将一幅位图从一个设备场景复制到另一个。源和目标DC相互间必须兼容。
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="nXDest"></param>
        /// <param name="nYDest"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="nXSrc"></param>
        /// <param name="nYSrc"></param>
        /// <param name="dwRop"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern bool BitBlt(
       IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
       IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        /// <summary>
        /// 将一幅位图从一个设备场景复制到另一个
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="nXDest"></param>
        /// <param name="nYDest"></param>
        /// <param name="nWidthDest"></param>
        /// <param name="nHeightDest"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="nXSrc"></param>
        /// <param name="nYSrc"></param>
        /// <param name="nWidthSrc"></param>
        /// <param name="nHeightSrc"></param>
        /// <param name="dwRop"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidthSrc, int nHeightSrc, int dwRop);

        [DllImport("gdi32.dll")]
        internal static extern int SetStretchBltMode(IntPtr hdc, int iStretchMode);

        [DllImport("gdi32.dll")]
        internal static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("user32.dll")]
        internal static extern int FillRect(IntPtr hDC, ref RECT lprc, IntPtr hbr);

        [DllImport("gdi32.dll")]
        internal static extern bool Ellipse(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        internal static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        internal static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
        [DllImport("gdi32.dll")]
        internal static extern bool PolyBezier(IntPtr hdc, [In] POINT[] apt, int cpt);
        /// <summary>
        /// 输出文本
        /// </summary>
        /// <param name="hdc">设备上下文的句柄</param>
        /// <param name="nXStart"></param>
        /// <param name="nYStart"></param>
        /// <param name="lpString">要输出的字符串</param>
        /// <param name="cbString">字符串的长度</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
        /// <summary>
        /// 逻辑字体
        /// </summary>
        /// <param name="nHeight"> 字体的高度（以逻辑单位表示）。如果这个值是正数，它指定字符的高度；如果是负数，它指定字符的细胞或em方块的高度</param>
        /// <param name="nWidth">字体的平均字符宽度（以逻辑单位表示）。如果这个值为0，则系统会根据字体的高度自动计算宽度。</param>
        /// <param name="nEscapement"> 以十分之一度表示的文本的输出角度。例如，450表示45度。</param>
        /// <param name="nOrientation">以十分之一度表示的每个字符的基线相对于页面的方向。通常，对于左到右的文本，这个值和nEscapement相同。</param>
        /// <param name="fnWeight">字体的粗细，可以是普通（400）、加粗（700）等值。</param>
        /// <param name="fdwItalic">是否斜体。如果这个值非零，则字体是斜体的。</param>
        /// <param name="fdwUnderline"> 是否有下划线。如果这个值非零，则字体有下划线。</param>
        /// <param name="fdwStrikeOut">是否有删除线。如果这个值非零，则字体有删除线。</param>
        /// <param name="fdwCharSet"> 字符集标识符，决定了字体支持哪种字符集。</param>
        /// <param name="fdwOutputPrecision">输出精度，定义了字体的质量。</param>
        /// <param name="fdwClipPrecision">裁剪精度，定义了如何裁剪字符。</param>
        /// <param name="fdwQuality"> 输出质量，定义了字体的清晰度和抗锯齿能力。</param>
        /// <param name="fdwPitchAndFamily"> 字体的间距和类别。</param>
        /// <param name="lpszFace">字体名称。</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight,
            uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut, uint fdwCharSet,
            uint fdwOutputPrecision, uint fdwClipPrecision, uint fdwQuality,
            uint fdwPitchAndFamily, string lpszFace);
        /// <summary>
        /// 设置文本颜色
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="crColor"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern uint SetTextColor(IntPtr hdc, uint crColor);
        /// <summary>
        /// 创建一支笔
        /// </summary>
        /// <param name="fnPenStyle">
        /// 指定笔的风格
        /// </param>
        /// <param name="nWidth">指定笔的宽度，以逻辑单位为单位</param>
        /// <param name="crColor">指定笔的颜色，使用 RGB 值表示</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);
        [DllImport("gdi32.dll")]
        internal static extern bool SetDCPenColor(IntPtr hdc, uint crColor);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateSolidBrush(uint color);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateHatchBrush(int fnStyle, uint color);
        /// <summary>
        /// 设置背景透明
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="iBkMode">0不透明；1透明</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        internal static extern int SetBkMode(IntPtr hdc, int iBkMode);


        [DllImport("msimg32.dll", SetLastError = true)]
        public static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest,
        IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, BLENDFUNCTION blendFunction);

        [DllImport("msimg32.dll", EntryPoint = "GradientFill")]
        internal static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, int nVertex,
       GRADIENT_RECT[] pMesh, int nMesh, int ulMode);
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, [In] BITMAPINFO pbmi, uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);
    }
    // 定义TRIVERTEX结构
    [StructLayout(LayoutKind.Sequential)]
    public struct TRIVERTEX
    {
        public int X;
        public int Y;
        public uint Red;
        public uint Green;
        public uint Blue;
        public uint Alpha;
    }

    // 定义GRADIENT_RECT结构
    [StructLayout(LayoutKind.Sequential)]
    public struct GRADIENT_RECT
    {
        public int UpperLeft;
        public int LowerRight;
    }
    public enum PenStyle
    {
        /// <summary>
        /// 实线
        /// </summary>
        PS_SOLID = 0,
        /// <summary>
        /// 短划线
        /// </summary>
        PS_DASH = 1,
        /// <summary>
        /// 点线
        /// </summary>
        PS_DOT = 2,
        /// <summary>
        /// 短划线-点线交替
        /// </summary>
        PS_DASHDOT = 3,
        /// <summary>
        /// 短划线-点线-点线交替
        /// </summary>
        PS_DASHDOTDOT = 4,
        /// <summary>
        /// 创建一个不可见的笔
        /// </summary>
        PS_NULL = 5,
        /// <summary>
        /// 在多边形或路径的内部绘制一条笔画
        /// </summary>
        PS_INSIDEFRAME = 6
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        public int[] bmiColors;
        public BITMAPINFO(BITMAPINFOHEADER bITMAPINFOHEADER)
        {
            bmiHeader = bITMAPINFOHEADER;
            bmiHeader.biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
        }
        public BITMAPINFO(int width, int height, int colorDepth, BI bI = BI.BI_RGB)
        {
            bmiHeader = new BITMAPINFOHEADER
            {
                biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER)),
                biWidth = width,
                biHeight = height,
                biPlanes = 1,
                biBitCount = (short)colorDepth,
                biCompression = (int)bI,
                //biSizeImage =width*height* colorDepth/8, //((width * colorDepth + 31) / 32) * 4 * height,
                biXPelsPerMeter = 0,
                biYPelsPerMeter = 0,
                biClrUsed = 0,
                biClrImportant = 0
            };

            if (colorDepth <= 8)
            {
                // 为8位或更低位图创建颜色调色板
                bmiColors = new int[1 << colorDepth]; // 例如，8位位图需要256个颜色条目
                                                      // 填充bmiColors数组
            }
            else
            {
                bmiColors = new int[1];
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public int biSize;  // 此结构体的大小
        public int biWidth; // 位图的宽度（像素）
        public int biHeight; // 位图的高度（像素）
        public short biPlanes; // 目标设备的位平面数，通常为1
        public short biBitCount; // 每个像素的位数，即颜色深度
        public int biCompression;// 位图的压缩类型
        public int biSizeImage; // 位图数据的大小（字节）
        public int biXPelsPerMeter;// 位图水平分辨率（每米像素数）
        public int biYPelsPerMeter; // 位图垂直分辨率（每米像素数）
        public int biClrUsed;// 用于位图的彩色表中的彩色项数
        public int biClrImportant;// 重要的彩色项数
    }
    public enum BI : uint
    {
        BI_RGB = 0,// 表示位图不是压缩的，并且使用RGB值。
        BI_RLE8 = 1, // 表示使用行程长度编码（Run Length Encoding）压缩8位位图。
        BI_RLE4 = 2, // 表示使用行程长度编码压缩4位位图。
        BI_BITFIELDS = 3, // 表示使用位字段来定义颜色，通常用于16位或32位位图。
        BI_JPEG = 4, // 表示使用JPEG格式压缩位图。
        BI_PNG = 5 // 表示使用PNG格式压缩位图。
    }
}
