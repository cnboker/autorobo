using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutoRobo.Core
{
    public class Method<T>
    {
        static private log4net.ILog logger = log4net.LogManager.GetLogger("Watcher");

        static public  void Watch(string message, Action action){
            if (action == null) return;
            DateTime now = DateTime.Now;
            action();            
            logger.Info(message + " elapse time:" + DateTime.Now.Subtract(now).TotalMilliseconds.ToString());
        }

        static public T Watch(string message, Func<T> func)
        {            
            DateTime now = DateTime.Now;
            T t = func();
            logger.Info(message);
            logger.Info("elapse time:" + DateTime.Now.Subtract(now).TotalMilliseconds.ToString());
            return t;
        }

   
    }
}
