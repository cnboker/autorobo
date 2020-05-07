using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Makers.Models;


namespace AutoRobo.Makers.Views
{
    public interface IView
    {
  
        /// <summary>
        /// 视图初始化
        /// </summary>
        event EventHandler Initialize;
        /// <summary>
        /// 加载视图
        /// </summary>
        event EventHandler Load;
      
    }
}
