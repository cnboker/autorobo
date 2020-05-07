using System;
using System.Collections.Generic;
using System.Text;
using Jint;
using Jint.Delegates;
using System.Windows.Forms;
using AutoRobo.Core.UserControls;
using System.IO;
using Util;
using WatiN.Core;
using AutoRobo.Core.Models;
using System.Data;

namespace AutoRobo.Core.ns
{
    public class JintCreator
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger("jintCreator");
        private static System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private static JintEngine jintEngine = null;
        static IAppContext context = null;

        static public JintEngine Create()
        {
            if (jintEngine != null) return jintEngine;
            jintEngine = new JintEngine()
            .SetFunction("sleep", new Action<object>(Sleep))
            .SetFunction("alert", new Action<object>(Alert))
            .SetFunction("log", new Action<object>(logger.Info))
            .SetFunction("error", new Action<object>(logger.Fatal))
            .SetFunction("watchStart", new Jint.Delegates.Action(WatchStart))
            .SetFunction("watchStop", new Action<string>(WatchStop))
            .SetFunction("qq", new Jint.Delegates.Func<string>(qq))
            .SetFunction("name", new Jint.Delegates.Func<string>(name))
            .SetFunction("prompt", new Jint.Delegates.Func<string, string>(Prompt));            
            jintEngine.DisableSecurity();
            jintEngine.SetDebugMode(true);
            return jintEngine;
        }

        static public JintEngine Create(IAppContext appcontext)
        {
            context = appcontext;
            JintEngine jint = Create();
            WatiN.Core.Browser browser = new BrowserWindow(context.Browser);
            jint.SetParameter("ie", new JsBrowser(browser));
            var q = new Q(context, browser);
            jint.SetParameter("$", q);
            jint.SetFunction("Var", new Jint.Delegates.Func<string, object>(q.GetVariable));
            jint.SetFunction("getMapValue", new Jint.Delegates.Func<string, string>(getMapValue));
            return jint;
        }

        static public codeRichEditor CreateEditor(IAppContext appcontext)
        {
            context = appcontext;
            JintEngine jint = JintCreator.Create();
            codeRichEditor textEditor = new codeRichEditor();
            
            textEditor.Browser = context.Browser;
           
            textEditor.JintEngine = jint;
            textEditor.Dock = DockStyle.Fill;
            textEditor.TmpRead();
            textEditor.RunBefore += new EventHandler(textEditor_RunBefore);
            return textEditor;
        }

        static void textEditor_RunBefore(object sender, EventArgs e)
        {            
            codeRichEditor textEditor = sender as codeRichEditor;
            JintEngine jint = textEditor.JintEngine;
            WatiN.Core.Browser browser = new BrowserWindow(context.Browser);
            jint.SetParameter("ie", new JsBrowser(browser));
            jint.SetParameter("$", new Q(context,browser));
            jint.SetFunction("getMapValue", new Jint.Delegates.Func<string, string>(getMapValue));
            //jint.SetFunction("listprint", new Action<string>(listprint));
            //jint.SetFunction("tableprint", new Action<string>(tableprint));
            textEditor.FindForm().Tag = browser;
        }

        /// <summary>
        /// 打印数组数据
        /// </summary>
        /// <param name="listname"></param>
        //static void listprint(string listname) { 
        //    ModelSet ModelSet = WatinContextFactory.GetContext(myBrowser).ModelSet;
        //    List<string> list = ModelSet.VariableSet.GetArrayValue(listname);
        //    if (list != null) {
        //        foreach (var o in list) {
        //            Console.WriteLine(o);
        //        }
        //    }
        //}

        /// <summary>
        /// 打印表格数据
        /// </summary>
        /// <param name="tablename"></param>
        //static void tableprint(string tablename)
        //{
        //    ModelSet ModelSet = WatinContextFactory.GetContext(myBrowser).ModelSet;
        //    DataTable table = ModelSet.VariableSet.GetTable(tablename).Table;
        //    if (table != null)
        //    {
        //        Console.WriteLine(StringHelper.ConvertDataTableToString(table));
        //    }
        //}

     
        static string getMapValue(string key)
        {
            return "";
        }

        /// <summary>
        /// 随机用户名
        /// </summary>
        /// <returns></returns>
        static string name() {
            return StringHelper.GetRandom(4);
        }
        /// <summary>
        /// 获取随机qq号
        /// </summary>
        /// <returns></returns>
        static string qq() {
            return StringHelper.GetRandomDigit(7);
        }
        /// <summary>
        /// 提示用户输入信息
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        static string Prompt(string label)
        {
            PromptDialog pd = new PromptDialog();
            pd.LableText = label;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                return pd.InputText;
            }
            return "";
        }

        static void Alert(object obj) {
            if (obj == null)
            {
                MessageBox.Show("null");
            }
            else
            {
                MessageBox.Show(obj.ToString());
            }
        }
        static void WatchStart()
        {
            watch.Reset();
            watch.Start();
        }

        static void WatchStop()
        {
            WatchStop("");
        }  

        static void Sleep(object time)
        {
            System.Threading.Thread.Sleep(Convert.ToInt32(time));
        }

        static void WatchStop(string message)
        {
            watch.Stop();
            logger.Info(message + " elapse time is :" + watch.ElapsedMilliseconds);
        }
    }
}
