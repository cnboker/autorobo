using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Actions.Data
{
    /// <summary>
    /// 提取内容操作类型
    /// </summary>
    public enum ExtractType
    {
        None,
        /// <summary>
        /// 增加多项到表列
        /// </summary>
        AddMutiContentToTableColumn,
        /// <summary>
        /// 增加多项到表行
        /// </summary>
        AddMutiContentToTableRow
    }
}
