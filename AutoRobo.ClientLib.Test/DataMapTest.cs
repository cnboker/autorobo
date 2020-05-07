using AutoRobo.DataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 DataMapTest 的测试类，旨在
    ///包含所有 DataMapTest 单元测试
    ///</summary>
    [TestClass()]
    public class DataMapTest
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
        ///UpdateValues 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateValuesTest()
        {
            DataMap target = new DataMap(); // TODO: 初始化为适当的值
            string skm="[{'CssClass':'input_text','DisplayName':'标题','Value':'','Required':true},{'CssClass':'textarea','DisplayName':'内容','Value':'','Required':true},{'CssClass':'input_text','DisplayName':'标签','Value':'','Required':false}]";
            string val = "%u6807%u9898=aa&%u5185%u5bb9=dd&%u6807%u7b7e=dd"; // TODO: 初始化为适当的值
            target.Init(skm);
            target.UpdateValues(val);
            foreach (var o in target.Fields) {
                Console.WriteLine(o.DisplayName + ":" + o.Value);
            }
        }
    }
}
