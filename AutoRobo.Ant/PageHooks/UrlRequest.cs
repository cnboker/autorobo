using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageHooks;

namespace AutoRobo.ClientLib.PageHooks
{
    public class UrlRequest : IRequest
    {
        public UrlRequest() {
            
        }
        public string Url
        {
            get;
            set;
        }

        public FakeHttpContext HttpContext
        {
            get;
            set;
        }
 
    }
}
