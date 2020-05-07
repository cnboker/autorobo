using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 增加数组数据到表行
    /// </summary>
    public class ActionAddListToTableRow : ActionAddListToTable
    {
       
        public override string ActionDisplayName
        {
            get { return "增加表行数据"; }
        }

        public override string GetDescription()
        {
            return string.Format("将数组{0}作为表行数据添加到{1}(水平添加数据)", ObjectName, TableName);
        }
        

        public override void Perform()
        {
            base.Perform();
            this.VariableModel.AddListToTableRow(ObjectName, TableName);
        }

     
    }
}
