using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Models;
using AutoRobo.Core.UserControls;

namespace AutoRobo.Core.Actions
{
    public class ActionVariableDecrement : ActionVariableReference
    {        
        public override string ActionDisplayName
        {
            get { return string.Format("变量{0}减1", VariableName); }
        }

        public override void Perform()
        {
            ActionIntegerVariable action = AppContext.ActionModel.VariableActionModel.Find<ActionIntegerVariable>(VariableName);
            action.Decrement();
        }

        public override string GetDescription()
        {
            return "执行一次减1";
        }

     
    }
}
