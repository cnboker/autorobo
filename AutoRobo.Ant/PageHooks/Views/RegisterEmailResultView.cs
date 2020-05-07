using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.WebApi;

namespace AutoRobo.ClientLib.PageHooks.Views
{
    public class RegisterEmailResultView : ViewResult
    {
        public override void View()
        {
            FunCheckboxItem item = new FunCheckboxItem();            
            item.Text = "注册成功?";
            item.Model = Model;
            item.ProcessHandler = "RegisterEmailResultHandler";
            item.Click += new EventHandler(item_Click);
            Strip.Clean();
            Strip.SiteIn(item);
        }
        void item_Click(object sender, EventArgs e)
        {
            FunCheckboxItem item = sender as FunCheckboxItem;
            Microsoft.JScript.JSObject js = new Microsoft.JScript.JSObject();
            js.SetMemberValue2("isRegister", item.Checked ? "1" : "0");            
            ActionClick(item,js);
        }
    }
}
