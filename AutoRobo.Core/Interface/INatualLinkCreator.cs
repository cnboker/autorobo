using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core
{
    public interface INatualLinkCreator
    {
        /// <summary>
        /// 在文本中增加自然链
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Create(string text);
    }
}
