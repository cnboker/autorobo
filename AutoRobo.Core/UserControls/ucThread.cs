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
    public partial class ucThread : ucBaseEditor
    {
        public ucThread(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();            
        }
        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionThread)base.Action;
                action.ThreadCount = Convert.ToInt32(threadCountBox.Value);
                return base.Action;
            }
            set
            {
                var action = (ActionThread)value;
                threadCountBox.Value = action.ThreadCount;                
                base.Action = action;                
            }
        }
      
    }
}
