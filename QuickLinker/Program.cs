using QuickLinker.Model;
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
            var appConfig = AppArchitecture.Interface.GetModel<AppConfig>();
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

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}