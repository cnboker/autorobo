using AutoRobo.Core.ActionBuilder;
using Util;
using WatiN.Core;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 增加多个元素数据到数组
    /// </summary>
    public class ActionAddListToList : ActionAddMutiContentToObject
    {
       
        public override void Perform()
        {
            List<string> list = GetMutiContent();
            if (list.Count > 0)
            {
                ActionArrayVariable var = VariableModel.Find<ActionArrayVariable>(ObjectName);
                if (var == null)
                {
                    throw new ApplicationException("变量" + ObjectName + "未找到，请确定变量" + ObjectName + "是否定义");
                }
                var.AddItemsToList(list);
            }
        }

        public override string ActionDisplayName
        {
            get { return "增加多项到数组" + ObjectName; }
        }
     
    }
}
