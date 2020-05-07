using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text;

namespace AutoRobo.Core
{
    public class ExcelWriter
    {
        private const string QUOTE = "\"";
        private const string ESCAPED_QUOTE = "\"\"";
        private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };

        /// <summary>
        /// Export the information in a dataGridView to Excel
        /// </summary>
        /// <param name="dataGridView">DataGridView to export</param>
        /// <param name="pFullPath_toExport">Path to Excel file</param>
        /// <param name="nameSheet">Target sheet name</param>
        public void DataGridView2Excel(DataGridView dataGridView, string pFullPath_toExport, string nameSheet)
        {
            var dt = new System.Data.DataTable();

            //Get the DataTable from datagridview
            if (dataGridView.DataSource is DataSet)
            {
                dt = ((DataSet)dataGridView.DataSource).Tables.Count > 0 ? ((DataSet)dataGridView.DataSource).Tables[0] : new System.Data.DataTable();
            }
            else if (dataGridView.DataSource is System.Data.DataTable)
            {
                dt = (System.Data.DataTable)dataGridView.DataSource;
            }
            else if (dataGridView.DataSource is ArrayList)
            {
                var arr = (ArrayList)dataGridView.DataSource;
                dt = ArrayListToDataTable(arr);

            }
            DataTableToExcel(dt, dataGridView, pFullPath_toExport, nameSheet);
        }

        public string ExportToCVS(System.Data.DataTable pDataTable, bool containHeader)
        {
            return ExportToCVS(pDataTable, null, containHeader);
        }
        /// <summary>
        /// 保存到临时cvs文件
        /// </summary>
        /// <param name="pDataTable"></param>
        /// <param name="dgv"></param>
        /// <param name="nameSheet"></param>
        /// <returns></returns>
        public string ExportToCVS(System.Data.DataTable pDataTable, DataGridView dgv, bool containHeader)
        {      
            StringBuilder sb = new StringBuilder();
            if (containHeader)
            {
                //If there is a datagridview, get the names of columns and visibility of the same
                if (dgv != null)
                {
                    foreach (DataColumn dc in pDataTable.Columns)
                    {
                        string title = string.Empty;

                        //get the title that appears in the grid
                        //Note that these must be synchronized with the columns in the detail
                        if (dgv.Columns != null)
                            if (dgv.Columns[dc.Caption] != null)
                            {
                                //Get the header text of the grid
                                if (dgv.Columns != null) title = dgv.Columns[dc.Caption].HeaderText;
                                sb.Append(title + ControlChars.Tab);
                            }
                    }
                }
                else
                {
                    //If there is no datagridview, take the column name of the DataTable
                    foreach (DataColumn dc in pDataTable.Columns)
                    {
                        string title = dc.Caption;
                        sb.Append(title + ControlChars.Tab);

                    }
                }

                sb.AppendLine();
            }
            //for each row of data
            foreach (DataRow dr in pDataTable.Rows)
            {
                int i = 0;
                //for each data column
                foreach (DataColumn dc in pDataTable.Columns)
                {
                    // show only those columns belong to the grid                    
                    // note that these must be synchronized with the column headers
                    if (dgv != null && dgv.Columns[dc.Caption] != null)
                    {
                        //Generate the line of the registration
                        sb.Append((Information.IsDBNull(dr[i]) ? string.Empty : Escape(dr[i].ToString())) + ControlChars.Tab);

                    }
                    else if (dgv == null)
                    {
                        //Generate the line of the registration
                        sb.Append((Information.IsDBNull(dr[i]) ? string.Empty : Escape(dr[i].ToString())) + ControlChars.Tab);
                    }
                    i++;
                }
                sb.AppendLine();               
            }
            return sb.ToString();
        }

        public static string Escape(string s)
        {
            if (s.Contains(QUOTE))
                s = s.Replace(QUOTE, ESCAPED_QUOTE);

            if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
                s = QUOTE + s + QUOTE;

            return s;
        }

