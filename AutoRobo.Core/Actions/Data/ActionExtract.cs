using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 提取数据活动
    /// </summary>
    public class ActionExtract : ActionFetchText
    {
        public override string ActionDisplayName
        {
            get { return "提取内容"; }
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            string cssSelector = Selector.CssSelector;
            IHTMLElement el = Selector.SelectorElement;
            if (string.IsNullOrEmpty(cssSelector)) {
                MessageBox.Show("通过\"选择器\"先选择内容");
                return false;
            }
            IsContextBinding = LoopRowSelector.Instance.Check(parameter.Element);
            if (IsContextBinding)
            {
                ContextElement = LoopRowSelector.Instance.ContextElement;
                string selector = AppContext.Browser.Selector.GetContextSelector(LoopRowSelector.Instance.SelectorElement, parameter.Element);
                SetFindMethod(parameter.Element, FindMethods.CssSelector, selector);
            }
            else
            {
                SetFindMethod(el, FindMethods.CssSelector, cssSelector);
            }
            return true;
        }

        public override void Perform()
        {
            List<string> list = GetMutiContent();
            if (list.Count == 0) return;
            ActionVariable var1 = VariableModel.Find(ObjectName);
            if (ExtractType == Data.ExtractType.None)
            {
                if (var1 is ActionStringVariable) {
                    var1.Data = list[0];
                }
                else if (var1 is ActionArrayVariable) {
                    ((ActionArrayVariable)var1).AddItemsToList(list);
                }
            }
            else if (ExtractType == Data.ExtractType.AddMutiContentToTableColumn )
            {
                ActionTableVariable var = VariableModel.Find<ActionTableVariable>(ObjectName);
                var.AddListToTableColumn(list);
            }
            else if (ExtractType == Data.ExtractType.AddMutiContentToTableRow)
            {
                ActionTableVariable var = VariableModel.Find<ActionTableVariable>(ObjectName);
                var.AddListToTableRow(list);
            }           
        }

        /// <summary>
        /// 获取多项
        /// </summary>
        /// <returns></returns>
        List<string> GetMutiContent()
        {
            ElementCollection elements = GetWindow().Elements.Filter(GetConstraint());
            List<string> list = new List<string>();
            foreach (var el in elements)
            {
                string val = GetValue(el);
                if (!(string.IsNullOrEmpty(val) && FliterEmptyString))
                {
                    list.Add(val);
                }
            }
            return list;
        }
    }
}
