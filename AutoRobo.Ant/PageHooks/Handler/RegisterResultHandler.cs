using System.Collections.Generic;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using Newtonsoft.Json;
using Util;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    public class RegisterResultHandler : IRequestProcessor
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
            
        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            string modelString = expando.GetString("Model");
            string isRegister = expando.GetString("isRegister");
            ScriptObject so = JsonConvert.DeserializeObject<ScriptObject>(modelString);
            string url = ServerApiInvoker.Domain + "tasks/RegiserResult";
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("websiteId", so.WebSiteId);
            dict.Add("mockUserId", Identity.MockUser.Id);
            dict.Add("scriptId", so.Id);
            dict.Add("isOk", isRegister);
            HttpRequestWapper.Post_Data(url, dict, Identity.Ticket);
            context.EnableTracing = true;
            return new MatchHandler().HandleRequest(new UrlRequest() { Url = context.PageUrl, HttpContext = context });
          
        }
    }
}
