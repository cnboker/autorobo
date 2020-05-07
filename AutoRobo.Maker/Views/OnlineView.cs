using System;
using System.Windows.Forms;
using AutoRobo.ClientLib.Ants;
using AutoRobo.Core;
using AutoRobo.Makers.Models.Repositories;
using AutoRobo.Makers.Presentation;
using AutoRobo.Protocol;
using Util;

namespace AutoRobo.Makers.Views
{
    public partial class OnlineView : Form,  INewWindow
    {
        
        public bool AllowNewWindow { get; set; }

        public OnlineView()
        {           
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;            
            AllowNewWindow = true;                              
        }

        protected override void OnLoad(EventArgs e)
        {
            //解决启动录制器后会导致IHTMLDocument2.body无法访问，
            ///抛出对于方法找不到异常，对于cEXWeb.cs, 3054行
            AutoBrowser fireBrowser = new AutoBrowser();
            LoadBrowser(fireBrowser);

            fireBrowser.Browser.Navigate(StringHelper.Domain);   
            mytab.TabClosing += new EventHandler<TabControlCancelEventArgs>(mytab_TabClosing);            
            base.OnLoad(e);
        }

        void mytab_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            FireBrowser child = e.TabPage.Controls[0] as FireBrowser;
            child.Close();
            if (this.mytab.Controls.Count == 1)
            {
                this.Close();
            }
            else {
                mytab.SelectedIndex = mytab.TabCount - 2;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //ctrl+T
            if (keyData == (Keys.T | Keys.Control))
            {
                Goto("about:blank", "_blank");
                return true;
            }
            else if (keyData == (Keys.W | Keys.Control))
            {
                //mytab.Controls[mytab.SelectedIndex])
                return true;
            }
            else if (keyData == (Keys.Tab | Keys.Control))
            {
                if (mytab.SelectedIndex < mytab.TabCount - 1)
                {
                    mytab.SelectedIndex += 1;
                }
                else
                {
                    mytab.SelectedIndex = 0;
                }
                // 避免重复执行
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Goto(string url, string target)
        {
            FireBrowser browser = null;
            if (target == "_blank")
            {
                browser = new AutoBrowser();
                browser.RedirectUrl = url;
                LoadBrowser(browser);
                if (AllowNewWindow)
                {
                    ((MyBrowser)browser.Browser).NewWin = this;
                }
            }
            else
            {
                browser = ActiveMdiChild as FireBrowser;
                browser.Browser.Navigate(url);
            }            
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="child"></param>
        /// <param name="title"></param>
        private void LoadBrowser(FireBrowser child)
        {
            string title = "新标签页";
            bool contain = false;
            child.Dock = DockStyle.Fill;            
            foreach (Control c in mytab.Controls)
            {
                if (c.Controls[0] == this)
                {
                    contain = true;
                    break;
                }
            }
            if (contain) return;
            //TopLevel for form is set to false
            child.TopLevel = false;            
            TabPage page = new TabPage();
            mytab.Controls.Add(page);
            page.Controls.Add(child);
            page.Text = title;
            mytab.SelectTab(mytab.Controls.Count - 1);
            //Added form to tabpage
            child.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            child.WindowState = FormWindowState.Maximized;
            child.Show();
        }

        public void AssignBrowserObject(ref object obj)
        {
            FireBrowser hb = this.ActiveMdiChild as FireBrowser;
            if (hb == null) return;
            obj = ((MyBrowser)hb.Browser).WebbrowserObject;
        }

        public event EventHandler Initialize;


        //public void Goto(ICoreBrowser parent, string url)
        //{
        //    throw new NotImplementedException();
        //}

     

        public ICoreBrowser Switch(string browserName)
        {
            throw new NotImplementedException();
        }
    }
}
