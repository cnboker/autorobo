using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.SEO
{
    /// <summary>
    /// 匹配结果
    /// </summary>
    public class NLResult
    {
        /// <summary>
        /// 生成带自然链的文本
        /// </summary>
        public string OutputText { get; set; }
        /// <summary>
        /// 匹配的关键字列表
        /// </summary>
        public List<string> MatchWords { get; set; }
    }
}
