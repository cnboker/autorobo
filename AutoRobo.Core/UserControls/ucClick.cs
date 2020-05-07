using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucClick : ucBaseElement
    {
        public ucClick(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public ucClick() {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionClick) base.Action;
                action.FindMechanism = GuiToObject();
                action.NoWait = chkNoWait.Checked;                
                return action;
            }
            set
            {
                chkNoWait.Checked = ((ActionClick)value).NoWait;                   
                base.Action = value;               
                ObjectToGui((ActionClick)value);           
            }
        }

    }
}
