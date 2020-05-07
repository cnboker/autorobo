using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace AutoRobo.Core.ActionBuilder
{
    public class ActionAddListToListParamter : MultiStepActionParameter
    {
        /// <summary>
        /// 相似元素
        /// </summary>
        public IHTMLElement PairElement { get; set; }

        public override bool Eval()
        {
            return (PairElement != null);
        }

        public override void ReceiveClick(IHTMLElement element)
        {
            PairElement = element;
        }
    }
}
