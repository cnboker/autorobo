using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using AutoRobo.WebApi;
using Util;
using AutoRobo.Core;
using IfacesEnumsStructsClasses;
using System.Windows.Forms;
using Newtonsoft.Json;
using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.ClientLib.PageServices;

namespace AutoRobo.ClientLib.PageHooks.Views
{
    /// <summary>
    /// TaskPanel
    /// </summary>
    public class ViewResult : IResult
    {

        public bool Continue
        {
            get;
            set;
        }

        public object Model
        {
            get;
            set;
        }
        public IStripContainer Strip
        {
            get;
            set;
        }

        public ICustomIdentity Identity {
            get {
                ICustomIdentity identity = ServiceLocator.Instance.GetService<ICustomIdentity>();
                return identity;
            }
        }

        public virtual void View()
        {
              
        }

        public void ActionClick(IFunStripItem item)
        {
            Microsoft.JScript.JSObject js = new Microsoft.JScript.JSObject();
            ActionClick(item, js);
        }

        public void ActionClick(IFunStripItem item, Microsoft.JScript.JSObject js)
        {
            if (item.Model != null)
            {
                if (item.Model is string)
                {
                    js.SetMemberValue2("Model", item.Model);
                }
                else
                {
                    js.SetMemberValue2("Model", JsonConvert.SerializeObject(item.Model));
                }
            }
            Strip.Context.Call(item.ProcessHandler, js);
        }

        protected string GetProcessHandler(string scriptType)
        {
            ScriptTypeEnum pr = (ScriptTypeEnum)Convert.ToInt16(scriptType);
            if (pr == ScriptTypeEnum.Thread)
            {
                return "MatchResultHandler";
            }
            else if (pr == ScriptTypeEnum.Login)
            {
                return "LoginHandler";
            }         
            else if (pr == ScriptTypeEnum.Register)
            {
                return "RegisterHandler";
            }
            else {
                return "";
                //throw new ApplicationException(pr.ToString() + "对于的处理类未找到");
            }
        }
    }
}
