using System.Collections.Generic;
using System.Linq;
using WatiN.Core;
using AutoRobo.WebApi.Entities;
using AutoRobo.Core;
using AutoRobo.Core.Actions;

namespace AutoRobo.ClientLib.PageHooks.Handler.Recognize
{
    public class HashCodeRecognizer : IRecognizer
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AutoLoginHandler));

        public bool Recognize(ScriptObject so)
        {           
            string[] arr = so.Similarity.Split(",".ToArray());
            var inputs = Context.Browser.GetElementsByTag("input");
            List<string> inputXPaths = new List<string>();
            foreach (var o in inputs)
            {
                string xpath = XPathFinder.GetXPath(o);            
                inputXPaths.Add(xpath.GetHashCode().ToString());
            }
            //var inputs = context.Browser.TextFields.Select(c => XPathFinder.GetXPath(c).GetHashCode().ToString()).ToArray();
            return arr.Intersect(inputXPaths).Count() > 0;
        }


        public FakeHttpContext Context
        {
            get;
            set;
        }
    }
}
