using AutoRobo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    public class ActionAddItemToStringVariable : ActionFetchText
    {
       
        public override string ActionDisplayName
        {
            get { return "增加单项到字符串变量" + ObjectName; }
        }

        public override void Perform()
        {
            string value = GetValue(GetElement());
            if (!(string.IsNullOrEmpty(value) && FliterEmptyString))
            {
                ActionStringVariable arr = VariableModel.Find<ActionStringVariable>(ObjectName);
                if (arr == null)
                {
                    throw new ApplicationException("变量" + ObjectName + "未找到");
                }
                arr.Data = value;
            }
        }

    }
}
