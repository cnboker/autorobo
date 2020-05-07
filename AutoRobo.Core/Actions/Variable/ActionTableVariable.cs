using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Models;
using System.Data;
using AutoRobo.DataMapping;
using AutoRobo.WebHelper;
using Jint.Native;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 表变量
    /// </summary>
    public class ActionTableVariable : ActionVariable
    {
        private DataTable Table { get; set; }
       
        public ActionTableVariable() {
            Table = new DataTable();
            Direction = VariableDirection.Output;
        }

        public ActionTableVariable(VariableDirection direct):this() {      
            Direction = direct;
        }


        public override string GetTypeName()
        {
            return "表格";
        }
    
        public override void Reset()
        {
            if (Table != null) {                
                Table.Rows.Clear();
                Table.Columns.Clear();
            }
        }

        public IMapAttribute GetMapAttributeSchema()
        {

            DataMap dataMap = new DataMap();
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                dataMap.Fields.Add(new DataMapField() { DisplayName = Table.Columns[i].ColumnName });
            }
            return dataMap;
        }
        /// <summary>
        /// 数据表行转换为IMapAttribute
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public IMapAttribute GetMapAttribute(int rowIndex)
        {
            DataRow dr = Table.Rows[rowIndex];
            DataMap dataMap = new DataMap();
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                dataMap.Fields.Add(new DataMapField() { DisplayName = Name + "." + Table.Columns[i].ColumnName, Value = dr[i].ToString() });
            }
            return dataMap;
        }

        /// <summary>
        /// 增加数组到表行
        /// </summary>
        /// <param name="list"></param>
        public void add(object array) {

            JsArray arr = array as JsArray;
            List<string> list = new List<string>();
            for (int i = 0; i < arr.Length; i++) {
                list.Add(arr.get(i).ToString());
            }

            Table.ExpandRow(Table.Rows.Count);
            DataRow dr = Table.Rows[Table.Rows.Count-1];
            for (int i = 0; i < list.Count; i++)
            {
                int col = i;
                Table.ExpandColumn(col);
                dr[col] = string.IsNullOrEmpty(list[i]) ? "" : list[i];
            }
            OnDataUpdate(Table);
        }
    
        /// <summary>
        /// 将数组数据作为行添加到表格（水平添加数据）
        /// </summary>      
        public void AddListToTableRow(List<string> items)
        {
            
            int startColumnIndex = 0;
            int startRowIndex = Table.Rows.Count;
            Table.ExpandRow(startRowIndex);
            DataRow dr = Table.Rows[startRowIndex];
            for (int i = 0; i < items.Count; i++)
            {
                int col = i + startColumnIndex;
                Table.ExpandColumn(col);
                dr[col] = (items[i] == null ? "" : items[i]);
            }
            
            OnDataUpdate(Table);
            
        }

        /// <summary>
        /// 将数组数据作为列添加到表格（垂直添加数据)
        /// </summary>
        /// <param name="tableName">添加的表格名称</param>
        /// <param name="columnName">列名</param>
        /// <param name="startRow">表格起始行</param>
        /// <param name="items">需要添加的数据</param>
        public void AddListToTableColumn(List<string> items)
        {
            int startColumnIndex = Table.Columns.Count;
            Table.ExpandColumn(startColumnIndex);            
            for (int i = 0; i < items.Count; i++)
            {
                Table.ExpandRow(i);
                Table.Rows[i][startColumnIndex] = (items[i] == null ? "" : items[i]);
            }
            Table.EndLoadData();
            OnDataUpdate(Table);
            
        }

        public override string GetDescription()
        {
            return "存储表格数据";
        }

        public override string ToString()
        {
            return "";
        }

        public override object Data
        {
            get {
                if (string.IsNullOrEmpty(Table.TableName)) {
                    Table.TableName = Name;
                }
                return Table; 
            }
            set {
                Table = (DataTable)value;
            }
        }

      
    }
}
