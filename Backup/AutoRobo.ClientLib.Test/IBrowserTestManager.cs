using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace AutoRobo.ClientLib.Test
{
    public interface IBrowserTestManager
    {
        Browser CreateBrowser(Uri uri);
        Browser GetBrowser(Uri uri);
        void CloseBrowser();
    }
}
