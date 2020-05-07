using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;
using AutoRobo.Core;
using System.Windows.Forms;
using mshtml;
using AutoRobo.UserControls;
using WatiN.Core.Native;

namespace AutoRobo.Core
{
   
    public interface ICoreBrowser 
    {       
        /// <summary>
        /// 文档标题
        /// </summary>
        string DocumentTitle { get; }
        /// <summary>
        /// 是否允许弹出提示框
        /// </summary>
        bool AllowAlert { get; set; }
  
        string LocationUrl { get; set; }
        void Navigate(string urlString);
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        bool GoBack();
        bool GoForward();
        void GoHome();
        void Refresh();
        void Stop();
        /// <summary>
        /// 选择器， 由插件注入对象
        /// </summary>
        Selector Selector { get; }
        object IWebBrowser2 { get; }
        BrowserWindow WatinBrowser
        {
            get;
        }
        /// <summary>
        /// 打开新页面运行
        /// </summary>
        /// <param name="parentBrowser"></param>
        void OpenNewBrowser(string url);

        WatiN.Core.ElementCollection SelectMany(string _cssSelector);
        bool SelectAny(string selector);
    }
}
