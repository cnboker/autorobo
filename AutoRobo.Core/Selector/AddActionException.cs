using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core
{
    public class AddActionException : ApplicationException
    {
        public AddActionException(string message) : base(message) { }
    }
}
