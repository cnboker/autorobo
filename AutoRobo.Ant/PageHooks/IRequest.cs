using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.ClientLib.PageHooks
{
    public interface IRequest
    {
        string Url { get; set; }
    
        FakeHttpContext HttpContext { get; set; }
    }
}
