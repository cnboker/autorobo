using System;
using System.Xml;
using AutoRobo.Core.ns;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;
using Jint;


namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionJavascriptInterpreter : ActionBase
    {              
        private JintEngine engine = null;

        public JintEngine Engine
        {
            get
            {
                if (engine == null) {
                    InitEngine();
                }
                return this.engine;
            }
        }
        public override string ActionDisplayName
        {
            get { return "脚本"; }
        }


        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            return true;
        }

        private void InitEngine() {
            engine = JintCreator.Create();
            //.SetFunction("getMapValue", new Jint.Delegates.Func<string, string>(getMapValue));
            engine.SetParameter("ie", new JsBrowser(Window));
            engine.SetParameter("$", new Q(AppContext, Window));
        }

      
        //private string getMapValue(string name) {
        //    if (MapAttribute == null) return "";
        //    return MapAttribute.Get(name);
        //}

        public string Script { get; set; }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("JavaScript.png");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucJavascriptInterpreter(editorAction);
           
        }

        public override void Perform()
        {
            if (!string.IsNullOrEmpty(Script))
            {
                Engine.Run(Script);
            }
            
        }

        public override string GetDescription()
        {
            return "脚本解释器 ";
        }

      

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node); 
            XmlNode scriptNode = node.SelectSingleNode("ScriptSection");
            if (scriptNode != null)
            {
                Script = scriptNode.InnerText;
            }
        }

     

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteStartElement("ScriptSection");
            writer.WriteString(Script);
            writer.WriteEndElement();

        }
       
    }
}
