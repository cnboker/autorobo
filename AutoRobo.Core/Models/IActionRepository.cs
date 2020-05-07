using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Models
{
    /// <summary>
    /// 活动读写接口
    /// </summary>
    public interface IActionRepository : IActionReadRepository, IActionWriteRepository
    {     
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        string[] GetActionModulers();
        /// <summary>
        /// 获取模块数据模型
        /// </summary>
        /// <param name="modulerId">模块id</param>
        /// <returns></returns>
        ActionXmlSet GetModulerModel(string modulerId);
        void New(string projectName);
    }
}
