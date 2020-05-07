using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.ActionBuilder;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    public class AddChildrenToList : ActionFetchText
    {
        public override string ActionDisplayName
        {
            get { return "增加子元素到数组"; }
        }       

        public override void Perform()
        {
            ElementCollection elements = GetWindow().Elements.Filter(GetConstraint());
            List<string> list = new List<string>();
            foreach (var el in elements)
            {
                string val = GetValue(el);
                if (!string.IsNullOrEmpty(val))
                {
                    list.Add(val);
                }
            }
            if (list.Count > 0)
            {
                VariableModel.Find<ActionArrayVariable>(ObjectName).AddItemsToList(list);
            }
        }


        public override string GetDescription()
        {
            return "增加子元素到数组";
        }
    }
}
