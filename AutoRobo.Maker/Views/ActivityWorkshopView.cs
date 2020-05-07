using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using AutoRobo.Makers.EventArguments;
using AutoRobo.Makers.Presentation;
using SourceGrid;
using AutoRobo.Core.Models;
using AutoRobo.Core.UserControls;

namespace AutoRobo.Makers.Views
{
    public partial class ActivityWorkshopView : ViewBase, IActivityWorkshopView, IEditorAction
    {
        private ActivityWorkshopPresenter presenter = null;
        private Panel hiddenPanel = null;
        private const string ClipboardFormat = "ActionFormat";
        /// <summary>
        /// 剪切数据
        /// </summary>
        private List<ActionBase> clipBoardData = null;

        public ActivityWorkshopView()
        {
            InitializeComponent();
            this.TabText = "活动列表";
            gridSource.Selection.EnableMultiSelection = true;
            presenter = new ActivityWorkshopPresenter(this);
            hiddenPanel = new Panel();
            gridSource.PreviewKeyDown += new PreviewKeyDownEventHandler(gridSource_PreviewKeyDown);
            ModelSet.DataSourceChanged += new Action<ActionList>(ModelSet_DataSourceUpdate);
            CloseButtonVisible = false;
        }

        void ModelSet_DataSourceUpdate(ActionList obj)
        {
            if (Pane == null) return;           
            Pane.ActiveContent = this;  
            DataSource = obj;
            DataBind();
        }

      

        void gridSource_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {                    
                case (Keys.X | Keys.Control): //ctrl+x 剪切
                    ActionCut();
                    break;
                case Keys.V | Keys.Control: //ctrl+c 粘帖
                    ActionPaste();
                    break;
                case (Keys.Oemplus | Keys.Control): //ctrl + +
                    presenter.ActionRowMove("tsbScriptMovePervious");
                    break;
                case (Keys.OemMinus | Keys.Control): //ctrl + -
                    presenter.ActionRowMove("tsbScriptMoveNext");
                    break;
                case (Keys.Home | Keys.Control):
                    presenter.ActionRowMove("tsbScriptMoveTop");
                    break;
                case (Keys.End | Keys.Control):
                    presenter.ActionRowMove("tsbScriptMoveBottom");
                    break;
                case Keys.Delete:
                    if (RowDelete != null) {
                        RowDelete(this, EventArgs.Empty);
                    }
                    break;
                case System.Windows.Forms.Keys.Up:
                    if (gridSource.Selection.ActivePosition.Row > 1)
                    {
                        Context.State.SelectItemIndex = gridSource.Selection.ActivePosition.Row - 1;
                    }
                    break;
                case System.Windows.Forms.Keys.Down:
                    if (gridSource.Selection.ActivePosition.Row < gridSource.RowsCount)
                    {
                        Context.State.SelectItemIndex = gridSource.Selection.ActivePosition.Row + 1;
                    }
                    break;
                case Keys.F4:
                    if (ActionEdit != null)
                    {
                        ActionEdit(editToolStripButton, EventArgs.Empty);
                    }
                    break;
                default: break;
            }
        }
        /// <summary>
        /// 活动剪切
        /// </summary>
        private void ActionCut() {
            RangeRegion region = gridSource.Selection.GetSelectionRegion();
            int[] index = region.GetRowsIndex();
            if (index.Length == 0) return;
            clipBoardData = new List<ActionBase>();
            foreach (var i in index)
            {
                clipBoardData.Add(DataSource[i - 1]);
            }         
        }
        /// <summary>
        /// 活动粘帖
        /// </summary>
        private void ActionPaste() {
            if (clipBoardData == null) return;
            if (clipBoardData.Count == 0) return;
            ActionList source = clipBoardData[0].ParentTest;
            for (int i = 0; i < clipBoardData.Count; i++)
            {
                ActionBase action = clipBoardData[i];
                source.Remove(action);
                if (Context.State.SelectItemIndex != -1)
                {
                    int insertIndex = Context.State.SelectItemIndex + i;
                    if(insertIndex >= DataSource.Count){
                        insertIndex = DataSource.Count - 1;
                    }
                    DataSource.Insert(insertIndex, action);
                }
                else
                {
                    DataSource.Add(action);
                }
                
            }
            DataBind();
            clipBoardData = null;
        }
 
