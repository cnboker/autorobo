using System;
using AutoRobo.Core;
using AutoRobo.Makers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoRobo.ClientLib.Test.AutoRobo.Maker
{
    [TestClass]
    public class InstallInitializerTest
    {
        [TestMethod]
        public void ConfigureTest()
        {
            InstallInitializer.Configure();
            Assert.AreEqual(@"D:\bitrun", AppSettings.Instance.LibraryPath);
        }

    }
}
