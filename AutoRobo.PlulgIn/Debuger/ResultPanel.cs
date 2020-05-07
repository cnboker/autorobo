using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Models;
using System.IO;
using AutoRobo.Core;

namespace AutoRobo.PlulgIn.Debuger
{
    public partial class ResultPanel : UserControl
    {
        //private VariableSet resultSet;

        public ResultPanel()
        {
            //this.resultSet = resultSet;
            InitializeComponent();
        }

        public void Bind(DataTable table) {
            dataView.DataSource = table;         
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "xls文件 (*.xls)|*.xls";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Can use dialog.FileName
                    ExcelWriter writer = new ExcelWriter();
                    writer.DataGridView2Excel(dataView, dialog.FileName, "sheet1");                   
                }
            }
        }
    }
}
