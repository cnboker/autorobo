using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.DataMapping;
using AutoRobo.Core.Interface;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 非页面元素活动块
    /// </summary>
    public abstract class ActionDataBlock : ActionBase, IActionBlock
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public ActionList Actions { get; set; }

        public ActionDataBlock() {
            Actions = new ActionList() { Parent = this };
            CheckDuplication = false;
        }

        public DataMapping.IMapAttribute MapAttribute
        {
            get;
            set;
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("block.png");
        }

        
        public override void Perform()
        {
            ActionsModelView parentModelView = AppContext.CurrentWorker.ModelView;
            var viewModel = new ActionsModelView(Actions, MapAttribute);
            viewModel.RunStepped += new EventHandler(viewModel_RunStepped);
            viewModel.RunUpdateStatus += new EventHandler(viewModel_RunUpdateStatus);
            AppContext.CurrentWorker.ModelView = viewModel;
            AppContext.CurrentWorker.ModelView.Run();
            AppContext.CurrentWorker.ModelView = parentModelView;
        }

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
        /// 预处理
        /// </summary>
        /// <param name="action"></param>
        public virtual void PrePerform(ActionBase action) { }
        /// <summary>
        /// 执行完后处理
        /// </summary>
        /// <param name="action"></param>
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

        public override string ActionDisplayName
        {
            get { throw new NotImplementedException(); }
        }

   
    }
}
