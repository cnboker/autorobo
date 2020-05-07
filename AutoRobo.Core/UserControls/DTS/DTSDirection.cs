using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.UserControls.DTS
{
    public enum DTSDirection
    {
        /// <summary>
        /// 导入数据， 从excel, xls,database导入到datatable
        /// </summary>
        Import,
        /// <summary>
        /// 导出数据, 从datatable导出到excel,xls,database
        /// </summary>
        Export
    }
}
