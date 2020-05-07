using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.UserControls;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    /// <summary>
    /// 选择整数变量
    /// </summary>
    public partial class ucSelectVariable : ucBaseEditor
    {
        public ucSelectVariable(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionVariableReference action = (ActionVariableReference)base.Action;
                action.VariableName = varComboBox.SelectedItem.ToString();                
                return action;
            }
            set
            {
                var action = (ActionVariableReference)value;
                if (action is ActionVariableDecrement || action is ActionVariableIncrement)
                {                    
                    varComboBox.DataSource = value.VariableModel.OfType<ActionIntegerVariable>().Select(c => c.Name).ToArray();
                }
                else
                {
                    varComboBox.DataSource = value.VariableModel.OfType<ActionVariable>().Select(c => c.Name).ToArray();
                }
                varComboBox.SelectedItem = action.VariableName;                
                base.Action = action;
            }
        }

        protected override void OnValidate()
        {
            if (varComboBox.SelectedIndex == -1) {
                throw new ApplicationException("必须选择变量,如果没有变量请先定义变量");
            }

        }
    }
}
