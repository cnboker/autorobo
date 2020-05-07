using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Reflection;
using System.IO;
using AutoRobo.Makers.AutoUpdate.Framework;

namespace AutoRobo.Makers
{
    public class Updater
    {
    
        public string AppVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        private readonly string updaterPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                  "updater.exe");

        public  void CheckForUpdate() {
            UpdateManager updManager = UpdateManager.Instance;

            //update configuration
            updManager.UpdateExePath = updaterPath;
            updManager.AppFeedUrl = System.Configuration.ConfigurationManager.AppSettings["updateFeedUrl"];
            updManager.UpdateExeBinary = Properties.Resources.updater;

            //always clean up at the beginning of the exe because we cant do it at the end
            updManager.CleanUp();

            if (updManager.CheckForUpdate())
            {
                DialogResult dr = MessageBox.Show("新版本可用. 您现在要升级吗?", "软件升级", MessageBoxButtons.OKCancel);
                if (DialogResult.OK == dr)
                {
                    new DownloadUpdateDialog(updManager).Show();
                }
            }
            else {
                MessageBox.Show("已是最新版本");
            }
        }
   
    }
}
