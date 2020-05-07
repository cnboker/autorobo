using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.Core.Models;
using AutoRobo.WebApi;
using Util;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 检查邮箱
    /// </summary>
    public class CheckEmailHandler :  IRequestProcessor
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
       
        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            string url = string.Format(ServerApiInvoker.Domain + "tasks/GetLoginScriptByEmail?email={0}", Identity.MockUser.Email);
            string script = HttpRequestWapper.GetData(url, Identity.Ticket);
                      
            var model = new ModelSet(new LocalScriptRepository(script));
            
            var currentWorker = new ActionRunnable(model);
            currentWorker.Run(null, true);
          
            return new EmptyResult();
        }
    }
}
