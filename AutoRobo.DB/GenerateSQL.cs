using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoRobo.DB
{
    public static class GenerateSQL
    {
        #region create table schema
        public static string CreateSqlTableScheamFromDataTable(string tableName, DataTable table)
        {
            string sql = "CREATE TABLE [" + tableName + "] (\n";
            // columns
            foreach (DataColumn column in table.Columns)
            {
                sql += "[" + column.ColumnName + "] " + SQLGetType(column) + IsNotNullString(column) + IsUniqueString(column) + ",\n";
            }
            sql = sql.TrimEnd(new char[] { ',', '\n' }) + "\n";
            // primary keys
            if (table.PrimaryKey.Length > 0)
            {
                sql += ", CONSTRAINT [PK_" + tableName + "] PRIMARY KEY (";
                foreach (DataColumn column in table.PrimaryKey)
                {
                    sql += "[" + column.ColumnName + "],";
                }
                sql = sql.TrimEnd(new char[] { ',' }) + "))\n";
            }
            else
            {
                sql += ")";
            }

            return sql;
        }

        private static string IsNotNullString(DataColumn dc)
        {
            return dc.AllowDBNull ? "" : " NOT NULL";
        }

        private static string IsUniqueString(DataColumn dc)
        {
            return dc.Unique ? " UNIQUE" : "";
        }

        public static string[] GetPrimaryKeys(DataTable schema)
        {
            List<string> keys = new List<string>();

            foreach (DataRow column in schema.Rows)
            {
                if (schema.Columns.Contains("IsKey") && (bool)column["IsKey"])
                    keys.Add(column["ColumnName"].ToString());
            }

            return keys.ToArray();
        }

        // Return T-SQL data type definition, based on schema definition for a column
        public static string SQLGetType(object type, int columnSize, int numericPrecision, int numericScale)
        {
            switch (type.ToString())
            {
                case "System.String":
                    return "NVARCHAR(" + ((columnSize == -1) ? 255 : columnSize) + ")";

                case "System.Decimal":
                    if (numericScale > 0)
                        return "REAL";
                    else if (numericPrecision > 10)
                        return "BIGINT";
                    else
                        return "INT";

                case "System.Double":
                case "System.Single":
                    return "REAL";

                case "System.Int64":
                    return "BIGINT";

                case "System.Int16":
                case "System.Int32":
                    return "INT";

                case "System.DateTime":
                    return "DATETIME";

                default:
                    throw new Exception(type.ToString() + " not implemented.");
            }
        }

        // Overload based on row from schema table
        public static string SQLGetType(DataRow schemaRow)
        {
            return SQLGetType(schemaRow["DataType"],
                                int.Parse(schemaRow["ColumnSize"].ToString()),
                                int.Parse(schemaRow["NumericPrecision"].ToString()),
                                int.Parse(schemaRow["NumericScale"].ToString()));
        }

        // Overload based on DataColumn from DataTable type
        public static string SQLGetType(DataColumn column)
        {
            return SQLGetType(column.DataType, column.MaxLength, 10, 2);
        }

        #endregion

        public static string GenerateSqlInserts(ArrayList aryColumns,
                                             DataTable dtTable,
                                             string sTargetTableName)
        {
            string sSqlInserts = string.Empty;
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);
            // loop thru each record of the datatable
            foreach (DataRow drow in dtTable.Rows)
            {
                sbSqlStatements.Append(GenerateSqlInserts(aryColumns, drow, sTargetTableName));
            }
            sSqlInserts = sbSqlStatements.ToString();
            return sSqlInserts;
        }

        private static string GetColumns(ArrayList aryColumns)
        {
            // create the columns portion of the INSERT statement
            string sColumns = string.Empty;
            foreach (string colname in aryColumns)
            {
                if (sColumns != string.Empty)
                    sColumns += ", ";

                sColumns += "[" + colname + "]";
            }
            return sColumns;
        }

        public static string GenerateSqlInserts(ArrayList aryColumns, DataRow drow, string sTargetTableName)
        {
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);

            // loop thru each column, and include
            // the value if the column is in the array
            string sValues = string.Empty;
            foreach (string col in aryColumns)
            {
                if (sValues != string.Empty)
                    sValues += ", ";

                // need to do a case to check the column-value types
                // (quote strings(check for dups first), convert bools)
                string sType = string.Empty;
                try
                {
                    sType = drow[col].GetType().ToString();
                    switch (sType.Trim().ToLower())
                    {
                        case "system.boolean":
                            sValues += (Convert.ToBoolean(drow[col])
                                        == true ? "1" : "0");
                            break;

                        case "system.string":
                            sValues += string.Format("'{0}'",
                                       QuoteSQLString(drow[col]));
                            break;

                        case "system.datetime":
                            sValues += string.Format("'{0}'",
                                       QuoteSQLString(drow[col]));
                            break;

                        default:
                            if (drow[col] == System.DBNull.Value)
                                sValues += "NULL";
                            else
                                sValues += Convert.ToString(drow[col]);
                            break;
                    }
                }
                catch
                {
                    sValues += string.Format("'{0}'",
                               QuoteSQLString(drow[col]));
                }
            }

            //   INSERT INTO Tabs(Name) 
            //      VALUES('Referrals')
            // write the insert line out to the stringbuilder
            string snewsql = string.Format("INSERT INTO {0}({1}) ",
                                            sTargetTableName, GetColumns(aryColumns));
            sbSqlStatements.Append(snewsql);
            sbSqlStatements.AppendLine();
            sbSqlStatements.Append('\t');
            snewsql = string.Format("VALUES({0})", sValues);
            sbSqlStatements.Append(snewsql);
            sbSqlStatements.AppendLine();
            sbSqlStatements.AppendLine();
            return sbSqlStatements.ToString();
        }

        public static string GenerateSqlUpdates(ArrayList aryColumns,
                             ArrayList aryWhereColumns,
                             DataTable dtTable, string sTargetTableName)
        {
            string sSqlUpdates = string.Empty;
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);

            // UPDATE table SET col1 = 3, col2 = 4 WHERE (select cols)
            // loop thru each record of the datatable
            foreach (DataRow drow in dtTable.Rows)
            {
                // VALUES clause:  loop thru each column, and include 
                // the value if the column is in the array
                string sValues = string.Empty;
                foreach (string col in aryColumns)
                {
                    string sNewValue = "[" + col + "]" + " = ";
                    if (sValues != string.Empty)
                        sValues += ", ";

                    // need to do a case to check the column-value types 
                    // (quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sNewValue += (Convert.ToBoolean(drow[col]) ==
                                              true ? "1" : "0");
                                break;

                            case "system.string":
                                sNewValue += string.Format("'{0}'",
                                             QuoteSQLString(drow[col]));
                                break;

                            case "system.datetime":
                                sNewValue += string.Format("'{0}'",
                                             QuoteSQLString(drow[col]));
                                break;

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sNewValue += "NULL";
                                else
                                    sNewValue += Convert.ToString(drow[col]);
                                break;
                        }
                    }
                    catch
                    {
                        sNewValue += string.Format("'{0}'",
                                     QuoteSQLString(drow[col]));
                    }

                    sValues += sNewValue;
                }

                // WHERE clause: loop thru each column, and include
                // the value if column is in array
                string sWhereValues = string.Empty;
                foreach (string col in aryWhereColumns)
                {
                    string sNewValue = col + " = ";
                    if (sWhereValues != string.Empty)
                        sWhereValues += " AND ";

                    // need to do a case to check the column-value types
                    // (quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sNewValue += (Convert.ToBoolean(drow[col]) ==
                                              true ? "1" : "0");
                                break;

                            case "system.string":
                                sNewValue += string.Format("'{0}'",
                                             QuoteSQLString(drow[col]));
                                break;

                            case "system.datetime":
                                sNewValue += string.Format("'{0}'",
                                             QuoteSQLString(drow[col]));
                                break;

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sNewValue += "NULL";
                                else
                                    sNewValue += Convert.ToString(drow[col]);
                                break;
                        }
                    }
                    catch
                    {
                        sNewValue += string.Format("'{0}'",
                                     QuoteSQLString(drow[col]));
                    }

                    sWhereValues += sNewValue;
                }

                // UPDATE table SET col1 = 3, col2 = 4 WHERE (select cols)
                // write the line out to the stringbuilder
                string snewsql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                                                sTargetTableName, sValues,
                                                sWhereValues);
                sbSqlStatements.Append(snewsql);
                sbSqlStatements.AppendLine();
                sbSqlStatements.AppendLine();
            }

            sSqlUpdates = sbSqlStatements.ToString();
            return sSqlUpdates;
        }

        public static string GenerateSqlDeletes(ArrayList aryColumns,
                                                DataTable dtTable,
                                                string sTargetTableName)
        {
            string sSqlDeletes = string.Empty;
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);

            // loop thru each record of the datatable
            foreach (DataRow drow in dtTable.Rows)
            {
                // loop thru each column, and include 
                // the value if the column is in the array
                string sValues = string.Empty;
                foreach (string col in aryColumns)
                {
                    string sNewValue = "[" + col + "]" + " = ";
                    if (sValues != string.Empty)
                        sValues += " AND ";

                    // need to do a case to check the column-value types
                    // (quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sNewValue += (Convert.ToBoolean(drow[col]) ==
                                              true ? "1" : "0");
                                break;

                            case "system.string":
                                sNewValue += string.Format("'{0}'",
                                             QuoteSQLString(drow[col]));
                                break;

                            case "system.datetime":
                                sNewValue += string.Format("'{0}'",
                                             QuoteSQLString(drow[col]));
                                break;

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sNewValue += "NULL";
                                else
                                    sNewValue += Convert.ToString(drow[col]);
                                break;
                        }
                    }
                    catch
                    {
                        sNewValue += string.Format("'{0}'",
                                     QuoteSQLString(drow[col]));
                    }

                    sValues += sNewValue;
                }

                // DELETE FROM table WHERE col1 = 3 AND col2 = '4'
                // write the line out to the stringbuilder
                string snewsql = string.Format("DELETE FROM {0} WHERE {1}",
                                                sTargetTableName, sValues);
                sbSqlStatements.Append(snewsql);
                sbSqlStatements.AppendLine();
                sbSqlStatements.AppendLine();
            }

            sSqlDeletes = sbSqlStatements.ToString();
            return sSqlDeletes;
        }

        public static string QuoteSQLString(object ostr)
        {
            return ostr.ToString().Replace("'", "''");
        }

        public static string QuoteSQLString(string str)
        {
            return str.Replace("'", "''");
        }
    }
}
