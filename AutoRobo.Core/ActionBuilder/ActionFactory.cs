using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.ActionBuilder
{
    public class ActionFactory
    {
        static public ActionParameter CreateParameter(string ActionObjectString) {
            ActionParameter parameter = null;
            switch (ActionObjectString)
            {
                case "ActionClick":
                    parameter = new ActionClickParameter()
                    {                        
                        Filter = ActionFliter.Lower
                    };
                    break;
                case "ActionSelectList":
                    parameter = new SelectParameter()
                    {                        
                        ByValue = false
                    };
                    break;
                case "ActionNavigate":
                    parameter = new ActionNavigateParameter() {  Url=""};
                    break;              
                case "ActionAddListToList":
                case "ActionAddMutiContentToTableColumn":
                case "ActionAddMutiContentToTableRow":
                    parameter = new ActionAddListToListParamter();
                    break;
                default:
                    parameter = new ActionParameter();                          
                    break;
            }
            parameter.ActionName = ActionObjectString;
            return parameter;
        }

        static public ActionBase Create(string ActionObjectString)
        {
            ActionBase output = null;
            try
            {
                output = Activator.CreateInstance("AutoRobo.Core", "AutoRobo.Core.Actions." + ActionObjectString).Unwrap() as ActionBase;
            }
            catch
            {
                //兼容老版本数据
                switch (ActionObjectString)
                {
                    case "BrowserClick": output = new ActionClick(); break;   
                    case "Navigate": output = new ActionNavigate(); break;                    
                    case "ScriptPart": output = new ActionScriptPart(); break;
                    case "AlertHandler": output = new ActionAlertHandler(); break;                    
                    case "ActionDoubleClick": output = new ActionDoubleClick(); break;
                    case "ActionFireEvent": output = new ActionFireEvent(); break;
                    case "ActionKey": output = new ActionKey(); break;
                    case "Mouse": output = new ActionMouse(); break;
                    case "RadioButton": output = new ActionRadio(); break;
                    case "Checkbox": output = new ActionCheckbox(); break;
                    case "SelectList": output = new ActionSelectList(); break;
                    case "TypeText": output = new ActionTypeText(); break;
                    case "DirectionKey": output = new ActionDirectionKey(); break;
                    case "Sleep": output = new ActionSleep(); break;
                    case "Wait": output = new ActionWait(); break;
                    case "ValidateCode": output = new ActionValidateCode(); break;
                    case "FileUpload": output = new ActionFileDialog(); break;
                    case "ValidateImage": output = new ActionValidateImage(); break;
                    case "JavascriptInterpreter": output = new ActionJavascriptInterpreter(); break;
                    case "SubmitClick": output = new ActionSubmitClick(); break;
                    case "CloseWindow": output = new ActionCloseWindow(); break;
                    case "WindowBack": output = new ActionBack(); break;
                    case "WindowForward": output = new ActionForward(); break;                    
                    case "WindowOpen": output = new ActionOpenWindow(); break;
                    case "WindowRefresh": output = new ActionRefresh(); break;
                    case "SubElementFinder": output = new ActionSubElements(); break;                    
                    case "CallFunction": output = new ActionCall(); break;
                    case "ActionForeach": output = new ActionForeach(); break;                    
                    case "ActionBrowser": output = new ActionBrowser(); break;
                    case "ActionThread": output = new ActionThread(); break;
                    //case "AddChildrenToList": output = new AddChildrenToList(); break;
                }
            }
            //if (output == null)
            //{
            //    throw new ApplicationException(string.Format("{0}对应的活动未找到", ActionObjectString));
            //}
            
            return output;
        }
    }
}
