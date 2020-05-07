using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.WebApi.Entities;
using Util;
using AutoRobo.Core;
using WatiN.Core;
using WatiN.Core.Constraints;
using System.Linq;

namespace AutoRobo.ClientLib.PageHooks.Handler.Recognize
{
    public class CssRecognizer : IRecognizer
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CssRecognizer));
       

        /// <summary>
        /// 注入脚本或执行脚本必须再主线程执行，否则会导致
        /// System.Runtime.InteropServices.InvalidComObjectException: COM 对象与其基础 RCW 分开后就不能再使用。
        ///在 mshtml.HTMLDocumentClass.IHTMLDocument2_get_parentWindow()
        ///的错误
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private bool Any(string selector)
        {
            //返回主线程
            //if (Context.CoreBrowser.InvokeRequired) {
            //    return (bool)Context.CoreBrowser.Invoke(new Func<string, bool>(Any), selector);
            //}

            //return Convert.ToBoolean(Context.Browser.Eval(string.Format("$jq('{0}').any();", selector)));
            //logger.Info("current thread is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return Context.CoreBrowser.SelectAny(selector);
        }


        public bool Recognize(ScriptObject so)
        {  
            //remove comments // or /**/
            string selector = System.Text.RegularExpressions.Regex.Replace(so.Similarity, "^\\s*//.*$", "");
            selector = System.Text.RegularExpressions.Regex.Replace(selector, "/\\*.*?\\*/", "");
            selector = selector.Replace("\n", "");
            logger.Info("selector:" + selector);
            return Context.CoreBrowser.SelectAny(selector);
        }


        public FakeHttpContext Context
        {
            get;
            set;
        }
    }
}
