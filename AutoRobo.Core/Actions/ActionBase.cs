using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Models;
using AutoRobo.DataMapping;
using System.Threading;
using Util;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public abstract class ActionBase : IActionParser
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger("actionBase");
        /// <summary>
        /// 当前action是否在ActionList的里面， 如果没有改状态为New, 如果已经存在则为Edit,默认为Edit
        /// </summary>
        public ActionStatue RowStatue = ActionStatue.Edit;
        /// <summary>
        /// 说明性标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 说明性描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 活动在活动列表显示名称
        /// </summary>
        public abstract string ActionDisplayName { get;  }
        /// <summary>
        /// 需要依赖注入
        /// </summary>
        public IAppContext AppContext { get; set; }
        
        /// <summary>
        ///  对于值显示名称, 比如"a@abc.com"的显示名称是电子邮件, 通过该名称获取用户输入信息
        /// </summary>
        public string MapName { get; set; }
       
     
        public BrowserWindow Window
        {
            get
            {
                return AppContext.Browser.WatinBrowser;
            }

        }   
        /// <summary>
        /// 通过映射字段获取值，如果没有mapName,则通过ID获取检查MapAttribute是否包含用户设置的
        /// 缺省值
        /// </summary>
        /// <returns></returns>
        protected string Eval(string BindName)
        {
            var val = "";
            if (AppContext.CurrentWorker.ModelView.MapAttribute != null)
            {
                if (!string.IsNullOrEmpty(BindName))
                {
                    val = AppContext.CurrentWorker.ModelView.MapAttribute.Get(BindName);
                }
            }
            return val;
        }

        public BreakpointIndicators Breakpoint = BreakpointIndicators.NoBreakpoint;

        private bool checkDuplication = true;
        /// <summary>
        /// 是否检查该Action是否已增加到ActionList;
        /// </summary>
        public bool CheckDuplication
        {
            get
            {
                return checkDuplication;
            }
            set
            {
                checkDuplication = value;
            }
        }
     
        /// <summary>
        /// error message (if any) for the action
        /// </summary>
        public string ErrorMessage = "";

        /// <summary>
        /// virtual method to retrieve the icon for the action
        /// </summary>
        /// <returns>icon as a bitmap</returns>
        public abstract Bitmap GetIcon();

        private Bitmap bitmap = null;
        internal Bitmap GetIconFromFile(string Filename)
        {
            if (bitmap != null) return bitmap; 
            bitmap = new Bitmap(
              System.Reflection.Assembly.GetEntryAssembly().
                GetManifestResourceStream(string.Format("AutoRobo.Makers.Icons.{0}", Filename)));
            bitmap.MakeTransparent(Color.Fuchsia);
            //Filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Icons\\" + Filename);
            //if (!File.Exists(Filename)) Filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Icons\\broken.png");
            //if (!File.Exists(Filename)) throw new FileNotFoundException("File not found: " + Filename);
            //var bmpBrowser = new Bitmap(Filename);
            //bmpBrowser.MakeTransparent(Color.Fuchsia);
            return bitmap;
            //return ResizeImage(bmp, new Size(16,16));
        }
        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }

                return b;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// executes this action
        /// </summary>        
        public abstract void Perform();

        /// <summary>
        /// validates the action
        /// </summary>
        /// <returns>true on success</returns>
        public virtual bool Validate()
        {
            return true;
        }
    
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string ElementName { get; set; }
        /// <summary>
        /// 值（设计器设置的缺省值)
        /// </summary>
        public virtual string DefaultValue { get; set; }
        /// <summary>
        /// 获取页面元素实际值
        /// </summary>
        //public virtual string IHTMLElementValue { get; set; }
        /// <summary>
        /// retrieves the description text about this action
        /// </summary>
        /// <returns>description of the action</returns>
        public abstract string GetDescription();

      

        public string PerformReplacement(string Incoming)
        {
           
            return Incoming;

        //    string output = Incoming;
        //    foreach (DataColumn column in ReplacementRow.Table.Columns)
        //    {
        //        output = Regex.Replace(output, "\\[%" + column.ColumnName + "%\\]", ReplacementRow[column.ColumnName].ToString(),
        //                                     RegexOptions.IgnoreCase);
        //    }
        //    return output;
        }

        /// <summary>
        /// status of this action
        /// </summary>
        public StatusIndicators Status { get; set; }


        public string GetAttribute(XmlNode node, string name)
        {
            if (node.Attributes[name] != null)
            {
                return node.Attributes[name].Value;
            }
            return "";
        }

        public bool GetBooleanAttribute(XmlNode node, string name)
        {
            if (node.Attributes[name] != null)
            {
                return node.Attributes[name].Value == "1";
            }
            else {
                return false;
            }
        }
        public int GetIntAttribute(XmlNode node, string name)
        {
            if (node.Attributes[name] != null)
            {
                return Convert.ToInt32(node.Attributes[name].Value);
            }
            else
            {
                return 0;
            }
        }
        public virtual void LoadFromXml(XmlNode node)
        {
            if (node.Attributes["Title"] != null) Title = node.Attributes["Title"].Value;
            if (node.Attributes["Description"] != null) Description = node.Attributes["Description"].Value;

        }

        public void SaveToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Action");
            writer.WriteAttributeString("ActionType", this.GetType().Name);
            if (!string.IsNullOrEmpty(Title))
            {
                writer.WriteAttributeString("Title", Title);
            }
            if (!string.IsNullOrEmpty(Description))
            {
                writer.WriteAttributeString("Description", Description);
            }
            WriterAddAttribute(writer);
            writer.WriteEndElement();
        }

        public virtual void WriterAddAttribute(XmlWriter writer)
        {

        }

        public virtual ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return null;
        }

        /// <summary>
        /// 当前活动容器
        /// </summary>
        public ActionList ParentTest { get; set; }
        /// <summary>
        /// 运行时试图
        /// </summary>
        //public ActionsModelView ModelView { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual string ID {
            get {
                return (GetType().ToString() + "." + ElementName);
                
            }
        }

        public virtual bool Parse(ActionParameter parameter)
        {
            return true;
        }

        public VariableActionModel<ActionVariable> VariableModel {
            get {
                return AppContext.ActionModel.VariableActionModel;
            }
        }

        public ModelSet ActionModel {
            get {
                return AppContext.ActionModel;
            }
        }

        public void LogWrite(string message) {
            AppContext.Logger.Output(message);
        }


        private bool _serialized = true;
        /// <summary>
        /// 当前变量是否需要存储,比如系统参数字符串变量是不需要存储的
        /// </summary>
        public bool Serialized
        {
            get
            {
                return _serialized;
            }
            set
            {
                _serialized = value;
            }
        }

    
    }
}
