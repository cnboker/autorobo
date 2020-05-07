using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices.Expando;
using System.Reflection;
using Newtonsoft.Json;

namespace AutoRobo.ClientLib.Test
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class ScriptCall
    {
        //public string Call(object arg)
        //{
        //    IExpando expando = arg as IExpando;
        //    PropertyInfo property = expando.GetProperty("Name", BindingFlags.Instance);
        //    string name = property.GetValue(expando, null).ToString();
        //    property = expando.GetProperty("Age", BindingFlags.Instance);
        //    int age = Convert.ToInt32(property.GetValue(expando, null));
             
        //    //return new Newtonsoft.Json.JavaScriptObject() {  };
        //    JavaScriptObject jso = new JavaScriptObject();
        //    jso["Name"] = name;
        //    jso["Age"] = age;
        //    return JavaScriptConvert.SerializeObject(jso);
        //}

        public object Test(string test) {
            return test;
        }
    }
}
