using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.IO;
using AutoRobo.Core.Models;

namespace AutoRobo.Core
{
    public interface IAppContext
    {
        /// <summary>
        /// 活动数据集
        /// </summary>
        ModelSet ActionModel { get; set; }
        /// <summary>
        /// 主浏览器
        /// </summary>
        ICoreBrowser Browser { get; set; }
        /// <summary>
        /// 脚本运行管理器.
        /// </summary>
        ActionRunnable CurrentWorker { get; set; }
        /// <summary>
        /// 活动日志
        /// </summary>
        AutoRobo.Core.Logger.ILog Logger { get; set; }
        /// <summary>
        /// 主程序接口
        /// </summary>
        INewWindow MainWindow { get; set; }
        /// <summary>
        /// IE
        /// </summary>
        //BrowserWindow Window { get; }
    }
}