        public Core.ActionList DataSource
        {
            get;
            set;
        }

        
        public void DataBind()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(DataBind));
                return;
            }
            ActionGrid grid = null;
            //变量列表
            if (ProjectSelectIndex == 2)
            {
                varActionGrid.ClearGrid();
                grid = varActionGrid;
            }
            else {
                //主活动列表或过程活动列表
                gridSource.ClearGrid();
                gridSource.Method = Context.ActionModel.DataMethod;
                backToolStripButton.Enabled = (DataSource != null && DataSource.Parent != null);
                grid = gridSource;
            }
            if (DataSource == null) return;
            
            foreach (ActionBase action in DataSource) {
                grid.AddGridRowItem(action);
            }
        }

        public void SelectRow(int value)
        {
            if (InvokeRequired) {
                Invoke(new Action<int>(SelectRow), value);
                return;
            }
            
            gridSource.Selection.ResetSelection(false);
            if (value != -1)
            {
                gridSource.Selection.SelectRow(value, true);
                gridSource.Selection.FocusRow(value);
            }
        }

        public event EventHandler MoveToPrevious;

        public event EventHandler MoveToNext;

        public event EventHandler MoveFirst;

        public event EventHandler MoveLast;

        public event EventHandler Clear;

        public event EventHandler Order;

        public event EventHandler RowDelete;

        public event EventHandler ActionEdit;

        public event EventHandler ActionTabSelectedIndexChanged;

        public event EventHandler GoBack;

        public event ToolStripItemClickedEventHandler ModelBind;

   
        private void tsbScriptMoveTop_Click(object sender, EventArgs e)
        {
            if (MoveFirst != null) { MoveFirst(sender, e); }
        }

        private void tsbScriptMovePervious_Click(object sender, EventArgs e)
        {
            if (MoveToPrevious != null) { MoveToPrevious(sender, e); }
        }

        private void tsbScriptMoveNext_Click(object sender, EventArgs e)
        {
            if (MoveToNext != null) { MoveToNext(sender, e); }
        }

        private void tsbScriptMoveBottom_Click(object sender, EventArgs e)
        {
            if (MoveLast != null) { MoveLast(sender, e); }
        }

        private void tsbDeleteAction_Click(object sender, EventArgs e)
        {
            if (RowDelete != null) { RowDelete(sender, e); }
        }

        private void tsbClearTest_Click(object sender, EventArgs e)
        {
            if (Clear != null) { Clear(sender, e); }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (Order != null) { Order(sender, e); }
        }

        private void mapDropdownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ModelBind != null) { ModelBind(sender, e); }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex != 2)
            {
                tabControl.TabPages[tabControl.SelectedIndex].Controls.Add(container);                
            }
            if (ActionTabSelectedIndexChanged != null) { ActionTabSelectedIndexChanged(sender, e); }
        }

        private void gridSource_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ActionEdit != null) {
                ActionEdit(sender, e);
            }
        }

        private void gridSource_MouseClick(object sender, MouseEventArgs e)
        {
            if (ProjectSelectIndex == 2)
            {
                Context.State.SelectItemIndex = this.varActionGrid.Selection.ActivePosition.Row;
            }
            else
            {
                Context.State.SelectItemIndex = gridSource.Selection.ActivePosition.Row;
            }
        }


        public int ProjectSelectIndex
        {
            get {
                int index = 0;
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        index = tabControl.SelectedIndex;
                    }));
                }
                else {
                    index = tabControl.SelectedIndex;
                }
                return index;
            }
            set {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        if (tabControl.SelectedIndex != value)
                        {
                            tabControl.SelectedIndex = value;
                            tabControl_SelectedIndexChanged(this, EventArgs.Empty);
                        }

                    }));
                }
                else {
                    if (tabControl.SelectedIndex != value)
                    {
                        tabControl.SelectedIndex = value;
                        tabControl_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// 显示活动编辑器
        /// </summary>
        /// <param name="UCEditor"></param>
        public void ShowEditAction(UserControls.ucBaseEditor UCEditor)
        {
            Pane.ActiveContent = this;  
            foreach(Control c in this.Controls){
                hiddenPanel.Controls.Add(c);
            }
            UCEditor.Dock = DockStyle.Fill;
            Controls.Add(UCEditor);
        }
        /// <summary>
        /// 确定或取消活动编辑器
        /// </summary>
        /// <param name="UCEditor"></param>
        /// <param name="result"></param>
        public void CloseEditAction(UserControls.ucBaseEditor UCEditor, DialogResult result)
        {           
            Controls.Remove(UCEditor);
            foreach (Control c in hiddenPanel.Controls)
            {
                Controls.Add(c);
            }        
            if (ActionEditOK != null)
            {
                ActionEditOK(this, new ActionEditEventArgs() { EditAction = UCEditor.Action, Result = result });
            }    
        }
        /// <summary>
        /// 删除所有断点
        /// </summary>
        public void DeleteAllBreakpoints()
        {
            int counter = 1;
            if (tabControl.SelectedIndex != 0) return;
            foreach (ActionBase action in DataSource)
            {
                if (action.Breakpoint != BreakpointIndicators.NoBreakpoint)
                {
                    action.Breakpoint = BreakpointIndicators.NoBreakpoint;
                    gridSource.UpdateCellIcon(counter, action);  
                    
                }
                counter++;
            }
        }

        public void UpdateCellIcon(int rowIndex, ActionBase action)
        {
            gridSource.UpdateCellIcon(rowIndex, action);
        }

        public void SetBreakPoint()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SetBreakPoint));
                return;
            }
            if (Context.State.SelectItemIndex <= 0) return;
            //变量列表不能设置断点
            if (tabControl.SelectedIndex == 2) return;

            var action = DataSource[Context.State.SelectItemIndex - 1];
            if (action == null) return;
            if (action.Breakpoint == BreakpointIndicators.NoBreakpoint)
            {
                action.Breakpoint = BreakpointIndicators.ActiveBreakpoint;
            }
            else if (action.Breakpoint == BreakpointIndicators.ActiveBreakpoint)
            {
                action.Breakpoint = BreakpointIndicators.NoBreakpoint;
            }
            gridSource.UpdateCellIcon(Context.State.SelectItemIndex, action);            
        }

        public event EventHandler ActionEditOK;

     
        private void backToolStripButton_Click(object sender, EventArgs e)
        {
            if (GoBack != null) {
                GoBack(sender, e);
            }
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            if (ActionEdit != null) {
                ActionEdit(sender, e);
            }
        }

        private void varEditStripBtn_Click(object sender, EventArgs e)
        {
            VarManager varManager = new VarManager(Context.ActionModel.VariableActionModel);
            varManager.FormClosed += new FormClosedEventHandler(varManager_FormClosed);
            varManager.ShowDialog();
        }

        void varManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ProjectSelectIndex == 2)
            {
                DataBind();
            }
        }
    }
}
