

using Antlr4.StringTemplate;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.ClientLib.Test.Formatters
{
  
    [TestClass()]
    public class STTest
    {
        [TestMethod()]
        public void ReadTemplateTest()
        {
            string template = TPLoader.Instance.ReadTemplate("ActionTypeText");
            Template t = new Template(template);
            t.Add("ID", "actionid");
            t.Add("Value", "this is test");
            
            Console.WriteLine(t.Render());
        }

        [TestMethod()]
        public void STIterateTest() {
            string template = TPLoader.Instance.ReadTemplate("FindId");
            Template t = new Template(template);
            t.Add("selector", ".a");
            t.Add("frames", new string[]{".b", ".c", ".d"});

            Console.WriteLine(t.Render());
        }

        [TestMethod()]
        public void STIfTest() {
           
            string template = TPLoader.Instance.ReadTemplate("ActionSelectList");
            Template t = new Template(template);
            t.Add("ID", "myid");
            t.Add("Value", "1");
            t.Add("ByValue", false);
            Console.WriteLine(t.Render());
        }
    }
}
