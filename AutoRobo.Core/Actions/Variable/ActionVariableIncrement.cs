using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    public class ActionVariableIncrement : ActionVariableReference
    {
      
        public override string ActionDisplayName
        {
            get { return string.Format("变量{0}加1",VariableName); }
        }

        public override void Perform()
        {
            AppContext.ActionModel.VariableActionModel.Find<ActionIntegerVariable>(VariableName).Increment();
        }

        public override string GetDescription()
        {
            return "执行一次加1";
        }


    }
}
