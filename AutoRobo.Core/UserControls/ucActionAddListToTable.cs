using System;
using AutoRobo.Core.Actions;
using AutoRobo.UserControls;
using System.Collections.Generic;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    public partial class ucActionAddListToTable : ucBaseEditor
    {
        public ucActionAddListToTable(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionAddListToTable)base.Action;
                action.TableName = tableComboBox.Text;
                action.ObjectName = arrayComboBox.SelectedItem.ToString();                
                return action;
            }
            set
            {
                var action = (ActionAddListToTable)value;                

                arrayComboBox.DataSource = value.VariableModel.OfType<ActionArrayVariable>().Select(c => c.Name).ToArray(); 
                arrayComboBox.SelectedItem = action.ObjectName;

                tableComboBox.DataSource = value.VariableModel.OfType<ActionTableVariable>().Select(c => c.Name).ToArray(); 
                tableComboBox.SelectedItem = action.TableName;

                base.Action = action;
            }
        }

      
        private void varBtn_Click(object sender, EventArgs e)
        {
            VariableDefine();
        }

        protected override void RefreshView(ActionBase value)
        {
            var action = (ActionAddListToTable)value;

            var model = action.AppContext.ActionModel.VariableActionModel;
            tableComboBox.DataSource = value.VariableModel.OfType<ActionTableVariable>().Select(c => c.Name).ToArray();
            tableComboBox.SelectedItem = action.TableName;
            base.Action = action;
        }
    }
}
