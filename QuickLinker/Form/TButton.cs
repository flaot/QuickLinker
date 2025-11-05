using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace QuickLinker
{
    public partial class TButton : UserControl
    {
        private Image _image;

        [Category(nameof(CategoryAttribute.Appearance))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Image { get => _image; set { _image = value; Refresh(); } }

        [Category(nameof(CategoryAttribute.Appearance))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool allowStyle { get; set; }

        bool _leftClick;
        bool _mouseHover;

        private static ImageAttributes _grayImageAttr;
        private static ImageAttributes GrayImageAttr
        {
            get
            {
                if (_grayImageAttr != null)
                    return _grayImageAttr;
                _grayImageAttr = new ImageAttributes();
                float[][] colorMatrixElements = {
                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                };
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                _grayImageAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                return _grayImageAttr;
            }
        }

        public TButton()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_image != null)
            {
                Rectangle destRect = new Rectangle(0, 0, Width, Height);
                e.Graphics.InterpolationMode = InterpolationMode.High;
                e.Graphics.DrawImage(_image, destRect, 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel, Enabled ? null : GrayImageAttr);
            }
            if (!_leftClick && _mouseHover)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                        Color.White, 1, ButtonBorderStyle.Solid, //左边
                        Color.White, 1, ButtonBorderStyle.Solid, //上边
                        Color.DimGray, 1, ButtonBorderStyle.Inset, //右边
                        Color.DimGray, 1, ButtonBorderStyle.Inset);//底边
            }
            if (DesignMode || (Focused && !_leftClick))
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Gray, ButtonBorderStyle.Dashed);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (allowStyle)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _leftClick = true;
                    BorderStyle = BorderStyle.Fixed3D;
                }
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (allowStyle)
            {
                if (!_leftClick) return;
                if (e.X < 0 || e.Y < 0 || e.X > Width || e.Y > Height)
                    BorderStyle = BorderStyle.None;
                else
                    BorderStyle = BorderStyle.Fixed3D;
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (allowStyle)
            {
                if (!_leftClick)
                    return;
                _leftClick = false;
                bool click = BorderStyle == BorderStyle.Fixed3D;
                BorderStyle = BorderStyle.None;
            }
            base.OnMouseUp(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (allowStyle)
            {
                _mouseHover = true;
                if (!_leftClick)
                    Refresh();
            }
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (allowStyle)
            {
                _mouseHover = false;
                if (!_leftClick)
                    Refresh();
            }
            base.OnMouseLeave(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Refresh();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Refresh();
        }
    }
}
