using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Systems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AppConfig = QuickLinker.Model.AppConfig;

namespace QuickLinker
{
    public partial class TPanel : UserControl, IController
    {
        private List<IUnRegister> _unRegisters;
        private Entity _entity;
        private int _defaultIndex;
        private string _text;
        private bool _haveGridSpace;
        private bool _haveFlatButton;
        private bool _invertBackColor;
        private bool _invertImage;
        private int _showTip;
        private bool _showName;

        private Image showImage;
        bool _leftClick;
        private string _title;

        /// <summary> 一般情况下，可使用index反推出几行几列 </summary>
        public int Index { get => _defaultIndex; }
        public Entity Entity => _entity;
        public string Title => _title;

        public IArchitecture GetArchitecture() => AppArchitecture.Interface;
        public TPanel()
        {
            _defaultIndex = -1;
            _unRegisters = new List<IUnRegister>();
            InitializeComponent();
        }
        public TPanel(int defaultIndex) : this()
        {
            _defaultIndex = defaultIndex;
        }
        private void TPanel_Load(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(255, 240, 240, 240);
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            SetEntity(entitySystem.Find(_defaultIndex), false);
            entitySystem.ChangeEntityEvent.Register(_defaultIndex, Event_Change);

            var config = this.GetModel<AppConfig>();
            _haveGridSpace = config.grid.Value > 0;
            _haveFlatButton = config.flatButton.Value;

            _unRegisters.Add(config.showButtonTip.RegisterWithInitValue(Event_ShowName));
            _unRegisters.Add(config.grid.Register(Event_GridSpace));
            _unRegisters.Add(config.analyzeDrapLink.RegisterWithInitValue(Event_AllowDrop));
            _unRegisters.Add(config.flatButton.Register(Event_FlatButton));

        }
        public void UnLoad()
        {
            _unRegisters.ForEach(item => item.UnRegister());
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.ChangeEntityEvent.UnRegister(_defaultIndex, Event_Change);
        }
        private void Event_Change(Entity entity) => SetEntity(entity, true);
        private void Event_ShowName(bool showName)
        {
            _showName = showName;
            Refresh();
        }
        private void Event_GridSpace(int gridSpace)
        {
            _haveGridSpace = gridSpace > 0;
            Refresh();
        }
        private void Event_AllowDrop(bool allowDrop)
        {
            AllowDrop = allowDrop;
        }
        private void Event_FlatButton(bool flatButton)
        {
            _haveFlatButton = flatButton;
            Refresh();
        }
        public void SetIndex(int index)
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            entitySystem.ChangeEntityEvent.UnRegister(_defaultIndex, Event_Change);
            _defaultIndex = index;
            SetEntity(entitySystem.Find(_defaultIndex), true);
            entitySystem.ChangeEntityEvent.Register(_defaultIndex, Event_Change);
        }

        /// <summary> 反相 </summary>
        public void Invert(bool enable)
        {
            if (enable != _invertBackColor)
                InvertBackColor();
            if (enable != _invertImage)
            {
                _invertImage = enable;
                if (showImage != null)
                    showImage = InvertImage(showImage);
            }
        }
        private void InvertBackColor()
        {
            var color = BackColor;
            var r = 0xFF - color.R;
            var g = 0xFF - color.G;
            var b = 0xFF - color.B;
            BackColor = Color.FromArgb(r, g, b);
            _invertBackColor = !_invertBackColor;
        }
        private Image InvertImage(Image image)
        {
            var bm = new Bitmap(image);
            int x, y, resultR, resultG, resultB;
            Color pixel;
            for (x = 0; x < bm.Width; x++)
            {
                for (y = 0; y < bm.Height; y++)
                {
                    pixel = bm.GetPixel(x, y);//获取当前坐标的像素值
                    resultR = 0xFF - pixel.R;//反红
                    resultG = 0xFF - pixel.G;//反绿
                    resultB = 0xFF - pixel.B;//反蓝
                    bm.SetPixel(x, y, Color.FromArgb(pixel.A, resultR, resultG, resultB));//绘图
                }
            }
            return bm;
        }

