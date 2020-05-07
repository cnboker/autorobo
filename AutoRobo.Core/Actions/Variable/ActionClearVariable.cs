using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls;

namespace AutoRobo.Core.Actions
{
    public class ActionClearVariable : ActionVariableReference
    {
        public override string ActionDisplayName
        {
            get { return string.Format("清除数组{0}变量数据", VariableName); }
        }

        public override void Perform()
        {
            AppContext.ActionModel.VariableActionModel.Find<ActionVariable>(VariableName).Reset();
        }

        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSelectVariable(editorAction);
        }

        public override string GetDescription()
        {
            return "清除数组变量数据"; 
        }

    
    }
}
