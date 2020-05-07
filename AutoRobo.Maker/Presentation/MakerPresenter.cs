using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AutoRobo.ClientLib;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.Makers.EventArguments;
using AutoRobo.Makers.Models.Repositories;
using AutoRobo.Makers.Views;
using AutoRobo.Robo.Application;
using mshtml;
using AutoRobo.ClientLib.Ants;
using Util;

namespace AutoRobo.Makers.Presentation
{
    public class MakerPresenter : Presenter<IMakerView>
    {
        private ProxyManager proxyManager = new ProxyManager();
        public MakerPresenter(IMakerView view)
            : base(view)
        {                                           
            view.Run += new EventHandler(view_Run);
            view.StepRun += new EventHandler(view_StepRun);
            view.StopRun += new EventHandler(view_StopRun);
            view.StopRecord += new EventHandler(view_StopRecord);
            view.Record += new EventHandler(view_Record);
            view.New += new EventHandler(view_New);
            view.Open += new EventHandler(view_Open);
            view.Save += new EventHandler(view_Save);
            view.SaveAs += new EventHandler(view_SaveAs);
            view.Exit += new EventHandler(view_Exit);            
            view.TagLabelEvent += new EventHandler(view_TagLabelEvent);
            view.BreakPoint += new EventHandler(view_BreakPoint);
            view.AddAction += Context.State.ActionAdd;
        }

      
        void view_BreakPoint(object sender, EventArgs e)
        {
            Context.State.IsBreakPoint = !Context.State.IsBreakPoint;
        }


        /// <summary>
        /// 为活动选择元素增加标签说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void view_TagLabelEvent(object sender, EventArgs e)
        {
            ICoreBrowser browser = Context.Browser;
            IHTMLElement activeElement = null;
            if (browser.Selector != null && browser.Selector.SelectorElement != null)
            {
                activeElement = browser.Selector.SelectorElement;
            }
            if (activeElement == null) return;
            
            if (string.IsNullOrEmpty(activeElement.innerText)) return;

            //int selectIndex = Context.State.SelectItemIndex;
            //if (selectIndex == -1) return;
            ActionBase actionBase = Context.State.ActiveAction;//wsv.DataSource[wsv.SelectItemIndex - 1] as ActionBase;
            if (actionBase == null) return;
            ActionElementBase ae = actionBase as ActionElementBase;
            if (ae == null) return;

            List<ActionElementBase> aes = Model.ActiveActionModel.GetTheElementActions(ae.GetConstraint());
            string labelText = activeElement.innerText.Replace(":", "").Replace("：", "").Trim();
            foreach (var o in aes)
            {
                o.Title = labelText;                            
            }
           // wsv.DataBind();
        }

        /// <summary>
        /// 停止运行脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void view_StopRun(object sender, EventArgs e)
        {
            proxyManager.Stop();
            Context.State.DoRun(false);
            if (Context.CurrentWorker != null)
            {
                Context.CurrentWorker.Stop();
            }
        }

        protected override void OnViewInitialize(object sender, EventArgs e)
        {
            MyBrowser browser = Context.Browser as MyBrowser;                       
            base.OnViewInitialize(sender, e);
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
               
            base.OnViewLoad(sender, e);
        }
     
        void view_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void view_SaveAs(object sender, EventArgs e)
        {          
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "bit文件 (*.bit)|*.bit";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = false;
                dialog.InitialDirectory = AppSettings.Instance.LibraryPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    
                    // Can use dialog.FileName
                    using (Stream stream = dialog.OpenFile())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(Context.ActionModel.Export());
                        // Save data
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }
                }
            }
        }

        void view_Save(object sender, EventArgs e)
        {
            Context.ActionModel.Save();        
        }

        void view_Open(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "打开工程文件";
            fDialog.Filter = "BIT Files|*.bit";
            fDialog.InitialDirectory = AppSettings.Instance.LibraryPath;
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                ModelSet model = new ModelSet(new FileActionRepository(fDialog.FileName));
                Context.ActionModel = model;
                
                Context.State.DataPrepared = true;
            }        
            Context.State.IsRecord = false;

        }

        void view_New(object sender, EventArgs e)
        {
            NewFileEventArgs args = e as NewFileEventArgs;
            ModelSet model = new ModelSet(new FileActionRepository());     
            Context.ActionModel = model;
            model.New(args.ProjectName,args.StartUrl, args.Method);
            Context.State.DataPrepared = true;
            Context.State.IsRecord = true;
            
        }

        void view_Record(object sender, EventArgs e)
        {            
            Context.State.DoRecord(true);
        }

        void view_StopRecord(object sender, EventArgs e)
        {            
            Context.State.DoRecord(false);
        }
           
        void view_StepRun(object sender, EventArgs e)
        {
            Control c = View as Control;
            if (c.InvokeRequired)
            {
                c.Invoke(new Action<object, EventArgs>(view_StepRun), sender, e);
                return;
            }            
            int selectIndex = Context.State.SelectItemIndex;
            if (selectIndex > 0)
            {
                Context.CurrentWorker.RunStep(selectIndex);
            }
        }

        void view_Run(object sender, EventArgs e)
        {
            proxyManager.Start();
            Context.Browser = Context.MainWindow.Switch("MainBrowser");
            if (Context.State.BreakReached) {
                Context.State.BreakReached = false;
                Context.CurrentWorker.Continue();
            }
            else
            {
                //回到主活动
                Context.ActionModel.ActiveActionModel = Context.ActionModel.MainActionModel;
                var work = new ActionRunnable(Context.ActionModel);
                Context.CurrentWorker = work;
                Context.State.DoRun(true);
                //if (Context.Identity == null && Context.ActionModel.Target != "System")
                //{
                //    Context.Browser.Navigate(StringHelper.Domain + "/account/ticket?returnUrl=" + Context.Browser.LocationUrl);
                //    view_StopRun(sender, e);
                //    return;
                //}
                work.Run(WaitComplete, true);
            }
        }

        private void WaitComplete(AsyncResult result)
        {
            proxyManager.Stop();            
            Context.State.IsRunning = false;            
        }
    
    }
}
