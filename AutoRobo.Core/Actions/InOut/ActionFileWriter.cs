using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using AutoRobo.Core.Actions.InOut;
using AutoRobo.Core.Formatters;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;
using CsvHelper;
using ExcelLibrary.SpreadSheet;
using System.Collections.Generic;


namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 保存变量到文件
    /// </summary>
    public class ActionFileWriter : ActionBase, ICodeFormatterAcceptor
    {
        /// <summary>
        /// 要保存的变量名称, 多个使用","隔开
        /// </summary>
        public string VariableName { get; set; }
        public FileType FileType { get; set; }
        /// <summary>
        /// 保存的文件路径
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 是否写起始行
        /// </summary>
        public bool HasHeaderRecord { get; set; }
        /// <summary>
        /// 多表存储的文件路径
        /// </summary>
        private Dictionary<string,string> filePaths = new Dictionary<string,string>();

        public ActionFileWriter()
        {
            VariableName = "";
        }

        /// <summary>
        /// 清除文件名缓存
        /// </summary>
        public void Reset() {
            filePaths.Clear();
        }

        private void InitializeFilePath(){
            if(filePaths.Count > 0)return;
            string _fileName = VariableModel.Parse(FileName);
            string path = Path.Combine(AppSettings.Instance.CurrentExecutePath, "data");
            _fileName = Path.Combine(path, GetFileName(_fileName));

            string[] vars = VariableName.Split(",".ToCharArray());
            foreach(string v in vars){
                filePaths.Add(v, _fileName.Insert(_fileName.IndexOf("."), "_" + v));
            }
        }

        public override string ActionDisplayName
        {
            get { return "保存内容到文件"; }
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucFileWriter(editorAction);
        }  

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("save.png");
        }

        private string GetFileName(string fileName)
        {
            if (fileName.IndexOf(".") > 0) return fileName;
            //导出xls需要先生成XLS，让后在转成XLS
            if(FileType == InOut.FileType.XLS){
                return fileName + ".csv";
            }
            return fileName + "." + FileType.ToString().ToLower();
        }

        public override void Perform()
        {
            try
            {        
                InitializeFilePath();
                string[] vars = VariableName.Split(",".ToCharArray());
                foreach (var varName in vars)
                {
                    Save(VariableModel.Find<ActionTableVariable>(varName), filePaths[varName]);
                }
                if (FileType == InOut.FileType.XLS) {
                    AppContext.CurrentWorker.RunComplete -= CurrentWorker_RunComplete;
                    AppContext.CurrentWorker.RunComplete += CurrentWorker_RunComplete;
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.Message);
            }
        }

        /// <summary>
        /// 脚本执行完成， 将生成的csv文件合并成XLS文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentWorker_RunComplete(object sender, EventArgs e)
        {
            if (FileType != InOut.FileType.XLS) return;
            Workbook workbook = new Workbook();
            foreach (string variable in filePaths.Keys) {
                Worksheet worksheet = new Worksheet(variable);
                using (var csv = new CsvReader(new StreamReader(filePaths[variable], Encoding.UTF8)))
                {
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.HasHeaderRecord = false;
                    int rowindex = 0;
                    while (csv.Read())
                    {
                        string[] records = csv.CurrentRecord;
                       
                        for (int i = 0; i < records.Length; i++)
                        {
                            worksheet.Cells[rowindex, i] = new Cell(records[i]);                            
                        }
                        rowindex++;
                    }
                }
                workbook.Worksheets.Add(worksheet);
            }
            
            FileStream stream = null;
            try
            {
                string path = Path.Combine(AppSettings.Instance.CurrentExecutePath, "data");
                string file = VariableModel.Parse(FileName);
                string outputFile = Path.Combine(path, file.IndexOf(".") == -1 ? (file + ".xls") : (file.Substring(0, file.IndexOf(".")) + ".xls"));
                stream = new FileStream(outputFile,FileMode.Create);
                workbook.SaveToStream(stream);
            }
            finally
            {
                foreach (string file in filePaths.Values)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
                Reset();
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

  
        private void Save(ActionTableVariable tableVar, string outFile)
        {
            DataTable dt = tableVar.Data as DataTable;
            if (dt == null) return;
            try
            {               
                if (FileType == InOut.FileType.CSV || FileType == InOut.FileType.XLS)
                {
                    ExportToCSV(dt, outFile);
                }
                else if (FileType == InOut.FileType.JSON)
                {
                    ExportToJSON(dt, outFile);
                }
                else if (FileType == InOut.FileType.TSV)
                {
                    ExportToTSV(dt, outFile);
                }
                else if (FileType == InOut.FileType.XML)
                {
                    ExportToXML(dt, outFile);
                }
            }
            catch (Exception ex)
            {
                LogWrite(string.Format("保存变量{0}失败", tableVar.Name));
                LogWrite(ex.Message);
            }
        }

        private void ExportToXSV(DataTable outputTable, string writeFileName, string delimiter)
        {
            using (var csv = new CsvWriter(new StreamWriter(writeFileName, true, Encoding.UTF8)))
            {
                csv.Configuration.Delimiter = delimiter;
                
                foreach(DataRow dr in outputTable.Rows)
                {
                    for (int col = 0; col < outputTable.Columns.Count; col++)
                    {
                        csv.WriteField(dr[col].ToString());
                    }
                    csv.NextRecord();
                }               
            }
        }

        private void ExportToJSON(DataTable outputTable, string writeFileName) {
            using (var sw = new StreamWriter(writeFileName, false, Encoding.GetEncoding("utf-8")))
            {
                
                sw.WriteLine("\t[\n");
                bool firstRow = true;
                foreach (DataRow dr in outputTable.Rows)
                {
                    if (!firstRow)
                    {
                        sw.WriteLine("\t,");
                    }
                    if (firstRow)
                    {
                        firstRow = false;
                    }

                    sw.WriteLine("\t{\n");
                    for (int col = 0; col < outputTable.Columns.Count; col++)
                    {
                        string columnName = outputTable.Columns[col].ColumnName;
                        string delimiter = ",";
                        if (col == outputTable.Columns.Count - 1)
                        {
                            delimiter = "";
                        }
                        sw.WriteLine("\t\t\"" + columnName + "\": \"" + GetJsonText(dr[col].ToString()) + "\"" + delimiter);

                    }
                    sw.WriteLine("\t}");

                }
                sw.WriteLine("\t]\n");
                sw.Close();
            }
        }

        private string GetJsonText(object text)
        {
            if (text == null) return "";
            if (string.IsNullOrEmpty(text.ToString())) return "";
            return text.ToString().Replace("\"", "\\\"");
        }

        private void ExportToTSV(DataTable outputTable, string writeFileName) {
            ExportToXSV(outputTable, writeFileName, "\t");
        }

        private void ExportToCSV(DataTable outputTable, string writeFileName)
        {
            ExportToXSV(outputTable, writeFileName, ",");
        }

        private void ExportToXML(DataTable outputTable, string writeFileName) {
            using (var sw = new StreamWriter(writeFileName, false, Encoding.GetEncoding("utf-8")))
            {

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"" + Encoding.GetEncoding("utf-8").WebName + "\"?>");
                sw.WriteLine("<DATA>");

                foreach(DataRow dr in outputTable.Rows)
                {               
                    sw.WriteLine("<item>");
                    for (int col = 0; col < outputTable.Columns.Count; col++)
                    {
                        string columnName = outputTable.Columns[col].ColumnName;
                        sw.WriteLine("<" + columnName + ">" + XMLizeString(dr[col].ToString()) + "</" + columnName + ">");
                    }
                    sw.WriteLine("</item>");
                }
                sw.WriteLine("</DATA>");               
            }
        }

        private static string XMLizeString(string xml)
        {
            xml = xml.Replace("&", "&amp;");
            xml = xml.Replace("<", "&lt;");
            xml = xml.Replace(">", "&gt;");
            xml = xml.Replace("\"", "&quot;");
            xml = xml.Replace("'", "&#39;");
            return xml;
        }

        public override string GetDescription()
        {
            return "保存变量内容到文件";
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            VariableName = GetAttribute(node, "VariableName"); 
            FileName = GetAttribute(node, "FileName");
            HasHeaderRecord = GetBooleanAttribute(node, "HasHeaderRecord");            
            FileType = (FileType)GetIntAttribute(node, "FileType");
            
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("FileName", FileName);
            writer.WriteAttributeString("HasHeaderRecord", HasHeaderRecord ? "1" : "0");            
            writer.WriteAttributeString("VariableName", VariableName);
            writer.WriteAttributeString("FileType", ((int)FileType).ToString());
            base.WriterAddAttribute(writer);
        }

        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
