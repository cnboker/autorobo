using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace AutoRobo.Core
{
    public interface ISelector
    {
        IHTMLElement SelectorElement { get; }
        /// <summary>
        /// 是否已经选择元素
        /// </summary>
        bool Highlighting { get; }
        /// <summary>
        /// 加亮选择元素
        /// </summary>
        /// <param name="element"></param>
        void Highlight(IHTMLElement element);      
        /// <summary>
        /// 恢复元素原来状态
        /// </summary>
        void Restore();
        
    }
}
