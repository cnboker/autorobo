using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace AutoRobo.Core.Interface
{
    /// <summary>
    /// 元素容器遍历, 需要通过父容器查找子元素的活动需要实现此接口
    /// </summary>
    public interface IElementBinding
    {
        /// <summary>
        /// 遍历项元素， 当元素属性IsContextBinding设置为true的时候，元素查找器的容器设置为该元素
        /// 即：ContextElement.Find("li")形式
        /// </summary>
        IElementContainer ContextElement { get; set; }
        /// <summary>
        /// 当前元素是否为遍历元素子元素，且元素selector设置section selector(短selector)
        /// </summary>
        bool IsContextBinding { get; set; }
    }
}
