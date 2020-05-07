
using AutoRobo.Core;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Models;
using System.Threading;

namespace AutoRobo.Core
{
    public class AppContext : ObservableObject, IAppContext
    {
        private Core.ActionBuilder.MultiStepActionParameter actionParameter = null;
        private ModelSet modelSet;
        private static AppContext current = null;
     
        private AppContext() {
            State = new AppState();
        }

        public static AppContext Current {
            get {
                if (current == null) {
                    current = new AppContext();
                }
                return current;
            }
        }

        public ICoreBrowser Browser { get; set; }
        public ActionRunnable CurrentWorker { get; set; }

        public ICustomIdentity Identity { get; set; }

        public ModelSet ActionModel {
            get {
                return modelSet;
            }
            set {
                if (value != modelSet)
                {
                    modelSet = value;
                    OnPropertyChanged("ActionModel");
                }
            }
        }

        public MultiStepActionParameter MultiStepActionParameter
        {
            get {
                return actionParameter;
            }
            set {
                if (value != actionParameter)
                {
                    actionParameter = value;
                    if (value != null)
                    {
                        OnPropertyChanged("MultiStepActionParameter");
                    }
                }
            }
        }  
       
        public AppState State { get; set; }

        public bool Offline { get; set; }   

        public Core.Logger.ILog Logger
        {
            get;
            set;
        }

        public INewWindow MainWindow
        {
            get;
            set;
        }

        private int _browserThread;
        private BrowserWindow browserWindow;
     
        public BrowserWindow Window
        {
            get {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                if (currentThreadId != _browserThread)
                {
                    browserWindow = new BrowserWindow(Browser) { };
                    _browserThread = currentThreadId;
                }
                return browserWindow;
            }
        }
    }
}
