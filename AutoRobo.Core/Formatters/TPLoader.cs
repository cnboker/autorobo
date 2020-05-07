using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Formatters
{
    public class TPLoader
    {
        private static TPLoader loader = null;
        private Dictionary<string, string> cache = new Dictionary<string, string>();

        public static TPLoader Instance {
            get {
                if (loader == null) {
                    loader = new TPLoader();
                }
                return loader;
            }
        }

        TPLoader() { }

        public string ReadTemplate(string actionClassName)
        {
            if (cache.ContainsKey(actionClassName)) return cache[actionClassName];
            Stream stream = System.Reflection.Assembly.GetAssembly(typeof(TPLoader)).
                  GetManifestResourceStream(string.Format("AutoRobo.Core.Formatters.CasperjsTemplate.{0}.st", actionClassName));
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(stream);
                string str = sr.ReadToEnd();
                cache.Add(actionClassName, str);
                return str;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }
    }
}
