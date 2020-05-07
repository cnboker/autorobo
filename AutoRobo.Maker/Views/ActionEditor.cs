using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using AutoRobo.UserControls;

namespace AutoRobo.Makers.Views
{
    public class ActionEditor : MyDialog, IEditorAction
    {        
        public ActionEditor(ActionBase action) {            
            var editor = action.GetEditor(this);            
            editor.Action = action;
            this.ShowEditAction(editor);
        }
      
        public void ShowEditAction(UserControls.ucBaseEditor UCEditor)
        {
            this.Size = UCEditor.Size;
            this.Controls.Add(UCEditor);
        }

        public void CloseEditAction(UserControls.ucBaseEditor UCEditor, System.Windows.Forms.DialogResult result)
        {
            this.Close();
        }
    }
}
