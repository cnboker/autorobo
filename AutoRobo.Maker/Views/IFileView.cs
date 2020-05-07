using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutoRobo.Makers.Views
{
    public interface IFileView : IView
    {       
        /// <summary>
        /// 加载文件内容
        /// </summary>
        /// <param name="fileName"></param>
        event EventHandler LoadFile;
    }
}
