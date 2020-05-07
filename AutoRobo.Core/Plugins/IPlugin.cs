using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.Plugins
{
    /// <summary>
    /// Generic plugin interface
    /// </summary>
    public interface IPlugin
    {      
        /// <summary>
        /// 安装插件容器
        /// </summary>
        IPluginHost Host { get; set; }
        /// <summary>
        /// 插件显示
        /// </summary>
        void Show();
        /// <summary>
        /// 工具栏插件按钮显示文本
        /// </summary>
        string StripItemText { get; }
        /// <summary>
        /// 工具栏插件按钮显示图片
        /// </summary>
        System.Drawing.Bitmap StripItemImage { get; }
        /// <summary>
        /// 快捷键
        /// </summary>
        Keys DataKey { get;  }
    }

}
