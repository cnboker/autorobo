using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.PageHooks
{
    /// <summary>
    /// 支持异步调用
    /// </summary>
    public interface IAsyncResult : IResult
    {
        FunCallback AsyncCall { get; set; }
    }
}
