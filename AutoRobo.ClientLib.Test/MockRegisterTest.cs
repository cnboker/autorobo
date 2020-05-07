using AutoRobo.ClientLib.PageServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using WatiN.Core;
using System.Text;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UITesting;
using AutoRobo.ClientLib.PageHooks.Handler;
using AutoRobo.ClientLib.PageHooks.Views;
using Rhino.Mocks;


namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 MockRegisterTest 的测试类，旨在
    ///包含所有 MockRegisterTest 单元测试
    ///</summary>
   [CodedUITest]
    public class MockRegisterTest
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
        ///IsRegister 的测试
        /////</summary>
        //[TestMethod()]
        //public void IsRegisterTest()
        //{
        //    MockRegister target = new MockRegister(); // TODO: 初始化为适当的值
        //    string websiteId = "19" ; // TODO: 初始化为适当的值
        //    string mockuserId = "585"; // TODO: 初始化为适当的值
        //    bool expected = true; // TODO: 初始化为适当的值
        //    bool actual;
        //    string cookie = TestHelper.GetCookie();

        //    actual = target.IsRegister(cookie, websiteId, mockuserId);
        //    Assert.AreEqual(expected, actual);

        //}


        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookie(string lpszUrl, string lpszCookieName,
            StringBuilder lpszCookieData, ref int lpdwSize);
        [TestMethod()]
        public void GetCookieTest() {
            // find out how big a buffer is needed
            int size = 0;
            InternetGetCookie("localhost", ".ASPXAUTH", null, ref size);

            // create buffer of correct size
            StringBuilder lpszCookieData = new StringBuilder(size);
            InternetGetCookie("localhost", ".ASPXAUTH", lpszCookieData, ref size);

            // get cookie
            string cookie = lpszCookieData.ToString();
           
            Console.WriteLine("cooke:" + cookie);

          

        }


        /// <summary>
        ///UpdateResult 的测试
        ///</summary>
        //[TestMethod()]
        //public void UpdateResultTest()
        //{
        //    MockRegister target = new MockRegister(); // TODO: 初始化为适当的值
        //    string ticket = TestHelper.GetCookie();
        //    string registerResultId = "676";
        //    bool isRegister = true; // TODO: 初始化为适当的值
        //    target.UpdateRegisterResult(ticket, registerResultId, isRegister);
            
        //}

        /// <summary>
        ///RequireEmailActive 的测试
        ///</summary>
        //[TestMethod()]
        //public void RequireEmailActiveTest()
        //{
        //    MockRegister target = new MockRegister(); // TODO: 初始化为适当的值
        //    string ticket = TestHelper.GetCookie();
        //    string websiteId = "19";
        //    bool expected = true; // TODO: 初始化为适当的值
        //    bool actual;
        //    actual = target.RequireEmailActive(ticket, websiteId);
        //    Assert.AreEqual(expected, actual);
         
        //}

        /// <summary>
        ///EmailIsActive 的测试
        ///</summary>
        //[TestMethod()]
        //public void EmailIsActiveTest()
        //{
        //    MockRegister target = new MockRegister(); // TODO: 初始化为适当的值
        //    string ticket = TestHelper.GetCookie();
        //    string mockUserId = "570";
        //    bool expected = false; // TODO: 初始化为适当的值
        //    bool actual;
        //    actual = target.EmailIsActive(ticket, mockUserId);
        //    Assert.AreEqual(expected, actual);
            
        //}

        /// <summary>
        ///CreateOrGetMockUser 的测试
        ///</summary>
        //[TestMethod()]
        //public void CreateOrGetMockUserTest()
        //{
        //    MockRegister target = new MockRegister(); // TODO: 初始化为适当的值
        //    string ticket = TestHelper.GetCookie();
        //    string userId = "34";
        //    //string expected = "; // TODO: 初始化为适当的值
        //    JavaScriptObject actual;
        //    actual = target.CreateOrGetMockUser(ticket, userId);
        //    Assert.IsNotNull(actual);
        //}

        //[TestMethod]
        //public void EmailRegister_RequestHandler_Test() {
        //    TestHelper.UIRun((browser) => {
        //        browser.GoTo("http://www.baidu.com");
               
        //        RegisterEmailView eh = new RegisterEmailView();
        //        eh.View(browser);
        //    });
        //}
    }
}
