using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Formatters
{
    public interface ICodeFormatterAcceptor
    {
        void Accept(ICodeFormatterVisitor visitor);
    }
}
