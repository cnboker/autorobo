﻿using AutoRobo.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoRobo.WebApi.Entities;
using System.Collections.Generic;

namespace AutoRobo.ClientLib.Test
{
    
    
    /// <summary>
    ///这是 ServerApiInvokerTest 的测试类，旨在
    ///包含所有 ServerApiInvokerTest 单元测试
    ///</summary>
    [TestClass()]
    public class ServerApiInvokerTest
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
        ///Get_shcema_objects 的测试
        ///</summary>
        [TestMethod()]
        public void Get_shcema_objectsTest()
        {
            string userId = "3";
            string scriptId = "71";          
            List<SchemaObject> actual;
            actual = ServerApiInvoker.Get_shcema_objects(userId, scriptId);
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void GetScript_return_char_test() {
            string scriptId = "197";
            List<ScriptObject> actual;
            actual = ServerApiInvoker.GetScriptObjectByUrl("weibo.com");
            foreach (var o in actual) {
                Console.WriteLine(o.Similarity);
            }
        }
    }
}