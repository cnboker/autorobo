using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.Plugins
{
    public delegate void SendKeysEvent(Keys key);
    /// <summary>
    /// The host
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// 插件容器浏览器对象
        /// </summary>
        ICoreBrowser Browser { get; set; }
        /// <summary>
        /// 注册插件
        /// </summary>
        /// <param name="pluginName"></param>
        void Register(string pluginName);
        /// <summary>
        /// 显示插件
        /// </summary>
        /// <param name="ipi"></param>
        void Show(IPlugin ipi);
        /// <summary>
        /// 插件容器键盘事件
        /// </summary>
        event SendKeysEvent SendKeyHandler;
        /// <summary>
        /// 插件容器工具条
        /// </summary>
        ToolStrip Strip {get;}
    }
}
