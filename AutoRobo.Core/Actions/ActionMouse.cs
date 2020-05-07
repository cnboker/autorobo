using System;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionMouse : ActionElementBase
    {
     
        public enum MouseFunctions
        {
            Up, Down, Enter
        }

        public MouseFunctions MouseFunction { get; set; }

        public ActionMouse() {}

       

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucMouse(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("mouse-move.png");
        }

        public override string GetDescription()
        {
            return "ЪѓБъ " + MouseFunction;
        }

        public override void Perform()
        {
            Element element = GetElement();

            if (element != null)
            {
                if (MouseFunction == MouseFunctions.Up) element.MouseUp();
                else if (MouseFunction == MouseFunctions.Down) element.MouseDown();                    
                else element.MouseEnter();
            }
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

        //public override CodeLine ToCode(ICodeFormatter Formatter)
        //{
        //    var line = new CodeLine();
        //    line.Attributes = FindMechanism;
        //    var builder = new StringBuilder();
        //    line.Frames = FrameList;
        //    line.ModelPath = GetDocumentPath(Formatter);
        //    builder.Append(line.ModelPath);
        //    builder.Append(Formatter.MethodSeparator);
        //    builder.Append(ElementType);
        //    builder.Append("(" + FindMechanism.ToString() + ")");
        //    line.ModelLocalProperty = builder.ToString();
        //    builder.Append(Formatter.MethodSeparator);

        //    if (MouseFunction == MouseFunctions.Up) line.ModelFunction = "MouseUp()";
        //    else if (MouseFunction == MouseFunctions.Down) line.ModelFunction = "MouseDown()";
        //    else line.ModelFunction = "MouseEnter()";
        //    line.ModelFunction += Formatter.LineEnding;
        //    builder.Append(line.ModelFunction);
        //    line.FullLine = builder.ToString();
        //    return line;
        //}

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);            
            MouseFunction = (MouseFunctions)Enum.Parse(typeof(MouseFunctions), node.Attributes["MouseFunction"].Value);
        }

  
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("MouseFunction", MouseFunction.ToString());
            base.WriterAddAttribute(writer);
        }
        public override string ActionDisplayName
        {
            get { return "ЪѓБъ"; }
        }
    }
}
