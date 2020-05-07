using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using Newtonsoft.Json;
using Util;
using System;
using AutoRobo.WebApi;
using System.Collections.Generic;
using AutoRobo.WebApi.Entities;
using AutoRobo.Core;
using Newtonsoft.Json.Linq;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 每次URL请求都传入这里检查ICustomIdentity是否获取授权；
    /// 如果没有授权则创建隐藏iframe重新请求验证信息，未获取到验证信息则redirect到www.cnboker.com进行登录授权，
    /// </summary>
    public class AuthenticateHandler : IRequestHandler, IRequestProcessor
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
      
        public IResult HandleRequest(IRequest req)
        {           
            if (!Identity.IsAuthenticated) {
                req.HttpContext.Browser.GoTo(StringHelper.Domain + "/account/ticket?returnUrl=" + req.Url);
                return new EmptyResult() { Continue = false };
            }
            return new EmptyResult();
        }

        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            if (expando.Contain("IsAuthenticated"))
            Identity.IsAuthenticated = expando.GetBoolean("IsAuthenticated");
            
            if (expando.Contain("UserId"))
            Identity.UserId = expando.GetString("UserId");
            if(expando.Contain("Ticket")){       
                Identity.Ticket = expando.GetString("Ticket");
            }
            UpdateMockuser();
            
            return new EmptyResult();
        }

        private void UpdateMockuser()
        {
            JObject jso = CreateOrGetMockUser(Identity.Ticket, Identity.UserId);

            Identity.MockUser = new MockUser()
            {
                Email = jso["Email"].ToString(),
                Password = jso["Password"].ToString(),
                UserName = jso["UserName"].ToString(),
                Id = jso["Id"].ToString(),
                UserId = jso["UserId"].Value<string>(),
                Mobile = jso["Mobile"].Value<string>(),
                CompanyName = jso["CompanyName"].Value<string>(),
                Description = jso["Description"].Value<string>(),
                IsActiveMail = jso["IsActiveMail"].Value<bool?>()
            };
           
        }

        private JObject CreateOrGetMockUser(string ticket, string userId)
        {
            string url = ServerApiInvoker.Domain + "tasks/CreateOrGetMockUser";
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("userId", userId);
            string json = HttpRequestWapper.Post(url, dict, ticket);
            System.Diagnostics.Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<JObject>(json);
        }
    }
}
