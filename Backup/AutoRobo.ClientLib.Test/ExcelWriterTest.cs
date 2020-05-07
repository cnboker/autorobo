using AutoRobo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 ExcelWriterTest 的测试类，旨在
    ///包含所有 ExcelWriterTest 单元测试
    ///</summary>
    [TestClass()]
    public class ExcelWriterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///ExportToCVS 的测试
        ///</summary>
        [TestMethod()]
        public void ExportToCVSTest()
        {
            ExcelWriter target = new ExcelWriter(); // TODO: 初始化为适当的值
            DataTable pDataTable = new DataTable();
            pDataTable.Columns.Add("Title", typeof(string));
            pDataTable.Columns.Add("Description", typeof(string));
            DataRow dr = pDataTable.NewRow();
            dr["Title"] = "酒类";
            dr["Description"] = "葡萄酒有益的健康";
            pDataTable.Rows.Add(dr);

            DataGridView dgv = null; // TODO: 初始化为适当的值
            bool containHeader = false; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
        
            actual = target.ExportToCVS(pDataTable, dgv, containHeader);
            SaveFile(actual);
            SaveFile(actual);
        
        }

        private void SaveFile(string content) {
            string FileName = "d:/test.csv";
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(FileName, true, Encoding.UTF8);
                ExcelWriter writer = new ExcelWriter();                
                sw.Write(content);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
