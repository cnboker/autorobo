using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.Core.UserControls;
using AutoRobo.Core.Models;
using AutoRobo.Core.Formatters;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 变量活动
    /// </summary>
    public abstract class ActionVariable : ActionBase, ICodeFormatterAcceptor
    {
        
        /// <summary>
        /// 变量数据更新委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void VariableDataUpdate(object sender, VariableDataArgs args);
       
        /// <summary>
        /// 变量输出方向
        /// </summary>
        public VariableDirection Direction { get; set; }
        /// <summary>
        /// 变量活动数据
        /// </summary>
        public abstract object Data { get; set; }
          /// <summary>
        /// 变量数据修改事件通知
        /// </summary>
        public event VariableDataUpdate DataUpdate;
        /// <summary>
        /// 变量类型
        /// </summary>
        /// <summary>
        /// 变量名称
        /// </summary>
        public string Name { get; set; }

        public override string ElementName
        {
            get
            {
                return Name;
            }
        }

        public override string ActionDisplayName
        {
            get
            {
                string direction = "输入";
                if (Direction == Models.VariableDirection.Output)
                {
                    direction = "输出";
                }
                else if (Direction == Models.VariableDirection.InOut)
                {
                    direction = "输入输出";
                }
                return direction + GetTypeName() + "变量";
            }
        }

        public virtual string GetTypeName()
        {
            return "";
        }
     
        /// <summary>
        /// 清除变量内容
        /// </summary>
        public virtual void Reset() {
        
        }
        /// <summary>
        /// jint外部调用方法
        /// </summary>
        /// <param name="list"></param>
        public virtual void add(string[] list) { 
        }

        protected virtual void OnDataUpdate(object dataSource){
            if (DataUpdate != null)
            {
                VariableDataArgs args = new VariableDataArgs()
                {                    
                    DataSource = dataSource,
                    VariableName = Name
                };
                DataUpdate(this, args);
            }
        }

        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucVariable(editorAction);
        }
        
        public override void Perform()
        {
            throw new NotImplementedException();
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            Name = GetAttribute(node, "Name");
            Direction = (VariableDirection)GetIntAttribute(node, "Direction");
        }

       
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Direction", ((int)Direction).ToString());
            base.WriterAddAttribute(writer);
        }
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("variable.png");
        }

        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
