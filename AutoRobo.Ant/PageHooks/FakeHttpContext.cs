using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Expando;
using System.Security.Permissions;
using System.Threading;
using AutoRobo.ClientLib.PageHooks.Handler;
using AutoRobo.Core;
using WatiN.Core;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core.Models;

namespace AutoRobo.ClientLib.PageHooks
{

    /// <summary>
    /// 跟踪工作区与浏览器交换数据入口， httpHandler处理流程：
    /// AuthenticateHandler->PageHandler->RegisterHandler->LoginHandler->MatchHandler->
    /// </summary>
    /// 
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class FakeHttpContext
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FakeHttpContext));
        /// <summary>
        /// 多个浏览器共享同一个验证Handler
        /// </summary>
        static private AuthenticateHandler authenticateHandler = new AuthenticateHandler();
        public const string  BlankUrl = "about:blank";

        List<Type> handlers = new List<Type>();
        ICoreBrowser coreBrowser = null;
        public bool IsRun { get; set; }
        
        /// <summary>
        /// 页面设别功能列表内容显示容器
        /// </summary>
        public IStripContainer ViewContainer { get; set; }

        private int _browserThread;
        private BrowserWindow browserWindow;

        public BrowserWindow Browser
        {
            get
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                if (currentThreadId != _browserThread)
                {
                    browserWindow = new BrowserWindow((MyBrowser)CoreBrowser) { };
                    _browserThread = currentThreadId;
                }
                return browserWindow;
            }

        }

        public MyBrowser CoreBrowser {
            get {
                return coreBrowser as MyBrowser;
            }
        }

        public string PageUrl {
            get {
                return coreBrowser.LocationUrl;
            }
        }

        /// <summary>
        /// 是否启动任务跟踪
        /// </summary>
        public bool EnableTracing { get; set; }
   
        /// <summary>
        /// 一个浏览器对应一个FakeHttpContext
        /// </summary>
        /// <param name="coreBrowser"></param>
        public FakeHttpContext(ICoreBrowser coreBrowser)
        {
            this.coreBrowser = coreBrowser;
            EnableTracing = true;
        }     

        public void Clear() {
            handlers.Clear();
        }


        public void GoToBlank()
        {
           // GetMainWin().Goto(BlankUrl, "_blank");
        }

        public void GoTo(string url, string target)
        {           
          //  GetMainWin().Goto(url, "_blank");            
        }

        private INewWindow GetMainWin() {
            System.Windows.Forms.Control c = coreBrowser as System.Windows.Forms.Control;
            INewWindow win = c.TopLevelControl as INewWindow;          
            return win;
        }
      
        /// <summary>
        /// 获取用户验证数据
        /// </summary>
        /// <returns></returns>
        public bool Authenticate(string returnUrl) {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? FakeHttpContext.BlankUrl : returnUrl;
            var req = new UrlRequest() { Url = returnUrl };
            req.HttpContext = this;
            IResult result = authenticateHandler.HandleRequest(req);
            return result.Continue;
        }

        public void Handle(IRequest request)
        {
            IsRun = true;
            try
            {
                request.HttpContext = this;
                authenticateHandler.HandleRequest(request);
                foreach (var handlerType in handlers)
                {
                    IRequestHandler handler = Activator.CreateInstance(handlerType) as IRequestHandler;
                    IResult result = handler.HandleRequest(request);
                    if (result != null)
                    {
                        result.Strip = ViewContainer;
                        result.View();
                        if (!result.Continue)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
            finally
            {
                IsRun = false;
            }
        }

        private bool callThreadIsRunnging = false;

        /// <summary>
        /// 页面数据已经准备好，对页面结构识别
        /// </summary>
        public void DOMReady() {
            if (!EnableTracing) return;            
            if (IsRun) return;
            logger.Info("domReady:" + CoreBrowser.LocationUrl);
            Thread t = new Thread(() =>
            {                
                Handle(new UrlRequest() { Url = CoreBrowser.LocationUrl, HttpContext = this });
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

      
        public void Call(string serviceType, object args) {
            //如果线程正在运行，则返回不处理
            if (callThreadIsRunnging) return;
            
            //这里不能使用多线程， 因为args是com对象，com对象必须在同一线程执行，多线程会导致
            //expando的数据无法访问        
            try
            {
                callThreadIsRunnging = true;
                IRequestProcessor handler = GetHandler(serviceType);
                IExpando expando = args as IExpando;
                IResult result = handler.Process(this, expando);
                if (result != null)
                {
                    result.Strip = ViewContainer;
                    result.View();
                }
            }          
            finally
            {
                callThreadIsRunnging = false;
            }
          
        }

        private IRequestProcessor GetHandler(string serviceType)
        {
            try
            {
                return Activator.CreateInstance(null, "AutoRobo.ClientLib.PageHooks.Handler." + serviceType).Unwrap() as IRequestProcessor;
            }
            catch (Exception ex) {
                logger.Fatal(ex);
                return null;
            }
        }

        public void Initialize()
        {           
//            handlers.Add(typeof(PageHandler));
            handlers.Add(typeof(AccountHandler));            
            handlers.Add(typeof(AjaxRequestHandler));    
            handlers.Add(typeof(MatchHandler));

            ServiceLocator.Instance.RegisterType(typeof(ICustomIdentity),
              typeof(CustomIdentity));
        }

        
    }
}
