using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.PageHooks
{
    /// <summary>
    /// 视图显示接口
    /// </summary>
    public interface IResult
    {
        IStripContainer Strip { get; set; }
        bool Continue { get; set; }
        object Model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document">render view 容器</param>
        void View();
    }
}
