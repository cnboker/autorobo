using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.DataMapping;

namespace AutoRobo.Core
{

    public delegate void FunCallback(AsyncResult result);
    /// <summary>
    /// 活动运行器
    /// </summary>
    public class ActionRunnable
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ActionRunnable");
        private AsyncResult asyncResult = new AsyncResult() { IsComplete = false };
        private Thread runnerThread = null;
        private ActionsModelView modelView;
        private ModelSet Modelset { get; set; }

        public event EventHandler ModelViewChanged;
        public bool IsRunning = false;
  
        /// <summary>
        /// 断点到达
        /// </summary>
        public event EventHandler BreakpointReached
        {
            add
            {
                modelView.BreakpointReached += value;
            }
            remove
            {
                modelView.BreakpointReached -= value;
            }
        }
        /// <summary>
        /// 执行单步通知事件（执行前）
        /// </summary>
        public event EventHandler RunStepped
        {
            add
            {
                modelView.RunStepped += value;
            }
            remove
            {
                modelView.RunStepped -= value;
            }
        }
        /// <summary>
        /// 执行单步通知事件（执行后）更新状态
        /// </summary>
        public event EventHandler RunUpdateStatus
        {
            add
            {
                modelView.RunUpdateStatus += value;
            }
            remove
            {
                modelView.RunUpdateStatus -= value;
            }
        }

        public ActionsModelView ModelView
        {
            get
            {
                return modelView;
            }
            set
            {
                if (modelView != value)
                {
                    modelView = value;
                    if (ModelViewChanged != null)
                    {
                        ModelViewChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler RunComplete;

        public ActionRunnable(ModelSet Modelset)
        {
            this.Modelset = Modelset;
            ModelView = new ActionsModelView(Modelset.MainActionModel, Modelset.VariableActionModel.GetInputVariableAttribute());
        }
    
        /// <summary>
        /// 执行活动流程
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="ansync">是否异步执行</param>
        /// <returns></returns>
        public AsyncResult Run(FunCallback callback, bool ansync)
        {
            asyncResult.IsComplete = false;
            asyncResult.CallBack = callback;
            //回收内存
            //new Thread(() => {
            //    while (!asyncResult.IsComplete) {
            //        App.MemoryRelease();
            //        Thread.Sleep(1000);
            //    }
            //}).Start();
            //初始化结果集变量
            Modelset.VariableActionModel.Initialize();
            runnerThread = App.Invoke(() =>
            {
                IsRunning = true;               
                modelView.Run();
                IsRunning = false;
                if (RunComplete != null)
                {
                    RunComplete(this, EventArgs.Empty);
                }
                CallBack();
            }, ansync);
            return asyncResult;
        }
      

        private void CallBack() {
            asyncResult.IsComplete = true;
          
            if (asyncResult.CallBack != null)
            {
                asyncResult.CallBack(asyncResult);
            }
        }

        public void Continue()
        {
            modelView.Continue();
        }

        public void Stop()
        {
            try
            {
                asyncResult.Status = StatusIndicators.Exit;
                asyncResult.IsComplete = true;
                IsRunning = false;
                if (runnerThread != null)
                {
                    runnerThread.Abort();
                }
            }
            catch (Exception ){ }
        }

        /// <summary>
        /// 跳到指定Action执行
        /// </summary>
        /// <param name="actionID"></param>
        public void GoTo(string actionID)
        {
            modelView.GoTo(actionID);
        }

        public void RunStep(int index)
        {
            modelView.RunStep(index);
        }

    }

}
