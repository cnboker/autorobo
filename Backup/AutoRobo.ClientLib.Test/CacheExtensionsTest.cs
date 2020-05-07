using System.Web.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Linq;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 CacheExtensionsTest 的测试类，旨在
    ///包含所有 CacheExtensionsTest 单元测试
    ///</summary>
    [TestClass()]
    public class CacheExtensionsTest
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


        ///// <summary>
        /////Data 的测试
        /////</summary>
        //public void DataTestHelper()
        //{
        //    string cacheKey = "key";
        //    int expirationSeconds = 10; // TODO: 初始化为适当的值
           
        //    string expected = "val1";
        //    string actual;
        //    actual = CacheExtensions.Data<string>(cacheKey, expirationSeconds, () => { return "val1"; });
        //    Assert.AreEqual(expected, actual);

        //    expected = "val2";

        //    actual = CacheExtensions.Data<string>(cacheKey, expirationSeconds, () => { return "val2"; });
        //    Assert.AreEqual(expected, actual);

        //}

        //[TestMethod]
        //public void DataTestHelper_same_function_name()
        //{
        //    string cacheKey = "key";
        //    int expirationSeconds = 10; // TODO: 初始化为适当的值
        //    Global.EnableCache = true;
        //    string actual;
        //    string expected = CacheExtensions.Data<string>(cacheKey, expirationSeconds, GetValue);
        //    Thread.Sleep(20);
        //    actual = CacheExtensions.Data<string>(cacheKey, expirationSeconds, GetValue);
        //    Assert.AreEqual(expected, actual);

        //}

        //[TestMethod]
        //public void DataTestHelper_no_cache()
        //{
        //    string cacheKey = "key";
        //    Global.EnableCache = false;
        //    int expirationSeconds = 10; // TODO: 初始化为适当的值
        //    string actual;
        //    string expected = CacheExtensions.Data<string>(cacheKey, expirationSeconds, GetValue);
        //    Thread.Sleep(20);
        //    actual = CacheExtensions.Data<string>(cacheKey, expirationSeconds, GetValue);
        //    Assert.AreNotEqual(expected, actual);

        //}

        private string GetValue() {
            return DateTime.Now.Ticks.ToString();
        }

        private string GetValue1() {
            return "val2";
        }
    
    }
}
