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
    public partial class ucValidateImage : ucBaseElement
    {
        public ucValidateImage()
        {
            InitializeComponent();
        }

        public ucValidateImage(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionValidateImage)base.Action;
                action.GroupName = groupNameBox.Text;
                action.IsHide = imageDisplayStatue.Checked;
                ((ActionElementBase)action).FindMechanism = GuiToObject();                          
                return (ActionBase)action;
            }
            set
            {
                var action = (ActionValidateImage)value;              
                groupNameBox.Text = action.GroupName;
                imageDisplayStatue.Checked = action.IsHide;
                base.Action = (ActionBase)action;
                ObjectToGui(((ActionElementBase)action));                
            }
        }

    }
}
