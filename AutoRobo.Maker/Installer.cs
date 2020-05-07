using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using AutoRobo.Core;
using Microsoft.Win32;

namespace AutoRobo.Makers
{
    public class Installer
    {
        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;

        [DllImport("urlmon.dll")]
        [PreserveSig] 
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        /// <summary>
        /// 禁止IE点击声音
        /// </summary>
        static void DisableClickSounds()
        {
            CoInternetSetFeatureEnabled(
                FEATURE_DISABLE_NAVIGATION_SOUNDS,
                SET_FEATURE_ON_PROCESS,
                true);
        }

      
        public static bool IsFirstRun() {
            return (string.IsNullOrEmpty(ConfigurationManager.AppSettings["isFirstRun"])
                || ConfigurationManager.AppSettings["isFirstRun"] == "true");
        }

        public static void Configure(string applicationName)
        {
            DisableClickSounds();
            InstallInitializer.Configure();
            
            if (!IsFirstRun()) return;
            UpdateSetting();
            InstallInitializer.ConfigSample();
#if (DEBUG)
            RegisterCustomProtocol("autoRobo", applicationName);
#endif
        }

        public static void RegisterCustomProtocol(string protocolName, string applicationName)
        {
            // Check if the current user is administrator 
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(windowsIdentity);
            bool isAdmin = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);

            // If not, restart the application to show the gui.
            // Using the "runas" verb will bring up the 
            // UAC prompt on windows vista.
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = Application.ExecutablePath;
            if (!isAdmin)
            {
                psi.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(psi);
                }
                catch
                {
                }
            }
            else
            {                
                string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                path = System.IO.Path.Combine(path, applicationName);
                Installer.RegisterCustomProtocol(protocolName, path, "1%", Registry.LocalMachine);
            }
        }

        static private void UpdateSetting() {
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // Add an Application Setting.
            config.AppSettings.Settings.Remove("isFirstRun");
            config.AppSettings.Settings.Add("isFirstRun", "false");
            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        public  static string RunBatFile()
        {
            string exceptionMessage = "";
            try
            {
                UpdateSetting();
                string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                path = System.IO.Path.Combine(path, "regdll.bat");
                //logger.Info("使用regdll.bat注册ComUtilities.dll, regdll.bat路径:" + path);
                Process.Start(path);
                Thread.Sleep(new TimeSpan(0, 0, 1));
                Application.DoEvents();
                Thread.Sleep(new TimeSpan(0, 0, 1));
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }
            return exceptionMessage;
        }

        public static void Run(string fileName) {
            
            try
            {
                string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                path = System.IO.Path.Combine(path, fileName);                
                Process.Start(path);
                Thread.Sleep(new TimeSpan(0, 0, 1));
                Application.DoEvents();
                Thread.Sleep(new TimeSpan(0, 0, 1));
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public static void UnRegisterCustomProtocol(string protocol) {
            if (Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + protocol) != null)
                Registry.CurrentUser.OpenSubKey("Software\\Classes", true).DeleteSubKeyTree(protocol);
            if (Registry.CurrentUser.OpenSubKey("Software\\Wow6432Node\\Classes\\" + protocol) != null)
                Registry.CurrentUser.OpenSubKey("Software\\Wow6432Node\\Classes", true).DeleteSubKeyTree(protocol);
            if (Registry.LocalMachine.OpenSubKey("Software\\Classes\\" + protocol) != null)
                Registry.LocalMachine.OpenSubKey("Software\\Classes", true).DeleteSubKeyTree(protocol);
            if (Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Classes\\" + protocol) != null)
                Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Classes", true).DeleteSubKeyTree(protocol);
        }

        /// <summary>
        /// 注册自定义协议
        /// </summary>
        /// <param name="protocol">autoRobo</param>
        /// <param name="application">execute path</param>
        /// <param name="arguments">1%</param>
        /// <param name="registry"></param>
        /// <returns></returns>
        public static bool RegisterCustomProtocol(string protocol, string application, string arguments, RegistryKey registry)
        {
            RegistryKey cl = Registry.ClassesRoot.OpenSubKey(protocol);

            //if (cl != null && cl.GetValue("URL Protocol") != null && cl.GetValue("CustomUrlApplication") != null)
            //    if (System.Windows.Forms.MessageBox.Show("协议 '" + protocol + "' 已经注册，你想覆盖当前数据吗?", "autoRobo", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
            //        return false;

            try
            {
                RegistryKey r;
                r = registry.OpenSubKey("SOFTWARE\\Classes\\" + protocol, true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Classes\\" + protocol);
                r.SetValue("", "URL: Protocol handled by CustomURL");
                r.SetValue("URL Protocol", "");
                r.SetValue("CustomUrlApplication", application);
                r.SetValue("CustomUrlArguments", arguments);

                r = registry.OpenSubKey("SOFTWARE\\Classes\\" + protocol + "\\DefaultIcon", true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Classes\\" + protocol + "\\DefaultIcon");
                r.SetValue("", application);

                r = registry.OpenSubKey("SOFTWARE\\Classes\\" + protocol + "\\shell\\open\\command", true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Classes\\" + protocol + "\\shell\\open\\command");

                r.SetValue("", string.Format("{0} \"%1\"",  application));


                // If 64-bit OS, also register in the 32-bit registry area. 
                if (registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes") != null)
                {
                    r = registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol, true);
                    if (r == null)
                        r = registry.CreateSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol);
                    r.SetValue("", "URL: Protocol handled by CustomURL");
                    r.SetValue("URL Protocol", "");
                    r.SetValue("CustomUrlApplication", application);
                    r.SetValue("CustomUrlArguments", arguments);

                    r = registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\DefaultIcon", true);
                    if (r == null)
                        r = registry.CreateSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\DefaultIcon");
                    r.SetValue("", application);

                    r = registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\shell\\open\\command", true);
                    if (r == null)
                        r = registry.CreateSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\shell\\open\\command");

                    r.SetValue("", string.Format("{0} \"%1\"", application));

                }

            }
            catch (System.UnauthorizedAccessException ex)
            {
                
                MessageBox.Show(ex.Message + "\r\nYou do not have permission to make changes to the registry!\n\nMake sure that you have administrative rights on this computer.", "CustomURL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        private static void IEVersionCheck()
        {
            //if (!IE7OrHigher())
            //{
            //    var messageBuilder = new StringBuilder();
            //    messageBuilder.AppendLine(
            //        "This program requires Microsoft Internet Explorer version 7 or higher to operate.");
            //    messageBuilder.AppendLine("Please install IE7 from Microsoft's website before running.");
            //    MessageBox.Show(messageBuilder.ToString(), "Update Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
        }
        private static void CheckIEBlocker()
        {
            // popup blocker
            try
            {
                RegistryKey popupBlockerKey =
                                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\New Windows");
                if (popupBlockerKey != null
                    && popupBlockerKey.GetValue("PopupMgr").ToString() == "yes")
                {
                    //MessageBox.Show(
                    //    "IE Popup blocker is on." + Environment.NewLine +
                    //    "This may cause some sites not to function properly." + Environment.NewLine +
                    //    "For best results, please disable it.", "Popup Blocker", MessageBoxButtons.OK,
                    //    MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
            }

        }


        private static bool IE7OrHigher()
        {
            try
            {
                bool result = false;
                RegistryKey ieKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer");
                if (ieKey != null)
                {
                    string version = ieKey.GetValue("Version").ToString();
                    if (!version.StartsWith("5") && !version.StartsWith("6"))
                    {
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
