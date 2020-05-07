using AutoRobo.ClientLib.PageHooks.Handler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoRobo.ClientLib.PageHooks;
using AutoRobo.ClientLib.PageServices;
using Rhino.Mocks;
using Microsoft.VisualStudio.TestTools.UITesting;
using AutoRobo.ClientLib.PageHooks.Views;
using System.Text.RegularExpressions;
using Util;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 AjaxRequestHandlerTest 的测试类，旨在
    ///包含所有 AjaxRequestHandlerTest 单元测试
    ///</summary>
    [CodedUITest]
    public class AjaxRequestHandlerTest
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
        ///HandleRequest 的测试
        ///</summary>
        [TestMethod()]
        public void HandleRequestTest()
        {
            //TestHelper.UIRun((browser) => {
            //    browser.GoTo("http://localhost/id.cshtml");
            //    browser.WaitForComplete();
            //    IWebsiteService service = MockRepository.GenerateStub<IWebsiteService>();
            //    service.Stub(x => service.EnableAjaxMoniter("")).IgnoreArguments().Return(true);
            //    //service.Stub(x => service.GetFilterKeywords("")).IgnoreArguments().Return("keyword|test");
            //    AjaxRequestHandler target = MockRepository.GenerateMock<AjaxRequestHandler>();
            //    target.Expect(x => target.WebsiteService).Return(service);
            //    IRequest req = new UrlRequest();
            //    req.HttpContext = TestHelper.Context;
            //    Type expected = typeof(EmptyResult); // TODO: 初始化为适当的值
            //    IResult actual;
                
            //    browser.GoTo("http://localhost/test/ajaxMoniterTest.htm");
            //    actual = target.HandleRequest(req);
            //    target.VerifyAllExpectations();
            //    Assert.AreEqual(expected, actual.GetType());
            //});
           
            
        }

        [TestMethod]
        public void Log_script_inject_test() {
            TestHelper.UIRun((browser) =>
            {
                LogRequestHandler handler = new LogRequestHandler();
                
                browser.GoTo("http://localhost/test/logtest.htm");
                browser.WaitForComplete();
                handler.HandleRequest(new UrlRequest() { HttpContext = TestHelper.Context });
            });
        }

        //[TestMethod]
        //public void jquery_inject_test()
        //{
        //    TestHelper.UIRun((browser) =>
        //    {
        //        browser.GoTo("http://www.efwang.com/");
        //        browser.WaitForComplete();
        //        string jqueryScript = HttpRequestWapper.GetData(StringHelper.Domain + "/asserts/jqueryInject.js");
        //        System.Diagnostics.Debug.WriteLine(jqueryScript);
        //        browser.InjectScript(jqueryScript);
        //        browser.InjectScript("alert(typeof $jq);");
        //    });
        //}



        [TestMethod]
        public void String_mitiple_keywords_match() {
            string[] word_list = new string[] { "match", "express", "reat"};
            string regex = string.Format(@"\b{0}({0})\w*\b", string.Join("|", word_list));
            string input = "If you want to match \"great\" or \"reat\" you can express this by a pattern like:";
            bool result = Regex.IsMatch(input, regex);
            Assert.IsTrue(result);


        }

        [TestMethod]
        public void String_mitiple_keywords_match_1() {
            string[] word_list = new string[] { "match", "express", "test" };
            string regex = string.Format(@"\b{0}({0})\w*\b", string.Join("|", word_list));
            string input = "If you want to match \"great\" or \"reat\" you can express this by a pattern like:";
            bool result = Regex.IsMatch(input, regex);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void String_mitiple_keywords_not_match()
        {
            string[] word_list = new string[] {"test" };
            string regex = string.Format(@"\b{0}({0})\w*\b", string.Join("|", word_list));
            string input = "If you want to match \"great\" or \"reat\" you can express this by a pattern like:";
            bool result = Regex.IsMatch(input, regex);
            Assert.IsFalse(result);
        }
    }
}
