using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using System;
using AutoRobo.Core.UserControls;

namespace AutoRobo.UserControls
{
    public partial class ucBaseEditor : UserControl
    {
        public IEditorAction EditorAction { get; set; }
        private ActionBase action = null;
        private log4net.ILog logger = log4net.LogManager.GetLogger("ucBaseEditor");
        public string Title
        {
            get { return textBoxLabel.Text; }
        }

        public string Description
        {
            get
            {
                return descBox.Text;
            }
        }

        public virtual ActionBase Action
        {
            get
            {
                if (action == null) return null;
                action.Title = Title;
                action.Description = Description;
                return action;
            }
            set
            {
                action = value;
                descBox.Text = value.Description;
                textBoxLabel.Text = value.Title;
            }
        }

        public ucBaseEditor()
        {
            InitializeComponent();
        }

        public ucBaseEditor(IEditorAction EditorAction)
        {
            this.EditorAction = EditorAction;
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.tabControl1.SelectedIndex = 1;
        }

        private void okBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                HighlightHandle();                
                OnValidate();
                OnOK();
                //ActionElementBase ae = action as ActionElementBase;
                //if (ae == null) return;
                //ae.Perform();
            }
            catch (Exception ex) {               
                tabControl1.SelectedIndex = 1;
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                HighlightHandle();
                OnCancel();
            }
            catch { }
        }

        private void HighlightHandle() {
            ActionElementBase ae = action as ActionElementBase;
            if (ae == null) return;
           
            ae.Restore();            
        }
        /// <summary>
        /// 确定前校验规则
        /// </summary>
        protected virtual void OnValidate() { 
            
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        protected void RequiredValidate(string text, string errorMessage)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ApplicationException(errorMessage);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        protected void RequiredValidate(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ApplicationException("不能为空");
            }
        }

        protected void DigitValidate(string text, string errorMessage) {
            int result = 0;
            if (!int.TryParse(text, out result)) {
                throw new ApplicationException(errorMessage); 
            }
        }

        protected void DateValidate(string text) { 
        
        }

        protected virtual void OnOK() {
            this.Dock = DockStyle.Fill;
            EditorAction.CloseEditAction(this, DialogResult.OK);
        }

        protected virtual void OnCancel() {
            EditorAction.CloseEditAction(this, DialogResult.Cancel);
        }

        /// <summary>
        /// 活动变量管理
        /// </summary>
        protected void VariableDefine() {
            VarManager varManager = new VarManager(Action.AppContext.ActionModel.VariableActionModel);
            varManager.FormClosed += new FormClosedEventHandler(varManager_FormClosed);
            varManager.ShowDialog();
        }

        void varManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshView(action);
        }

        /// <summary>
        /// 重新更新视图
        /// </summary>
        protected virtual void RefreshView(ActionBase value)
        {
        
        }
    }
}
