using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.UserControls;

namespace AutoRobo.Core.Actions
{
    public class ActionSubElements : ActionElementBase
    {
        public override string ActionDisplayName
        {
            get { return "添加子元素"; }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("TypeText.bmp");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSelector(editorAction);
        }     

        public override void Perform()
        {
           
        }

        public override string GetDescription()
        {
            return "添加子输入框元素，包含隐藏字段";
        }

    
    }
}
