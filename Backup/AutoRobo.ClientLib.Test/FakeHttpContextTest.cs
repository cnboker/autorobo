using AutoRobo.ClientLib.PageHooks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.JScript;
using Rhino.Mocks;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageHooks.Handler;
using WatiN.Core;
using AutoRobo.ClientLib.PageServices;

namespace AutoRobo.ClientLib.Test
{ 
    /// <summary>
    ///这是 FakeHttpContextTest 的测试类，旨在
    ///包含所有 FakeHttpContextTest 单元测试
    ///</summary>
    [TestClass()]
    public class FakeHttpContextTest 
    {
        /// <summary>
        ///Handle 的测试
        ///</summary>
        [TestMethod()]
        //public void firstHandler_isok_gotoNext()
        //{
        //   // TestHelper.UIRun();
        //    FakeHttpContext context = TestHelper.CreateContext(null);
            
        //    IRequestHandler first = MockRepository.GenerateMock<IRequestHandler>();
        //    IRequestHandler two = MockRepository.GenerateMock<IRequestHandler>();
         
        //    ViewResult result = new ViewResult() { Continue = true };
        //    var req = new UrlRequest() { Url = "http://www.sohu.com" };
        //    first.Stub(x => x.HandleRequest(req)).Return(result);
        //    two.Expect(x => x.HandleRequest(req));
        //    context.Handle(req);
        //    two.VerifyAllExpectations();
        //}

        /// <summary>
        ///Handle 的测试
        ///</summary>
        //[TestMethod()]
        //public void firstHandler_isFalse_stop()
        //{
        //    // TestHelper.UIRun();
        //    FakeHttpContext context = TestHelper.CreateContext(null);       
        //    IRequestHandler first = MockRepository.GenerateMock<IRequestHandler>();
        //    IRequestHandler two = MockRepository.GenerateMock<IRequestHandler>();
        //    //context.Push(first);
        //    //context.Push(two);
        //    ViewResult result = new ViewResult() { Continue = false };
        //    var req = new UrlRequest() { Url = "http://www.sohu.com" };
        //    first.Stub(x => x.HandleRequest(req)).Return(result);
        //    two.AssertWasNotCalled(x => x.HandleRequest(req));
        //    context.Handle(req);
        //    two.VerifyAllExpectations();
        //}

        //[TestMethod]
        //public void UnAuthentication_goto_login_page()
        //{                        
        //    ICustomIdentity identity = MockRepository.GenerateMock<ICustomIdentity>();
        //    identity.Expect(x => x.IsAuthenticated).Return(false);

        //    using (IE ie = new IE("http://localhost"))
        //    {

        //        FakeHttpContext context = null;// TestHelper.CreateContext(ie.NativeBrowser);            
        //        context.Handle(new UrlRequest() { Url = "http://www.baidu.com" });
        //        Assert.IsTrue(ie.ContainsText("请输入用户名和密码"));
        //    }
        //}

        //[TestMethod]
        //public void PageHandler_not_staff_goto_emptyView() {
            
        //    PageHandler page = MockRepository.GenerateMock<PageHandler>();
        //    ICustomIdentity identity = MockRepository.GenerateMock<ICustomIdentity>();

        //    IWebsiteService websiteService = MockRepository.GenerateMock<IWebsiteService>();
        //    //为接口做stub,测试的类需要使用Expect
        //    websiteService.Stub(x => x.GetWebsiteId("")).Return("1");
        //    websiteService.Stub(x => x.HasStaff("")).Return(false);
        //    //expect 前调用该方法，实例化mock对象
            
        //    identity.IsAuthenticated = true;
        //    page.Expect(x => x.WebsiteService).Return(websiteService);
        //    page.Expect(x => x.Identity).Return(identity);     

        //    IResult result = page.HandleRequest(new UrlRequest());
        //    Assert.AreEqual(typeof(EmptyResult), result.GetType() );
        //    Assert.IsFalse(result.Continue);
        //    page.VerifyAllExpectations();
        //}

        //[TestMethod]
        //public void PageHandler_has_staff_goto_emptyView_return_true()
        //{

        //    PageHandler page = MockRepository.GenerateMock<PageHandler>();
        //    ICustomIdentity identity = MockRepository.GenerateMock<ICustomIdentity>();

        //    IWebsiteService websiteService = MockRepository.GenerateMock<IWebsiteService>();
        //    //为接口做stub,测试的类需要使用Expect
        //    websiteService.Stub(x => x.GetWebsiteId("")).Return("1");
        //    websiteService.Stub(x => x.HasStaff("")).IgnoreArguments().Return(true);
        //    //expect 前调用该方法，实例化mock对象

        //    identity.IsAuthenticated = true;
        //    page.Expect(x => x.WebsiteService).Return(websiteService);
        //    page.Expect(x => x.Identity).Return(identity);

        //    IResult result = page.HandleRequest(new UrlRequest());
        //    Assert.AreEqual(typeof(EmptyResult), result.GetType());
        //    Assert.IsTrue(result.Continue);
        //    page.VerifyAllExpectations();
        //}

        //[TestMethod]
        public void UnRegister_MockUser_goto_register_handler() { 
            
        }

        [TestMethod]
        public void Register_unconfirm_goto_registerResult_handler() {
        
        }

        [TestMethod]
        public void Register_confirm_goto_require_email_active() { 
        
        }

        //[TestMethod]
        //public void Register_goto_register_email()
        //{
        //    ICustomIdentity identity = TestHelper.MockIdentity;
        //    RegisterEmailHandler handler = MockRepository.GenerateMock<RegisterEmailHandler>();
        //    handler.Expect(c => c.Identity).Return(identity);
        //    handler.Expect(c => c.WebsiteService).Return(TestHelper.MockWebsiteService);
        //    handler.Expect(c => c.MockRegister).Return(ServiceLocator.Instance.GetService<IMockRegister>());
        //    IResult result = handler.Process(TestHelper.Context, null);

        //    Assert.AreEqual(typeof(EmptyResult), result.GetType());
        //    Assert.IsFalse(result.Continue);
        //    handler.VerifyAllExpectations();
        //}

        [TestMethod]
        public void Register_goto_register_email_result_view() { 
        
        }

        [TestMethod]
        public void Match_handler_test() { 
        
        }
        /// <summary>
        ///Call 的测试
        ///</summary>
        //[TestMethod()]
        //public void CallTest()
        //{
        //    TestHelper.UIRun((browser) => {
        //        browser.GoTo("http://localhost/test/calltest.htm");
        //        ICustomIdentity identity = ServiceLocator.Instance.GetService<ICustomIdentity>();
        //        Assert.IsTrue(identity.IsAuthenticated);
        //        Assert.AreEqual("ticket", identity.Ticket);
        //        Assert.AreEqual("3", identity.UserId);
        //    });
         
        //}

        /// <summary>
        ///Call 的测试
        ///</summary>
        [TestMethod()]
        public void Disable_downlaod_image_test()
        {
            TestHelper.UIRun((browser) =>
            {
                browser.GoTo("www.cnboker.com");              
            });

        }
    }
}
