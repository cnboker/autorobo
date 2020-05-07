using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace AutoRobo.Core
{
    public interface ISelectors
    {
        List<IHTMLElement> SelectorElements { get; }
        /// <summary>
        /// 是否已经选择元素
        /// </summary>
        bool Highlighting { get; }
        /// <summary>
        /// 加亮一组元素
        /// </summary>
        /// <param name="elements"></param>
        void Highlight(IHTMLElement[] elements);
        /// <summary>
        /// 恢复元素原来状态
        /// </summary>
        void Restore();       
    }
}
