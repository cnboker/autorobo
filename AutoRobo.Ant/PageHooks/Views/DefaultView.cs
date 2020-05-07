using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.WebApi.Entities;
using AutoRobo.ClientLib.PageHooks.Models;
using WatiN.Core;
using Newtonsoft.Json;
using System.Windows.Forms;
using AutoRobo.ClientLib.Properties;
using AutoRobo.ClientLib.PageServices;


namespace AutoRobo.ClientLib.PageHooks.Views
{
    public class DefaultView : ViewResult
    {       
        public override void View()
        {
            Strip.Clean();
            PopulateAccountTools();
            PopulateEmail();
            PopulateFun();
        }

        private void PopulateAccountTools() {
            FunToolStripButton dd = new FunToolStripButton();
            dd.Name = "copyName";                    
            dd.Text = "拷贝账号";            
            dd.Click += new EventHandler(account_Click);
            Strip.SiteIn(dd);

            dd = new FunToolStripButton();
            dd.Name = "copypwd";
            dd.Text = "拷贝密码";
            dd.Click += new EventHandler(account_Click);
            Strip.SiteIn(dd);
        }

        void account_Click(object sender, EventArgs e)
        {
            FunToolStripButton btn = sender as FunToolStripButton;
            if (btn.Name == "copyName")
            {
                Clipboard.SetText(Identity.MockUser.UserName);
            }
            else {
                Clipboard.SetText(Identity.MockUser.Password);            
            }
        }

        private void PopulateEmail() {
            IFunStripItem stripItem = null;
            if (Identity.MockUser.IsActiveMail??false)
            {
                FunToolStripButton dd = new FunToolStripButton();
                dd.ProcessHandler = "CheckEmailHandler";
                dd.Name = "checkEmail";
                dd.Image = Resources.mail;
                dd.Text = "查看邮件";
                dd.ToolTipText = dd.Text;
                dd.Click += new EventHandler(email_Click);
                stripItem = dd;
            }
            else {
                FunToolStripButton dd = new FunToolStripButton();
                dd.Name = "registerEmail";
                dd.ProcessHandler = "RegisterEmailHandler";
                dd.Image = Resources.register_email;
                dd.Text = "注册邮箱";
                dd.ToolTipText = dd.Text;
                dd.Click += new EventHandler(email_Click);
                stripItem = dd;
            }
            Strip.SiteIn(stripItem);
        }

        void email_Click(object sender, EventArgs e)
        {
            FunToolStripButton item = sender as FunToolStripButton;            
            ActionClick(item);
        }

        private void PopulateFun() {
            List<ScriptObject> sos = Model as List<ScriptObject>;
            foreach (var so in sos)
            {
                FunToolStripButton item = new FunToolStripButton();
                item.Model = so;
                item.Text = so.Title;
                item.ProcessHandler = GetProcessHandler(so.ScriptType);
                if (string.IsNullOrEmpty(item.ProcessHandler)) continue;
                item.Click += new EventHandler(item_Click);
                Strip.SiteIn(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            FunToolStripButton item = sender as FunToolStripButton;            
            ActionClick(item);
        }

    }
}
