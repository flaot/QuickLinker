using QuickLinker.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandPlugin
{
    public class TestCommand : IPluginCommand
    {
        public string Name => "测试";
        public string Description => "测试命令的描述";
        public string Icon => "Test.icon";

        public void Action(string[] dropFileOrDirs)
        {
            MessageBox.Show("TestCommand:" + string.Join("\n", dropFileOrDirs));
        }
    }
}
