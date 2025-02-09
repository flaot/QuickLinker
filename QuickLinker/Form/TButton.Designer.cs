using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker
{
    partial class TButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // TButton
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            BackgroundImageLayout = ImageLayout.Stretch;
            Name = "TButton";
            Paint += TButton_Paint;
            MouseDown += TButton_MouseDown;
            MouseMove += TButton_MouseMove;
            MouseUp += TButton_MouseUp;
            MouseEnter += TButton_MouseEnter;
            MouseLeave += TButton_MouseLeave;
            ResumeLayout(false);
        }
        #endregion
    }
}
