using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.UserControls;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 元素不存在增加空字符串到数据，主要为了补充位置
    /// </summary>
    public class ActionElementNotExistAddEmptyToList : ActionAddItemToList
    {
        public override string ActionDisplayName
        {
            get { return "元素不存在增加空字符串到变量" + ObjectName; }
        }
        
        public override void Perform()
        {
            Element element = GetElement();
            if (!element.Exists) {
                ActionArrayVariable arr = VariableModel.Find<ActionArrayVariable>(ObjectName);
                if (arr == null)
                {
                    throw new ApplicationException("变量" + ObjectName + "未找到");
                }
                arr.AddItemToArray("");
            }
        }
    }
}
