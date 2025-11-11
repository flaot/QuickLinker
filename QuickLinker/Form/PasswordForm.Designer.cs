using System.Drawing;
using System.Windows.Forms;

namespace QuickLinker
{
    partial class PasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordForm));
            panel1 = new Panel();
            Txt_Password = new TextBox();
            label1 = new Label();
            Btn_Cancel = new Button();
            Btn_Ok = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(Txt_Password);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(7, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(294, 46);
            panel1.TabIndex = 0;
            // 
            // Txt_Password
            // 
            Txt_Password.AllowDrop = true;
            Txt_Password.Location = new Point(95, 11);
            Txt_Password.Name = "Txt_Password";
            Txt_Password.Size = new Size(180, 23);
            Txt_Password.TabIndex = 1;
            Txt_Password.UseSystemPasswordChar = true;
            Txt_Password.TextChanged += Txt_Password_TextChanged;
            // 
            // label1
            // 
            label1.Location = new Point(4, 14);
            label1.Name = "label1";
            label1.Size = new Size(85, 17);
            label1.TabIndex = 0;
            label1.Text = "输入密码(&P)：";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // Btn_Cancel
            // 
            Btn_Cancel.DialogResult = DialogResult.Cancel;
            Btn_Cancel.Location = new Point(226, 69);
            Btn_Cancel.Name = "Btn_Cancel";
            Btn_Cancel.Size = new Size(75, 25);
            Btn_Cancel.TabIndex = 2;
            Btn_Cancel.Text = "取消";
            Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // Btn_Ok
            // 
            Btn_Ok.Location = new Point(145, 69);
            Btn_Ok.Name = "Btn_Ok";
            Btn_Ok.Size = new Size(75, 25);
            Btn_Ok.TabIndex = 1;
            Btn_Ok.Text = "确定";
            Btn_Ok.UseVisualStyleBackColor = true;
            Btn_Ok.Click += Btn_Ok_Click;
            // 
            // PasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 106);
            Controls.Add(Btn_Cancel);
            Controls.Add(Btn_Ok);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PasswordForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "需要密码";
            Load += GroupNameForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox Txt_Password;
        private Label label1;
        private Button Btn_Cancel;
        private Button Btn_Ok;
    }
}