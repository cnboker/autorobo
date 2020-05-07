using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WatiN.Core;
using AutoRobo.Core.Interface;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 活动块，活动包含子活动，比如活动块，循环块
    /// </summary>
    public abstract class ActionBlock : ActionElementBase, IActionBlock
    {         
      
        /// <summary>
        /// 活动列表
        /// </summary>
        public ActionList Actions { get; set; }
        public DataMapping.IMapAttribute MapAttribute
        {
            get;
            set;
        }
        public ActionBlock() {
            Actions = new ActionList() { Parent = this };
            CheckDuplication = false;
        }

        public override void Perform()
        {
            if (Actions.Count == 0) return;
            ActionsModelView parentModelView = AppContext.CurrentWorker.ModelView;
            var viewModel = new ActionsModelView(Actions, MapAttribute);
            viewModel.RunStepped += new EventHandler(viewModel_RunStepped);
            viewModel.RunUpdateStatus += new EventHandler(viewModel_RunUpdateStatus);
            AppContext.CurrentWorker.ModelView = viewModel;
            viewModel.Run();
            AppContext.CurrentWorker.ModelView = parentModelView;
        }

        public abstract void Enter();
        public abstract void Exit();
        /// <summary>
        /// 执行子活动之后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_RunUpdateStatus(object sender, EventArgs e)
        {
            RunningActionEventArgs arg = e as RunningActionEventArgs;
            PostPerform(arg.Action);
        }

        /// <summary>
        /// 执行子活动之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_RunStepped(object sender, EventArgs e)
        {
            RunningActionEventArgs arg = e as RunningActionEventArgs;
            PrePerform(arg.Action);
        }

        /// <summary>
        /// 子活动预处理
        /// </summary>
        /// <param name="action">当前子活动</param>
        public virtual void PrePerform(ActionBase action) { }
        /// <summary>
        /// 子活动执行完后处理
        /// </summary>
        /// <param name="action">当前子活动</param>
        public virtual void PostPerform(ActionBase action) { }
      
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            XmlNode actions = node.SelectSingleNode("Actions");
            if (actions == null) return;

            Actions.LoadScript(AppContext, actions);      
        }

     
        public override void WriterAddAttribute(XmlWriter writer)
        {
            Actions.SaveXml("Actions", (XmlTextWriter)writer);
            base.WriterAddAttribute(writer);
        }
    }
}
