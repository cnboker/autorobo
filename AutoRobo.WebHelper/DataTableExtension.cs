using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace System.Data
{
    static public class DataTableExtension
    {
        static public DataTable ConvertListToDataTable(string columnName, List<string> list)
        {
            // New table.
            DataTable table = new DataTable();
            table.Columns.Add(columnName);
            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }
            return table;
        }
        /// <summary>
        /// 通过列生成列名称
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string ExcelColumnFromNumber(int column)
        {
            string columnString = "";
            decimal columnNumber = column;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }
        /// <summary>
        /// 通过列名称获取列数
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static int NumberFromExcelColumn(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        /// <summary>
        /// 拓展行
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowIndex"></param>
        static public void ExpandRow(this DataTable table, int rowIndex)
        {
            var drs = table.Rows;
            while (rowIndex >= drs.Count)
            {
                DataRow dr = table.NewRow();
                table.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 拓展列
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnIndex"></param>
        static public void ExpandColumn(this DataTable table, int columnIndex)
        {
            //如果起始列大于总列数则追加列
            while (columnIndex >= table.Columns.Count)
            {
                var column = new DataColumn(ExcelColumnFromNumber(table.Columns.Count + 1), typeof(string));
                table.Columns.Add(column);
            }
        }

        /// <summary>
        /// 转换表格到json
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DataTableToJSON(DataTable table)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(list);
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }


    }
}
