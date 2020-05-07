using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 NatualLinkCreatorTest 的测试类，旨在
    ///包含所有 NatualLinkCreatorTest 单元测试
    ///</summary>
    [TestClass()]
    public class NatualLinkCreatorTest
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


//        /// <summary>
//        ///Create 的测试
//        ///</summary>
//        [TestMethod()]
//        public void CreateTest()
//        {
//            NatualLinkCreator_Accessor target = new NatualLinkCreator_Accessor(); // TODO: 初始化为适当的值
//            string text = @"电子商务网站的推广正道是什么？搜索引擎很多人回答是SEO。SEO对于电子商务网站的推广具有很重要的作用，但是，如果陷入SEO推广误区那么就会适得其反。
//最近两年，接触了非常多的B2C电子商务网站，虽然绝大部分B2C网站公司都聘请了专业的SEO技术人员，但是大多的SEO效果不是非常理想，这主要是因为这些B2C网站的SEO策略进入了以下几大误区："; // TODO: 初始化为适当的值
//            string expected = string.Empty; // TODO: 初始化为适当的值
//            string actual;
//            actual = target.Create(text);
//            Console.Write(actual);
//        }
    }
}
