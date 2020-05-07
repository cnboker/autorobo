using System;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucMouse : ucBaseElement
    {
        public ucMouse(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionMouse)base.Action;
                action.FindMechanism = GuiToObject();
                action.MouseFunction = (ActionMouse.MouseFunctions)Enum.Parse(typeof(ActionMouse.MouseFunctions), ddlMouseFunction.SelectedItem.ToString());
                return base.Action;
            }
            set
            {
                var action = (ActionMouse) value;
                ddlMouseFunction.SelectedItem = action.MouseFunction.ToString();
                base.Action = action;
                ObjectToGui(action);                
            }
        }

     
    }
}
