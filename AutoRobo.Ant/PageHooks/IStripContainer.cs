using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.ClientLib.PageHooks
{
    /// <summary>
    /// 动态功能按钮管理器
    /// </summary>
    public interface IStripContainer
    {
        FakeHttpContext Context { get; }
        void Clean();
        void SiteIn(IFunStripItem item);
    
    }
}
