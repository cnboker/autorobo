using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace AutoRobo.Core
{
	/// <summary>
	/// Summary description for ExcelReader.
	/// </summary>
	public class ExcelReader : IDisposable
	{
		#region Variables
		private int[] PkCol;
		private string StrExcelFilename;
		private bool BlnMixedData=true;
	    private string StrSheetName;
		private string StrSheetRange;
	    private OleDbConnection OleConn; 
		private OleDbCommand OleCmdSelect;
		private OleDbCommand OleCmdUpdate;
		#endregion

		#region properties

		public int[] PKCols
		{
			get {return PkCol;}
			set {PkCol=value;}
		}

		public string ColName(int intCol)
		{
		    string sColName;
			if (intCol<26)
				sColName= Convert.ToString(Convert.ToChar((Convert.ToByte('A')+intCol)) );
			else
			{
				int intFirst = (intCol / 26);
				int intSecond = (intCol % 26);
				sColName= Convert.ToString(Convert.ToByte('A')+intFirst);
				sColName += Convert.ToString(Convert.ToByte('A')+intSecond);
			}
			return sColName;
		}

		public int ColNumber(string strCol)
		{
			strCol = strCol.ToUpper(); 
			int intColNumber;
			if (strCol.Length>1) 
			{
				intColNumber = Convert.ToInt16(Convert.ToByte(strCol[1])-65);  
				intColNumber += Convert.ToInt16(Convert.ToByte(strCol[1])-64)*26; 
			}
			else
				intColNumber = Convert.ToInt16(Convert.ToByte(strCol[0])-65);  
			return intColNumber;
		}
	


		public String[] GetExcelSheetNames()
		{
			
			DataTable dt = null;

			try
			{
				if (OleConn ==null) Open();
				
				// Get the data table containing the schema
			    if (OleConn != null) dt = OleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

			    if(dt == null)
				{
					return null;
				}

				var excelSheets = new String[dt.Rows.Count];
				int i = 0;

				// Add the sheet name to the string array.
				foreach(DataRow row in dt.Rows)
				{
					string strSheetTableName = row["TABLE_NAME"].ToString();
					excelSheets[i] = strSheetTableName.Substring(0,strSheetTableName.Length-1); 
					i++;
				}
				

				return excelSheets;
			}
			catch(Exception)
			{
				return null;
			}
			finally
			{
				// Clean up.
				if(KeepConnectionOpen==false)
				{
					Close();
				}
				if(dt != null)
				{
					dt.Dispose();
				}
			}
		}
															
		public string ExcelFilename
		{
			get { return StrExcelFilename;}
			set { StrExcelFilename=value;}
		}

		public string SheetName
		{
			get { return StrSheetName;}
			set { StrSheetName=value;}
		}

		public string SheetRange
		{
			get {return StrSheetRange;}
			set 
			{
				if (value.IndexOf(":")==-1) throw new Exception("Invalid range length"); 
				StrSheetRange=value;}
		}

	    public bool KeepConnectionOpen { get; set; }

	    public bool Headers { get; set; }

	    public bool MixedData
		{
			get {return BlnMixedData;}
			set {BlnMixedData=value;}
		}
		#endregion

		#region Methods

		#region Excel Connection
		private string ExcelConnectionOptions()
		{
			string strOpts="";
			if (MixedData)
				strOpts += "Imex=2;";
			if (Headers)
				strOpts += "HDR=Yes;";
			else	
				strOpts += "HDR=No;";
			return strOpts;
		}

        private string ExcelConnection()
        {
            try
            {
                return
                    @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                    @"Data Source=" + StrExcelFilename + ";" +
                    @"Extended Properties=" + Convert.ToChar(34) +
                    @"Excel 8.0;" + ExcelConnectionOptions() + Convert.ToChar(34);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message, "Excel Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
		#endregion

		#region Open / Close
		public void Open()
		{
			try
			{
				if (OleConn !=null)
				{
					if (OleConn.State==ConnectionState.Open)
					{
						OleConn.Close();
					}
					OleConn=null;
				}

				if (System.IO.File.Exists(StrExcelFilename)==false)
				{
					throw new Exception("Excel file " + StrExcelFilename +  "could not be found.");
				}
				OleConn = new OleDbConnection(ExcelConnection());  
				OleConn.Open();   				
			}
			catch (InvalidOperationException ex)
			{
                MessageBox.Show(ex.Message, "Excel File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message, "Excel File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		public void Close()
		{
			if (OleConn !=null)
			{
				if (OleConn.State != ConnectionState.Closed) 
					OleConn.Close(); 
				OleConn.Dispose();
				OleConn=null;
			}
		}
		#endregion

		#region Command Select
        private bool SetSheetQuerySelect()
        {
            if (OleConn == null)
            {
                throw new Exception("Connection is unassigned or closed.");
            }

            if (StrSheetName.Length == 0)
                throw new Exception("Sheetname was not assigned.");

            OleCmdSelect = new OleDbCommand(
                @"SELECT * FROM ["
                + StrSheetName
                + "$" + StrSheetRange
                + "]", OleConn);

            return true;
        }

	    #endregion

		#region Simple Utilities

		private static string AddWithComma(string strSource,string strAdd)
		{
			if (strSource != "") strSource = strSource + ", ";
			return strSource + strAdd;
		}

		private static string AddWithAnd(string strSource,string strAdd)
		{
			if (strSource != "") strSource = strSource + " and ";
			return strSource + strAdd;
		}
		#endregion

        private OleDbDataAdapter SetSheetQueryAdapter(DataTable dt)
        {
            // Deleting in Excel workbook is not possible
            // So this command is not defined
            if (OleConn == null)
            {
                throw new Exception("Connection is unassigned or closed.");
            }

            if (StrSheetName.Length == 0)
                throw new Exception("Sheetname was not assigned.");

            if (PKCols == null)
                throw new Exception("Cannot update excel sheet with no primarykey set.");
            if (PKCols.Length < 1)
                throw new Exception("Cannot update excel sheet with no primarykey set.");

            var oleda = new OleDbDataAdapter(OleCmdSelect);
            string strUpdate = "";
            string strInsertPar = "";
            string strInsert = "";
            string strWhere = "";

            for (int iPk = 0; iPk < PKCols.Length; iPk++)
            {
                strWhere = AddWithAnd(strWhere, dt.Columns[iPk].ColumnName + "=?");
            }
            strWhere = " Where " + strWhere;

            for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
            {
                strInsert = AddWithComma(strInsert, dt.Columns[iCol].ColumnName);
                strInsertPar = AddWithComma(strInsertPar, "?");
                strUpdate = AddWithComma(strUpdate, dt.Columns[iCol].ColumnName) + "=?";
            }

            string strTable = "[" + SheetName + "$" + SheetRange + "]";
            strInsert = "INSERT INTO " + strTable + "(" + strInsert + ") Values (" + strInsertPar + ")";
            strUpdate = "Update " + strTable + " Set " + strUpdate + strWhere;

            oleda.InsertCommand = new OleDbCommand(strInsert, OleConn);
            oleda.UpdateCommand = new OleDbCommand(strUpdate, OleConn);
            OleDbParameter oleParIns;
            OleDbParameter oleParUpd;
            for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
            {
                oleParIns = new OleDbParameter("?", dt.Columns[iCol].DataType.ToString());
                oleParUpd = new OleDbParameter("?", dt.Columns[iCol].DataType.ToString());
                oleParIns.SourceColumn = dt.Columns[iCol].ColumnName;
                oleParUpd.SourceColumn = dt.Columns[iCol].ColumnName;
                oleda.InsertCommand.Parameters.Add(oleParIns);
                oleda.UpdateCommand.Parameters.Add(oleParUpd);
            }

            for (int iPk = 0; iPk < PKCols.Length; iPk++)
            {
                oleParUpd = new OleDbParameter("?", dt.Columns[iPk].DataType.ToString())
                                {
                                    SourceColumn = dt.Columns[iPk].ColumnName,
                                    SourceVersion = DataRowVersion.Original
                                };
                oleda.UpdateCommand.Parameters.Add(oleParUpd);
            }
            return oleda;
        }

	    #region Command Single Value Update

        private bool SetSheetQuerySingleValUpdate(string strVal)
        {
            if (OleConn == null)
            {
                throw new Exception("Connection is unassigned or closed.");
            }

            if (StrSheetName.Length == 0)
                throw new Exception("Sheetname was not assigned.");

            OleCmdUpdate = new OleDbCommand(
                @" Update ["
                + StrSheetName
                + "$" + StrSheetRange
                + "] set F1=" + strVal, OleConn);
            return true;
        }

	    #endregion

		public void SetPrimaryKey(int intCol)
		{
			PkCol = new[] { intCol };			
		}

		public DataTable GetTable()
		{
			return GetTable("ExcelTable");
		}

        private void SetPrimaryKey(DataTable dt)
        {
            if (PKCols != null)
            {
                //set the primary key
                if (PKCols.Length > 0)
                {
                    var dc = new DataColumn[PKCols.Length];
                    for (int i = 0; i < PKCols.Length; i++)
                    {
                        dc[i] = dt.Columns[PKCols[i]];
                    }

                    dt.PrimaryKey = dc;
                }
            }
        }

	    public DataTable GetTable(string strTableName)
		{
            string crlfReplacementVal = Convert.ToChar(7).ToString();
			try
			{
				//Open and query
				if (OleConn ==null) Open();
			    if (OleConn != null)
			        if (OleConn.State != ConnectionState.Open)
			            throw new Exception("Connection cannot open error.");
			    if (SetSheetQuerySelect()==false) return null;

				//Fill table
				var oleAdapter = new OleDbDataAdapter {SelectCommand = OleCmdSelect};
			    var dt = new DataTable(strTableName);
				oleAdapter.FillSchema(dt,SchemaType.Source);  
				oleAdapter.Fill(dt);
				if (Headers ==false)
				{
					if (StrSheetRange.IndexOf(":")>0)
					{
						string firstCol = StrSheetRange.Substring(0,StrSheetRange.IndexOf(":")-1); 
						int intCol = ColNumber(firstCol);
						for (int intI=0;intI<dt.Columns.Count;intI++)
						{
							dt.Columns[intI].Caption =ColName(intCol+intI);
						}
					}
				}
				SetPrimaryKey(dt);
				//Cannot delete rows in Excel workbook
				dt.DefaultView.AllowDelete =false;
			
				//Clean up
				OleCmdSelect.Dispose();
				OleCmdSelect=null;
				oleAdapter.Dispose();
				if (KeepConnectionOpen==false) Close();
                // replace CRLFReplacementVal with '\n' in the Step Data field
                string tempVal;
                foreach (DataRow myRow in dt.Rows)
                {
                    object[] tempVals = myRow.ItemArray;
                    if (tempVals[1].GetType() != typeof(DBNull))
                    {
                        tempVal = (string)tempVals[1];
                        tempVal = tempVal.Replace(crlfReplacementVal, Convert.ToString('\n'));
                        tempVals[1] = tempVal;
                        myRow.ItemArray = tempVals;
                        dt.AcceptChanges();
                    }
                }
				return dt;			

			}
			catch (OleDbException ex)
			{
				MessageBox.Show(ex.Message,"File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
		}
        
		private void CheckPkExists(DataTable dt)
		{
			if (dt.PrimaryKey.Length==0) 
				if (PKCols !=null)
				{
					SetPrimaryKey(dt);
				}
				else throw new Exception("Provide an primary key to the datatable"); 
		}

        public DataTable SetTable(DataTable dt)
        {
            DataTable dtChanges = dt.GetChanges();
            if (dtChanges == null) throw new Exception("There are no changes to be saved!");
            CheckPkExists(dt);
            //Open and query
            if (OleConn == null) Open();
            if (OleConn != null)
                if (OleConn.State != ConnectionState.Open)
                    throw new Exception("Connection cannot open error.");
            if (SetSheetQuerySelect() == false) return null;

            //Fill table
            OleDbDataAdapter oleAdapter = SetSheetQueryAdapter(dtChanges);

            oleAdapter.Update(dtChanges);
            //Clean up
            OleCmdSelect.Dispose();
            OleCmdSelect = null;
            oleAdapter.Dispose();

            if (KeepConnectionOpen == false) Close();
            return dt;
        }

	    #region Get/Set Single Value

		public void SetSingleCellRange(string strCell)
		{
			StrSheetRange = strCell + ":" + strCell;
		}

		public object GetValue(string strCell)
		{
			SetSingleCellRange(strCell);
		    //Open and query
			if (OleConn ==null) Open();
		    if (OleConn != null)
		        if (OleConn.State != ConnectionState.Open)
		            throw new Exception("Connection is not open error.");

		    if (SetSheetQuerySelect()==false) return null;
			object objValue = OleCmdSelect.ExecuteScalar();

			OleCmdSelect.Dispose();
			OleCmdSelect=null;	
			if (KeepConnectionOpen==false) Close();
			return objValue;
		}

		public void SetValue(string strCell,object objValue)
		{
			
			try
			{

				SetSingleCellRange(strCell);
				//Open and query
				if (OleConn ==null) Open();
			    if (OleConn != null)
			        if (OleConn.State != ConnectionState.Open)
			            throw new Exception("Connection is not open error.");

			    if (SetSheetQuerySingleValUpdate(objValue.ToString()) == false) return;
				OleCmdUpdate.ExecuteNonQuery(); 

				OleCmdUpdate.Dispose();
				OleCmdUpdate=null;	
				if (KeepConnectionOpen==false) Close();
			}
			finally
			{
				if (OleCmdUpdate != null)
				{
					OleCmdUpdate.Dispose();
					OleCmdUpdate=null;
				}
			}
			
		}
		#endregion
		

		#endregion

		public 

		#region Dispose / Destructor
		void Dispose()
		{
			if (OleConn !=null)
			{
				OleConn.Dispose();
				OleConn=null;
			}
			if (OleCmdSelect!=null)
			{
				OleCmdSelect.Dispose(); 
					OleCmdSelect=null;
			}
			// Dispose of remaining objects.
		}
#endregion
	
		#region CTOR

	    #endregion
	}
}
