using System;
using System.Xml;
using AutoRobo.Core.Actions.InOut;
using AutoRobo.Core.Formatters;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;


namespace AutoRobo.Core.Actions
{
    public class ActionFileReader : ActionBase, ICodeFormatterAcceptor
    {
        /// <summary>
        /// 保存的文件路径
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 是否导入起始行
        /// </summary>
        public bool HasHeaderRecord { get; set; }
        /// <summary>
        /// 要保存的变量名称, 多个使用","隔开
        /// </summary>
        public string VariableName { get; set; }
        public FileType FileType { get; set; }

        public override string ActionDisplayName
        {
            get { return "读文件数据"; }
        }
       
        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucFileReader(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("read.png");
        }

        public override void Perform()
        {
            string _fileName = VariableModel.Parse(FileName);
            AutoRobo.Core.IO.DataTableReader reader = new AutoRobo.Core.IO.DataTableReader(FileType, _fileName, HasHeaderRecord);
            var varObj = ActionModel.VariableActionModel.Find<ActionTableVariable>(VariableName);
            try
            {
                varObj.Data = reader.Read();
            }
            catch (Exception ex) {
                LogWrite(string.Format("读文件{0}失败", _fileName));
                LogWrite(ex.Message);
            }
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            FileName = GetAttribute(node, "FileName");
            HasHeaderRecord = GetBooleanAttribute(node, "HasHeaderRecord");
            VariableName = GetAttribute(node, "VariableName");
            FileType = (FileType)GetIntAttribute(node, "FileType");
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("VariableName", VariableName);  
            writer.WriteAttributeString("FileName", FileName);
            writer.WriteAttributeString("HasHeaderRecord", HasHeaderRecord ? "1" : "0");
            writer.WriteAttributeString("FileType", ((int)FileType).ToString());
            base.WriterAddAttribute(writer);
        }

        public override string GetDescription()
        {
            return "读文件数据";
        }



        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
