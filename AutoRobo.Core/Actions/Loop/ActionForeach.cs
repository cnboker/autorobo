using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using AutoRobo.Core.Interface;
using AutoRobo.Core.UserControls;
using mshtml;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 元素遍历
    /// </summary>    
    [Serializable]
    public class ActionForeach : ActionBlock
    {  
        public event EventHandler ForeachComplete = null;
       
        /// <summary>
        /// 起始行（正向数，从0开始）
        /// </summary>
        public int FirstNumber { get; set; }
        /// <summary>
        /// 结束行(倒着数，从0开始）
        /// </summary>
        public int LastNumber { get; set; }
        /// <summary>
        /// 步长
        /// </summary>
        public int StepNumber { get; set; }
    

        public override string ActionDisplayName
        {
            get { return "遍历"; }
        }
       
        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucLoopByElements(editorAction);
        }
        public override string GetDescription()
        {
            return "遍历执行活动列表";
        }
        /// <summary>
        /// 遍历当前行
        /// </summary>
        private IElementContainer CurrentRow { get; set; }

        public ActionForeach() {
            StepNumber = 1;
        }
            
        /// <summary>
        /// 进入活动流程操作
        /// 通过条件定位元素，界面通过增加边框定位元素
        /// </summary>
        public override void Enter()
        {                    
            LoopRowSelector.Instance.Enter(this);   
        }

        /// <summary>
        /// 退出活动流程还原
        /// </summary>
        public override void Exit()
        {
            LoopRowSelector.Instance.Exit();
        }

        public override void Perform()
        {           
            ElementCollection elements = GetElements();
            if (elements == null) return;
            for (int i = FirstNumber; i < elements.Count - LastNumber;) {
                var e = elements[i];
                
                CurrentRow = e as IElementContainer;
                base.Perform();
                i = i + StepNumber;                
            }
            if (ForeachComplete != null)
            {
                ForeachComplete(this, EventArgs.Empty);
            }
        }

        public override void PrePerform(ActionBase action)
        {
            if (action is IElementBinding)
            {                
                ((IElementBinding)action).ContextElement = CurrentRow;
            }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("foreach.png");
        }


        public override void LoadFromXml(XmlNode node)
        {                        
            FirstNumber = Convert.ToInt32(GetAttribute(node, "FirstNumber"));
            LastNumber = Convert.ToInt32(GetAttribute(node, "LastNumber"));
            StepNumber = Convert.ToInt32(GetAttribute(node, "StepNumber"));            
            base.LoadFromXml(node);
        }

     
        public override void WriterAddAttribute(XmlWriter writer)
        {           
            writer.WriteAttributeString("FirstNumber", FirstNumber.ToString());
            writer.WriteAttributeString("LastNumber", LastNumber.ToString());
            writer.WriteAttributeString("StepNumber", StepNumber.ToString());            
            base.WriterAddAttribute(writer);
        }

        private List<IHTMLElement> ToHTMLElement(ElementCollection elements)
        {
            if (elements == null) return null;
            List<IHTMLElement> list = new List<IHTMLElement>();
            foreach (var element in elements)
            {
                var nativeElement = element.NativeElement as IEElement;
                list.Add((IHTMLElement)nativeElement.AsHtmlElement);
            }
            return list;
        }
    }
}
