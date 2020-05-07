using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Models
{
    /// <summary>
    /// 仅读数据
    /// </summary>
    public interface IActionReadRepository : IActionNameRepository
    {
        /// <summary>
        /// 读信息
        /// </summary>        
        ActionXmlSet Read();
    }
}
