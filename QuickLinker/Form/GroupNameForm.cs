
using QuickLinker.Model;
using System;
using System.Windows.Forms;

namespace QuickLinker
{
    public partial class GroupNameForm : Form
    {
        private string _groupName = string.Empty;

        public GroupNameForm(string groupName)
        {
            InitializeComponent();
        }
        private void GroupNameForm_Load(object sender, EventArgs e)
        {
            Txt_GroupName.Text = _groupName;
        }
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            _groupName = Txt_GroupName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        public static string Show(string groupName)
        {
            var config = AppArchitecture.Interface.GetModel<AppConfig>();
            using (GroupNameForm groupNameForm = new GroupNameForm(groupName))
            {
                groupNameForm.TopMost = config.topWindow.Value;
                if (groupNameForm.ShowDialog() != DialogResult.OK)
                    return string.Empty;
                if (string.Equals(groupNameForm._groupName, groupName))
                    return string.Empty;
                return groupNameForm._groupName;
            }
        }
    }
}
