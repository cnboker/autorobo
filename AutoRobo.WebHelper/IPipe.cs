using System;
using System.Collections.Generic;

using System.Text;

namespace Util
{
    public interface IPipe
    {
        void Process(object context);
        IPipe NextPiple { get; set; }
    }
}
