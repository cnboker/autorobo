using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.WebApi;
using AutoRobo.Core.Logger;

namespace AutoRobo.ClientLib.DataSource
{
    public class SqlHealthyTracer : HealthyTracer
    {
        public SqlHealthyTracer()  { }

        public override void Report(Core.Actions.ActionBase action, Exception ex)
        {
            ActionException aex = CreateActionException(action, ex);
            ServerApiInvoker.ExceptionReport(aex.ScriptId, aex.ActionId
                , aex.Message, aex.ActionIndex.ToString(), aex.ActionType, action.Title);            
        }

        public override void Report(Exception ex)
        {
            
        }
    }
}
