using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Xml;
using AutoRobo.Core.UserControls;
using AutoRobo.DataMapping;
using AutoRobo.UserControls;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 通过数组遍历
    /// </summary>
    public class ActionForeachByVariable : ActionDataBlock
    {       
        /// <summary>
        /// 数组名称
        /// </summary>
        public string VarName { get; set; }        
        public bool Reverse { get; set; }     
        /// <summary>
        /// 变量前N条数据, -1表示忽略该条件
        /// </summary>
        public int TopN { get; set; }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucLoopByArray(editorAction);
        }  

        public override string ActionDisplayName
        {
            get { return string.Format("通过数组{0}遍历",VarName); }
        }

        public override string GetDescription()
        {
            return "通过数组遍历";
        }

        public override void Perform()
        {
            ActionVariable var = AppContext.ActionModel.VariableActionModel.Find(VarName);
            //遍历数组变量
            if (var is ActionArrayVariable)
            {
                ArrayPerform(var);
            }
            else if (var is ActionTableVariable)
            {
                TablePerform(var);
            }
            else if (var is ActionIntegerVariable)
            {
                IntegerPerform(var);
            }           
        }

        private void IntegerPerform(ActionVariable var)
        {
            int val = (int)var.Data;
            for (int i = 0; i < val; i++) {
                base.Perform();
            }
        }

        private void ArrayPerform(ActionVariable var)
        {
            ActionArrayVariable arrVar = var as ActionArrayVariable;            
            if (Reverse) {
                arrVar.Reverse();
            }
            int count = arrVar.Count;
            if (TopN > 0 && TopN < count) {
                count = TopN;
            }
            for (int i = 0; i < count; i++)
            {
                MapAttribute  = arrVar.GetMapAttribute(i);                               
                base.Perform();
            }   
        }

        private void TablePerform(ActionVariable tableVar)
        {
            ActionTableVariable var = tableVar as ActionTableVariable;
            DataTable data = var.Data as DataTable;
            if (Reverse) {
                data = ReverseRowsInDataTable(data);
            }
            int count = data.Rows.Count;
            if (TopN > 0 && TopN < count)
            {
                count = TopN;
            }
            for (int i = 0; i< count ; i++)
            {
                MapAttribute = var.GetMapAttribute(i);               
                base.Perform();
            }       
        }

        public DataTable ReverseRowsInDataTable(DataTable inputTable)
        {
            DataTable outputTable = inputTable.Clone();

            for (int i = inputTable.Rows.Count - 1; i >= 0; i--)
            {
                outputTable.ImportRow(inputTable.Rows[i]);
            }

            return outputTable;
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            VarName = GetAttribute(node, "VarName");
            Reverse = GetBooleanAttribute(node, "Reverse");
            TopN = GetIntAttribute(node, "TopN");            
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("VarName", VarName);
            writer.WriteAttributeString("Reverse", Reverse ? "1" : "0");            
            writer.WriteAttributeString("TopN", TopN.ToString());
            base.WriterAddAttribute(writer);
        }

      
    }
}
