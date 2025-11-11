using QFramework;
using QuickLinker.Model;
using QuickLinker.Plugin;
using QuickLinker.Plugin.Events;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace QuickLinker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe"));
            System.Environment.CurrentDirectory = rootPath;

            if (args.Length > 0 && args[0] == "--")
            {
                Selection.isBatchMode = true;
                var appArchitecture = AppArchitecture.Interface;
                var appConfig = appArchitecture.GetModel<AppConfig>();
                Uri uri = new Uri(args[1]);
                var system = appArchitecture.GetSystem<QuickEntitySystem>();
                //初始化插件
                appArchitecture.GetSystem<IPluginSystem>().LoadAll();
                appArchitecture.GetSystem<IMenuSystem>().RequestResetAll();
                var commandSystem = appArchitecture.GetSystem<ICommandSystem>();
                commandSystem.RequestResetAll();
                //执行命令
                if (Guid.TryParse(uri.LocalPath, out var guid))
                {
                    var entity = system.QueryWithGuid(guid);
                    if (entity == null)
                    {
                        MessageBox.Show(Resources.RUN_URI_ERROR);
                        return;
                    }
                    Selection.activeContext = null;
                    Selection.activeEntity = entity;
                    TypeEventSystem.Global.Send(new ClickItemPreEvent());
                    if (Selection.activeEntity != null)
                    {
                        appArchitecture.SendCommand(new QuickEntityOpenCommand() { index = entity.index });
                        TypeEventSystem.Global.Send(new ClickItemPostEvent());
                    }
                }
                else
                {
                    //调用插件命令
                    commandSystem.RunCommand(uri.LocalPath, Array.Empty<string>());
                }
                return;
            }
            else
            {
                var appArchitecture = AppArchitecture.Interface;
                var appConfig = appArchitecture.GetModel<AppConfig>();
                if (appConfig.blockRepeatRun.Value)
                {
                    var singleApp = AppArchitecture.Interface.GetUtility<SingleAppUtil>();
                    Process process = singleApp.RunningInstance();
                    if (process != null)
                    {
                        singleApp.HandleRunningInstance(process);
                        return;
                    }
                }
                appConfig.TirggerSaveEvent.Register(Event_TirggerSave);

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new MainForm());
            }
        }

        private static void Event_TirggerSave()
        { 
            var appArchitecture = AppArchitecture.Interface;
            var appConfig = appArchitecture.GetModel<AppConfig>();
            appArchitecture.GetSystem<IStroeSystem>().Save(appConfig);
        }
    }
}