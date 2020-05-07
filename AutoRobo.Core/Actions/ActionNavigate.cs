using System;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using SHDocVw;
using WatiN.Core;
using AutoRobo.Core.ActionBuilder;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionNavigate : ActionBase, ICodeFormatterAcceptor
    {
        private int tryCount = 0;

        public override string DefaultValue
        {
            get
            {
                return URL;
            }
           
        }

      
        public override bool Parse(ActionParameter parameter)
        {
            ActionNavigateParameter p = parameter as ActionNavigateParameter;
            string url = "";
            if (!string.IsNullOrEmpty(p.Url))
            {
                url = p.Url;
            }
            else
            {
                if (parameter.Element != null)
                {
                    mshtml.IHTMLAnchorElement anchor = parameter.Element as mshtml.IHTMLAnchorElement;
                    //if element is span, try parent element
                    if (anchor == null) {
                        anchor = parameter.Element.parentElement as mshtml.IHTMLAnchorElement;
                    }
                    if (anchor != null)
                    {
                        url = anchor.href;
                    }
                }
                if(string.IsNullOrEmpty(url))
                {
                    url = Window.NativeBrowser.NativeDocument.Url;
                }
            }

            var thread = new Thread(() =>
            {
                Goto(url);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
           
            
            URL = url;
            Username = "";
            Password = "";     
            return true;
        }
        /// <summary>
        /// 动态参数绑定，读取IAtribute传入的参数
        /// </summary>
        public string BindName { get; set; }
        /// <summary>
        /// Url to navigate to
        /// </summary>
        internal string URL
        {
            get
            {
                if (!Regex.IsMatch(_url ?? "", @"\b(http|https|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]", RegexOptions.IgnoreCase))                    
                {
                    _url = "";
                }
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        private string _url = "";

        /// <summary>
        /// Username for entry into the web page
        /// </summary>
        internal string Username = "";

        /// <summary>
        /// Password for entry into the web page
        /// </summary>
        internal string Password = "";

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucNavigate(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("Navigate.bmp");
        }

        /// <summary>
        /// navigates to the indicated Url
        /// </summary>
        /// <returns>true on success</returns>
        public override void Perform()
        {
            
            try
            {
               
                GoToURL();
                Status = StatusIndicators.StepContinue;
            }
            catch (ThreadAbortException ) { 
            //手动停止运行，不处理
            }
            catch (Exception ex)
            {
                //如果打开页面失败，在试一次
                if (tryCount < 3)
                {
                    tryCount++;
                    Perform();
                }
                else {
                    Status = StatusIndicators.StepFailure;
                }              
            }
        }

        public virtual void GoToURL()
        {
            string bindVal = Eval(BindName);
            if (!string.IsNullOrEmpty(bindVal)) {
                URL = bindVal;
            }
            string url = VariableModel.Parse(URL);
            Goto(url);
        }

        private void Goto(string url) {
            Window.GoTo(url);

            //以下代码是必须的，不然采集数据会不完整
            while (((InternetExplorer)Window.InternetExplorer).Busy
                || ((InternetExplorer)Window.InternetExplorer).ReadyState != tagREADYSTATE.READYSTATE_COMPLETE)
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }
        }
        /// <summary>
        /// 多个导航链接模拟CLICK事件处理， 这里解决需要点击多个链接才能到发布页面的问题
        /// </summary>
        /// <param name="urls"></param>
        private void NavigateClick(string[] urls) {
            for (int i = 1; i < urls.Length; i++){
                var url = urls[i];
                logger.Info("NavigateClick URL:" + url);
                Window.Link(Find.ByUrl(url)).Click();                
            }
        }
        /// <summary>
        /// test whether we can get to the page
        /// </summary>
        /// <returns>true on success</returns>
        public override bool Validate()
        {
            bool result = false;

            // test whether we can get to the page
            var web = new WebClient();

            try
            {
                web.Credentials = new NetworkCredential(Username, Password, "");
#pragma warning disable 618,612
                ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
#pragma warning restore 618,612
                web.DownloadData(URL);

                Status = StatusIndicators.StepContinue;
                result = true;
            }
            catch (Exception ex)
            {
                Status = StatusIndicators.StepFailure;
                ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// retrieves the description text about this action
        /// </summary>
        /// <returns>description of the action</returns>
        public override string GetDescription()
        {
            return "导航到 "+URL;
        }

        /// <summary>
        /// returns object as code, formatted for a specific language
        /// </summary>
        /// <param name="Formatter">language-specific formatting</param>
        /// <returns>code from the object</returns>
        //public override CodeLine ToCode(ICodeFormatter Formatter)
        //{
        //    var line = new CodeLine();
        //    line.NoModel = true;
        //    var sb = new StringBuilder();
        //    string browserFriendlyName = Window.FriendlyName;

        //    if (Username != "")
        //    {
        //        string strUser = PerformReplacement(Username);
        //        string strPass = PerformReplacement(Password);
        //       // sb.AppendLine(Formatter.ClassNameFormat(typeof(WatiN.Core.DialogHandlers.LogonDialogHandler), "logon"+ParentPage.FriendlyName) + "  = " + Formatter.NewDeclaration + " LogonDialogHandler(\"" + strUser + "\",\"" + strPass + "\")" + Formatter.LineEnding);
        //        sb.AppendLine(Formatter.VariableDeclarator + browserFriendlyName + Formatter.MethodSeparator + "AddDialogHandler(dhdlLogon)" + Formatter.LineEnding);
        //    }

        //    string localurl = PerformReplacement(URL);
        //    if (localurl.Contains("\\")) sb.Append(Formatter.VariableDeclarator + browserFriendlyName + Formatter.MethodSeparator + "GoTo(@\"" + localurl + "\")" + Formatter.LineEnding);
        //    else sb.Append(Formatter.VariableDeclarator + browserFriendlyName + Formatter.MethodSeparator + "GoTo(\"" + localurl + "\")" + Formatter.LineEnding);
        //    line.FullLine = sb.ToString();
        //    return line;
        //}

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            BindName = node.Attributes["BindName"] == null ? "" : node.Attributes["BindName"].Value;         
            URL = node.InnerText;         
        }

     
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("BindName", BindName);
            writer.WriteValue(URL);
        }

        public override string ActionDisplayName
        {
            get { return "导航"; }
        }

        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }



    #region Certificate Policy Class
    /// <summary>
    /// Shell policy class to accept all certificates
    /// since Centris is too cheap to have Verisign Certs
    /// </summary>
    sealed class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint,
            X509Certificate certificate, WebRequest request,
            int certificateProblem)
        {
            // Just accept. 
            return true;
        }
    }
    #endregion
}
