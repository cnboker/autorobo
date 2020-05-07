using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AutoRobo.WebApi.Entities;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.PageHooks.Handler.Recognize
{
    public class KeywordRecognizer : IRecognizer
    {
        public bool Recognize(ScriptObject so)
        {
            return Method<bool>.Watch("KeywordMatchHandler:", () =>
            {
                MyBrowser browser = Context.CoreBrowser as MyBrowser;
                string innerText = browser.GetActiveDocument().body.innerText;

                return (new Regex(so.Similarity)).IsMatch(innerText);
            });
        }

        public FakeHttpContext Context
        {
            get;
            set;
        }
    }
}
