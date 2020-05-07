using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;
using mshtml;

namespace AutoRobo.Core.ActionBuilder
{
    
    public interface IActionParser
    {  
        /// <summary>
        /// 将操作转化为可以持久化信息
        /// </summary>
        /// <param name="element">选择的元素， 可以为空，有的活动不需要选择元素，比如导航，延时等活动</param>
        /// <returns>是否满足条件解析成功</returns>
        bool Parse(ActionParameter parameter);
    }
}
