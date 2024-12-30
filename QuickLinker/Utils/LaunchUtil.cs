using Microsoft.Win32;
using QFramework;
using System;
using System.Reflection;

namespace QuickLinker.Utils
{
    internal class LaunchUtil : IUtility
    {
        public static string ApplicationName { get; } = Assembly.GetExecutingAssembly().GetName().Name;
        public static string ApplicationExecutable { get; } = Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");
        private const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private static readonly RegistryView RegView = Environment.Is64BitOperatingSystem ? RegistryView.Registry32 : RegistryView.Default;

        public void Set(bool enable)
        {
            if (CheckLaunchOnUserLogin() == enable)
                return;
            if (enable)
                EnableLaunchOnUserLogin();
            else
                DisableLaunchOnUserLogin();
        }

        /// <summary>
        /// Checks whether the current executable has been set as run-on-login
        /// </summary>
        /// <returns></returns>
        private bool CheckLaunchOnUserLogin()
        {
            try
            {
                using var localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegView);
                using var key = localKey.OpenSubKey(RunKey, false);
                var val = (string)key?.GetValue(ApplicationName, string.Empty);
                if (string.IsNullOrWhiteSpace(val)) return false;

                return val.Contains(ApplicationExecutable);
            }
            catch (Exception ex)
            {
                LogKit.E(ex.ToString() + "[LAUNCH] Unable to get status of run-on-login: {msg}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Places the executable in HKCU's run key
        /// </summary>
        /// <returns></returns>
        private bool EnableLaunchOnUserLogin()
        {
            try
            {
                using var localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegView);
                using var key = localKey.CreateSubKey(RunKey, true);
                key.OpenSubKey("Run", true);
                key.SetValue(ApplicationName, $"\"{ApplicationExecutable}\"", RegistryValueKind.String);
                key.Flush();

                return true;
            }
            catch (Exception ex)
            {
                LogKit.E(ex.ToString() + "[LAUNCH] Unable to set executable as run-on-login: {msg}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Removes the executable from HKCU's run key
        /// </summary>
        /// <returns></returns>
        private bool DisableLaunchOnUserLogin()
        {
            try
            {
                using var localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegView);
                using var key = localKey.OpenSubKey(RunKey, true);
                key?.DeleteValue(ApplicationName, false);
                key?.Flush();
                return true;
            }
            catch (Exception ex)
            {
                LogKit.E(ex.ToString() + "[LAUNCH] Unable to remove executable from run-on-login: {msg}", ex.Message);
                return false;
            }
        }
    }
}
