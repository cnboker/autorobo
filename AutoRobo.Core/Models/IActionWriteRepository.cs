using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Models
{
    public interface IActionWriteRepository : IActionNameRepository
    {
        /// <summary>
        /// 写活动脚本内容
        /// </summary>
        /// <param name="activeScript"></param>
        void Write(string actionxml);
    }
}
