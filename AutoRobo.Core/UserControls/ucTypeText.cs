using System.Windows.Forms;
using AutoRobo.Core.Actions;
using System;
using AutoRobo.Core;
using Util;
using AutoRobo.Core.UserControls;

namespace AutoRobo.UserControls
{
    public partial class ucTypeText : ucBaseElement
    {
      
        public ucTypeText()
        {
            InitializeComponent();
           
        }

        public ucTypeText(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();
            
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionTypeText) base.Action;
                action.FindMechanism = GuiToObject();
                action.TextToType = txtTextToType.Text;
                action.FormatString = Getter();
                action.ValueOnly = chkValueOnly.Checked;
                action.MapName = inputVariableControl.BindName;
                return action;
            }
            set
            {
                var action = (ActionTypeText) value;
                Setter(action);
                chkValueOnly.Checked = action.ValueOnly;
                inputVariableControl.Initialize(action.AppContext.ActionModel.VariableActionModel);
                inputVariableControl.Bind(action.MapName);
                base.Action = action;
                ObjectToGui(action);                
            }
        }

        /// <summary>
        /// 如果是随机数则格式为{类型:长度}
        /// </summary>
        private string Getter() {
            if (typeComboBox.SelectedIndex != -1 && randomCheckBox.Checked)
            {
                return "{" + typeComboBox.SelectedIndex.ToString() + ":" + Convert.ToInt16(lenNumericUpDown.Value) + "}";
            }
            return "";
        }


        private void Setter(ActionTypeText action)
        {
            var text = action.FormatString;
            if (string.IsNullOrEmpty(text)) {
                txtTextToType.Text = action.TextToType;
                return;
            }
            if (text[0] == '{' && text[text.Length - 1] == '}')
            {
                var arr = text.Substring(1, text.Length - 2).Split(":".ToCharArray());
                typeComboBox.SelectedIndex = Convert.ToInt16(arr[0]);
                lenNumericUpDown.Value = Convert.ToInt16(arr[1]);
                randomCheckBox.Checked = true;
            }      
        }

  
        //private void btnDataReplacementType_Click(object sender, System.EventArgs e)
        //{
        //   // AddItemsToMenu(menuReplacement);
        //    menuReplacement.Show(btnDataReplacementType, 0, btnDataReplacementType.Height);
        //}

        private void testBtn_Click(object sender, System.EventArgs e)
        {
            if (typeComboBox.SelectedIndex == -1) return;
            var t = (RandomType)Enum.Parse(typeof(RandomType), typeComboBox.SelectedIndex.ToString());
            txtTextToType.Text = RandomStringHelper.RandomString(t, Convert.ToInt16(lenNumericUpDown.Value));
        }
    }
}
