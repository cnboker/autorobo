using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using AutoRobo.Core.UserControls.Wizard;
using Util;
using CsvHelper;

namespace AutoRobo.Core.UserControls.DTS
{
    public partial class CSVWizardPage : WizardPage
    {
        public CSVWizardPage()
        {
            InitializeComponent();
            SetActive += new System.ComponentModel.CancelEventHandler(ImportCSVFile_SetActive);
        }

        void ImportCSVFile_SetActive(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Finish);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            OnWizardFinish(new System.ComponentModel.CancelEventArgs());
            DataTable dt = GetContext().Data;
            lblProgress.Text = "导入:" + dt.Rows.Count.ToString() + "行";
            dataGridView_preView.AutoGenerateColumns = true;
            dataGridView_preView.DataSource = dt;            
        }

        public override void OnWizardFinish(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFileToImport.Text))
                {
                    throw new ApplicationException("请选择CSV文件");
                }
               
                DTSWizardBook wizardBook = GetWizard() as DTSWizardBook;
                if (wizardBook.Session.Direction == DTSDirection.Import)
                {
                    Import();
                }
                else {
                    Export();
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message);
            }
        }

        private void Import() {
            if (!File.Exists(txtFileToImport.Text))
            {
                throw new ApplicationException("文件不存在");
            }
            CsvContext context = GetContext() as CsvContext;
            context.CSVFile = txtFileToImport.Text;
            context.SpliterChar = GetSpliterChar();
            context.IncludeHeader = chkFirstRowColumnNames.Checked;            
            context.Data = GetData(context);
        }

        private void Export() {
            using (StreamWriter sw = new StreamWriter(txtFileToImport.Text, false, System.Text.Encoding.UTF8))
            using (var writer = new CsvWriter(sw))
            {
                CsvContext context = GetContext() as CsvContext;                
                DataTable table = context.Data;
                foreach (DataColumn c in table.Columns)
                {
                    writer.WriteField(c.ColumnName);
                }
                writer.NextRecord();
                foreach (DataRow dr in table.Rows)
                {
                    foreach (DataColumn c in table.Columns)
                    {
                        writer.WriteField(dr[c.ColumnName]);
                    }
                    writer.NextRecord();
                }
            }        
        }

        private  DataTable GetData(CsvContext context)
        {
            DataTable dt = new DataTable();
            try
            {
                System.Text.Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");     
                using (var csv = new CsvHelper.CsvReader(new StreamReader(context.CSVFile, encode)))
                {
                    CsvHelper.Configuration.CsvConfiguration config = csv.Configuration;
                    config.Encoding = encode;
                    config.Delimiter = context.SpliterChar.ToString();
                    //csvhelper bug fix
                    config.HasHeaderRecord = !context.IncludeHeader;
                    int rowIndex = 0;
                    int columnIndex = 0;
                    while (csv.Read())
                    {                      
                        dt.ExpandRow(rowIndex);
                        for (int i = 0; i < csv.CurrentRecord.Length; i++)
                        {
                            dt.ExpandColumn(columnIndex);
                            dt.Rows[rowIndex][columnIndex] = csv.GetField(i);
                            columnIndex++;
                        }
                        columnIndex = 0;
                        rowIndex++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        private char GetSpliterChar() {
            char spliter = ';';
            if (rdbComma.Checked)
            {
                spliter = ',';
            }
            else if (rdbSemicolon.Checked) {
                spliter = ';';
            }
            else if (rdbTab.Checked)
            {
                spliter = '\t';
            }
            else if (rdbSeparatorOther.Checked)
            {
                if (txtSeparatorOtherChar.Text.Length == 0)
                {
                    throw new ApplicationException("必须输入其他字符");
                }
                spliter = txtSeparatorOtherChar.Text[0];
            }
            return spliter;
        }
    
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DTSWizardBook wizardBook = GetWizard() as DTSWizardBook;
            if (wizardBook.Session.Direction == DTSDirection.Import)
            {
                OpenFileDialog openFileDialogCSV = new OpenFileDialog();

                openFileDialogCSV.InitialDirectory = Application.ExecutablePath.ToString();
                openFileDialogCSV.Filter = "CSV文件 (*.csv)|*.csv;*.txt";
                openFileDialogCSV.FilterIndex = 1;
                openFileDialogCSV.RestoreDirectory = true;

                if (openFileDialogCSV.ShowDialog() == DialogResult.OK)
                {
                    this.txtFileToImport.Text = openFileDialogCSV.FileName.ToString();
                }
            }
            else {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "CSV文件 (*.csv)|*.csv;*.txt";
                    dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = false;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.txtFileToImport.Text = dialog.FileName;                       
                    }
                }
            }
        }
    }
}
