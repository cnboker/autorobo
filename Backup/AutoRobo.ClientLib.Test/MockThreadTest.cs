using AutoRobo.ClientLib.PageServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoRobo.ClientLib.PageHooks.Views;
using System.Windows.Forms;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 MockThreadTest 的测试类，旨在
    ///包含所有 MockThreadTest 单元测试
    ///</summary>
    [TestClass()]
    public class MockThreadTest
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
        ///AutoRobo.ClientLib.PageServices.IMockThread.Publish 的测试
        ///</summary>
        //[TestMethod()]
        //[DeploymentItem("AutoRobo.ClientLib.dll")]
        //public void PublishTest()
        //{
        //    IMockThread target = new MockThread(); // TODO: 初始化为适当的值
        //    string ticket = TestHelper.GetCookie();
        //    string currentUserId = "3";
        //    string mockUserId = "585";
        //    string scriptId = "2";
        //    string schemaObjectId = "11";
        //  //  target.Publish(ticket, currentUserId, mockUserId, scriptId, schemaObjectId);
            
        //}

        [TestMethod]
        public void SchemaObjectSelect_loaddata_test() {
            //SchemaObjectSelect so = new SchemaObjectSelect("3", "71");
           // Application.Run(so);
        }
    }
}
