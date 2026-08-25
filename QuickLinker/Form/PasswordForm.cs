
using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using System;
using System.Windows.Forms;

namespace QuickLinker
{
    public partial class PasswordForm : Form, IController
    {
        private AppConfig _appConfig;

        public IArchitecture GetArchitecture() => AppArchitecture.Interface;
        public PasswordForm()
        {
            InitializeComponent();
        }
        private void GroupNameForm_Load(object sender, EventArgs e)
        {
            _appConfig = this.GetModel<AppConfig>();
        }
        private void Txt_Password_TextChanged(object sender, EventArgs e)
        {
            Btn_Ok.Enabled = !string.IsNullOrEmpty(Txt_Password.Text);
        }
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (string.Equals(Txt_Password.Text, _appConfig.password.Value))
                DialogResult = DialogResult.OK;
            Close();
        }

        public static bool ShowForm()
        {
            var config = AppArchitecture.Interface.GetModel<AppConfig>();
            if (string.IsNullOrEmpty(config.password.Value))
                return true;
            try
            {
                bool result = false;
                MainForm.ignoreDeactivate++;
                while (!result)
                {
                    using (PasswordForm groupNameForm = new PasswordForm())
                    {
                        groupNameForm.TopMost = config.topWindow.Value;
                        result = groupNameForm.ShowDialog() == DialogResult.OK;
                    }
                    if (!result)
                    {
                        DialogResult dialogResult = MessageBox.Show(Resources.PasswordForm_Tip, Resources.MSGBox_Tip, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        if (dialogResult != DialogResult.Retry)
                            break;
                    }
                }
                return result;
            }
            finally
            {
                MainForm.ignoreDeactivate--;
            }
        }


    }
}
