using System;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.Core;
using WatiN.Core;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 是否保存密码提示页面
    /// </summary>
    public class AutoLoginHandler : IRequestHandler
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AutoLoginHandler));

        public IResult HandleRequest(IRequest req)
        {
            if (req.HttpContext.CoreBrowser.SelectAny("input:password"))
            {
                RestoreData(req);
            }
            return new EmptyResult();
        }

        private void RestoreData(IRequest req)
        {
            //PasswordRecovery pr = new PasswordRecovery();
            //string url = (new Uri(req.Url)).Host;
            //string data = pr.Recovery(url);
            //if (string.IsNullOrEmpty(data)) return;
            //req.HttpContext.Browser.RunScript(
            //    string.Format("$jq('input:password').get(0).form.deserialize('{0}',false);", data));
        }

    }
}
