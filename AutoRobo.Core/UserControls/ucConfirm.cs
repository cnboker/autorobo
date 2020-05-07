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
    public partial class ucConfirm :   ucBaseEditor
    {
        public ucConfirm(IEditorAction Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionConfirmHandler)base.Action;
                action.DialogResult = rbOK.Checked?DialogResult.OK:DialogResult.Cancel;
                return base.Action;
            }
            set
            {
                var action = (ActionConfirmHandler)value;
               rbCancel.Checked = !(rbOK.Checked = action.DialogResult == DialogResult.OK);
               base.Action = action;
            }
        }

  
    }
}
