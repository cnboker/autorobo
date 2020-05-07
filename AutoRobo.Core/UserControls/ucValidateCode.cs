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
    public partial class ucValidateCode : ucTypeText
    {
        public ucValidateCode()
        {
            InitializeComponent();
        }

        public ucValidateCode(IEditorAction editorAction)
            : base(editorAction)
        {
            InitializeComponent();
        }

        public override Actions.ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (IValidateGroup)base.Action;
                action.GroupName = groupNameBox.Text;
                ((ActionValidateCode)action).IsMobileValidate = mobileVaildateCheckBox.Checked;
                return base.Action;
            }
            set
            {
                var action = (IValidateGroup)value;
                mobileVaildateCheckBox.Checked = ((ActionValidateCode)action).IsMobileValidate;
                groupNameBox.Text = action.GroupName;
                base.Action = value;
            }
        }
    }
}
