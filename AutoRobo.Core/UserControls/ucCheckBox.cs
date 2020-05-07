using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucCheckBox : ucBaseElement
    {
        public ucCheckBox(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionCheckbox)base.Action;
                action.FindMechanism = GuiToObject();
                action.Checked = chk.Checked;
                return action;
            }
            set
            {
                chk.Checked = ((ActionCheckbox)value).Checked;
                base.Action = value;
                ObjectToGui((ActionCheckbox)value);
            }
        }

   
    }
}
