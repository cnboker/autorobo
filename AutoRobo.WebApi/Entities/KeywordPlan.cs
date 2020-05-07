using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutoRobo.WebApi.Entities
{
    /// <summary>
    /// 关键词优化数据
    /// </summary>
    /// 
    [Serializable]
    public class KeywordPlan : WorkObjectBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        public string Keyword { get; set; }
      
        public string Url { get; set; }
        public string Engine { get; set; }     
        public string Scheduler { get; set; }
        /// <summary>
        /// 当前关键字位于搜索结果排名
        /// </summary>
        public string Rank { get; set; }
        public int Score { get; set; }
        /// <summary>
        /// 标题信息
        /// </summary>
        public string Title { get; set; }
    }
}
