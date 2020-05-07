using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AutoRobo.Core.Actions;
using WatiN.Core;
using System.Data;
using System.Collections;
using System.Text;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Models;
using AutoRobo.Core.Formatters;
using System.Collections.ObjectModel;

namespace AutoRobo.Core
{
    /// <summary>
    /// action load
    /// </summary>    
    [Serializable]
    public class ActionList : ObservableCollection<ActionBase> 
    {                          
        private log4net.ILog logger = log4net.LogManager.GetLogger("ActionList");

        public object Parent { get; set; }

        public ActionList() { }

        public new void Add(ActionBase item) {
            base.Add(item);
            item.ParentTest = this;
        }
        /// <summary>
        /// Loads code from a file, must be in native format
        /// </summary>    
        /// <param name="parentNode">node to load from</param>
        public void LoadScript(IAppContext context, XmlNode parentNode)
        {
            XmlNodeList nodeList = parentNode.SelectNodes("Action");
            if (nodeList == null) return;
            foreach (XmlNode node in nodeList)
            {
                string actionType = node.Attributes["ActionType"].Value;
                ActionBase action = ActionFactory.Create(actionType);
                if (action == null) continue;
                action.AppContext = context;
                if (action == null)
                {
                    MessageBox.Show("Action " + node.Name + " could not be mapped-- application needs updating.",
                                    "Load Map Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                action.ParentTest = this;                
                action.LoadFromXml(node);
                Add(action);
            }
        }

   
        /// <summary>
        /// 对活动自动排序， 对输入框，下拉列表排序
        /// </summary>
        public void OrderById() {
            Hashtable hash = new Hashtable();
            for(int i = 0; i < this.Count; i++){
                ActionElementBase ab = this[i] as ActionElementBase;
                if (ab == null) continue;             
                if (ab is ActionClick) continue;
                if (hash.Contains(ab)) continue;
                int clickIndex = FindClickAction(ab);
                int target = i;
                if (clickIndex < i) {
                    target -= 1;
                }
                if (clickIndex != -1)
                {
                    hash.Add(ab, ab);
                    MoveTo(clickIndex, target);
                }
            }
        }

        private int FindClickAction(ActionElementBase ab)
        {
            int index = -1;
            for(int i=0;i <this.Count;i++) {
                ActionElementBase action = this[i] as ActionElementBase;
                if (action == null) continue;
                if ((action is ActionClick) && action.GetConstraint().ToString() == ab.GetConstraint().ToString())                  
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

    
      

        public void SaveXml(string name, XmlTextWriter writer)
        {
            writer.WriteStartElement(name);
            //writer.WriteAttributeString("Name", name);
            foreach (ActionBase action in this)
            {
                if (!action.Serialized) continue;
                action.SaveToXml(writer);
            }
            writer.WriteEndElement();
        }

     

        //public void DisableAllBreakpoints()
        //{
        //    int counter = 1;
        //    foreach (ActionBase item in this)
        //    {
        //        if (item.Breakpoint != BreakpointIndicators.NoBreakpoint)
        //        {
        //            item.Breakpoint = BreakpointIndicators.InactiveBreakpoint;
        //            if (GridAction != null)
        //            {
        //                GridAction.SetCellBreakpoint(counter, BreakpointIndicators.InactiveBreakpoint);
        //            }
        //        }
        //        counter++;
        //    }
        //}

        //public void ReEnableBreakpoints()
        //{
        //    int counter = 1;
        //    foreach (ActionBase item in this)
        //    {
        //        if (item.Breakpoint != BreakpointIndicators.NoBreakpoint)
        //        {
        //            item.Breakpoint = BreakpointIndicators.ActiveBreakpoint;
        //            if (GridAction != null)
        //            {
        //                GridAction.SetCellBreakpoint(counter, BreakpointIndicators.ActiveBreakpoint);
        //            }
        //        }
        //        counter++;
        //    }
        //}

   
        //移动,dID传入前处理
        /// <summary>
        /// 移动步骤
        /// </summary>
        /// <param name="sID">原位置</param>
        /// <param name="dID">目标位置</param>
        /// <returns></returns>
        public bool MoveTo(int sID, int dID)
        {
            if (sID >= this.Count || dID >= this.Count || sID < 0 || dID < 0 || sID == dID)
            { return false; }
            if (sID == dID) return false;
            try
            {
                var value = this[sID];
                this.RemoveAt(sID);
                this.Insert(dID, value);                
                return true;
            }
            catch (Exception)
            {
                return false; ;
            }
        }

        /// <summary>
        /// 获取同一个元素的活动项
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public List<ActionElementBase> GetTheElementActions(WatiN.Core.Constraints.Constraint constraint) {
            string cs = constraint.ToString();
            List<ActionElementBase> aes = new List<ActionElementBase>();
            foreach (var o in this) {
                ActionElementBase ae = o as ActionElementBase;
                if (ae == null) continue;
                if (ae.GetConstraint().ToString() == cs) {
                    aes.Add(ae);
                }
            }
            return aes;
        }

        public int GetActiveIndex(string actionID)
        {
            int index = 0;
            bool contain = false;
            foreach (var o in this)
            {
                if (o.ID.ToString() == actionID)
                {
                    contain = true;
                    break;
                }
                index++;
            }
            if (!contain)
            {
                index = -1;
            }
            return index;
        }
        /// <summary>
        /// 查找活动的位置
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public int GetActiveIndex(ActionBase action)
        {
            int index = 0;
            bool contain = false;
            foreach (var o in this)
            {
                //id相等， 同时在同一个页面
                if (o.ID == action.ID)
                {
                    contain = true;
                    break;
                }
                index++;
            }
            if (!contain) {
                index = -1;
            }
            return index;
        }

        public int GetElementIndex(ActionElementBase element)
        {
            int index = 0;
            
            foreach (var o in this)
            {
                ActionElementBase a = o as ActionElementBase;
                if (a != null)
                {
                    if (a.ID== element.ID)
                    {
                        break;
                    }
                }
                index++;               
            }
            return index == this.Count ? -1 : index;
        }

    
        public bool Contain(ActionBase action)
        {
            //if (action is ActionNavigate) return false;
            //if (action is ActionDirectionKey) return false;
            //if (action is ActionVariable) return false;
            //非页面元素活动不检查
            ActionElementBase el = action as ActionElementBase;
            if (el == null) return false;
            bool ret = false;
            object obj = new object();
            lock (obj)
            {
                foreach (var o in this)
                {
                    if (o.ID == action.ID)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }


        /// <summary>
        /// 检查是否有不存在的元素，如果有则修改状态为Faulted
        /// </summary>
        /// <param name="browser"></param>
        public void ValidateAction(csExWB.cEXWB browser)
        {
            foreach (ActionBase action in this)
            {
                string typeName = action.GetType().Name;
                if (typeName == "ActionClick" || typeName == "ActionSleep")
                {
                    action.Status = StatusIndicators.StepContinue;
                    continue;
                }
                ActionElementBase actionE = action as ActionElementBase;
                if (actionE != null)
                {
                    action.Status = StatusIndicators.StepContinue;
                    string id = actionE.ElementId;
                    if (!string.IsNullOrEmpty(id))
                    {
                        var e = browser.GetElementByID(false, id);
                        if (e == null)
                        {
                            action.Status = StatusIndicators.StepFailure;
                        }
                    }
                }
            }
        }

    }
}
