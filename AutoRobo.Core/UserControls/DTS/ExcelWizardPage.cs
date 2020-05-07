using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using AutoRobo.WebHelper;
using AutoRobo.Core.UserControls.Wizard;

namespace AutoRobo.Core.UserControls.DTS
{
    public partial class ExcelWizardPage : WizardPage
    {
        public ExcelWizardPage()
        {
            InitializeComponent();
            SetActive += new CancelEventHandler(ImprtExcelFile_SetActive);
        }

        void ImprtExcelFile_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Finish);
        }

        public override void OnWizardFinish(CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFileToImport.Text))
                {
                    throw new ApplicationException("请选择excel文件");
                }
                ExcelContext context = GetContext() as ExcelContext;
                context.ExcelFile = txtFileToImport.Text;
                context.IncludeHeader = chkFirstRowColumnNames.Checked;
                DTSWizardBook wizardBook = GetWizard() as DTSWizardBook;
                if (wizardBook.Session.Direction == DTSDirection.Import)
                {
                    Import(context);
                }
                else
                {
                    Export(context);
                }
            }
            catch (Exception ex) {
                e.Cancel = true;
                MessageBox.Show(ex.Message);
            }
        }

        private void Import(ExcelContext context)
        {
            if (!File.Exists(txtFileToImport.Text))
            {
                throw new ApplicationException("文件不存在");
            }          
            context.Data = GetData(context);
        }

        private void Export(ExcelContext context)
        {            
            DataSet ds = new DataSet();
            DataTable dt = context.Data.Copy();
            dt.TableName = "tmp";
            ds.Tables.Add(dt);
            //Here's the easy part. Create the Excel worksheet from the data set
            ExcelLibrary.DataSetHelper.CreateWorkbook(context.ExcelFile, ds);            
            ds.Dispose();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DTSWizardBook wizardBook = GetWizard() as DTSWizardBook;
            if (wizardBook.Session.Direction == DTSDirection.Import)
            {
                OpenFileDialog openFileDialogCSV = new OpenFileDialog();

                openFileDialogCSV.InitialDirectory = Application.ExecutablePath.ToString();
                openFileDialogCSV.Filter = "excel文件|*.xls;*.xlsx";
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
                    dialog.Filter = "excel文件|*.xls;*.xlsx";
                    dialog.FilterIndex = 2;
                    dialog.RestoreDirectory = false;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.txtFileToImport.Text = dialog.FileName;
                    }
                }
            }
        }

        private DataTable GetData(ExcelContext context)
        {
            DataTable dt = new DataTable();
            try
            {
                string ext = Path.GetExtension(context.ExcelFile);
                Net.SourceForge.Koogra.IWorkbook wb = null;
                if (ext.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    wb = (Net.SourceForge.Koogra.WorkbookFactory.GetExcel2007Reader(context.ExcelFile));                    
                }
                else if (ext.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    wb = (Net.SourceForge.Koogra.WorkbookFactory.GetExcelBIFFReader(context.ExcelFile));
                    //读链接内容
                    Net.SourceForge.Koogra.Excel.Workbook workbook = wb as Net.SourceForge.Koogra.Excel.Workbook;
                    context.Links = workbook.HyperLinks.AsEnumerable().Select(c => c.Link).ToList();
                }
                
                Net.SourceForge.Koogra.IWorksheet ws = wb.Worksheets.GetWorksheetByIndex(0);                
                int rowIndex = 0;
                int columnIndex = 0;
                uint firstRow = ws.FirstRow;
                if (!context.IncludeHeader) {
                    firstRow += 1;
                }
                for (uint r = firstRow; r <= ws.LastRow; ++r)
                {
                    dt.ExpandRow(rowIndex);
                    Net.SourceForge.Koogra.IRow row = ws.Rows.GetRow(r);
                    if (row != null)
                    {
                        for (uint colCount = ws.FirstCol; colCount <= ws.LastCol; ++colCount)
                        {
                            dt.ExpandColumn(columnIndex);
                            if (row.GetCell(colCount) != null && row.GetCell(colCount).Value != null)
                            {
                                dt.Rows[rowIndex][columnIndex] = row.GetCell(colCount).Value.ToString();
                            }
                            else
                            {
                                dt.Rows[rowIndex][columnIndex] = "";
                            }
                            columnIndex++;
                        }
                        columnIndex = 0;
                    }
                    rowIndex++;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

      

 
        private void btnPreview_Click(object sender, EventArgs e)
        {
            OnWizardFinish(new CancelEventArgs());
            DataTable dt = GetContext().Data;
            lblProgress.Text = "导入:" + dt.Rows.Count.ToString() + "行";
            dataGridView_preView.AutoGenerateColumns = true;
            dataGridView_preView.DataSource = dt;
        }

        private void linkBtn_Click(object sender, EventArgs e)
        {
            OnWizardFinish(new CancelEventArgs());
            ExcelContext context = GetContext() as ExcelContext;
            StringBuilder sb = new StringBuilder();
            foreach (var s in context.Links) {
                sb.Append(s.Replace("\0", ""));
                sb.AppendLine();
            }            
            NotepadHelper.ShowMessage(sb.ToString(), "当前excel文档包含链接");
        }

     
    }
}