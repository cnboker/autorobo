using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Linq;

namespace AutoRobo.Core.Plugins
{
    public class PluginLoader
    {
        private List<Type> ipi = null;
        private static PluginLoader loader = null;
  
        static public PluginLoader Loader {
            get {
                if (loader == null) {
                    loader = new PluginLoader();
                }
                return loader;
            }
        }
        private PluginLoader() { }
        /// <summary>
        /// 通过名称获取插件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IPlugin Query(string className) {
            if (ipi == null)
            {
                Load();
            }
            Type t =  ipi.Where(c => c.Name == className).Single();
            return (IPlugin)Activator.CreateInstance(t);         
        }

        private void Load() {            
            ipi = new List<Type>();
            Assembly ass = Assembly.LoadFrom("AutoRobo.PlulgIn.dll");
            if (ass != null)
            {
                Type[] types = ass.GetTypes();
                foreach (Type t in types)
                {
                    if (t.GetInterface(typeof(IPlugin).FullName) != null)
                    {
                        ipi.Add(t);
                    }
                }
            }      
        }


    }
}
