using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.Logger
{
    public class HealthyTracer
    {
       

        public HealthyTracer() {
            
        }

        /// <summary>
        /// 收集异常Action
        /// </summary>
        /// <param name="action"></param>
        public virtual void Report(ActionBase action, Exception ex) {
                      
        }

        public virtual void Report(Exception ex)
        {
            
        }

        protected ActionException CreateActionException(ActionBase action, Exception ex) {
            ActionException aex = new ActionException();
            aex.ActionId = action.ElementName;
            aex.ActionType = action.GetType().Name;
            //aex.ActionIndex = wscript.ActionItems.GetActiveIndex(action);
            aex.Message = ex.Message + "\n" + ex.StackTrace;
            //aex.ScriptId = wscript.ScriptId;
            aex.ActionLable = action.Title;
            return aex;
        }
    }
}
