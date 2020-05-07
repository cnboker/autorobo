using System;
using System.Drawing;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 测试元素匹配
    /// </summary>
    [Serializable]
    public class ActionConditionByAttribute : ActionBlock
    {
    
        public enum AvailableTests { AreEqual, AreNotEqual, Greater, Less, GreaterOrEqual, LessOrEqual, IsTrue, IsFalse }
        public AvailableTests TestToPerform;
        public string TestingProperty;
        public string TestingValue;
        public string ExceptionMessage;

        public override Bitmap GetIcon()
        {
            return GetIconFromFile("TestElement.bmp");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucTestElement(editorAction);
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            try
            {
                SetFindMethod(parameter.Element);
            }
            catch {
                return false;
            }
            return true;
        }

        public override void Perform()
        {
            bool result = false;
            var element = GetElement();
            string propertyvalue = element.GetAttributeValue(TestingProperty);
            switch (TestToPerform)
            {
                case AvailableTests.AreEqual:
                    result = propertyvalue == TestingValue;
                    break;
                case AvailableTests.AreNotEqual:
                    result = propertyvalue != TestingValue;
                    break;
                case AvailableTests.Greater:
                    result = float.Parse(TestingValue) < float.Parse(propertyvalue);
                    break;
                case AvailableTests.GreaterOrEqual:
                    result = float.Parse(TestingValue) <= float.Parse(propertyvalue);
                    break;
                case AvailableTests.Less:
                    result = float.Parse(TestingValue) > float.Parse(propertyvalue);
                    break;
                case AvailableTests.LessOrEqual:
                    result = float.Parse(TestingValue) >= float.Parse(propertyvalue);
                    break;
                case AvailableTests.IsTrue:
                    result = bool.Parse(propertyvalue);
                    break;
                case AvailableTests.IsFalse:
                    result = !bool.Parse(propertyvalue);
                    break;
            }
            if (result) {
                base.Perform();
            }
        }

        public override string GetDescription()
        {
            return "执行测试";
        }

     
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            TestToPerform = (AvailableTests) Enum.Parse(typeof (AvailableTests), node.Attributes["TestToPerform"].Value);
            TestingProperty = node.Attributes["TestingProperty"].Value;
            TestingValue = node.Attributes["TestingValue"].Value;
            ExceptionMessage = node.Attributes["ExceptionMessage"].Value;            
        }

  

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("TestToPerform", TestToPerform.ToString());
            writer.WriteAttributeString("TestingProperty", TestingProperty);
            writer.WriteAttributeString("TestingValue", TestingValue);
            writer.WriteAttributeString("ExceptionMessage", ExceptionMessage);
            base.WriterAddAttribute(writer);
        }
        public override string ActionDisplayName
        {
            get { return "执行测试"; }
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
