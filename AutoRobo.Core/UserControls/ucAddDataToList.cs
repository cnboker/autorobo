using System;
using AutoRobo.Core.Actions;
using AutoRobo.UserControls;
using WatiN.Core;
using System.Linq;
using AutoRobo.Core.Actions.Data;

namespace AutoRobo.Core.UserControls
{
    public partial class ucAddDataToList : ucBaseElement
    {
        public ucAddDataToList(IEditorAction Caller):base(Caller)
        {
            InitializeComponent();
            attrCombox.SelectedIndexChanged += new EventHandler(attrCombox_SelectedIndexChanged);
            arrayCombox.SelectedIndexChanged += arrayCombox_SelectedIndexChanged;
        }

        void arrayCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionFetchText action = (ActionFetchText)base.Action;
            if (arrayCombox.SelectedItem != null)
            {
                ActionVariable var1 = action.VariableModel.Find(arrayCombox.SelectedItem.ToString());
                methodComboBox1.Enabled = false;
                if (var1 is ActionTableVariable)
                {
                    methodComboBox1.Enabled = true;
                }
            }
        }

        void attrCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyTextbox.Enabled = attrCombox.SelectedIndex == 2;
            if (propertyTextbox.Enabled) {
                ActionElementBase action = base.Action as ActionElementBase;
                if (action != null)
                {
                    if (action.ElementType == ElementTypes.Link)
                    {
                        propertyTextbox.Text = "href";
                    }
                    else if (action.ElementType == ElementTypes.Image)
                    {
                        propertyTextbox.Text = "src";
                    }
                }
            }
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                ActionFetchText action = (ActionFetchText)base.Action;

                SetPropertyName(action);
             
                action.FindMechanism = GuiToObject();
                action.ObjectName = arrayCombox.Text;
                action.FliterEmptyString = fliterEmptyStringCheckBox.Checked;
                action.ExtractType = (ExtractType)methodComboBox1.SelectedIndex;
                return action;
            }
            set
            {
                ActionFetchText action = (ActionFetchText)value;
                if (action.PropertyName == "Text" || string.IsNullOrEmpty(action.PropertyName))
                {
                    attrCombox.SelectedIndex = 0;
                }
                else if (action.PropertyName == "Html")
                {
                    attrCombox.SelectedIndex = 1;
                }
                else
                {
                    attrCombox.SelectedIndex = 2;
                    propertyTextbox.Enabled = true;
                    propertyTextbox.Text = action.PropertyName;
                }
                methodComboBox1.SelectedIndex = (int)action.ExtractType;
                ObjectToGui(action);
                base.Action = action;
                
                //if (action is ActionAddMutiContentToTableColumn || action is ActionAddMutiContentToTableRow)
                //{
                //    arrayCombox.DataSource = value.VariableModel.OfType<ActionTableVariable>().Select(c => c.Name).ToArray();
                //}
                //else
                //{
                //    arrayCombox.DataSource = value.VariableModel.OfType<ActionArrayVariable>().Select(c => c.Name).ToArray();
                //}
                arrayCombox.DataSource = value.VariableModel.OfType<ActionVariable>().Select(c => c.Name).ToArray();
                arrayCombox.SelectedItem = action.ObjectName;
                fliterEmptyStringCheckBox.Checked = action.FliterEmptyString;
            }
        }

        private void SetPropertyName(ActionFetchText action)
        {
            if (attrCombox.Text == "Text" || attrCombox.Text == "Html")
            {
                action.PropertyName = attrCombox.Text;
            }
            else
            {
                action.PropertyName = propertyTextbox.Text;
            }
        }

        protected override void OnValidate()
        {
            if (string.IsNullOrEmpty(arrayCombox.Text))
            {
                throw new ApplicationException("数组名称不能为空");
            }
            if (attrCombox.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(propertyTextbox.Text))
                {
                    throw new ApplicationException("元素属性值不能为空");
                }
            }
        }
           

        private void ruleButton_Click(object sender, EventArgs e)
        {
            ActionFetchText action = (ActionFetchText)base.Action;
            SetPropertyName(action);
            StringRegexForm regex = new StringRegexForm(action.Pattern, action.GetDefaultValue());
            if (regex.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                action.Pattern = regex.Pattern;                
            }
        }

        private void varBtn_Click(object sender, EventArgs e)
        {
            VariableDefine();
        }

        protected override void RefreshView(ActionBase value)
        {
            ActionFetchText action = (ActionFetchText)value;

            arrayCombox.DataSource = value.VariableModel.OfType<ActionVariable>().Select(c => c.Name).ToArray();
            arrayCombox.SelectedItem = action.ObjectName;
        }
    }
}
