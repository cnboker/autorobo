using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.ClientLib.PageHooks.Views
{
    public class EmptyResult : ViewResult
    {
        public EmptyResult() {
            this.Continue = true;
        }
           
    }
}
