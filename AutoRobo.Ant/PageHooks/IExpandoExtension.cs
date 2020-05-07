using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.Expando;
using System.Reflection;
using System.Threading;
using System.ComponentModel;

namespace AutoRobo.ClientLib.PageHooks
{
    public static class IExpandoExtension
    {
        public static bool Contain(this IExpando expando, string key) { 
            
             PropertyInfo property = expando.GetProperty(key, BindingFlags.Instance);
             if (property == null)
             {
                 FieldInfo f = expando.GetField(key, BindingFlags.Instance);
                 if (f == null)
                 {
                     return false;
                 }                 
             }
             return true;
        }

        public static int GetInt(this IExpando expando, string key)
        {            
            var val = GetValue(expando, key);
            return val == null ? 0 : Convert.ToInt32(val);
        }

        public static bool GetBoolean(this IExpando expando, string key)
        {
            var val = GetValue(expando, key);
            return val == null ? false : Convert.ToBoolean(val);
        }

        public static string GetString(this IExpando expando, string key)
        {
            var val = GetValue(expando, key);
            return val == null ? "" : val.ToString();
        }

        public static DateTime GetDate(this IExpando expando, string key)
        {
            var val = GetValue(expando, key);
            return val == null ? DateTime.MinValue : Convert.ToDateTime(val);            
        }

        public static object GetValue(this IExpando expando, string key)
        {
           
            PropertyInfo property = expando.GetProperty(key, BindingFlags.Instance);
            if (property == null) {
                FieldInfo f = expando.GetField(key, BindingFlags.Instance);
                if (f == null) {
                    return null;
                }
                return f.GetValue(expando);
            }
            return property.GetValue(expando, null);
        }
    }
}
