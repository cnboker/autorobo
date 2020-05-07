using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucNavigate : ucBaseEditor
    {
        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var nav = (ActionNavigate) base.Action;
                nav.URL = txtURL.Text;
                nav.BindName = parameterName.Text;                
                return nav;
            }
            set
            {
                var action=(ActionNavigate)value;
                txtURL.Text = action.URL;
                parameterName.Text = action.BindName;                
                base.Action =action;
            }
        }

        public ucNavigate(IEditorAction editorAction):base(editorAction)
        {
            InitializeComponent();
        }

  
    }
}