        public void SetEntity(Entity entity, bool refresh)
        {
            _entity = entity;
            if (_entity != null)
            {
                //支持SVG
                //    增加数据存储，可以动态刷新图标大小
                //支持dll中图标资源选取(shell.dll)
                //    ExtractIcon https://learn.microsoft.com/zh-cn/windows/win32/api/shellapi/nf-shellapi-extracticonw
                //支持更改图标来源
                //    比如：拖入的exe文件，默认是exe文件的图标，后续可以自定义更改展示的图标

                //pictureBox1.Image = ImageUtil.SvgToImage(pictureBox1.Width, pictureBox1.Height,
                //    "<svg t=\"1725528264339\" class=\"icon\" viewBox=\"0 0 1024 1024\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" p-id=\"1220\" width=\"200\" height=\"200\"><path d=\"M85.333333 0v938.666667h938.666667v85.333333H0V0h85.333333z m844.544 168.32a42.666667 42.666667 0 0 1 8.533334 25.514667v574.250666a85.333333 85.333333 0 0 1-85.333334 85.333334H254.976a85.333333 85.333333 0 0 1-85.333333-85.333334V342.442667L401.664 187.733333a42.666667 42.666667 0 0 1 51.84 3.413334l172.032 151.253333 244.650667-182.784a42.666667 42.666667 0 0 1 59.733333 8.661333zM421.76 276.906667L255.061333 388.053333l-0.042666 191.189334 195.456-165.034667 157.013333 133.248 245.546667-148.736 0.042666-119.765333-231.893333 173.226666L421.76 276.906667z\" p-id=\"1221\"></path></svg>");
                //pictureBox1.Image = _entity.bitmapImage;
                showImage = _entity.bitmapImage;
                if (_invertImage)
                    showImage = InvertImage(_entity.bitmapImage);
                _text = string.IsNullOrWhiteSpace(_entity.desc) ? _entity.Path : _entity.desc;
                _title = _text;
            }
            else
            {
                showImage = null;
                _text = string.Empty;
                _title = Resources.TPanel_None;
            }
            if (refresh)
                Refresh();
        }
        private void TPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void TPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                this.SendCommand(new QuickEntityInsertCommand() { filePath = file, index = _defaultIndex, canParse = true });
                break;
            }
        }
        private void TPanel_Paint(object sender, PaintEventArgs e)
        {
            if (showImage != null)
            {
                Rectangle destRect = new Rectangle(0, 0, Width, Height);
                Rectangle srcRect = new Rectangle(0, 0, showImage.Width, showImage.Height);
                e.Graphics.DrawImage(showImage, destRect, srcRect, GraphicsUnit.Pixel);
            }
            if (_showName && !string.IsNullOrEmpty(_text))
            {
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.DrawString(_text, Font, brush, 0, 0);
                }
            }
            if (_haveFlatButton)
                return;
            if (_haveGridSpace)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    Color.DimGray, 1, ButtonBorderStyle.Outset, //左边
                    Color.DimGray, 1, ButtonBorderStyle.Outset, //上边
                    Color.DimGray, 1, ButtonBorderStyle.Inset, //右边
                    Color.DimGray, 1, ButtonBorderStyle.Inset);//底边
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    Color.White, 1, ButtonBorderStyle.Solid, //左边
                    Color.White, 1, ButtonBorderStyle.Solid, //上边
                    Color.DimGray, 1, ButtonBorderStyle.Inset, //右边
                    Color.DimGray, 1, ButtonBorderStyle.Inset);//底边
            }
        }

        public override string ToString()
        {
            if (_entity == null)
                return _defaultIndex.ToString();
            else
                return "X";

        }

        private void TPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftClick = true;
                BorderStyle = BorderStyle.Fixed3D;
            }
            if (e.Button == MouseButtons.Right)
                TypeEventSystem.Global.Send(new ClickMenuTPanelEvent(this));
        }
        private void TPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_leftClick) return;
            if (e.X < 0 || e.Y < 0 || e.X > Width || e.Y > Height)
                BorderStyle = BorderStyle.None;
            else
                BorderStyle = BorderStyle.Fixed3D;
        }
        private void TPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_leftClick)
                return;
            _leftClick = false;
            bool click = BorderStyle == BorderStyle.Fixed3D;
            BorderStyle = BorderStyle.None;
            if (click)
                TypeEventSystem.Global.Send(new ClickTPanelEvent(this));
        }
        private void TPanel_MouseEnter(object sender, EventArgs e)
        {
            var config = this.GetModel<AppConfig>();
            if (config.showStateTip.Value)
            {
                _showTip = 1;
                TypeEventSystem.Global.Send(new RefreshStateTextEvent(_text));
            }
            if (config.showToolTip.Value)
            {
                _showTip = 2;
                TypeEventSystem.Global.Send(new ShowToolTipEvent(this, _text));
            }
        }
        private void TPanel_MouseLeave(object sender, EventArgs e)
        {
            if (_showTip == 1)
                TypeEventSystem.Global.Send(new RefreshStateTextEvent(string.Empty));
            if (_showTip == 2)
                TypeEventSystem.Global.Send(new ShowToolTipEvent(this, null));
            _showTip = 0;
        }

    }
}
