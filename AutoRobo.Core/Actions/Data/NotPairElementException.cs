using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    public class NotPairElementException : ApplicationException
    {
        public NotPairElementException(string message) : base(message) { }
    }
}
