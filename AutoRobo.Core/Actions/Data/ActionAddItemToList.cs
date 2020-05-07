using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using System.Xml;
using AutoRobo.Core.UserControls;
using AutoRobo.Core.Interface;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 增加单个元素数据到数组
    /// </summary>
    public class ActionAddItemToList : ActionFetchText
    {
        public override string ActionDisplayName
        {
            get { return "增加单项到数组" + ObjectName; }
        }

    
        public override void Perform()
        {            
            string value = GetValue(GetElement());
            if (!(string.IsNullOrEmpty(value) && FliterEmptyString))
            {
                ActionArrayVariable arr = VariableModel.Find<ActionArrayVariable>(ObjectName);
                if (arr == null) {
                    throw new ApplicationException("变量" + ObjectName + "未找到");
                }
                arr.AddItemToArray(value);
            }
        }

    }
}
