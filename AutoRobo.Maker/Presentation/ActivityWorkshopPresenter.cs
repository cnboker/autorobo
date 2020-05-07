using System;
using System.Windows.Forms;
using AutoRobo.ClientLib;
using AutoRobo.Core;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.DataMapping;
using AutoRobo.Makers.EventArguments;
using AutoRobo.Makers.Views;
using AutoRobo.UserControls;
using mshtml;
using AutoRobo.Core.UserControls;

namespace AutoRobo.Makers.Presentation
{
    public class ActivityWorkshopPresenter : Presenter<IActivityWorkshopView>
    {
        
        public ActivityWorkshopPresenter(IActivityWorkshopView view)
            : base(view)
        {            
            view.ActionEdit += new EventHandler(View_ActionEdit);
            view.ActionTabSelectedIndexChanged += new EventHandler(View_ActionTabSelectedIndexChanged);
            view.Clear += new EventHandler(View_Clear);
            view.GoBack += new EventHandler(View_GoBack);
            view.MoveFirst += new EventHandler(View_MoveFirst);
            view.MoveLast += new EventHandler(View_MoveLast);
            view.MoveToNext += new EventHandler(View_MoveToNext);
            view.MoveToPrevious += new EventHandler(View_MoveToPrevious);
            view.Order += new EventHandler(View_Order);
            view.RowDelete += new EventHandler(View_RowDelete);
            view.ModelBind += new System.Windows.Forms.ToolStripItemClickedEventHandler(View_ModelBind);
            view.ActionEditOK += new EventHandler(view_ActionEditOK);                  
            view.Initialize += new EventHandler(view_Initialize);
            
        }

        void view_Initialize(object sender, EventArgs e)
        {            
            Context.State.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(State_PropertyChanged);
            Context.State.AddAction += new EventHandler(view_AddAction);
            Context.PropertyChanged += Context_PropertyChanged;
        }

        void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
              if (e.PropertyName == "MultiStepActionParameter") { 
                 MyBrowser browser = Context.Browser as MyBrowser;

                 if (AppSettings.Instance.ListToListAllowPrompt)
                 {
                     CollectDialog dialog = new CollectDialog();
                     dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                     dialog.ShowDialog();
                 }
            
            }
        }

