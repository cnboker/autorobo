using System;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucKey : ucBaseElement
    {
        public ucKey(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionKey) base.Action;
                action.FindMechanism = GuiToObject();
                action.KeyToPress = Convert.ToChar(txtKeyCharacter.Text);
                action.KeyFunction = (ActionKey.KeyFunctions) Enum.Parse(typeof (ActionKey.KeyFunctions), ddlKeyFunction.SelectedItem.ToString());
                return base.Action;
            }
            set
            {
                var action = (ActionKey) value;
                txtKeyCharacter.Text = action.KeyToPress.ToString();
                ddlKeyFunction.SelectedItem = action.KeyFunction.ToString();
                base.Action = action;
                ObjectToGui(action);                
            }
        }


        protected override void OnValidate()
        {
            Convert.ToChar(txtKeyCharacter.Text);
        }
    }
}
