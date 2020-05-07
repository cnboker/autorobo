using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;

namespace AutoRobo.DB
{
    public class SqlAdapter
    {
        private ServerType _serverType;
        private IDbCommand sqlCmd;
        private IDbConnection sqlConn;

        private string server;
        private string databaseName;
        private string uid;
        private string password;
        private bool integratedSecurity;
        private string connectionString;

        public SqlAdapter(ServerType serverType, string connectionString)
        {
            _serverType = serverType;
            this.connectionString = connectionString;
            if (_serverType == ServerType.MSSQL)
            {
                var dbBuilder = new SqlConnectionStringBuilder(connectionString);
                server = dbBuilder.DataSource;
                databaseName = dbBuilder.InitialCatalog;
                integratedSecurity = dbBuilder.IntegratedSecurity;
                if (!dbBuilder.IntegratedSecurity) {
                    uid = dbBuilder.UserID;
                    password = dbBuilder.Password;
                }
                sqlCmd = new SqlCommand();
                sqlConn = new SqlConnection(connectionString);                
            }
            else if(serverType == ServerType.MySQL)
            {
                var f = DbProviderFactories.GetFactory("MySql.Data.MySqlClient"); //your provider
                var dbBuilder = f.CreateConnectionStringBuilder();
                server = dbBuilder["Server"].ToString();
                databaseName = dbBuilder["database"].ToString();
                uid = dbBuilder["Uid"].ToString();
                password = dbBuilder["Pwd"].ToString();
                sqlCmd = new MySqlCommand();
                sqlConn = new MySqlConnection() { ConnectionString = connectionString };                
            }
            sqlCmd.Connection = sqlConn; 
        }

        private string GetMasterConnectionString()
        {
            switch (this._serverType)
            {
                case ServerType.MSSQL:
                    if (!integratedSecurity)
                    {
                        return ("server=" + server + ";Initial Catalog=master;User ID=" + uid + ";Password=" + password + ";");
                    }
                    return ("server=" + server + ";Initial Catalog=master;Integrated Security=true;");

                case ServerType.MySQL:
                    return ("Server=" + server + ";Database=" + databaseName + ";Uid=" + uid + ";Pwd=" + password + ";");
            }
            return "";
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        public bool ConnectToDatabase()
        {
            bool flag = true;            
            try
            {
                this.sqlConn.Open();
            }
            catch (Exception)
            {
                flag = false;
            }
            finally {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            if (flag)
            {
                this.sqlCmd.CommandType = CommandType.Text;
                this.sqlCmd.Connection = this.sqlConn;
            }
            return flag;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="databaseName"></param>
        public void CreateDatabase(string databaseName)
        {            
            this.sqlCmd.CommandType = CommandType.Text;
            this.sqlCmd.CommandText = "CREATE DATABASE " + databaseName;
            if (this._serverType == ServerType.MySQL)
            {
                this.sqlCmd.CommandText = "CREATE DATABASE IF NOT EXISTS " + databaseName;
            }
            sqlConn.ConnectionString = GetMasterConnectionString();
            try
            {
                sqlConn.Open();
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                throw ex;
            }         
            finally
            {                
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                    sqlConn.ConnectionString = connectionString;
                }
            }
        }

        public void DropDatabase(string databaseName) {            
            this.sqlCmd.CommandType = CommandType.Text;
            this.sqlCmd.CommandText = "drop DATABASE " + databaseName;                       
            try
            {
                sqlConn.Open();
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }   
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }
       
        public bool DBExist(string databaseName)
        {            
            this.sqlCmd.CommandType = CommandType.Text;
            if (this._serverType == ServerType.MSSQL)
            {
                this.sqlCmd.CommandText = "SELECT database_id FROM sys.databases WHERE Name = '" + databaseName + "'";
            }
            else
            {
                this.sqlCmd.CommandText = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '" + databaseName + "'";
            }
            try
            {                
                sqlConn.Open();
                int num = (int)this.sqlCmd.ExecuteScalar();                
                return (num > 0);
            }
            catch (Exception)
            {
                return false;
            }
            finally {
                if (sqlConn.State == ConnectionState.Open) {
                    sqlConn.Close();
                }
            }
        }

        public bool TableExist(string table)
        {
            bool flag = true;
            try
            {
                sqlConn.Open();
                this.sqlCmd.CommandText = "SELECT COUNT(*) FROM " + table;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
            return flag;
        }


        public void Update(DataTable dt)
        {
            if (!DBExist(databaseName))
            {
                CreateDatabase(databaseName);
            }
            ArrayList columns = new ArrayList();
            foreach(DataColumn c in dt.Columns){
                columns.Add(c.ColumnName);
            }
            string sql;
            if (!TableExist(dt.TableName)) {
                sql = GenerateSQL.CreateSqlTableScheamFromDataTable(dt.TableName, dt);
                try
                {
                    sqlConn.Open();
                    sqlCmd.CommandText = sql;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }   
                finally {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    } 
                }
            }
            
            sql = GenerateSQL.GenerateSqlInserts(columns, dt, dt.TableName);
                     
            try
            {
                sqlConn.Open();
                sqlCmd.CommandText = sql;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }   
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }
    }
}
