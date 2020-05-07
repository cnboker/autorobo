using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core
{
    /// <summary>
    /// 主程序需要实现该接口处理打开新窗口的情况
    /// </summary>
    public interface INewWindow
    {
        ///// <summary>
        ///// 将新窗口IWebBrowser2赋值与NewWindow2EventArgs
        ///// </summary>
        ///// <param name="obj">IWebBrowser2</param>
        //void AssignBrowserObject(ref object obj);
        /// <summary>
        /// 打开新链接
        /// </summary>
        ///<param name="url"></param>
        ///<param name="levelIndex">创建浏览器的索引，默认只包含主浏览器,主程序会管理浏览器组，levelIndex为浏览器索引，如果不存在则主程序创建</param>
        ///<returns>切换浏览器，不存在则创建它</returns>
        ICoreBrowser Switch(string browserName);
        ///// <summary>
        ///// 打开新页面,比如当前页面包含10个链接，在打开第一个链接的时候创建一个新的CoreBrowser,
        ///// 后续打开的链接都使用新创建的浏览器
        ///// </summary>
        ///// <param name="parent">当前链接浏览器</param>
        ///// <param name="url"></param>
        //void Goto(ICoreBrowser parent, string url);
    }
}
