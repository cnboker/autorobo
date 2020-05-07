using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageServices;

namespace AutoRobo.ClientLib.PageHooks
{
    /// <summary>
    /// Get Request
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        ///外部网站链接处理
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IResult HandleRequest(IRequest req);              
      }
}
