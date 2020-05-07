using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Windows.Forms;
using AutoRobo.ClientLib.PageHooks.Handler;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.ClientLib.Properties;
using AutoRobo.Core;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using WatiN.Core;

namespace AutoRobo.ClientLib.PageHooks
{
    public class TaskToolStrip : ToolStrip, IStripContainer
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("TaskToolStrip");

        private MyBrowser browser = null;        
        private FakeHttpContext context;
        private ToolStripMenuItem navLastSelect = null;
        private ToolStripMenuItem accLastSelect = null;
        private ToolStripMenuItem pr = null;

        public MyBrowser Browser { get { return browser; } }

        public FakeHttpContext Context
        {
            get
            {
                return browser.ObjectForScripting as FakeHttpContext;
            }
        }

        public TaskToolStrip(MyBrowser browser)
        {            
            this.browser = browser;
            this.context = browser.ObjectForScripting as FakeHttpContext;
            this.context.ViewContainer = this;
            browser.NavigateComplete2 +=new csExWB.NavigateComplete2EventHandler(browser_NavigateComplete2);
        }     

        void browser_NavigateComplete2(object sender, csExWB.NavigateComplete2EventArgs e)
        {
            if (e.url == "about:blank") return;
            if (e.istoplevel)
            {
                pr.Text = "PR:" + GetPR.MyPR(e.url);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;
            PR();
            BuildNavMenu();
            BuildShiftAccountMenu();
           
            this.Items.Add(new ToolStripSeparator());      
        }

        public void PR() {
            pr = new ToolStripMenuItem();
            pr.Name = "pr";
            pr.Text = "PR:";            
            Items.Add(pr);  
        }
        /// <summary>
        /// 清除功能操作按钮
        /// </summary>
        public void Clean() {
            logger.Info("remove tag:");
            List<ToolStripItem> removes = new List<ToolStripItem>();
            foreach (ToolStripItem o in Items)
            {
                if (o is IFunStripItem)
                {
                    removes.Add(o);
                }
            }
            foreach (var o in removes) {
                logger.Info("remove tag:" + o.Text);
                Items.Remove(o);
            }          
        }


        public void SiteIn(IFunStripItem item)
        {
            this.Items.Add((ToolStripItem)item);
        }
        /// <summary>
        /// 构建切换帐号按钮
        /// </summary>
        private void BuildShiftAccountMenu()
        {
            ToolStripDropDownButton dd = new ToolStripDropDownButton();
            dd.Name = "shiftAccount";
            dd.Image = Resources.account;
            dd.Text = "切换帐号";
            dd.ToolTipText = dd.Text;
            Items.Add(dd);          
        }

        void refresh_Click(object sender, EventArgs e)
        {
            context.DOMReady();
        }

        void accountitem_Click(object sender, EventArgs e)
        {
            if (accLastSelect != null) {
                accLastSelect.Image = null;
            }
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            accLastSelect = item;
            item.Select();
            item.Image = Resources.selection;
            MockUser mockUser = item.Tag as MockUser;
            ICustomIdentity identity = ServiceLocator.Instance.GetService<ICustomIdentity>();
            identity.MockUser = mockUser;
      
            ServerApiInvoker.Shift_MockUserAccount(mockUser.Id);

            ToolStripDropDownButton dd = Items["shiftAccount"] as ToolStripDropDownButton;
            dd.Text = item.Text;
            Home();
        }

        /// <summary>
        /// 构建前50导航链接
        /// </summary>
        private void BuildNavMenu()
        {
            ToolStripDropDownButton dd = new ToolStripDropDownButton();
            dd.Name = "navList";
            dd.Image = Resources.nav;
            dd.Text = "导航到...";
            dd.ToolTipText = dd.Text;
            Items.Add(dd);         
        }

    
        void item_Click(object sender, EventArgs e)
        {
            if (navLastSelect != null) {
                navLastSelect.Image = null;
            }
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            navLastSelect = item;
            item.Image = Resources.selection;  
            item.Select();
            browser.Navigate(item.Tag.ToString());
        }

        public bool IsQQ { get; set; }
        public bool AccIsBind { get; set; }

        public void AccountBind(List<MockUser> mokerUsers)
        {
            if (this.InvokeRequired) {
                Invoke(new Action<List<MockUser>>(AccountBind), mokerUsers);
                return;
            }
            AccIsBind = true;
            mokerUsers = mokerUsers.OrderByDescending(c => c.CreateDate).ToList();
            ToolStripDropDownButton dd = Items["shiftAccount"] as ToolStripDropDownButton;
            dd.DropDownItems.Clear();
            foreach (var o in mokerUsers)
            {
                if (o.Email.ToLower().IndexOf("qq.com") > 0) {
                    IsQQ = true;
                }
                ToolStripMenuItem accountitem = new ToolStripMenuItem() { Text = o.Email, Tag = o };
                if (o.IsDefault)
                {                    
                    accountitem.Select();
                    accountitem.Image = Resources.selection;
                    accLastSelect = accountitem;
                    dd.Text = accountitem.Text;                    
                }
                accountitem.Click += new EventHandler(accountitem_Click);
                dd.DropDownItems.Add(accountitem);
            }
        }


        public void NavBind() {
            NavBind(GetTopWebsite());
        }

        public List<WebApi.Entities.WebSite> GetTopWebsite()
        {
            return CacheExtensions.Data("topwebsite", () =>
            {
                return ServerApiInvoker.Get_TopWebsite();
            });
        }

        void NavBind(List<WebSite> websites)
        {       
            //  if (this.InvokeRequired) { 
            //    Invoke(new Action<List<WebSite>>(NavBind),websites);
            //    return;
            //}
            ToolStripDropDownButton dd = Items["navList"] as ToolStripDropDownButton;
            dd.DropDownItems.Clear();
            foreach (var o in websites)
            {
                ToolStripMenuItem item = new ToolStripMenuItem() { Text = o.Name, Tag = o.Url };
                item.Click += new EventHandler(item_Click);
                dd.DropDownItems.Add(item);
            }
           
        }

        private void Home()
        {            
            IResult res = new MatchHandler().HandleRequest(new UrlRequest() { Url = context.PageUrl, HttpContext = context });
            res.Strip = this;
            res.View();
        }
    }

    
}
