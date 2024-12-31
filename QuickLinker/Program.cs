using QFramework;
using QuickLinker.Model;
using QuickLinker.Systems;
using QuickLinker.Utils;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace QuickLinker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
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

        private static void Event_TirggerSave()
        { 
            var appArchitecture = AppArchitecture.Interface;
            var appConfig = appArchitecture.GetModel<AppConfig>();
            appArchitecture.GetSystem<IStroeSystem>().Save(appConfig);
        }
    }
}