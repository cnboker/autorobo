using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.WebApi.Entities;

namespace AutoRobo.ClientLib.PageHooks.Handler.Recognize
{
    public interface IRecognizer
    {
        bool Recognize(ScriptObject so);
        FakeHttpContext Context { get; set; }
    }
}
