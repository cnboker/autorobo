using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;

using CsvHelper;

using ExcelLibrary.SpreadSheet;
using ErikEJ.SqlCeScripting;

namespace AutoRobo.DB
{
    public class SqlceDataBase
    {       
        private string _connecitonString;
        private string _dbFile;

        public SqlceDataBase(string inputFile) {
            this._dbFile = inputFile;
            _connecitonString = String.Format("Data Source='{0}'", inputFile);
            CreateDatabase(false);            
        }     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="create">true, 文件已经存在则覆盖</param>
        /// <returns></returns>
        public void CreateDatabase(bool create)
        {
            bool exist = File.Exists(_dbFile);
            if (!exist || (exist && create))
            {
                if (exist) {
                    File.Delete(_dbFile);
                }
                SqlCeEngine engine = new SqlCeEngine(_connecitonString);
                engine.CreateDatabase();
                File.SetAttributes(_dbFile, FileAttributes.Hidden);
            }
                                
        }
        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableExist(string tableName)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("SELECT 1 FROM Information_Schema.Tables WHERE TABLE_NAME ='{0}'", tableName);
                cmd.CommandType = CommandType.Text;
                object result = cmd.ExecuteScalar();
                return (result != null && Convert.ToInt16(result) == 1);
            }
        }

        /// <summary>
        /// 迁移ce数据库到ms sql server
        /// </summary>
        /// <param name="sqlceFile"></param>
        /// <param name="mssqlConnection"></param>
        //public void Migrate(string mssqlConnection) {
        //    using (IRepository ceRepository = new DB4Repository(_connecitonString))
        //    {
        //        string fileName = Path.GetTempFileName();
        //        var generator = new Generator4(ceRepository, fileName);
        //        generator.ScriptDatabaseToFile(Scope.SchemaData);
        //        using (IRepository serverRepository = new ServerDBRepository4(mssqlConnection))
        //        {
        //            serverRepository.ExecuteSqlFile(fileName);
        //        }
        //    }
        //}

        /// <summary>
        /// 将表数据导出到excel文件
        /// </summary>
        /// <param name="sqlceFile"></param>
        /// <param name="tableName"></param>
        /// <param name="outputFile"></param>
        public void ExportToExcel(string tablesString, string outputFile) {
            FileStream stream = null;
            try
            {
                stream = new FileStream(outputFile, FileMode.Create);
                Workbook workbook = new Workbook();
                string[] tables = tablesString.Split(",".ToArray());
                foreach (var tableName in tables)
                {
                    Worksheet worksheet = CreateWorkSheet(tableName);
                    workbook.Worksheets.Add(worksheet);
                }
                workbook.SaveToStream(stream);                
            }
            finally {
                if (stream != null) {
                    stream.Close();
                }
            }
        }

        private Worksheet CreateWorkSheet(string tableName) {
            Worksheet worksheet = new Worksheet(tableName);
            string sql = string.Format("select * from {0}", tableName);
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString)) {
                conn.Open();
                SqlCeCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                SqlCeDataReader reader = cmd.ExecuteReader();
                int rowIndex = 0;
                while (reader.Read())
                {
                    for (int col = 0; col < reader.FieldCount; col++)
                    {
                        worksheet.Cells[rowIndex, col] = GetCellValue(reader, col);
                    }
                    rowIndex++;
                }
                reader.Close();
            }
            return worksheet;
        }

        private Cell GetCellValue(SqlCeDataReader reader, int col)
        {           
            if (reader.GetFieldType(col).Name == "DateTime")
            {
                return new Cell(reader[col].ToString());
            }
            else {
                return new Cell(reader[col]);
            }
        }

        public DataTable GetTableData(string tableName, int top) {
            DataTable dt = null;
            if (!TableExist(tableName)) return null;
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                string sql = string.Format("select top {0} * from {1}", top, tableName);
                SqlCeDataAdapter adpter = new SqlCeDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                adpter.Fill(ds);
                if (ds.Tables.Count > 0) {
                    dt = ds.Tables[0];
                }
                conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 将表数据导出到csv文件        
        /// </summary>
        /// <param name="sqlceFile"></param>
        /// <param name="tableName"></param>
        /// <param name="outputFile"></param>
        public void ExportToXSV(string tableName, string outputFile, string delimiter)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                using (var csv = new CsvWriter(new StreamWriter(outputFile, false, Encoding.UTF8)))
                {
                    csv.Configuration.Delimiter = delimiter;
                    conn.Open();
                    string sql = string.Format("select * from {0}", tableName);
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int col = 0; col < reader.FieldCount; col++)
                        {
                            csv.WriteField(reader[col]);
                        }
                        csv.NextRecord();
                    }
                    reader.Close();                    
                }
            }
        }

        public void ExportToXML(string tableName, string outputFile)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                using (var sw = new StreamWriter(outputFile, false, Encoding.GetEncoding("utf-8")))
                {

                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"" + Encoding.GetEncoding("utf-8").WebName + "\"?>");
                    sw.WriteLine("<DATA>");

                    string sql = string.Format("select * from {0}", tableName);
                    SqlCeCommand cmd = new SqlCeCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sw.WriteLine("<item>");
                        for (int col = 0; col < reader.FieldCount; col++)
                        {
                            string columnName = reader.GetName(col);
                            sw.WriteLine("<" + columnName + ">" + XMLizeString(reader[col].ToString()) + "</" + columnName + ">");
                        }
                        sw.WriteLine("</item>");
                    }
                    sw.WriteLine("</DATA>");
                    reader.Close();                    
                }
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

        public void ExportToCSV(string tableName, string outputFile)
        {
            ExportToXSV(tableName, outputFile, ",");
        }

        public void ExportToTSV(string tableName, string outputFile)
        {
            ExportToXSV(tableName, outputFile, "\t");
        }

        public void ExportToJSON(string tableName, string outputFile)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                using (var sw = new StreamWriter(outputFile, false, Encoding.GetEncoding("utf-8")))
                {
                    string sql = string.Format("select * from {0}", tableName);
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;                    
                    SqlCeDataReader reader = cmd.ExecuteReader();
                    sw.WriteLine("\t[\n");
                    bool firstRow = true;
                    while (reader.Read())
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
                        for (int col = 0; col < reader.FieldCount; col++)
                        {
                            string columnName = reader.GetName(col);
                            string delimiter = ",";
                            if (col == reader.FieldCount - 1)
                            {
                                delimiter = "";
                            }
                            sw.WriteLine("\t\t\"" + columnName + "\": \"" + GetJsonText(reader[col]) + "\"" + delimiter);

                        }
                        sw.WriteLine("\t}");

                    }
                    reader.Close(); 
                    sw.WriteLine("\t]\n");
                    sw.Close();
                                      
                }
            }
        }


        private  string GetJsonText(object text) {
            if (text == null) return "";
            if (string.IsNullOrEmpty(text.ToString())) return "";
            return text.ToString().Replace("\"", "\\\"");
        }

        /// <summary>
        ///     Allows the programmer to easily delete all data from the DB.
        /// </summary>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public void ClearDB()
        {
            DataTable tables = this.GetDataTable("select TABLE_NAME from information_schema.tables where TABLE_TYPE <> 'VIEW'");
            foreach (DataRow table in tables.Rows)
            {
                DropTable(table["TABLE_NAME"].ToString());
            }
        }

        private DataTable GetDataTable(string sql)
        {
            
            DataTable dt = new DataTable();
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                SqlCeCommand mycommand = new SqlCeCommand(sql, conn);
                mycommand.CommandType = CommandType.Text;
                SqlCeDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            return dt;
        }

        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        private void DropTable(String table)
        {
            string sql = String.Format("drop table {0}", table);
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                SqlCeCommand mycommand = new SqlCeCommand(sql, conn);
                mycommand.CommandType = CommandType.Text;
                mycommand.ExecuteNonQuery();
            }
        }


        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        private void ClearTable(String table)
        {            
            string sql = String.Format("delete from {0}", table);
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                SqlCeCommand mycommand = new SqlCeCommand(sql, conn);
                mycommand.CommandType = CommandType.Text;
                mycommand.ExecuteNonQuery();
            }
        }

        public int CreateTableSchema(string tableName, DataTable table)
        {
            string sql = GenerateSQL.CreateSqlTableScheamFromDataTable(tableName, table);
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                //Console.WriteLine(sql);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 写数据到表
        /// </summary>
        /// <param name="table"></param>
        public void Write(DataTable table)
        {
            if (table.Columns.Count == 0) return;
            if (table.Rows.Count == 0) return;
            //表不存在则创建
            if (!TableExist(table.TableName))
            {
                CreateTableSchema(table.TableName, table);
            }
            using (SqlCeConnection conn = new SqlCeConnection(_connecitonString))
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                ArrayList columns = GetColumnNames(table);
                foreach (DataRow dr in table.Rows)
                {
                    string insertSql = GenerateSQL.GenerateSqlInserts(columns, dr, table.TableName);
                    //Console.WriteLine(insertSql);
                    cmd.CommandText = insertSql;                    
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private ArrayList GetColumnNames(DataTable table)
        {
            ArrayList list = new ArrayList();
            foreach (DataColumn column in table.Columns)
            {
                list.Add(column.ColumnName);
            }
            return list;
        }

    }
}
