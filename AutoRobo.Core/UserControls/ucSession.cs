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
    public partial class ucSession : ucBaseEditor
    {
        public ucSession(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionRestoreSession)base.Action;
                action.IsRestoreSession = chk.Checked;
                return action;
            }
            set
            {
                chk.Checked = ((ActionRestoreSession)value).IsRestoreSession;
                base.Action = value;
            }
        }

    }
}
