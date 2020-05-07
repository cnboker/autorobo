using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.Core.Models;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    public class LoginHandler : IRequestProcessor
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }        
       
        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {          
            ScriptRepository rep = new ScriptRepository(context.PageUrl);
            rep.ReadScriptType = ScriptTypeEnum.Login;

            var model = new ModelSet(rep);
            
            var currentWorker = new ActionRunnable(model);
            currentWorker.Run(null, true);

            return new EmptyResult() { Continue = false };
        }
      
    }
}
