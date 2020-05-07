using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.ClientLib.PageHooks.Models
{
    /// <summary>
    /// 页面识别类型(1:Hashcode识别页面, 2:样式表达式识别,3:XPath识别, 4:关键字)
    /// </summary>
    public enum PageRecognizeEnum
    {
        HashCode = 1,
        CssStyle,
        XPath,
        Keyword
    }
}
