using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using AutoRobo.ClientLib.PageHooks.Handler.Recognize;
using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using Util;
using WatiN.Core;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 通过页面结构识别
    /// </summary>
    public class MatchHandler : IRequestHandler, IRequestProcessor
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MatchHandler));
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
   
        protected FakeHttpContext context;
        /// <summary>
        /// 检查是否有注册和登录脚本逻辑
        /// </summary>
        //private bool requireLogin = false;
        private IRequest req = null;

        public IResult HandleRequest(IRequest req)
        {
            this.req = req;
            this.context = req.HttpContext;
            return Match(req.Url);
        }

        private IResult Match(string url)
        {
            List<ScriptObject> jso = GetScripts(url);
            foreach (var so in jso) {
                if ((ScriptTypeEnum)Convert.ToInt16(so.ScriptType) == ScriptTypeEnum.Register) {
                    if (IsRegister(so.WebSiteId, Identity.MockUser.Id)) {
                        jso.Remove(so);
                        break;
                    }
                }
            }
              //获取近似度不为空的脚本部件
            //jso = jso.Where(c => !string.IsNullOrEmpty(c.Similarity)).ToList();
            //List<ScriptObject> sos = SimilartyCompare(jso);
            return new DefaultView() { Model = jso, Continue = false };
        }

        private bool IsRegister(string websiteId, string userid)
        {
            return HttpRequestWapper.GetBoolean(ServerApiInvoker.Domain +
                string.Format("tasks/IsRegister?websiteId={0}&userid={1}", websiteId, Identity.MockUser.Id),
                    Identity.Ticket
                );
        }

        private List<WebApi.Entities.ScriptObject> GetScripts(string url)
        {
            url = StringHelper.GetDomain(url);
            return CacheExtensions.Data(url, () =>
            {
                return ServerApiInvoker.GetScriptObjectByUrl(url);
            });
        }
          
        /// <summary>
        /// 处理ajax事件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="expando"></param>
        /// <returns></returns>
        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            this.context = context;
            string evt = expando.GetString("event");
            logger.Info("evt:" + evt);
            logger.Info("keywords:" + expando.GetString("keywords"));
            if (evt == "ajaxComplete" || evt == "iframeLoaded")
            {
                return Match(context.PageUrl);
            }
            return new EmptyResult();
        }


   
        /// <summary>
        /// 检查有无匹配的脚本资源
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private List<ScriptObject> SimilartyCompare(List<ScriptObject> sol)
        {
            if (sol == null) return null;
            DateTime now = DateTime.Now;
            List<ScriptObject> result = new List<ScriptObject>();
            foreach (var so in sol)
            {
                bool b = SimilartyCompare(so);
                if (b)
                {
                    result.Add(so);
                }
            }
            logger.Info("SimilartyCompare elapse time:" + DateTime.Now.Subtract(now).TotalMilliseconds);
            return result;
        }
    
     
        /// <summary>
        /// 当前页与预定值比较相似度
        /// 非提交数据页面，类似页面后台需要找一个有特点标签生成xpath作为相似度比较依据
        /// 提交数据页面比较input标签xpath的hashcode
        /// </summary>
        /// <param name="source">返回相同数目</param>
        /// <returns></returns>
        public bool SimilartyCompare(ScriptObject so)
        {
            //logger.Info("current thread is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            PageRecognizeEnum pr = (PageRecognizeEnum)Convert.ToInt16(so.PageRecognize);
            try
            {
                return Recognizer.Create(this.context, pr).Recognize(so);        
            }
            catch (Exception ex)
            {
                //处理mshtml.HTMLDocumentClass.IHTMLDocument2_get_all()异常
                //System.Runtime.InteropServices.InvalidComObjectException: COM 对象与其基础 RCW 分开后就不能再使用
                //MyBrowser cb = context.CoreBrowser;
                //while (cb.Busy)
                //{
                //    System.Threading.Thread.Sleep(100);
                //}
                ////返回主线程执行脚本
                //if (cb.InvokeRequired)
                //{
                //    return (bool)cb.Invoke(new Func<ScriptObject, bool>(SimilartyCompare), so);
                //}
                logger.Fatal(ex);
                //返回false，则重新执行，直到超时
                return false;
            }    
        }
        
    }
}
