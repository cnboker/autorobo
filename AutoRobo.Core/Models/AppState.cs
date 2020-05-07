using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using System;

namespace AutoRobo.Core.Models
{
    /// <summary>
    /// 系统界面状态维护， 界面交互响应调用doXXX方法， 状态改变设置属性出发属性改变事件通知VIEW
    /// </summary>
    public class AppState : ObservableObject
    {
        private bool isRunning;
        private bool isRecord;
        private bool isBreakPoint;
        /// <summary>
        /// 是否执行到断点处
        /// </summary>
        private bool breakReached;
        private int selectItemIndex = -1;
        /// <summary>
        /// 数据是否就绪， 在打开脚本和新建脚本后，需要设置该值为true
        /// </summary>
        private bool dataPrepared = false;

        public ActionBase ActiveAction { get; set; }
        /// <summary>
        /// 增加活动事件
        /// </summary>
        public event EventHandler AddAction;

        public void ActionAdd(object sender, EventArgs args) {
            if (AddAction != null) {
                AddAction(sender, args);
            }
        }
        /// <summary>
        /// 是否设置断点
        /// </summary>
        public bool BreakReached
        {
            get
            {
                return breakReached;
            }
            set
            {
                if (value != breakReached)
                {
                    breakReached = value;
                    OnPropertyChanged("BreakReached");
                }
            }
        }
        /// <summary>
        /// 是否设置断点
        /// </summary>
        public bool IsBreakPoint
        {
            get
            {
                return isBreakPoint;
            }
            set
            {
                if (value != isBreakPoint)
                {
                    isBreakPoint = value;
                    OnPropertyChanged("IsBreakPoint");
                }
            }
        }

        /// <summary>
        /// 是否设置断点
        /// </summary>
        public bool DataPrepared
        {
            get
            {
                return dataPrepared;
            }
            set
            {
                if (value != dataPrepared)
                {
                    dataPrepared = value;
                    OnPropertyChanged("DataPrepared");
                }
            }
        }
        /// <summary>
        /// 是否在启动运行状态
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (value != isRunning)
                {
                    isRunning = value;
                    OnPropertyChanged("IsRunning");
                }
            }
        }
        /// <summary>
        /// 是否启动录制
        /// </summary>
        public bool IsRecord
        {
            get
            {
                return isRecord;
            }
            set
            {
                if (value != isRecord)
                {
                    isRecord = value;
                    OnPropertyChanged("IsRecord");
                }
            }
        }

        /// <summary>
        /// 活动选择项改变
        /// </summary>
        public int SelectItemIndex
        {
            get
            {
                return selectItemIndex;
            }
            set
            {
                if (value != selectItemIndex)
                {
                    selectItemIndex = value;
                    OnPropertyChanged("SelectItemIndex");
                }
            }
        }

        public void DoRecord(bool isRecord) {
            this.isRecord = isRecord;
        }

        public void DoRun(bool isRunning) {
            IsRecord = false;
            this.IsRunning = isRunning;
        }
    }
}
