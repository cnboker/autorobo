using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.Interface
{
    /// <summary>
    /// 界面显示区域管理
    /// </summary>
    public interface IViewArea
    {
        /// <summary>
        /// 区域增加控件
        /// </summary>
        /// <param name="child"></param>
        void AddControl(Control child);
        /// <summary>
        /// 显示区域
        /// </summary>
        void Show();
        /// <summary>
        /// 隐藏区域
        /// </summary>
        void Hide();
        /// <summary>
        /// 清除子控件
        /// </summary>
        void Clear();
        //获取区域宽度
        int Width { get; }
    }
}
