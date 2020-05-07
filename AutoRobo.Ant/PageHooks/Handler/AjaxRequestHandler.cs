using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageServices;
using WatiN.Core;
using AutoRobo.WebApi;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.Core;
using Util;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 页面AJAX请求监视器
    /// </summary>
    public class AjaxRequestHandler : IRequestHandler
    {
        log4net.ILog logger = log4net.LogManager.GetLogger("RegisterHandler");
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }

        private static string ajaxMoniterJavascript = "";
        private FakeHttpContext context;

        public IResult HandleRequest(IRequest req)
        {
            context = req.HttpContext;
         
            StartJQueryAjaxMonitoring(req.Url);
            return new EmptyResult();
        }

        /// <summary>
        /// 启动ajax监视器
        /// </summary>
        protected void StartJQueryAjaxMonitoring(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(ajaxMoniterJavascript))
                {
                    //ajaxMoniterJavascript = HttpRequestWapper.GetData(StringHelper.Domain + "/asserts/ajaxMonitorForWatin.js");
                    ajaxMoniterJavascript = ScriptLoaderExtend.GetAjaxMoniterInstallScript();
                }

                //string keywords = WebsiteService.GetFilterKeywords(url);
                //var script = ajaxMoniterJavascript;
                //if (!string.IsNullOrEmpty(keywords))
                //{
                //    string regex = string.Format(@"/\s({0}[^\s]*)|^({0}[^\s]*)/gi", keywords);
                //    script = ajaxMoniterJavascript.Replace("#regex#", regex);
                //}
                //else {
                //    script = ajaxMoniterJavascript.Replace("#regex#", "");
                //}

                context.Browser.InjectScript(ajaxMoniterJavascript);            
            }
            catch (Exception ex) {              
                logger.Fatal(ex);
            }
        }

        /// <summary>
        /// 等待ajax全部加载完成
        /// </summary>
        protected void WaitForAjaxComplete()
        {
            context.Browser.Wait(() => !Convert.ToBoolean(
                context.Browser.Eval("ajaxMonitorForWatiN.asyncInProgress()")));
        }
   
    
    }
}
