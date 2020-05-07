using System;
using System.IO;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using WatiN.Core.Native.InternetExplorer;
using System.Linq;
using WatiN.Core;


namespace AutoRobo.UserControls
{
    public partial class ucBaseElement : ucBaseEditor
    {  
        private ActionElementBase action;

        public ucBaseElement()
        {
            InitializeComponent();
        }

        public ucBaseElement(IEditorAction Caller):base(Caller)
        {            
            InitializeComponent();
        }

        internal FindAttribute GetFindAttribute()
        {
            var attribute = new FindAttribute
                                {
                                    FindName = methodComboBox.SelectedItem.ToString()
                                };
            if (Enum.IsDefined(typeof(FindMethods), attribute.FindName))
            {
                attribute.FindMethod = (FindMethods) Enum.Parse(typeof (FindMethods), attribute.FindName);
                attribute.FindName = null;
            }
            attribute.FindValue = valueTextBox.Text;
            attribute.Regex = regxCheckBox.Checked;
            return attribute;
        }

  
        public static string GetIconFullPath(string Filename)
        {
            Filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Icons\\" + Filename);
            return Filename;
        }

        public void ObjectToGui(ActionElementBase act)
        {
            this.action = act;      
            methodComboBox.DataSource = Enum.GetValues(typeof(FindMethods));
            methodComboBox.SelectedIndexChanged += new EventHandler(methodComboBox_SelectedIndexChanged);
            if (act.FindMechanism.Count > 0)
            {
                FindAttribute attribute = act.FindMechanism[0];
                methodComboBox.SelectedItem = attribute.FindMethod;
                regxCheckBox.Checked = attribute.Regex;
                valueTextBox.Text = attribute.FindValue;
                var currentElement = action.GetElement();
                if (currentElement != null && currentElement.Exists)
                {
                    contentTextBox.Text = currentElement.OuterHtml;
                }
                else
                {
                    contentTextBox.Text = "";
                }
            }                            
        }
         
        void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentElement = action.GetElement();
            if (currentElement == null) {
                valueTextBox.Text = "";
                return;
            }
            FindMethods value = (FindMethods)Enum.Parse(typeof(FindMethods), methodComboBox.SelectedItem.ToString(), true);
            switch (value)
            {
                case FindMethods.ProximityText:
                    valueTextBox.Text = Title;
                    break;
                case FindMethods.Alt:
                case FindMethods.Class:
                case FindMethods.For:
                case FindMethods.Href:
                case FindMethods.Id:
                case FindMethods.Index:
                case FindMethods.Name:
                case FindMethods.Src:
                case FindMethods.Title:
                case FindMethods.Url:
                case FindMethods.Value:
                //case FindMethods.Style:
                    if (currentElement.Exists)
                    {
                        valueTextBox.Text = currentElement.GetAttributeValue(value.ToString());
                    }
                    break;
                case FindMethods.XPath:
                    if (currentElement.Exists)
                    {
                        valueTextBox.Text = XPathFinder.GetXPath(currentElement);
                    }
                    break;
                case FindMethods.CssSelector:
                    if (currentElement.Exists)
                    {
                        valueTextBox.Text = currentElement.GetAttributeValue("__selector");
                    }
                    break;
                case FindMethods.Text:
                    if (currentElement.Exists)
                    {
                        valueTextBox.Text = currentElement.Text;
                    }
                    break;
                default:
                    break;
            }
        }

      
        public FindAttributeCollection GuiToObject()
        {
            var collection = new FindAttributeCollection();
            FindAttribute attribute = GetFindAttribute();
            collection.Add(attribute);  
            return collection;
        }

        protected override void OnValidate()
        {            
            if (string.IsNullOrEmpty(valueTextBox.Text))
            {
                throw new ApplicationException("元素定位器值不能为空");
            }
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            action.FindMechanism = GuiToObject();   
            action.Highlight();
            Element element = action.GetElement();
            if (element.Exists)
            {
                contentTextBox.Text = element.OuterHtml;
            }
            else {
                contentTextBox.Text = "";
            }
        }
      
    }
}
