using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.WebApi;
using AutoRobo.ClientLib.PageHooks.Views;
using Util;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 检查当前外部网站是否有脚本可用，如果有则
    /// </summary>
    public class PageHandler : IRequestHandler
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
     
        public IResult HandleRequest(IRequest req)
        {
            bool result = HasStaff(req.Url);
            
            if (!result) return new EmptyResult() { Continue = false };            
            return new EmptyResult();
        }

        public bool HasStaff(string url)
        {
            string domain = StringHelper.GetRootDomain(StringHelper.GetDomain(url));
            return HttpRequestWapper.GetBoolean(ServerApiInvoker.Domain +
               string.Format("tasks/HasStaff?url={0}", domain),
                   ""
               );
        }

    }
}
