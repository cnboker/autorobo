using System;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucWait : ucBaseElement
    {
        public ucWait(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionWait)base.Action;
                action.FindMechanism = GuiToObject();
                action.WaitType = (ActionWait.WaitTypes)ddlWaitType.SelectedIndex;
                action.AttributeName = txtAttribute.Text;
                action.AttributeValue = txtValue.Text;
                action.AttributeRegex = chkRegex.Checked;
                action.WaitTimeout = Convert.ToInt32(numTimeout.Value);
                return base.Action;
            }
            set
            {
                var action = (ActionWait)value;
                ddlWaitType.SelectedIndex = (int)action.WaitType;
                txtAttribute.Text = action.AttributeName;
                txtValue.Text = action.AttributeValue;
                chkRegex.Checked = action.AttributeRegex;
                if (action.WaitTimeout == 0) action.WaitTimeout = WatiN.Core.Settings.WaitUntilExistsTimeOut;
                numTimeout.Value = action.WaitTimeout;
                base.Action = action;
                ObjectToGui(action);
            }
        }

        protected override void OnValidate()
        {
            if (ddlWaitType.SelectedIndex == 2) {
                if (string.IsNullOrEmpty(txtAttribute.Text))
                {
                    throw new ApplicationException("属性不能为空");
                }
                if (string.IsNullOrEmpty(txtValue.Text))
                {
                    throw new ApplicationException("值不能为空");
                }
            }
        } 
    }
}
