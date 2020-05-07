using AutoRobo.Core;
using AutoRobo.Core.Models;
using AutoRobo.Makers;
using AutoRobo.Makers.Models.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.ClientLib.Test.AutoRobo.Core
{
     [TestClass()]
    public class MyIETest
    {
        [TestMethod()]
        public void IeTest() {
            AppSettings.Instance.Debug = true;
            string fileName = @"D:\bitrun\上市公司财务报表\script\提取上市公司3大报表数据.bit";
            AppContext.Current.Browser = new MyIE();
            //构造脚本模型
            var model = new ModelSet(new FileActionRepository(fileName));

            var work = new ActionRunnable(AppContext.Current.ActionModel);
            AppContext.Current.CurrentWorker = work;
            work.Run(null, true);
        }
    }
}
