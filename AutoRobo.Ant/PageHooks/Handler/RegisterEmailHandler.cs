using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageServices;
using Newtonsoft.Json;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.Core;
using AutoRobo.WebApi;
using Util;
using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.Core.Models;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 注册邮箱
    /// </summary>
    public class RegisterEmailHandler : IRequestProcessor
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }

        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            string url = string.Format(ServerApiInvoker.Domain + "tasks/GetScriptByEmail?email={0}", Identity.MockUser.Email);
            string script = HttpRequestWapper.GetData(url, Identity.Ticket);
            LocalScriptRepository rep = new LocalScriptRepository(script);
            var model = new ModelSet(rep);
            
            var currentWorker = new ActionRunnable(model);
            currentWorker.Run(null, true);

         
            return new RegisterEmailResultView();
        }
    }
}
