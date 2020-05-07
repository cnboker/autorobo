using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 采集多项到表列
    /// </summary>
    public class ActionAddMutiContentToTableColumn : ActionAddMutiContentToObject
    {
        public override string ActionDisplayName
        {
            get { return "采集多项到表" + ObjectName + "列"; }
        }

        public override void Perform()
        {
            List<string> list = GetMutiContent();
            if (list.Count > 0)
            {
                ActionTableVariable var = VariableModel.Find<ActionTableVariable>(ObjectName);
                if (var == null)
                {
                    throw new ApplicationException("变量" + ObjectName + "未找到，请确定变量" + ObjectName + "是否定义");
                }
                var.AddListToTableColumn(list);
            }
        }
    }
}
