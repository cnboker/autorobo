using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucSleep : ucBaseEditor
    {
        public ucSleep(IEditorAction editorAction):base(editorAction)
        {            
            InitializeComponent();            
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionSleep)base.Action;
                action.SleepTime = Convert.ToInt32(numSleep.Value);                
                return base.Action;
            }
            set
            {
                var action = (ActionSleep)value;
                numSleep.Value = action.SleepTime;                
                base.Action = action;
            }
        }

      
    }
}
