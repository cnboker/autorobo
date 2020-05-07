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
    public partial class ucLoopByElements : ucBaseElement
    {
        public ucLoopByElements(IEditorAction Caller):base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionForeach)base.Action;                
                action.FirstNumber = loopProperty1.FirstNumber;
                action.LastNumber = loopProperty1.LastNumber;
                action.StepNumber = loopProperty1.StepNumber;
                action.FindMechanism = GuiToObject();
                return action;
            }
            set
            {
                ActionForeach action = value as ActionForeach;                
                loopProperty1.FirstNumber = action.FirstNumber;
                loopProperty1.LastNumber = action.LastNumber;
                loopProperty1.StepNumber = action.StepNumber;
                base.Action = value;
                ObjectToGui((ActionForeach)value);  
            }
        }

        protected override void OnValidate()
        {        
            loopProperty1.Validate();
        }
    }
}
