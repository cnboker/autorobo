using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using WatiN.Core;
using AutoRobo.Core;
using csExWB;


namespace AutoRobo.ClientLib.Test
{
    /// <summary>
    /// CodedUITest1 的摘要说明
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        private Browser myBrowser = null;
        [TestMethod]
        public void ValidateCodeTest()
        {

            TestHelper.UIRun(
                (browser) =>
                {
                    browser.GoTo("http://localhost/test/validateCodeTest.htm");
                    myBrowser = browser;
                },
                () => {
                    ValidateCodeDialog dialog = new ValidateCodeDialog(TestHelper.WB);
                    dialog.OkButtonClick += new EventHandler(dialog_OkButtonClick);
                    dialog.Show(TestHelper.WB);
                   
                    TestHelper.MainForm.Activate();
                    dialog.Activate();
                }
            );
         
        }

        void dialog_OkButtonClick(object sender, EventArgs e)
        {
            
            ValidateCodeDialog d = sender as ValidateCodeDialog;           
            TextField input = myBrowser.TextField(Find.ById("code"));
            input.TypeText(d.ValidateCode);
        }

        #region 附加测试特性

        // 编写测试时，可以使用以下附加特性:

        ////运行每项测试之前使用 TestInitialize 运行代码 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
        //    // 有关生成的代码的详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////运行每项测试之后使用 TestCleanup 运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
        //    // 有关生成的代码的详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
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
        private TestContext testContextInstance;
    }
}
