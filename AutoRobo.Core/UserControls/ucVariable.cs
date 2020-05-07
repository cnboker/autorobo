using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.UserControls;
using AutoRobo.WebHelper;
using AutoRobo.Core.UserControls.Wizard;
using System.Collections.Generic;
using System.Linq;
using AutoRobo.Core.UserControls.DTS;

namespace AutoRobo.Core.UserControls
{
    public partial class ucVariable : ucBaseEditor
    {
        private VariableData data = null;

        public ucVariable(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
            dataSourceBtn.Visible = false;
            varTypeList.SelectedIndexChanged += new EventHandler(varTypeList_SelectedIndexChanged);
        }

        void varTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var action = (ActionVariable)base.Action;
            if (action is ActionTableVariable || action is ActionArrayVariable)
            {
                dataSourceBtn.Visible = (varTypeList.SelectedIndex != 0);
            }
        }
  
        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionVariable)base.Action;
                action.Name = nameTextbox.Text.Trim();
                action.Direction = (VariableDirection)varTypeList.SelectedIndex;
                if (action is ActionIntegerVariable || action is ActionStringVariable)
                {
                    action.Data = valueTextbox.Text;
                }
                return base.Action;
            }
            set
            {
                var action = (ActionVariable)value;
                nameTextbox.Text = action.Name;
                varTypeList.SelectedIndex = (int)action.Direction;
                if (action is ActionTableVariable || action is ActionArrayVariable)
                {
                    varTypeLabel.Visible = true;
                    varTypeList.Visible = true;
                    dataBtn.Visible = true;                    
                    dataSourceBtn.Visible = varTypeList.SelectedIndex != 0;
                }
                else if (action is ActionIntegerVariable || action is ActionStringVariable)
                {
                    defaultLabel.Visible = true;
                    valueTextbox.Visible = true;
                    valueTextbox.Text = action.Data.ToString();
                }
                base.Action = action;
            }
        }
       
        protected override void OnValidate()
        {
            RequiredValidate(nameTextbox.Text, "名称不能为空");
            if(valueTextbox.Visible){
                DigitValidate(valueTextbox.Text, "必须为整数");
            }
        }

        private void dataBtn_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        private void ViewData() {
            if (data != null) return;
            data = new VariableData((ActionVariable)Action);
            data.FormClosed += new FormClosedEventHandler(data_FormClosed);
            data.Show();
        }
        void data_FormClosed(object sender, FormClosedEventArgs e)
        {
            data = null;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataSourceBtn_Click(object sender, EventArgs e)
        {
            DTSWizardBook wizard = new DTSWizardBook();
            DataTable dt = wizard.Import();
            if (dt != null)
            {                
                 if (Action is ActionTableVariable)
                 {
                     ActionTableVariable tableVariable = Action as ActionTableVariable;
                     tableVariable.Data = dt;
                 }
                 else if (Action is ActionArrayVariable) {
                     ActionArrayVariable arr = Action as ActionArrayVariable;
                     arr.Data = TableToList(dt);
                 }
            }
        }

        private List<string> TableToList(DataTable dt) {
            List<string> rows = new List<string>();
            foreach (DataRow dataRow in dt.Rows)
            {
                string s = string.Join(";", dataRow.ItemArray.Select(item => item.ToString()));
                if (!string.IsNullOrEmpty(s))
                {
                    rows.Add(s);
                }
            }
            return rows;
        }
    }
}
