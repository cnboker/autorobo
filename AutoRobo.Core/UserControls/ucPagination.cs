using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.UserControls
{
    public partial class ucPagination : ucBaseElement
    {
        public ucPagination(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionPagination action = (ActionPagination)base.Action;
                action.FindMechanism = GuiToObject();
                if (existRadioButton.Checked)
                {
                    action.PageCount = -1;
                }
                else {
                    action.PageCount = Convert.ToInt32(pageCountTextbox.Text);
                }             
                return action;
            }
            set
            {
                var action = (ActionPagination)value;
                if (action.PageCount == -1)
                {
                    existRadioButton.Checked = true;
                }
                else {
                    countRadioButton.Checked = true;
                    pageCountTextbox.Text = action.PageCount.ToString();
                }
                ObjectToGui((ActionPagination)value);
                base.Action = action;
            }
        }

        protected override void OnValidate()
        {
            if (countRadioButton.Checked) { 
                int result = 0;
                bool b = int.TryParse(pageCountTextbox.Text, out result);
                if (!b) {
                    throw new ApplicationException("必须是数字");
                }
            }
            base.OnValidate();
        }
    }
}
