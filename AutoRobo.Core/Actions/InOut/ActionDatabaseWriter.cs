using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.Core.Models;
using System.Data;
using AutoRobo.UserControls;
using AutoRobo.Core.IO;
using AutoRobo.Core.UserControls;
using System.Data.SqlClient;
using System.Data.Common;
using AutoRobo.DB;
using AutoRobo.Core.Formatters;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 变量保存到数据库
    /// </summary>
    public class ActionDatabaseWriter : ActionBase, ICodeFormatterAcceptor
    {
        private SqlAdapter adapter = null;
        public string ConnectionString { get; set; }
        public ServerType ServerType { get; set; }
        /// <summary>
        /// 要保存的变量名称, 多个使用","隔开
        /// </summary>
        public string VariableName { get; set; }

        public override string ActionDisplayName
        {
            get { return "保存到数据库"; }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("Database.bmp");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucDatabaseWriter(editorAction);
        }

        public override void Perform()
        {
            if (adapter == null)
            {
                adapter = new SqlAdapter(ServerType, ConnectionString);
            }
            string[] vars = VariableName.Split(",".ToCharArray());
            if (vars.Length == 0) return;
            foreach (var varName in vars)
            {
                ActionTableVariable tableVar = VariableModel.Find<ActionTableVariable>(varName) as ActionTableVariable;
                if (tableVar != null)
                {
                    DataTable dt = tableVar.Data as DataTable;
                    dt.TableName = tableVar.Name;
                    try
                    {
                        adapter.Update(dt);
                        tableVar.Reset();
                    }
                    catch (Exception ex)
                    {
                        LogWrite(string.Format("写数据库{0}失败", tableVar.Name));
                        LogWrite(ex.Message);
                        if (AppSettings.Instance.Debug)
                        {
                            LogWrite(ex.StackTrace);
                        }
                        throw ex;
                    }
                }
            }
        }

        public override string GetDescription()
        {
            return "保存到数据库";
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            ConnectionString = GetAttribute(node, "ConnectionString");
            VariableName = GetAttribute(node, "VariableName");
            ServerType = (ServerType)GetIntAttribute(node, "ServerType");
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("ConnectionString", ConnectionString);
            writer.WriteAttributeString("VariableName", VariableName);
            writer.WriteAttributeString("ServerType", ((int)ServerType).ToString());
            base.WriterAddAttribute(writer);
        }

        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
