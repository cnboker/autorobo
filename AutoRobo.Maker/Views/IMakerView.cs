using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Makers.Models;

namespace AutoRobo.Makers.Views
{
  
    /// <summary>
    /// Maker接口
    /// </summary>
    public interface IMakerView : IView
    {                 
        /// <summary>
        /// 开始录制
        /// </summary>
        event EventHandler Record;
        /// <summary>
        /// 停止录制
        /// </summary>
        event EventHandler StopRecord;
        /// <summary>
        /// 测试运行
        /// </summary>
        event EventHandler Run;
        /// <summary>
        /// 测试停止
        /// </summary>
        event EventHandler StopRun;
       
        /// <summary>
        /// 分步执行
        /// </summary>
        event EventHandler StepRun;
        /// <summary>
        /// 新建
        /// </summary>
        event EventHandler New;
        /// <summary>
        /// 打开
        /// </summary>
        event EventHandler Open;
        /// <summary>
        /// 保存
        /// </summary>
        event EventHandler Save;
        /// <summary>
        /// 另存为
        /// </summary>
        event EventHandler SaveAs;
        /// <summary>
        /// 退出
        /// </summary>
        event EventHandler Exit;
        /// <summary>
        /// 贴标
        /// </summary>
        event EventHandler TagLabelEvent;
        /// <summary>
        /// 设置断点
        /// </summary>
        event EventHandler BreakPoint;
        /// <summary>
        /// 增加活动
        /// </summary>
        event EventHandler AddAction;
    }

}
