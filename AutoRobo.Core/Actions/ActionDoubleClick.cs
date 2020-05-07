using System;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using WatiN.Core;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionDoubleClick : ActionElementBase
    {      
        public ActionDoubleClick() {
        }
  
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("doubleclick.bmp");
        }

        public override string GetDescription()
        {
            return "Ë«»÷";
        }

        public override void Perform()
        {
            Element element = GetElement();
            if (element != null) element.DoubleClick();
        }

        public override bool Validate()
        {
            bool result;

            try
            {
                Element element = GetElement();
                result = element.Exists;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }

            return result;
        }

    

        public override string ActionDisplayName
        {
            get { return "Ë«»÷"; }
        }
    }
}
