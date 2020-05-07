using AutoRobo.Core;
using AutoRobo.Core.Formatters;
using AutoRobo.Core.Models;
using AutoRobo.Makers.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoRobo.ClientLib.Test.Formatters
{
    public class CasperTester
    {
        private string casperjsBase = "";
        private StringBuilder sb = new StringBuilder();
        
        private CasperjsCodeFormatterVisitor visitor = null;

        public CasperTester()
        {            
            casperjsBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata/casperjs");
            if (!Directory.Exists(casperjsBase)) {
                Directory.CreateDirectory(casperjsBase);
            }
            visitor = new CasperjsCodeFormatterVisitor();
        }

        public void Open(string htmlfile) {
            sb.Append("var casper = require('casper').create({clientScripts: ['jquery.js']});");
         
            sb.AppendLine();
            sb.AppendFormat("casper.start('{0}');", htmlfile);
            sb.AppendLine();
        }

        public void LoadScript(string scriptFile) {
           
            //构造脚本模型          
            CodeCreator creator = new CodeCreator(new ModelSet(new FileActionRepository(scriptFile)));
            
            creator.Accept(visitor);
            sb.Append(creator.CodeOutput);
            sb.AppendLine();
            sb.Append("casper.run();");
        }
              

        internal void AreEqual(string expect, string varName)
        {
            visitor.Assert(expect, varName);
        }

        internal void WriteJSFile(string outputJsFile)
        {
            string jsfile = Path.Combine(casperjsBase, outputJsFile);
            string jquery = Path.Combine(casperjsBase, "jquery.js");
            StreamWriter sw = new StreamWriter(jsfile, false);
            sw.Write(sb.ToString());
            sw.Close();
            if (!File.Exists(jquery))
            {
                sw = new StreamWriter(jquery, false);
                sw.Write(ScriptLoader.GetJqueryInstallScript());
                sw.Close();
            }
            
        }

        public void RunTest(string outputJsFile)
        {
            Process.Start("casperjs", outputJsFile);
        }
    }
}
