using AutoRobo.Core.Actions;
using AutoRobo.Core.Formatters;
using AutoRobo.Core.Models;
using AutoRobo.Makers.Models.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoRobo.ClientLib.Test.Formatters
{
     [TestClass()]
    public class CasperJSCodeFormatterTest
    {
         [TestMethod()]
         public void CodeGeneratorTest(){

             string fileName = @"D:\bitrun\表格数据采集\script\提取表格数据.bit";
                       
             CodeCreator creator = new CodeCreator(new ModelSet(new FileActionRepository(fileName)));             
             creator.Accept(new CasperjsCodeFormatterVisitor());
             Console.WriteLine(creator.CodeOutput);
         }

         [TestMethod]
         public void SelectTest() {
             var testSelect = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/script/testSelect.bit");
             
             Console.WriteLine(testSelect);
             CasperTester tester = new CasperTester();
             tester.Open("../getdata.html");
             tester.LoadScript(testSelect);             
             tester.AreEqual("2", "selectValue");
             tester.AreEqual("middle", "selectText");

             tester.WriteJSFile("casperjs_select.js");
             tester.RunTest("casperjs_select.js");
         }
    }
}
