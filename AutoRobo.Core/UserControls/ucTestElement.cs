using System;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;

namespace AutoRobo.UserControls
{
    public partial class ucTestElement : ucBaseElement
    {
        public ucTestElement(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionConditionByAttribute)base.Action;
                action.FindMechanism = GuiToObject();
                action.TestToPerform =
                    (ActionConditionByAttribute.AvailableTests)ddlTestToPerform.SelectedIndex;                    
                action.TestingProperty = txtProperty.Text;
                action.TestingValue = txtValue.Text;
                
                return base.Action;
            }
            set
            {
                var action = (ActionConditionByAttribute)value;
                txtProperty.Text = action.TestingProperty;
                txtValue.Text = action.TestingValue;
                
                ddlTestToPerform.SelectedIndex = (int)action.TestToPerform;
                base.Action = action;
                ObjectToGui(action);
            }
        }

        protected override void OnValidate()
        {
            if (string.IsNullOrEmpty(txtProperty.Text)) {
                throw new ApplicationException("测试属性不能为空");
            }
            if (string.IsNullOrEmpty(txtValue.Text)) {
                throw new ApplicationException("测试值不能为空");
            }
        }
    }
}
