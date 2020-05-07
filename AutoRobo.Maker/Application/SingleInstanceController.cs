using AutoRobo.Core.Models;
using AutoRobo.Makers.Models.Repositories;
using AutoRobo.Makers.Views;
using AutoRobo.Protocol;
using Microsoft.VisualBasic.ApplicationServices;
using System;


namespace AutoRobo.Makers
{
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("SingleInstanceController");
        protected override void OnCreateSplashScreen()
        {
            base.OnCreateSplashScreen();
            // You can replace the Splash2 screen to yours.
            #if (!DEBUG)
            this.SplashScreen = new SplashScreen();
            #endif
        }
        public string StartArgs
        {
            get
            {
                string[] args = Environment.GetCommandLineArgs();
                string arg = args.Length > 1 ? args[1] : "";
                return arg;
            }

        }
        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();
            var args = Environment.GetCommandLineArgs();
            IActionRepository repository = null;
            if (args.Length > 1)
            {
                ProtocolObject protocol = ProtocolObject.DeserializeObject(StartArgs);
                repository = new ActionRepository((RecorderProtocol)protocol);
                MainForm = new MakerView(repository);
                return;
            }
//#if (DEBUG)          
//            if (MessageBox.Show("在线模式?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
//            {
//                this.MainForm = new OnlineView();
//                return;
//            }
//#endif
            repository = new FileActionRepository();
            ////主UI容器
            this.MainForm = new MakerView(repository);  
        }
     
        public SingleInstanceController()
        {
             //var args = Environment.GetCommandLineArgs();
             IsSingleInstance = false;
            
            //StartupNextInstance += this_StartupNextInstance;          
        }   

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
          
        }


    }
}
