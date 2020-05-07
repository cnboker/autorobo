using System;
using System.Collections.Generic;

using System.Text;
using AutoRobo.ClientLib.PageHooks;
using AutoRobo.Core;
using System.Windows.Forms;
using WatiN.Core;
using System.Threading;
using System.Net;
using WatiN.Core.Native.InternetExplorer;
using System.Web.Security;
using WatiN.Core.Native;
using AutoRobo.ClientLib.PageServices;
using Rhino.Mocks;

namespace AutoRobo.ClientLib.Test
{
    public class TestHelper
    {

        public static FakeHttpContext Context;

        public static FakeHttpContext CreateContext(ICoreBrowser ie)
        {
            if (Context == null)
            {
                Context = new FakeHttpContext(ie);
                Context.Initialize();
            }
            return Context;
        }

        static public string GetCookie()
        {
            return "4CD6EA59FFC1C9C30A8527A71C19BBDC8A0D49525C44F55F15598FF9763A8449F8E98714F29E8DA6826FC4F8BDDF92F74C6D416BBB52A5DBEC556B1AFE17F7EFCC6258159A811AD686D94A6EDD3A6F594D07335072EE8448997F24F43FB826B4704BE5D7FDF180D5D65743915DE893421D499369B3D93CB63035D1532BD75F82EB32053B1838B3FF5B874E7936F0A895EC1D09F2";
           
        }
        public static MyBrowser WB
        {
            get;
            set;
        }
        static public void UIRun(System.Action<Browser> action) {
            Console.WriteLine("main thread is :" + Thread.CurrentThread.ManagedThreadId);
            WatiN.Core.Settings.WaitForCompleteTimeOut = 5;
            MainForm = new TestBrowser();
            MainForm.Browser.AllowAlert = true;
            MainForm.Browser.DownloadImages = true;
            WB = MainForm.Browser;
            MainForm.Browser.ObjectForScripting = CreateContext(MainForm.Browser);
            if (action != null)
            {
                var t = new Thread(o =>
                {
                    action(Context.Browser);
                    //Application.Exit();                                       
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();                
            }
            
            Application.Run(MainForm);
        }

        static public void SingleThreadUIRun(System.Action<Browser> action)
        {
            WatiN.Core.Settings.WaitForCompleteTimeOut = 5;
            MainForm = new TestBrowser();
            MainForm.Browser.AllowAlert = true;
            MainForm.Browser.DownloadImages = true;
            WB = MainForm.Browser;
            MainForm.Browser.ObjectForScripting = CreateContext(MainForm.Browser);
            if (action != null)
            {
                action(Context.Browser);
            }

            Application.Run(MainForm);
        }

        static public void Close() {
            Application.Exit();
        }

        static public TestBrowser MainForm
        {
            get;
            set;
        }

   
        static public void UIRun(System.Action<Browser> action, Action mainAction)
        {
            WatiN.Core.Settings.WaitForCompleteTimeOut = 5;
            MainForm = new TestBrowser();
            MainForm.Browser.AllowAlert = true;
            MainForm.Browser.EnableScriptError(true);
            WB = MainForm.Browser;
            MainForm.Browser.ObjectForScripting = CreateContext(MainForm.Browser);
            if (action != null)
            {
                var t = new Thread(o =>
                {
                    action(Context.Browser);
                    //Application.Exit();                                       
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            if (mainAction != null) {
                mainAction();
            }
            
            Application.Run(MainForm);
        }

      
        static public void Cleanup() {
            Application.Exit();
        }

        static public Frame Frame {
            get {
                return Context.Browser.Frame("___cnboker");
            }
        }

        static public bool FrameContain(string html) {
            
            try
            {
                Frame workPanel = Context.Browser.Frame("___cnboker");
                workPanel.WaitUntilContainsText(html, 5);
                return true;
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
           // return workPanel.Html.Contains(html);
        }
       
    }
}
