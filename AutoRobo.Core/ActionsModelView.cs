using System;
using System.Threading;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.DataMapping;
using AutoRobo.Core.Logger;

namespace AutoRobo.Core
{
    /// <summary>
    /// 子活动运行器
    /// </summary>
    public class ActionsModelView
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ActionsModelView");
        private bool BreakpointSleep = true;
        private bool SingleStep = false;
        private int SkipIndex = -1;
        private ActionList actions;
           /// <summary>
        /// 断点到达
        /// </summary>
        public event EventHandler BreakpointReached;
        /// <summary>
        /// 执行单步通知事件（执行前）
        /// </summary>
        public event EventHandler RunStepped;
        /// <summary>
        /// 执行单步通知事件（执行后）更新状态
        /// </summary>
        public event EventHandler RunUpdateStatus;

        public ActionList Model {
            get {
                return actions;
            }
        }

        public IMapAttribute MapAttribute
        {
            get;
            set;
        }

        public ActionsModelView(ActionList actionList, IMapAttribute mapAttribute)
        {       
            actions = actionList;
            MapAttribute = mapAttribute;
        }

        public void Run()
        {            
            var rowCounter = 1;
            for (int i = 0; i < actions.Count; i++)
            {
                ActionBase action = actions[i];
                if (RunStepped != null)
                {
                    RunStepped(this, new RunningActionEventArgs() { Action = action, RunningRowIndex = rowCounter });
                }
                StepIn(action, rowCounter);

                if (action.Status == StatusIndicators.StepBreak) break;

                if (action.Status == StatusIndicators.StepFailure && AppSettings.Instance.Debug)
                {
                    RunStep(i);
                }
                if (SkipIndex != -1)
                {
                    i = SkipIndex;
                    SkipIndex = -1;
                    rowCounter = SkipIndex + 1;
                    continue;
                }

                if (RunUpdateStatus != null)
                {
                    RunUpdateStatus(this, new RunningActionEventArgs() { Action = action, RunningRowIndex = rowCounter });
                }
                rowCounter++;
            }
        
        }

        public void Continue()
        {
            SingleStep = false;
            BreakpointSleep = false;
        }

        /// <summary>
        /// 跳到指定Action执行
        /// </summary>
        /// <param name="actionID"></param>
        public void GoTo(string actionID)
        {
            int index = actions.GetActiveIndex(actionID);
            SkipIndex = index;
        }

        public void RunStep(int index)
        {
            SingleStep = true;
            BreakpointSleep = false;
            //if (index >= actions.Count) return;
        }


        public void StepIn(ActionBase action, int RowIndex)
        {
            if (SingleStep)
            {
                BreakpointSleep = true;
                while (BreakpointSleep)
                {
                    //调试模式，当点击停止运行后防止“正在终止线程”异常出现
                    try
                    {
                        Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    catch (Exception) { }
                }
            }
            else if (action.Breakpoint == BreakpointIndicators.ActiveBreakpoint)
            {
                if (BreakpointReached != null)
                {
                    BreakpointReached(this, new RunningActionEventArgs() { RunningRowIndex = RowIndex, Action = action });
                }                
                BreakpointSleep = true;
                while (BreakpointSleep)
                {
                    Thread.Sleep(500);
                    Application.DoEvents();
                }                
            }
            try
            {
                action.Status = StatusIndicators.StepContinue;                
                if (action.Status == StatusIndicators.StepContinue)
                {
                    action.ErrorMessage = "";
                    action.Perform();
                }                
            }
            catch (WatiN.Core.Exceptions.ElementNotFoundException ex)
            {
                action.ErrorMessage = ex.Message;                
                //如果是调试模式，错误则停止运行
                if (AppSettings.Instance.Debug)
                {
                    ExceptionAlert alert = new ExceptionAlert(ex);
                    alert.Show();
                }
                action.Status = StatusIndicators.StepFailure;
            }//活动列表没有正确完成        
            catch (ThreadAbortException tae) {
                logger.Fatal(tae);
            }
            catch (Exception e1)
            {
                action.ErrorMessage = e1.Message;
                logger.Fatal(e1);                
                action.Status = StatusIndicators.StepFailure;
                if (AppSettings.Instance.Debug)
                {
                    ExceptionAlert alert = new ExceptionAlert(e1);
                    alert.Show();
                }
            }

        }
    }
}
