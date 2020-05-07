using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;

namespace AutoRobo.PlulgIn.Debuger
{
    public delegate void DHTMLEvent(IHTMLEventObj e);
    /// <summary>
    /// To getting rid of the System.NotImplementedException mark the DHTMLEventHandler class as visible for Com: 
    /// </summary>
    [ComVisible(true)]
    public class DHTMLEventHandler
    {
        public DHTMLEvent Handler;
        HTMLDocument Document;

        public DHTMLEventHandler(HTMLDocument doc)
        {
            this.Document = doc;
        }
        [DispId(0)]
        public void Call()
        {
            Handler(Document.parentWindow.@event);
        }
    }
}
