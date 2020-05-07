using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucSelectList : ucBaseElement
    {
        public ucSelectList(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionSelectList)base.Action;
                action.FindMechanism = GuiToObject();
                action.SelectedValue = txtSelection.Text;
                action.Regex = chkIsRegex.Checked;
                action.ByValue = chkByValue.Checked;
                return base.Action;
            }
            set
            {
                var action = (ActionSelectList) value;
                txtSelection.Text = action.SelectedValue;
                chkIsRegex.Checked = action.Regex;
                chkByValue.Checked = action.ByValue;
                base.Action = action;
                ObjectToGui(action);
            }
        }

  
    }
}
