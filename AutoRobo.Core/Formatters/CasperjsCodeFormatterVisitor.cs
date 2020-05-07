using Antlr4.StringTemplate;
using AutoRobo.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Formatters
{
    public class CasperjsCodeFormatterVisitor : ICodeFormatterVisitor
    {
        private StringBuilder code = new StringBuilder();

        public string CodeOutput
        {
            get {
                return code.ToString();
            }
        }

        public CasperjsCodeFormatterVisitor() { 
            
        }

        public void Start() {
            code.Append("var casper = require('casper').create();");
            code.AppendLine();
            code.Append("casper.start('about:blank');");
            code.AppendLine();
        }

        public void Visit(Actions.ActionTypeText action)
        {
            code.Append(GetElementReference(action));
            code.AppendLine();
            Template st = new Template(TPLoader.Instance.ReadTemplate(action.GetType().Name));
            st.Add("ID", action.ID);
            st.Add("Value", action.TextToType);
            code.Append(st.Render());
            code.AppendLine();
        }

        private string GetElementReference(ActionElementBase action) {
            StringBuilder sb = new StringBuilder();
            bool includeFrame = false;
            sb.AppendFormat("var {0};", action.ID);
            sb.AppendLine();
            //这里只能使用thenEvaluate,不知道为什么
            sb.Append("casper.thenEvaluate(function(){");
            sb.AppendLine();
            if(action.FrameList.Count > 0){
                sb.AppendFormat("   var frame = $('{0}').contents()", action.FrameList[0].FindValue);
                code.AppendLine();
                includeFrame = true;
            }

            for(int i=1; i<action.FrameList.Count;i++){
                sb.AppendFormat("   frame = frame.find('{0}');", action.FrameList[i]);
                code.AppendLine();
            }
            if (includeFrame)
            {
                sb.AppendFormat("   {0} = frame.find('{1}');", action.ID, action.FindMechanism[0].FindValue);
            }
            else {
                sb.AppendFormat("   {0} = $('{1}');", action.ID, action.FindMechanism[0].FindValue);
            }
            code.AppendLine();
            sb.Append("});");
            code.AppendLine();
            return sb.ToString();
        }

        public void Visit(Actions.ActionWait action)
        {
            //code.AppendFormat("casper.wait({0});", action.
        }

        public void Visit(Actions.ModuleCall action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionSleep action)
        {
            Template st = new Template(TPLoader.Instance.ReadTemplate(action.GetType().Name));
            st.Add("Time", action.SleepTime);            
            code.Append(st.Render());
            code.AppendLine();
        }

        public void Visit(Actions.ActionSelectList action)
        {
            code.Append(GetElementReference(action));
            code.AppendLine();
        }

        public void Visit(Actions.ActionRefresh action)
        {
            code.Append("casper.reload();");
            code.AppendLine();
        }

        public void Visit(Actions.ActionRadio action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionBack action)
        {
            code.Append("casper.back();");
            code.AppendLine();
        }

        public void Visit(Actions.ActionBrowser action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionCall action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionCheckbox action)
        {
            
        }

        public void Visit(Actions.ActionClick action)
        {
            code.Append(GetElementReference(action));
            code.AppendFormat("$('{0}').click();", action.ID);
            code.AppendLine();
        }

        public void Visit(Actions.ActionCloseWindow action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionDirectionKey action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionDoubleClick action)
        {
            code.Append(GetElementReference(action));
            Template st = new Template(TPLoader.Instance.ReadTemplate(action.GetType().Name));
            st.Add("ID", action.ID);
            code.Append(st.Render());
            code.AppendLine();
        }

        public void Visit(Actions.ActionFileDialog action)
        {
            throw new NotImplementedException();
        }

        public void Visit(Actions.ActionFireEvent action)
        {
           
        }

        public void Visit(Actions.ActionForward action)
        {
            code.Append("casper.forword();");
            code.AppendLine();
        }

        public void Visit(Actions.ActionKey action)
        {
            code.Append(GetElementReference(action));
            Template st = new Template(TPLoader.Instance.ReadTemplate(action.GetType().Name));
            st.Add("ID", action.ID);    
            code.Append(st.Render());
            code.AppendLine();
        }

        public void Visit(Actions.ActionMouse action)
        {
            code.Append(GetElementReference(action));
            Template st = new Template(TPLoader.Instance.ReadTemplate(action.GetType().Name));
            st.Add("ID", action.ID);
            st.Add("MouseFunctions", action.MouseFunction.ToString());
            code.Append(st.Render());
            code.AppendLine();
        }

        public void Visit(Actions.ActionNavigate action)
        {
            code.AppendFormat("casper.thenOpen('{0}');", action.URL);
            code.AppendLine();
        }

        public void Visit(Actions.ActionOpenWindow action)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 增加数据数据到表
        /// </summary>
        /// <param name="action"></param>
        public void Visit(ActionAddListToTable action)
        {
            if (action is ActionAddListToTableColumn)
            {

            }
            else if (action is ActionAddListToTableRow)
            {

            }
        }

        public void Visit(ActionDatabaseWriter action)
        {
           
        }

        public void Visit(ActionFileWriter action)
        {
           // throw new NotImplementedException();
        }

        public void Visit(ActionFileReader action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="action"></param>
        public void Visit(ActionFetchText action)
        {
            code.Append(GetElementReference(action));
            code.AppendLine();
            //throw new NotImplementedException();
            //if (action is ActionAddMutiContentToTableColumn) {

            //}
            //else if (action is ActionAddMutiContentToTableRow) {

            //}
            if (action is ActionAddItemToList) {
                ActionAddItemToList self = action as ActionAddItemToList;
                Template st = new Template(TPLoader.Instance.ReadTemplate("ActionAddItemToList"));
                st.Add("ID", action.ID);
                st.Add("Property", action.PropertyName);
                st.Add("VariableName", action.ObjectName);
                code.Append(st.Render());
                code.AppendLine();
            }
            //else if (action is ActionAddListToList) {

            //}
            else if (action is ActionAddItemToStringVariable) {
                ActionAddItemToList self = action as ActionAddItemToList;
                Template st = new Template(TPLoader.Instance.ReadTemplate("ActionAddItemToStringVariable"));
                st.Add("ID", action.ID);             
                st.Add("VariableName", action.ObjectName);
                code.Append(st.Render());
                code.AppendLine();                
            }
        }


        public void Visit(ActionVariable action)
        {
            if (action is ActionIntegerVariable) {
                code.AppendFormat("var {0}={1};", action.Name, action.Data.ToString());
            }
            else if (action is ActionStringVariable) {
                code.AppendFormat("var {0}='{1}';", action.Name, action.Data.ToString());
            }
            else if (action is ActionArrayVariable)
            {
                code.AppendFormat("var {0}=[];", action.Name);
            }
            else if (action is ActionTableVariable)
            {
                code.AppendFormat("var {0}={};", action.Name);
            }
            else
            {
                throw new NotImplementedException();
            }
            code.AppendLine();
        }


        public void Assert(string expect, string variableName)
        {
            Template st = new Template(TPLoader.Instance.ReadTemplate("Assert"));
            st.Add("Expect", expect);
            st.Add("VarName", variableName);
            code.Append(st.Render());
            code.AppendLine();
        }
    }
}
