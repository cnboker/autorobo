using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.Core.Models;
using System.Data;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 把数组数据作为表列数据
    /// </summary>
    public class ActionAddListToTableColumn : ActionAddListToTable
    {     
        public override void Perform()
        {
            base.Perform();
            VariableModel.AddListToTableColumn(ObjectName, TableName);                     
        }

        public override string ActionDisplayName
        {
            get { return "增加表列数据"; }
        }

        public override string GetDescription()
        {
            return string.Format("将数组{0}作为表列数据添加到{1}", ObjectName, TableName);
        }
 
    }
}