        void State_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRunning" && Context.State.IsRunning)
            {                
                ActionRunnable worker = Context.CurrentWorker;
                worker.ModelViewChanged += new EventHandler(worker_ModelViewChanged);
                ModelViewChangedRegisterEvent(worker);
            }
            else if (e.PropertyName == "IsBreakPoint")
            {
                View.SetBreakPoint();
            }
            else if (e.PropertyName == "SelectItemIndex")
            {
                View.SelectRow(Context.State.SelectItemIndex);
                if (Context.State.SelectItemIndex != -1)
                {
                    //Model.ShouldInsertActions = true;
                    Model.InsertPosition = Context.State.SelectItemIndex - 1;
                }
            }
           
        }

        void worker_ModelViewChanged(object sender, EventArgs e)
        {
            if (View.ProjectSelectIndex == 0)
            {
                ActionRunnable worker = Context.CurrentWorker;
                Model.ActiveActionModel = worker.ModelView.Model;
                ModelViewChangedRegisterEvent(worker);
            }
        }

        /// <summary>
        /// 试图数据更新后，重新注册事件
        /// </summary>
        /// <param name="worker"></param>
        private void ModelViewChangedRegisterEvent(ActionRunnable worker)
        {
            worker.RunUpdateStatus -= new EventHandler(runner_RunUpdateStatus);
            worker.RunUpdateStatus += new EventHandler(runner_RunUpdateStatus);
            worker.RunStepped -= new EventHandler(runner_RunStepped);
            worker.RunStepped += new EventHandler(runner_RunStepped);
            worker.BreakpointReached -= new EventHandler(runner_BreakpointReached);
            worker.BreakpointReached += new EventHandler(runner_BreakpointReached);
        }

        void runner_BreakpointReached(object sender, EventArgs e)
        {
            Context.State.BreakReached = true;
        }
        void view_BreakPoint(object sender, EventArgs e)
        {
            Control c = View as Control;
            if (c.InvokeRequired)
            {
                c.Invoke(new Action<object, EventArgs>(view_BreakPoint), sender, e);
                return;
            }
           
            View.SetBreakPoint();
        }
        void runner_RunStepped(object sender, EventArgs e)
        {
            Control c = View as Control;
            if (c.InvokeRequired)
            {
                c.Invoke(new Action<object, EventArgs>(runner_RunStepped), sender, e);
                return;
            }            
            if (View.ProjectSelectIndex != 0) return;
            RunningActionEventArgs arg = e as RunningActionEventArgs;
            Context.State.SelectItemIndex = arg.RunningRowIndex;
            
        }

        void runner_RunUpdateStatus(object sender, EventArgs e)
        {
            Control c = View as Control;
            if (c.InvokeRequired)
            {
                c.Invoke(new Action<object, EventArgs>(runner_RunUpdateStatus), sender, e);
                return;
            }            
            //当前界面不在主活动区域则不更新状态
            if (View.ProjectSelectIndex != 0) return;
            RunningActionEventArgs arg = e as RunningActionEventArgs;
            // logger.Info("runner_RunUpdateStatus: action is " + arg.Action.ID);
            View.UpdateCellIcon(arg.RunningRowIndex, arg.Action);
        }
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       public void view_AddAction(object sender, EventArgs arg)
        {            
            //确保在录制状态下
            Context.State.IsRecord = true;
            var item = sender as ToolStripItem;
            string actionName = item.Tag as string;
            if (string.IsNullOrEmpty(actionName)) {
                throw new ApplicationException("活动没有定义名称");
            }          
            ICoreBrowser browser = Context.Browser;
            IHTMLElement activeElement = null;
            if (browser.Selector.SelectorElement != null)
            {
                activeElement = browser.Selector.SelectorElement;
            }
          
            var parameter = ActionFactory.CreateParameter(actionName);
            parameter.Element = activeElement;
            if (parameter is MultiStepActionParameter)
            {
                //用户选择同类元素处理逻辑在browserPresenter.editBrowser_WBLButtonDown处理
                var mp = parameter as MultiStepActionParameter;
                mp.AddActionFun = new Action<string, ActionParameter>(AddAction);
                Context.MultiStepActionParameter = mp;
                return;
            }
            AddAction(actionName, parameter);
        }

    

        private void AddAction(string actionName, ActionParameter parameter)
        {
            ICoreBrowser browser = Context.Browser;
            ActionBase action = Model.CreateAction(Context,
              actionName,
              parameter);
            if (action == null) return;
          
            IEditorAction editorProvider = View as IEditorAction;
            var editor = action.GetEditor(editorProvider);
            if (editor != null)
            {
                action.RowStatue = ActionStatue.New;
                editor.Action = action;
                editorProvider.ShowEditAction(editor);
            }
            else
            {
                Model.AddAction(action);                
            }
        }

        /// <summary>
        /// 确定或取消活动编辑器逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void view_ActionEditOK(object sender, EventArgs e)
        {
            ActionEditEventArgs arg = e as ActionEditEventArgs;
            ActionBase action = arg.EditAction;
            if (arg.Result == System.Windows.Forms.DialogResult.OK)
            {
                int index = View.ProjectSelectIndex;
                View.ProjectSelectIndex = index;
                if (action is ActionVariable)
                {
                    View.ProjectSelectIndex = 2;
                }
                else {
                    if (View.ProjectSelectIndex == 2) {
                        View.ProjectSelectIndex = 0;
                    }
                }             
                if (action.RowStatue == ActionStatue.New)
                {
                    action.RowStatue = ActionStatue.Edit;
                    //Model.ShouldInsertActions = Context.State.SelectItemIndex != -1;
                    Model.AddAction(action);
                }
            }
            View.DataBind();
            View.SelectRow(Context.State.SelectItemIndex);
        }
  
        void View_ModelBind(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (Context.State.SelectItemIndex == -1) return;
            ActionBase actionElement = Model.ActiveActionModel[Context.State.SelectItemIndex - 1] as ActionBase;
            if (actionElement == null) return;
            if (actionElement.GetType().Name == "ActionClick")
            {
                return;
            }
            var mapField = (DataMapField)e.ClickedItem.Tag;
            actionElement.MapName = mapField.DisplayName;
            View.DataBind();
        }

        void View_RowDelete(object sender, EventArgs e)
        {
            int index = Context.State.SelectItemIndex;
            if (index <= 0) return;
            if (index <= Model.ActiveActionModel.Count)
            {
                Model.RemoveActiveAction(index - 1);
                View.DataBind();
                View.SelectRow(index);
            }            
        }

        void View_Order(object sender, EventArgs e)
        {
            Model.ActiveActionModel.OrderById();
            View.DataBind();
        }

        void View_MoveToPrevious(object sender, EventArgs e)
        {
            ActionRowMove((sender as ToolStripItem).Name);
        }

        void View_MoveToNext(object sender, EventArgs e)
        {
            ActionRowMove((sender as ToolStripItem).Name);
        }

        void View_MoveLast(object sender, EventArgs e)
        {
            ActionRowMove((sender as ToolStripItem).Name);
        }

        void View_MoveFirst(object sender, EventArgs e)
        {
            ActionRowMove((sender as ToolStripItem).Name);
        }

        void View_GoBack(object sender, EventArgs e)
        {
            ViewBase baseView = View as ViewBase;                   
            ActionBase action = View.DataSource.Parent as ActionBase;
            if (action is ActionBlock)
            {
                ((ActionBlock)action).Exit();                
            }
            Model.ActiveActionModel = action.ParentTest;
            View.DataSource = Model.ActiveActionModel;
            View.DataBind();
        }

        void View_Clear(object sender, EventArgs e)
        {
            Model.ActiveActionModel.Clear();
            View.DataBind();
        }

        void View_ActionTabSelectedIndexChanged(object sender, EventArgs e)
        {
            Model.Switch((StatementType)View.ProjectSelectIndex);           
        }
        /// <summary>
        /// 编辑活动或打开子活动列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void View_ActionEdit(object sender, EventArgs e)
        {
            if (Context.State.SelectItemIndex == -1) return;
            var action = Model.ActiveActionModel[Context.State.SelectItemIndex - 1];
            ViewBase baseView = View as ViewBase;            
            // 不是toolbar 编辑按钮触发事件
            if ((action is ActionBlock || action is ActionDataBlock) && !(sender is ToolStripButton))
            {
                ActionBlock block = action as ActionBlock;
                if (action is ActionBlock)
                {                    
                    ((ActionBlock)action).Enter();
                    Model.ActiveActionModel = ((ActionBlock)action).Actions;
                    
                }             
                else
                {
                    ActionDataBlock part = action as ActionDataBlock;
                    Model.ActiveActionModel = ((ActionDataBlock)action).Actions;
                }

                View.DataSource = Model.ActiveActionModel;
                Context.State.SelectItemIndex = -1;
                View.DataBind();
            }
            else
            {
                IEditorAction editView = View as IEditorAction;
                ucBaseEditor editor = action.GetEditor(editView);
                if (editor == null) return;
                editor.Action = action;
                editView.ShowEditAction(editor);
            }
            
        }
      
 
        /// <summary>
        /// grid 行操作
        /// </summary>
        /// <param name="action"></param>
        public void ActionRowMove(string action)
        {         
            int sID = Context.State.SelectItemIndex;

            if (sID == -1) return;
            sID--;
            int dID = 0;
            switch (action)
            {
                case "tsbScriptMoveTop":
                    dID = 0;
                    break;
                case "tsbScriptMovePervious":
                    if (sID == 0) return;
                    dID = sID - 1;
                    break;
                case "tsbScriptMoveNext":
                    if (sID == Model.ActiveActionModel.Count - 1) return;
                    dID = sID + 1;
                    break;
                case "tsbScriptMoveBottom":
                    dID = Model.ActiveActionModel.Count - 1;
                    break;
                default:
                    break;
            }

            Model.ActiveActionModel.MoveTo(sID, dID);
            View.DataBind();
            Context.State.SelectItemIndex = dID + 1;           
        }
    }
}
