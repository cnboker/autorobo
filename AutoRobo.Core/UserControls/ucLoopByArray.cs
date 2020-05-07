using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using System.Linq;

namespace AutoRobo.Core.UserControls
{
    public partial class ucLoopByArray : ucBaseEditor
    {
        public ucLoopByArray(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionForeachByVariable)base.Action;
                action.VarName = arrayComboBox.SelectedItem.ToString();
                //action.DataType = ((ActionVariable)arrayComboBox.SelectedItem).DataType;
                action.Reverse = reverseCheckBox.Checked;
                int topN = -1;
                int.TryParse(topTextBox.Text, out topN);
                action.TopN = topN;
                return action;
            }
            set
            {
                var action = (ActionForeachByVariable)value;

                              
                arrayComboBox.DataSource = value.VariableModel.OfType<ActionArrayVariable>().Select(c => c.Name).ToArray(); ;
                if (!string.IsNullOrEmpty(action.VarName))
                {
                    arrayComboBox.SelectedItem = action.VarName;
                }
                reverseCheckBox.Checked = action.Reverse;
                topTextBox.Text = action.TopN.ToString();
                base.Action = action;
            }
        }

      
        protected override void OnValidate()
        {
            if (arrayComboBox.SelectedIndex == -1)
            {
                throw new ApplicationException("数组列表必须选择");
            }           
        }

    

    }
}
