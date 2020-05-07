using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Models;
using AutoRobo.UserControls;
using AutoRobo.Core.UserControls;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 模块调用
    /// </summary>
    public class ModuleCall : ActionBase
    {
   
        public ModuleCall()
        {           
        }
        /// <summary>
        /// 调用活动名称
        /// </summary>
        public string FunName { get; set; }

        public override string ElementName
        {
            get
            {
                return FunName;
            }
        }

        public override string ActionDisplayName
        {
            get { return string.Format("{0}模块调用", ElementName); }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("module.png");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucActionCall(editorAction);
        }  

        public override void Perform()
        {
            IActionRepository repository = ServiceLocator.Instance.GetService<IActionRepository>();
            ActionXmlSet axs = repository.GetModulerModel(FunName);
            //ActionContext context = new ActionContext(axs);
            //context.Intialize(CoreBrowser);
            Window.AttachToIE();
            //context.Run(false);
        }

        public override string GetDescription()
        {
            return "模块调用";
        }



        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            FunName = GetAttribute(node, "FunName");
        }

       
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("FunName", FunName);
        }
    }
}
