using QFramework;
using QuickLinker.Plugin;
using System.Collections.Generic;
using System.Linq;

namespace QuickLinker.Systems
{
    public interface ICommandSystem : ISystem
    {
        /// <summary> 请求刷新所有命令 </summary>
        void RequestResetAll();
        string[] AllCommand { get; }
        bool RunCommand(string command, string[] dropFileOrDirs);
        IPluginCommand CommandInfo(string command);
    }
    internal class CommandSystem : AbstractSystem, ICommandSystem
    {
        private Dictionary<string, IPluginCommand> _dicCommandByName;

        public string[] AllCommand => _dicCommandByName.Keys.ToArray<string>();

        protected override void OnInit()
        {
            _dicCommandByName = new Dictionary<string, IPluginCommand>();
        }
        public void RequestResetAll()
        {
            _dicCommandByName.Clear();
            //得到插件命令项
            var pluginSystem = this.GetSystem<IPluginSystem>();
            if (pluginSystem != null)
            {
                foreach (var plugin in pluginSystem.Plugins)
                {
                    foreach (var item in plugin.commands)
                    {
                        _dicCommandByName.Add(item.GetType().FullName, item);
                    }
                }
            }
        }

        public bool RunCommand(string command, string[] dropFileOrDirs)
        {
            if (_dicCommandByName.TryGetValue(command, out IPluginCommand pluginCommand))
            { 
                pluginCommand.Action(dropFileOrDirs);
                return true;
            }
            return false;
        }

        public IPluginCommand CommandInfo(string command)
        {
            _dicCommandByName.TryGetValue(command, out IPluginCommand pluginCommand);
            return pluginCommand;
        }
    }
}
