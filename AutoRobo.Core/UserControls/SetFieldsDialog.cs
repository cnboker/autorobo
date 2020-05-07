using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CsvHelper;

namespace AutoRobo.Core.UserControls
{
    public partial class SetFieldsDialog : Form
    {
        private CsvReader csv = null;
        private string[] fieldHeaders = null;

        public DataTable GetDataTable() {
            List<string> headers = new List<string>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[0].Value != null)
                {
                    headers.Add(item.Cells[0].Value.ToString());
                }
            }
            if (headers.Count == 0) {
                throw new ApplicationException("必须为表定义表头");
            }
            DataTable table = new DataTable();
         
            foreach (var columnName in headers)
            {
                table.Columns.Add(new DataColumn(columnName, typeof(string)));
            }
            
            if (!HasHeaderRecordCheckbox.Checked) {
                DataRow newRow = table.NewRow();
                for (int i = 0; i < fieldHeaders.Length; i++)
                {
                    if (table.Columns.Count > i)
                    {
                        newRow[i] = fieldHeaders[i];
                    }
                }
                table.Rows.Add(newRow);
            }
            while (csv.Read())
            {
                DataRow newRow = table.NewRow();
                for (int i = 0; i < fieldHeaders.Length; i++)
                {
                    if (table.Columns.Count > i)
                    {
                        newRow[i] = csv.GetField(i);
                    }
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

    
        public SetFieldsDialog(CsvReader csv)
        {
            InitializeComponent();
            this.csv = csv;
            csv.Configuration.HasHeaderRecord = false;
            csv.Read();
            fieldHeaders = csv.CurrentRecord;
            dataGridView1.DataSource = ConvertListToDataTable(fieldHeaders);
            dataGridView1.MouseUp += new MouseEventHandler(dataGridView1_MouseUp);
        }

        void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
                    dataGridView1.BeginEdit(true);
                else
                    dataGridView1.EndEdit();
            }
        }

        private DataTable ConvertListToDataTable(string[] list)
        {
            // New table.
            DataTable table = new DataTable();
            table.Columns.Add("default");
            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }
            return table;
        }
    }
}
