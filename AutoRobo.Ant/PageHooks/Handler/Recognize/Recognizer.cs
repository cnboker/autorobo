using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageHooks.Models;

namespace AutoRobo.ClientLib.PageHooks.Handler.Recognize
{
    public class Recognizer
    {
        static public IRecognizer Create(FakeHttpContext context, PageRecognizeEnum pr)
        {
            IRecognizer recognizer = null;
            switch (pr)
            {
                case PageRecognizeEnum.CssStyle:
                    recognizer = new CssRecognizer();
                    break;
                case PageRecognizeEnum.HashCode:
                    recognizer = new HashCodeRecognizer();
                    break;
                case PageRecognizeEnum.Keyword:
                    recognizer = new KeywordRecognizer();
                    break;
                case PageRecognizeEnum.XPath:
                    recognizer = new XPathRecognizer();
                    break;
                default:
                    break;
            }
            recognizer.Context = context;
            return recognizer;


        }
    }
}
