using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using System.Data.SqlClient;

using AutoRobo.Core.IO;
using AutoRobo.Core.Actions;
using System.Data.Common;
using System.Data.OleDb;
using AutoRobo.DB;
using MySql.Data.MySqlClient;

namespace AutoRobo.Core.UserControls
{
  
    public partial class ucDatabaseWriter : ucBaseEditor
    {
        private SqlAdapter adapter = null;
        private ServerType SelectedServerType
        {
            get
            {
                return (ServerType)this.comboBoxServerType.SelectedIndex;
            }
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionDatabaseWriter action = (ActionDatabaseWriter)base.Action;
                action.VariableName = tableNameList.Get();
                action.ServerType = SelectedServerType;
                action.ConnectionString = GetConnectionString(true);
                return action;
            }
            set
            {
                var action = (ActionDatabaseWriter)value;
                var source = value.VariableModel.OfType<ActionTableVariable>();
                ActionVariable[] selectedItems = null;
                if (!string.IsNullOrEmpty(action.VariableName))
                {
                    selectedItems = value.VariableModel.GetVariableObjectsByName(action.VariableName.Split(",".ToCharArray()));
                }
                tableNameList.Set(source.ToArray(), selectedItems);
                comboBoxServerType.SelectedIndex = (int)action.ServerType;
                ObjectToGui(action.ConnectionString);
                base.Action = action;
            }
        }

        private void ObjectToGui(string connectionString) {
               if (SelectedServerType == ServerType.MSSQL) {
                    var dbBuilder= new SqlConnectionStringBuilder(connectionString);
                    textBoxServerAddress.Text = dbBuilder.DataSource;
                    textBoxDBName.Text = dbBuilder.InitialCatalog;
                    checkBoxWinAuth.Checked = dbBuilder.IntegratedSecurity;
                    textBoxPassword.Enabled = !dbBuilder.IntegratedSecurity;
                    textBoxUserName.Enabled = !dbBuilder.IntegratedSecurity;
                    if (!dbBuilder.IntegratedSecurity)
                    {
                        textBoxUserName.Text = dbBuilder.UserID;
                        textBoxPassword.Text = dbBuilder.Password;
                    }
                    else {
                        textBoxUserName.Text = "";
                        textBoxPassword.Text = "";
                    }
                }
                else if (SelectedServerType == ServerType.MySQL)
                {
                    var dbBuilder = new MySqlConnectionStringBuilder(connectionString);
                    textBoxServerAddress.Text = dbBuilder["Server"].ToString();
                    textBoxDBName.Text = dbBuilder["Database"].ToString();
                    checkBoxWinAuth.Checked = false;
                    textBoxPassword.Enabled = false;
                    textBoxUserName.Enabled = false;
                    textBoxUserName.Text = dbBuilder["Uid"].ToString();
                    textBoxPassword.Text = dbBuilder["Pwd"].ToString();
                }
                else {
                    throw new NotImplementedException();
                }
           
        }

        public ucDatabaseWriter(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        private void comboBoxServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedServerType == ServerType.MySQL)
            {
                this.checkBoxWinAuth.Checked = false;
                this.checkBoxWinAuth.Enabled = false;
            }
            else
            {
                this.checkBoxWinAuth.Enabled = true;
            }
        }


        private void Export_Click(object sender, EventArgs e)
        {
           
        }

    
        private string GetConnectionString(bool bConnectToDB)
        {
            switch (this.SelectedServerType)
            {
                case ServerType.MSSQL:
                    if (!this.checkBoxWinAuth.Checked)
                    {
                        return ("server=" + this.textBoxServerAddress.Text + ";Initial Catalog=" + (bConnectToDB ? this.textBoxDBName.Text : "") + ";User ID=" + this.textBoxUserName.Text + ";Password=" + this.textBoxPassword.Text + ";");
                    }
                    return ("server=" + this.textBoxServerAddress.Text + ";Initial Catalog=" + (bConnectToDB ? this.textBoxDBName.Text : "") + ";Integrated Security=true;");

                case ServerType.MySQL:
                    return ("Server=" + this.textBoxServerAddress.Text + ";Database=" + this.textBoxDBName.Text + ";Uid=" + this.textBoxUserName.Text + ";Pwd=" + this.textBoxPassword.Text + ";");
            }
            return "";
        }

        private void connectionTestBtn_Click(object sender, EventArgs e)
        {
            adapter = new SqlAdapter(SelectedServerType, GetConnectionString(false));
            if (adapter.ConnectToDatabase())
            {
                MessageBox.Show("连接成功");
            }
            else {
                MessageBox.Show("数据库连接失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
