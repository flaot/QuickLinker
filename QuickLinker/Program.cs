using QuickLinker.Model;
using QuickLinker.Plugin;
using QuickLinker.Properties;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Diagnostics;
using System.IO;
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
            string rootPath = Path.GetDirectoryName(Application.ExecutablePath);
            System.Environment.CurrentDirectory = rootPath;

            if (TryRunBatchUriMode(args))
                return;

            RunWinFormsApplication();
        }

        private static bool TryRunBatchUriMode(string[] args)
        {
            if (args.Length <= 1 || args[0] != "--")
                return false;

            Selection.isBatchMode = true;
            var appArchitecture = AppArchitecture.Interface;
            Uri uri = new Uri(args[1]);
            var system = appArchitecture.GetSystem<QuickEntitySystem>();
            appArchitecture.GetSystem<IPluginSystem>().LoadAll();
            appArchitecture.GetSystem<IMenuSystem>().RequestResetAll();
            var commandSystem = appArchitecture.GetSystem<ICommandSystem>();
            commandSystem.RequestResetAll();
            if (Guid.TryParse(uri.LocalPath, out var guid))
            {
                var entity = system.QueryWithGuid(guid);
                if (entity == null)
                {
                    MessageBox.Show(Resources.RUN_URI_ERROR);
                    return true;
                }
                Selection.activeContext = null;
                Selection.activeEntity = entity;
                appArchitecture.SendCommand(new QuickEntityOpenCommand() { index = entity.index });
            }
            else
            {
                commandSystem.RunCommand(uri.LocalPath, Array.Empty<string>());
            }
            return true;
        }

        private static void RunWinFormsApplication()
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

            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        private static void Event_TirggerSave()
        {
            var appArchitecture = AppArchitecture.Interface;
            var appConfig = appArchitecture.GetModel<AppConfig>();
            appArchitecture.GetSystem<IStroeSystem>().Save(appConfig);
        }
    }
}
