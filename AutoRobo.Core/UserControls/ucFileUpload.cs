using System;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucFileUpload : ucBaseElement
    {
        public ucFileUpload(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionFileDialog)base.Action;
                action.FindMechanism = GuiToObject();
                action.Filename = txtFile.Text;
                return base.Action;
            }
            set
            {
                var action = (ActionFileDialog) value;
                txtFile.Text = action.Filename;
                base.Action = action;
                ObjectToGui(action);                
            }
        }

   
    }
}