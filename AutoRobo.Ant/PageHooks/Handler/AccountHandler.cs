using System.Collections.Generic;
using System.Web.Caching;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using Util;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 绑定虚拟账户处理
    /// </summary>
    public class AccountHandler : IRequestHandler
    {
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
        //private bool QQ;

        public IResult HandleRequest(IRequest req)
        {
            TaskToolStrip strip = req.HttpContext.ViewContainer as TaskToolStrip;
            if (strip == null) return new EmptyResult() { Continue = true };       
            List<MockUser> users = GetMockUsers(Identity.UserId);
            bool result = HasStaff(req.Url);
            if (!result) {
                users = Filter(users);
            }
            strip.AccountBind(users);
            return new EmptyResult() { Continue = true };
        }

        public bool HasStaff(string url)
        {
            string domain = StringHelper.GetRootDomain(StringHelper.GetDomain(url));
            return HttpRequestWapper.GetBoolean(ServerApiInvoker.Domain +
               string.Format("tasks/HasStaff?url={0}", domain),
                   ""
               );
        }


        public List<WebApi.Entities.MockUser> GetMockUsers(string userid)
        {
            return CacheExtensions.Data(userid, () =>
            {
                return ServerApiInvoker.Get_MockUsers(userid);
            });
        }

        public List<MockUser> Filter(List<MockUser> mokerUsers)
        {
            List<MockUser> list = new List<MockUser>();
            foreach (MockUser user in mokerUsers)
            {
                if (user.Email.ToLower().IndexOf("@qq.com") > 0)
                {
                    list.Add(user);
                }
            }
            return list;
        }
    }
}
