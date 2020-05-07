using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using AutoRobo.Core.Actions.InOut;
using CsvHelper;

namespace AutoRobo.Core.IO
{
    public class DataTableReader
    {
        private FileType _fileType;
        private string _readFile;
        private bool _hasHeaderRecord;

        public DataTableReader(FileType fileType, string readFile, bool hasHeaderRecord)
        {
            _fileType = fileType;
            _readFile = readFile;
            _hasHeaderRecord = hasHeaderRecord;
        }

        public DataTable Read()
        {
            DataTable table = null;
            if (_fileType == FileType.CSV)
            {
                table = ReadFromCSV(_readFile);
            }
            else if (_fileType == FileType.TSV)
            {
                table = ReadFromTSV(_readFile);
            }
            else if (_fileType == FileType.XLS)
            {
                table = ReadFromXLS(_readFile);
            }
            else
            {
                new NotImplementedException();
            }
            return table;
        }

        private DataTable ReadFromCSV(string csvFile) {
            return ReadFromXSV(csvFile, ",");
        }

        private DataTable ReadFromXSV(string file, string delimiter)
        {
            DataTable table = new DataTable();
            using (var csv = new CsvReader(new StreamReader(file, Encoding.UTF8)))
            {
                csv.Configuration.Delimiter = delimiter;
                csv.Configuration.HasHeaderRecord = this._hasHeaderRecord;
                while (csv.Read())
                {
                    string[] records = csv.CurrentRecord;
                    table.ExpandColumn(records.Length);
                    DataRow dr = table.NewRow();
                    for (int i = 0; i < records.Length; i++)
                    {
                        dr[i] = records[i];
                    }
                    table.Rows.Add(dr);                    
                }
            }
            return table;
        }

        private DataTable ReadFromTSV(string tsvFile)
        {
            return ReadFromXSV(tsvFile, "\t");
        }

        private DataTable ReadFromXLS(string xlsFile)
        {
            Net.SourceForge.Koogra.IWorkbook wb = GetWorkBook(xlsFile);
            if (wb.Worksheets.Count > 0)
            {
                Net.SourceForge.Koogra.IWorksheet ws = wb.Worksheets.GetWorksheetByIndex(0);
                return GetDataTable(ws);
            }
            return null;
        }

        private Net.SourceForge.Koogra.IWorkbook GetWorkBook(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            Net.SourceForge.Koogra.IWorkbook wb = null;
            if (ext.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                wb = (Net.SourceForge.Koogra.WorkbookFactory.GetExcel2007Reader(fileName));
            }
            else if (ext.Equals(".xls", StringComparison.OrdinalIgnoreCase))
            {
                wb = (Net.SourceForge.Koogra.WorkbookFactory.GetExcelBIFFReader(fileName));
                //读链接内容
                Net.SourceForge.Koogra.Excel.Workbook workbook = wb as Net.SourceForge.Koogra.Excel.Workbook;
            }
            return wb;
        }

        private DataTable GetDataTable(Net.SourceForge.Koogra.IWorksheet ws)
        {
            DataTable table = new DataTable();
            int rowIndex = 0;
            int columnIndex = 0;
            uint firstRow = ws.FirstRow;
            for (uint r = firstRow; r <= ws.LastRow; ++r)
            {
                table.ExpandRow(rowIndex);
                Net.SourceForge.Koogra.IRow row = ws.Rows.GetRow(r);
                if (row != null)
                {
                    for (uint colCount = ws.FirstCol; colCount <= ws.LastCol; ++colCount)
                    {
                        table.ExpandColumn(columnIndex);
                        if (row.GetCell(colCount) != null && row.GetCell(colCount).Value != null)
                        {
                            table.Rows[rowIndex][columnIndex] = row.GetCell(colCount).Value.ToString();
                        }
                        else
                        {
                            table.Rows[rowIndex][columnIndex] = "";
                        }
                        columnIndex++;
                    }
                    columnIndex = 0;
                }
                rowIndex++;
            }
            return table;
        }

      
    }
}
