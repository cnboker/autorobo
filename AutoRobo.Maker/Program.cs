using System;
using System.Threading;
using System.Windows.Forms;
using AutoRobo.ClientLib;
using AutoRobo.Core.Logger;
using AutoRobo.Makers;
using AutoRobo.Core;

namespace AutoRobo
{
    /// <summary>
    /// 发布只能用DELOY形式，原因是sbin的atr更新不成功
    /// </summary>
     [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    static class Program
    {
        static log4net.ILog logger = log4net.LogManager.GetLogger("program");

        [STAThread]
        static void Main1()
        {
            Application.Run(new AutoRobo.ClientLib.Ants.BrowserSettings());
        }

        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            App.ChangeUserAgent("Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            //App.ChangeUserAgent("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0; MAMD; BOIE9;DEDE)");
            //Here you can specify the MainForm you want to start.
            Installer.Configure("AutoRobo.Makers.exe");            
            //if (DateTime.Now > new DateTime(2013,9,10)) {
            //    MessageBox.Show("您使用的是试用版本，请购买正版");
            //    return;
            //}

            new SingleInstanceController().Run(Environment.GetCommandLineArgs());            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionAlert ea = new ExceptionAlert((Exception)e.ExceptionObject);
            ea.Show();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionAlert ea = new ExceptionAlert(e.Exception);
            ea.Show();
        }



    }
}