using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.ClientLib.PageHooks
{
    /// <summary>
    /// Post Request
    /// </summary>
    public interface IRequestProcessor
    {
        /// <summary>
        /// 跟踪器提交操作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="expando"></param>
        /// <returns></returns>
        IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando);
    }
}
