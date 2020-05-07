using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.WebApi;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    public class RegisterEmailResultHandler : IRequestProcessor
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
      
        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            bool isRegister = expando.GetString("IsRegister") == "checked";
            ICustomIdentity identity = ServiceLocator.Instance.GetService<ICustomIdentity>();
            identity.MockUser.IsActiveMail = isRegister;
            ServerApiInvoker.PostEmailRegisterStatue(identity.MockUser.Id, isRegister);
            return new MatchHandler().HandleRequest(new UrlRequest() { Url = context.PageUrl, HttpContext = context });
        }
    }
}