        public static string Unescape(string s)
        {
            if (s.StartsWith(QUOTE) && s.EndsWith(QUOTE))
            {
                s = s.Substring(1, s.Length - 2);

                if (s.Contains(ESCAPED_QUOTE))
                    s = s.Replace(ESCAPED_QUOTE, QUOTE);
            }

            return s;
        }


      
        /// <summary>
        /// Export the information in a DataTable to Excel
        /// </summary>
        /// <param name="pDataTable">DataTable to use</param>
        /// <param name="dgv">DataGridView to use</param>
        /// <param name="pFullPathToExport">Path to Excel file</param>
        /// <param name="nameSheet">Target sheet name</param>
        public void DataTableToExcel(System.Data.DataTable pDataTable, DataGridView dgv, string pFullPathToExport, string nameSheet)
        {
            char crlfReplacementVal = Convert.ToChar(7);
            string vFileName = Path.GetTempFileName();
            
            FileSystem.FileOpen(1, vFileName, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

            string sb = ExportToCVS(pDataTable, dgv, true);
            FileSystem.PrintLine(1, sb);
            FileSystem.FileClose(1);
            TextToExcel(vFileName, pFullPathToExport, nameSheet);
        }

        /// <summary>
        /// Format character of the cell to export
        /// </summary>
        /// <param name="cell">DataRow cell to format</param>
        /// <returns>formatted string</returns>
        private static string FormatCell(Object cell)
        {
            string textToParse = Convert.ToString(cell);
            return textToParse.Replace(Environment.NewLine, string.Empty);
        }

        /// <summary>
        /// Export a text string to excel
        /// </summary>
        /// <param name="pFileName">Filename of the exported file</param>
        /// <param name="pFullPathToExport">Path of exported file</param>
        /// <param name="nameSheet">Target sheet name</param>
        private static void TextToExcel(string pFileName, string pFullPathToExport, string nameSheet)
        {
            System.Globalization.CultureInfo vCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            var exc = new Microsoft.Office.Interop.Excel.Application();
            exc.Workbooks.OpenText(pFileName, 
                Missing.Value, 
                1,
                XlTextParsingType.xlDelimited,
                XlTextQualifier.xlTextQualifierDoubleQuote,
                Missing.Value, true,
                Missing.Value, Missing.Value,
                Missing.Value, Missing.Value,
                Missing.Value, Missing.Value,
                Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value);

            Workbook wb = exc.ActiveWorkbook;
            var ws = (Worksheet)wb.ActiveSheet;
            ws.Name = nameSheet;

            try
            {
                //Header Format
                ws.get_Range(ws.Cells[1, 1], ws.Cells[ws.UsedRange.Rows.Count, ws.UsedRange.Columns.Count]).AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatClassic1, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch
            {
                ws.get_Range(ws.Cells[1, 1], ws.Cells[ws.UsedRange.Rows.Count, ws.UsedRange.Columns.Count]);
            }

            string tempPath = Path.GetTempFileName();

            pFileName = tempPath.Replace("tmp", "xls");
            File.Delete(pFileName);

            if (File.Exists(pFullPathToExport))
            {
                File.Delete(pFullPathToExport);
            }
            exc.ActiveWorkbook.SaveAs(pFullPathToExport, XlFileFormat.xlWorkbookNormal, null, null, null, null, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);

            exc.Workbooks.Close();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);

            exc.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exc);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            System.Threading.Thread.CurrentThread.CurrentCulture = vCulture;

        }

        /// <summary>
        /// Convert an ArrayList of objects in a DataTable from the 'properties' in the ArrayList
        /// </summary>
        /// <param name="array">ArrayList of Objects</param>
        /// <returns>DataTable output</returns>
        public static System.Data.DataTable ArrayListToDataTable(ArrayList array)
        {
            var dt = new System.Data.DataTable();
            if (array.Count > 0)
            {
                object obj = array[0];
                //Convert the properties of columns of the DataRow object
                foreach (PropertyInfo info in obj.GetType().GetProperties())
                {
                    dt.Columns.Add(info.Name, info.PropertyType);
                }
            }
            foreach (object obj in array)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    Type type = obj.GetType();

                    MemberInfo[] members = type.GetMember(col.ColumnName);

                    object valor;
                    if (members.Length != 0)
                    {
                        switch (members[0].MemberType)
                        {
                            case MemberTypes.Property:
                                //read object properties
                                var prop = (PropertyInfo)members[0];
                                try
                                {
                                    valor = prop.GetValue(obj, new object[0]);
                                }
                                catch
                                {
                                    valor = prop.GetValue(obj, null);
                                }

                                break;
                            case MemberTypes.Field:
                                //read the fields of the object not used 
                                //given we have populated the dt with properties ArrayList
                                var field = (FieldInfo)members[0];
                                valor = field.GetValue(obj);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        dr[col] = valor;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static string Readcell(Range oRange)
        {
            String result = string.Empty;
            if (oRange != null)
            {
                if (oRange.Text != null)
                {
                    result = oRange.Text.ToString();
                }
            }
            return result;
        }

        public DataSet CreateDataSetFromDataGrid(DataGridView myDataGridView)
        {
            // Create a DataSet
            var ds = new DataSet();
            // Add a table
            const string tableName = "Sheet1";
            ds.Tables.Add(tableName);

            //Create columns in Table
            foreach (DataGridViewColumn iColumn in myDataGridView.Columns)
            {
                ds.Tables[tableName].Columns.Add(iColumn.HeaderText, typeof(String));
            }

            //Inserts rows into the Table
            var myArray = new string[ds.Tables[tableName].Columns.Count];

            foreach (DataGridViewRow iRow in myDataGridView.Rows)
            {
                DataRow myRow = ds.Tables[tableName].NewRow();

                foreach (DataGridViewColumn iColumn in myDataGridView.Columns)
                {
                    myArray[iColumn.Index] = iRow.Cells[iColumn.Index].Value == null ? string.Empty : iRow.Cells[iColumn.Index].Value.ToString();
                }
                myRow.ItemArray = myArray;
                ds.Tables[tableName].Rows.Add(myRow);
                ds.AcceptChanges();
            }
            return ds;
        }
    }
}
