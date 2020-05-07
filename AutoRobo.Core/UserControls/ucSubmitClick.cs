using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Actions;

namespace AutoRobo.UserControls
{
    public partial class ucSubmitClick : ucClick
    {
        public ucSubmitClick()
        {
            InitializeComponent();
        }

        public ucSubmitClick(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionSubmitClick)base.Action;
                action.FindMechanism = GuiToObject();
                action.AutoSubmit = submitCheckbox.Checked;
                action.RecordMark = recordMarkCheckbox.Checked;
                return action;
            }
            set
            {
                submitCheckbox.Checked = ((ActionSubmitClick)value).AutoSubmit;
                recordMarkCheckbox.Checked = ((ActionSubmitClick)value).RecordMark;
                base.Action = value;
                ObjectToGui((ActionClick)value);
            }
        }
    }
}
