using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using mshtml;
using System.Threading;
using csExWB;
using WatiN.Core.Native.InternetExplorer;


namespace AutoRobo.Core
{
    [ComImport, InterfaceType((short)1), Guid("3050F669-98B5-11CF-BB82-00AA00BDCE0B")]
    interface IHTMLElementRenderFixed
    {
        void DrawToDC(IntPtr hdc);
        void SetDocumentPrinter(string bstrPrinterName, IntPtr hdc);
    }

 
    public partial class ValidateCodeDialog : MyDialog
    {
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        //private mshtml.IHTMLImgElement imgId;
        private IHTMLElement el;
        protected cEXWB webBrowser;
        public event EventHandler OkButtonClick;

        public string ValidateCode
        {
            get
            {
                return codeTextbox.Text;
            }
        }

        public ValidateCodeDialog()
        {
            InitializeComponent();
        }

        public ValidateCodeDialog(cEXWB webBrowser, IHTMLElement id)
        {
            this.webBrowser = webBrowser;
            this.el = id;
            InitializeComponent();
        }

        /// <summary>
        /// 普通验证框， 用于手机验证使用
        /// </summary>
        /// <param name="webBrowser"></param>
        public ValidateCodeDialog(cEXWB webBrowser)
        {
            this.webBrowser = webBrowser;            
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                Rectangle workingArea = Screen.GetWorkingArea(webBrowser);
                StartPosition = FormStartPosition.Manual;
                Location = new Point(workingArea.Right - Width,
                                          workingArea.Bottom - Height);
                codeTextbox.Focus();

                Image map = GetImage();

                if (map != null)
                {
                    pictureBox1.Image = map;
                }
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
            }
            base.OnClosing(e);
        }

     
        public Bitmap GetImage()
        {
            IHTMLElementRenderFixed render = null;
            try
            {
                
                if (el == null) return null;
                IHTMLImgElement t = el as IHTMLImgElement;
                int width, height;
                if (t != null)
                {
                    width = t.width;
                    height = t.height;
                }
                else
                {
                    Rectangle r = cEXWB.GetHtmlElementBounds((IHTMLElement)el);
                    width = r.Width;
                    height = r.Height + 5 ;
                }
                render = (IHTMLElementRenderFixed)el;
                Bitmap bmp = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(bmp);
                IntPtr hdc = g.GetHdc();
                render.DrawToDC(hdc);
                g.ReleaseHdc(hdc);
                return bmp;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        private void codeTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                if (OkButtonClick != null) {
                    OkButtonClick(this, EventArgs.Empty);
                }
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (OkButtonClick != null)
            {
                OkButtonClick(this, EventArgs.Empty);
            }
        }
    }
}
