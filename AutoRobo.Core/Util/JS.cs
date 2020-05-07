using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.Expando;
using System.Reflection;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo
{
    public static class JS
    {
        public static object FunEval(Document document, string code) {
            return Eval(document, "(function(){" + code + " })();");
        }

        public static object Eval(Document document, string code)
        {
            IExpando window = GetWindow(document);
            PropertyInfo property = JS.GetOrCreateProperty(window, "__lastEvalResult");

            document.RunScript("window.__lastEvalResult = " + code + ";");

            return property.GetValue(window, null);
        }

        private static PropertyInfo GetOrCreateProperty(IExpando expando, string name)
        {
            PropertyInfo property = expando.GetProperty(name, BindingFlags.Instance);
            if (property == null)
                property = expando.AddProperty(name);

            return property;
        }

        private static IExpando GetWindow(Document document)
        {
            IEDocument ieDoc = document.NativeDocument as IEDocument;
            return ieDoc.HtmlDocument.parentWindow as IExpando;
        }
    }
}
