using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.DataMapping;
using AutoRobo.Makers.Models;

namespace AutoRobo.Makers.Views
{
    public interface IActivityWorkshopView : IView
    {
        ActionList DataSource { get; set; }        
        /// <summary>
        /// 活动GRID数据绑定
        /// </summary>
        void DataBind();
        /// <summary>
        /// 选择行
        /// </summary>
        //int SelectItemIndex { get; set; }
        /// <summary>
        /// 活动项目选择项
        /// </summary>
        int ProjectSelectIndex { get; set; }
        /// <summary>
        /// 上移
        /// </summary>
        event EventHandler MoveToPrevious;
        /// <summary>
        /// 下移
        /// </summary>
        event EventHandler MoveToNext;
        /// <summary>
        /// 到顶
        /// </summary>
        event EventHandler MoveFirst;
        /// <summary>
        /// 到底
        /// </summary>
        event EventHandler MoveLast;
        /// <summary>
        /// 清除
        /// </summary>
        event EventHandler Clear;
        /// <summary>
        /// 重新排序
        /// </summary>
        event EventHandler Order;
        /// <summary>
        /// 删除选择行
        /// </summary>
        event EventHandler RowDelete;
        /// <summary>
        /// 编辑选择行
        /// </summary>
        event EventHandler ActionEdit;
        /// <summary>
        /// 编辑确定
        /// </summary>
        event EventHandler ActionEditOK;
        /// <summary>
        /// 活动类别改变
        /// </summary>
        event EventHandler ActionTabSelectedIndexChanged;
        /// <summary>
        /// 返回上一个活动视图
        /// </summary>
        event EventHandler GoBack;
        /// <summary>
        /// Model数据绑定
        /// </summary>
        event ToolStripItemClickedEventHandler ModelBind;
        /// <summary>
        /// 行选择
        /// </summary>
       // event EventHandler RowSelect;
        /// <summary>
        /// 设置断点
        /// </summary>     
        void SetBreakPoint();
        /// <summary>
        /// 清除断点
        /// </summary>
        void DeleteAllBreakpoints();
        /// <summary>
        /// 设置行状态
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="statusIndicators"></param>
        void UpdateCellIcon(int rowIndex, ActionBase action);
      
        /// <summary>
        /// grid选择行
        /// </summary>
        /// <param name="value"></param>
        void SelectRow(int value);
     

    }
}
