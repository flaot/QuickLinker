using Microsoft.Win32;
using QFramework;
using QuickLinker.QuickLaunch.Utils;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace QuickLinker.Utils
{
    internal class URIUtil : IURIUtil
    {
        private string _protocol = Assembly.GetExecutingAssembly().GetName().Name.ToLower();
        public string Protocol => _protocol;
        private string _applicationExecutable = Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");
        public string ApplicationExecutable => _applicationExecutable;
        private readonly RegistryView RegView = Environment.Is64BitOperatingSystem ? RegistryView.Registry32 : RegistryView.Default;
        public void Set(bool enable)
        {
            if (CheckURI() == enable)
                return;
            if (enable)
                EnableURI();
            else
                DisableURI();
        }
        private bool CheckURI()
        {
            try
            {
                string protocol = Protocol;
                string regRoot = "SOFTWARE\\Classes\\" + protocol;
                RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegView);
                return registry.OpenSubKey(regRoot + "\\shell\\open\\command", false) != null;
            }
            catch (Exception ex)
            {
                LogKit.E(ex.ToString() + "[URI] check: {msg}", ex.Message);
            }
            return false;
        }
        private void EnableURI()
        {
            string protocol = Protocol;
            string application = ApplicationExecutable;
            try
            {
                string regRoot = "SOFTWARE\\Classes\\" + protocol;
                RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegView);
                RegistryKey r = registry.OpenSubKey(regRoot, true);
                if (r == null)
                    r = registry.CreateSubKey(regRoot);
                r.SetValue("", $"URL:{protocol} protocol");
                r.SetValue("URL Protocol", "");

                r = registry.OpenSubKey(regRoot + "\\DefaultIcon", true);
                if (r == null)
                    r = registry.CreateSubKey(regRoot + "\\DefaultIcon");
                r.SetValue("", application);
                r = registry.OpenSubKey(regRoot + "\\shell\\open\\command", true);
                if (r == null)
                    r = registry.CreateSubKey(regRoot + "\\shell\\open\\command");
                r.SetValue("", $"\"{application}\" -- \"%1\"");
            }
            catch
            {
                MessageBox.Show("You do not have permission to make changes to the registry!\n\nMake sure that you have administrative rights on this computer.", "CustomURL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisableURI()
        {
            string protocol = Protocol;
            RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegView);
            if (registry.OpenSubKey("Software\\Classes\\" + protocol) != null)
                registry.OpenSubKey("Software\\Classes", true).DeleteSubKeyTree(protocol);
        }
    }
}