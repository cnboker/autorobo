using System;
using System.Linq;
using AutoRobo.Core;
using AutoRobo.WebApi.Entities;
using WatiN.Core;

namespace AutoRobo.ClientLib.PageHooks.Handler.Recognize
{
    public class XPathRecognizer : IRecognizer
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(XPathRecognizer));
     
        public bool Recognize(ScriptObject so)
        {
            bool compare = false;
            string[] paths = so.Similarity.Split("\n".ToArray());
            foreach (var o in paths)
            {
                if (string.IsNullOrEmpty(o)) continue;
                logger.Info("xpath:" + o);
                var elements = Context.Browser.GetElementsByXPath(o);
                compare = elements.Count > 0;
                if (compare) break;
            }
            return compare;
        }

        public FakeHttpContext Context
        {
            get;
            set;
        }
    }
}
