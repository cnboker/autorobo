
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices.Expando;
using System.Windows.Forms;
using Newtonsoft.Json;
using Microsoft.JScript;
using AutoRobo.ClientLib.PageServices;
using System.Threading;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 PageProxyTest 的测试类，旨在
    ///包含所有 PageProxyTest 单元测试
    ///</summary>
    [TestClass()]
    public class PageProxyTest
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


       

        TestBrowser tb = null;
        [TestMethod()]
        public void JavascriptCallTest() {
            tb = new TestBrowser();
            tb.Browser.AllowAlert = true;
            tb.Browser.ObjectForScripting = new ScriptCall();
            tb.InnerHtml = @"
                <html><body><script>
                var ret = window.external.Call({Name:'scott',Age:30});
                eval('var t = ' + ret + ';');
                alert(t.Name);
                //alert(window.external.Test('this is test'));
                </script></body></html>
            ";
            Application.Run(tb);
        }


        [TestMethod]
        public void ThreadTest() {
            Action action = new Action(DoSomething);
            IAsyncResult ar = action.BeginInvoke(null, null);
            //wait until processing is done with WaitOne
            //you can do other actions before this if needed
            ar.AsyncWaitHandle.WaitOne();
            Console.WriteLine("ThreadTest...");
        }

        private void DoSomething() {
            Console.WriteLine("dosomething...");
        }

        [TestMethod]
        public void ThreadTest1() {
            
            CallMethod();

        }

        [TestMethod]
        public void ThreadTest2()
        {
            Action action = new Action(Fun1);
            action += new Action(Fun2);
            action += new Action(Fun3);
            action();
        }

        private void Fun1() {
            Console.WriteLine("fun1");
        }

        private void Fun2() {
            Console.WriteLine("fun2");
        }

        private void Fun3() {
            Console.WriteLine("fun3");
        }

        private int WorkerFunction(string a, string b)
        {
            Console.WriteLine("WorkerFunction thread id:" + Thread.CurrentThread.ManagedThreadId);
            //this is the guy that is supposed to do the long running work 
            Console.WriteLine(a);
            Console.WriteLine(b);
            Thread.Sleep(5000);
            return a.Length + b.Length;
        }

        private void MyCallBack(IAsyncResult ar)
        {
            Func<string, string, int> function = ar.AsyncState as Func<string, string, int>;
            
            int result = function.EndInvoke(ar);
            Console.WriteLine("Result is {0}", result);
        }

        public void CallMethod()
        {
   
            Console.WriteLine("CallMethod thread id:" + Thread.CurrentThread.ManagedThreadId);
            Func<string, string, int> function = new Func<string, string, int>(WorkerFunction);
            IAsyncResult result = function.BeginInvoke("param1", "param2", MyCallBack, function);
           
            //result.AsyncWaitHandle.WaitOne();
            function.EndInvoke(result);
            Console.WriteLine("call methed dosomething...");
        }

        [TestMethod]
        public void ValidateCodeTest(){

            ValidateCodeDialog cd = new ValidateCodeDialog();
            Application.Run(cd);
            
        }

        delegate long dg(dg g, long n);
        delegate long df(long n);

        long f(dg g, long n) {
            if (n == 0) return 1;
            return n * g(g, n - 1);        
        }

        long fact(long n)
        {
            return f(f, n);
        }


        

    }
}
