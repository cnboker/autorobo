using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Logging;

namespace AutoRobo.ClientLib.Test
{
    public class IEBrowserTestManager : IBrowserTestManager
    {
        private IE ie;

        public Browser CreateBrowser(Uri uri)
        {
            return new IE(uri);
        }

        public Browser GetBrowser(Uri uri)
        {
            if (ie == null)
            {
                ie = (IE)CreateBrowser(uri);
            }

            return ie;
        }

        public void CloseBrowser()
        {
            if (ie == null) return;
            ie.Close();
            ie = null;
            if (IE.InternetExplorers().Count == 0) return;

            foreach (var explorer in IE.InternetExplorersNoWait())
            {
                Logger.LogDebug(explorer.Title + " (" + explorer.Url + ")");
                explorer.Close();
            }
            throw new Exception("Expected no open IE instances.");
        }
    }
}
