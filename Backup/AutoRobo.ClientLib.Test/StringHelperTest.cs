using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util;
using System.Data;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 StringHelperTest 的测试类，旨在
    ///包含所有 StringHelperTest 单元测试
    ///</summary>
    [TestClass()]
    public class StringHelperTest
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
        ///GetSecondLevelHostName 的测试
        ///</summary>
        [TestMethod()]
        public void GetSecondLevelHostNameTest()
        {
            string hostName = "http://www.qd8.com.cn"; // TODO: 初始化为适当的值
            string expected = "qd8.com.cn"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetRootDomain(hostName);
            Assert.AreEqual(expected, actual);

            hostName = "http://www.cnboker.com";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///GetSecondLevelHostName 的测试
        ///</summary>
        [TestMethod()]
        public void GetSecondLevelHostNameTest1()
        {
            string hostName = "http://www.cnboker.com"; // TODO: 初始化为适当的值
            string expected = "cnboker.com"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetRootDomain(hostName);
            Assert.AreEqual(expected, actual);

           
        }

        /// <summary>
        ///GetSecondLevelHostName 的测试
        ///</summary>
        [TestMethod()]
        public void GetSecondLevelHostNameTest2()
        {
            string hostName = "https://www.cnboker.cn"; // TODO: 初始化为适当的值
            string expected = "cnboker.cn"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetRootDomain(hostName);
            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        ///GetSecondLevelHostName 的测试
        ///</summary>
        [TestMethod()]
        public void GetSecondLevelHostNameTest3()
        {
            string hostName = "http://cnboker.cn"; // TODO: 初始化为适当的值
            string expected = "cnboker.cn"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetRootDomain(hostName);
            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        ///GetSecondLevelHostName 的测试
        ///</summary>
        [TestMethod()]
        public void GetDomainTest()
        {
            string hostName = "http://mail.cnboker.cn"; // TODO: 初始化为适当的值
            string expected = "mail.cnboker.cn"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetDomain(hostName);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void GetLongDomainTest_1()
        {
            string hostName = "http://work.china.alibaba.com/home/page/index.htm"; // TODO: 初始化为适当的值
            string expected = "work.china.alibaba.com"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetDomain(hostName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetLongRootDomainTest_1()
        {
            string hostName = "http://work.china.alibaba.com/home/page/index.htm"; // TODO: 初始化为适当的值
            string expected = "alibaba.com"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetRootDomain(hostName);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///Contain 的测试
        ///</summary>
        [TestMethod()]
        public void ContainTest()
        {
            string source = "If you want to match \"great\" or \"reat\" you can express this by a pattern like:";
            string[] keywords = new string[] { "want", "reat" }; // TODO: 初始化为适当的值
            bool expected = true; // TODO: 初始化为适当的值
            bool actual;
            actual = StringHelper.Contain(source, keywords);
            Assert.AreEqual(expected, actual);
           
        }



        /// <summary>
        ///ConvertDataTableToString 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertDataTableToStringTest()
        {
            DataTable dataTable = new DataTable();
            DataColumn dc = new DataColumn("name", typeof(string));
            dataTable.Columns.Add(dc);
            dc = new DataColumn("age", typeof(int));
            dataTable.Columns.Add(dc);
            dc = new DataColumn("desc", typeof(string));
            dataTable.Columns.Add(dc);
            for (int i = 0; i < 5; i++) {
                DataRow dr = dataTable.NewRow();
                dr["name"] = "scott";
                dr["age"] = 36;
                dr["desc"] = "excellent developer " + DateTime.Now.ToString();
                dataTable.Rows.Add(dr);
            }
            
            string actual;
            actual = StringHelper.ConvertDataTableToString(dataTable);
            Console.Write(actual);
        }

        /// <summary>
        ///XPathStringCompare 的测试
        ///</summary>
        //[TestMethod()]
        //public void XPathStringCompareTest()
        //{
        //    string xpath1 = "/html/body/div[3]/div[2]/div[1]/ol[2]/li[2]/div/div/h3"; // TODO: 初始化为适当的值
        //    string xpath2 = "/html/body/div[3]/div[2]/div[1]/ol[2]/li[50]/div/div/h3"; // TODO: 初始化为适当的值
        //    string expected = string.Empty; // TODO: 初始化为适当的值
        //    string actual;
        //    actual = StringHelper.XPathStringCompare(xpath1, xpath2);
        //    Console.Write(actual);
        //    Assert.IsNotNull(actual);
            
        //}

        /// <summary>
        ///XPathStringCompare 的测试
        ///</summary>
        //[TestMethod()]
        //public void XPathStringCompareTest1()
        //{
        //    string xpath1 = "/html/body/div[3]/div[2]/div[1]/ol[2]/li[2]/div/div/h3"; // TODO: 初始化为适当的值
        //    string xpath2 = "/html/body/div[3]/div[2]/div[1]/ol[1]/li[50]/div/div/h3"; // TODO: 初始化为适当的值
        //    string expected = string.Empty; // TODO: 初始化为适当的值
        //    string actual;
        //    actual = StringHelper.XPathStringCompare(xpath1, xpath2);
        //    Console.Write(actual);
        //    Assert.IsNull(actual);

        //}

        /// <summary>
        ///XPathStringCompare 的测试
        ///</summary>
        //[TestMethod()]
        //public void XPathStringCompareTest2()
        //{
        //    string xpath1 = "/html/body/div[3]/div[2]/div[1]/ol[2]/li[2]/div/div/h3"; // TODO: 初始化为适当的值
        //    string xpath2 = "/html/body/div[3]/div[2]/div[1]/ol[2]/li/div/div/h3"; // TODO: 初始化为适当的值
        //    string expected = string.Empty; // TODO: 初始化为适当的值
        //    string actual;
        //    actual = StringHelper.XPathStringCompare(xpath1, xpath2);
        //    Console.Write(actual);
        //    Assert.IsNotNull(actual);

        //}

        /// <summary>
        ///GetHttpDomain 的测试
        ///</summary>
        [TestMethod()]
        public void GetHttpDomainTest()
        {
            string rawUrl = "http://work.china.alibaba.com/list/new"; // TODO: 初始化为适当的值
            string expected = "http://work.china.alibaba.com"; // TODO: 初始化为适当的值
            string actual;
            actual = StringHelper.GetHttpDomain(rawUrl);
            Assert.AreEqual(expected, actual);
        }
    }
}
