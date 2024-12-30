
using System;
using System.Windows.Forms;

namespace QuickLinker
{
    public partial class GroupNameForm : Form
    {
        public string GroupName { get; set; } = string.Empty;
        public GroupNameForm()
        {
            InitializeComponent();
        }
        private void GroupNameForm_Load(object sender, EventArgs e)
        {
            Txt_GroupName.Text = GroupName;
        }
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            GroupName = Txt_GroupName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
