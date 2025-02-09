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
        private void TButton_Paint(object sender, PaintEventArgs e)
        {
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
        }

        private void TButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftClick = true;
                BorderStyle = BorderStyle.Fixed3D;
            }
            //if (e.Button == MouseButtons.Right)
            //    TypeEventSystem.Global.Send(new ClickMenuTPanelEvent(this));
        }
        private void TButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_leftClick) return;
            if (e.X < 0 || e.Y < 0 || e.X > Width || e.Y > Height)
                BorderStyle = BorderStyle.None;
            else
                BorderStyle = BorderStyle.Fixed3D;
        }
        private void TButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_leftClick)
                return;
            _leftClick = false;
            bool click = BorderStyle == BorderStyle.Fixed3D;
            BorderStyle = BorderStyle.None;
            //if (click)
            //    TypeEventSystem.Global.Send(new ClickTPanelEvent(this));
        }
        private void TButton_MouseEnter(object sender, EventArgs e)
        {
            _mouseHover = true;
            if (!_leftClick)
                Refresh();
        }
        private void TButton_MouseLeave(object sender, EventArgs e)
        {
            _mouseHover = false;
            if (!_leftClick)
                Refresh();
        }
    }
}
