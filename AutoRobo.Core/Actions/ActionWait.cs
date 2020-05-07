using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// �ȴ�ָ��ʱ�䣬ֱ��Ԫ�ش��ڻ��Ƴ���Ԫ������ֵƥ���ڴ�ֵ
    /// </summary>
    [Serializable]
    public class ActionWait : ActionElementBase
    {
        public static string ActionName { get { return "�ȴ�(������)"; } }

        public enum WaitTypes
        {
            Exists=0, Removed=1, AttributeValue=2
        }
        public WaitTypes WaitType { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public bool AttributeRegex { get; set; }
        public int WaitTimeout { get; set; }

        public ActionWait() { }

       

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucWait(editorAction);
        }

        public override Bitmap GetIcon()
        {
            // change image based on target
            return GetIconFromFile("Wait.bmp");
        }

     
        public override string ActionDisplayName
        {
            get { return "�ȴ�"; }
        }
        public override void Perform()
        {
            //����50������ӳ���Ҫ����ajax��ҳ�棬 �ȴ���ǰҳ������ʧЧ
            System.Threading.Thread.Sleep(300);
            Element element = GetElement();
            //ɾ���Ե�ǰҳ��nativeԪ�����ã����²��һ�ȡԪ��
            element.RefreshNativeElement();
            if (element != null)
            {
                if (WaitType == WaitTypes.Exists) element.WaitUntilExists(WaitTimeout);
                else if (WaitType == WaitTypes.Removed) element.WaitUntilRemoved(WaitTimeout);
                else
                {
                    if (AttributeRegex) element.WaitUntil(AttributeName, new Regex(AttributeValue), WaitTimeout);
                    else element.WaitUntil(AttributeName, AttributeValue, WaitTimeout);
                }
            }
        }

        public override bool Validate()
        {
            return true;
        }

     
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            AttributeName = GetAttribute(node, "AttributeName");
            AttributeValue = GetAttribute(node, "AttributeValue");
            AttributeRegex = GetBooleanAttribute(node, "AttributeRegex");
            WaitTimeout = GetIntAttribute(node, "WaitTimeout");            
        }     

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("AttributeName", AttributeName);
            writer.WriteAttributeString("AttributeValue", AttributeValue);
            writer.WriteAttributeString("AttributeRegex", AttributeRegex ? "1" : "0");
            writer.WriteAttributeString("WaitTimeout", WaitTimeout.ToString());
            base.WriterAddAttribute(writer);
        }
    }
}
