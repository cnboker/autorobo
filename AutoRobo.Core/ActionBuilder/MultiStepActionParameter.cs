using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace AutoRobo.Core.ActionBuilder
{
    /// <summary>
    /// 需要多步操作才能构造完成
    /// </summary>
    public abstract class MultiStepActionParameter : ActionParameter
    {     
        /// <summary>
        /// 步骤完成后回调过程
        /// </summary>
        public Action<string, ActionParameter> AddActionFun { get; set; }
        /// <summary>
        /// 评估参数是否构造完成
        /// </summary>
        /// <returns></returns>
        public abstract bool Eval();
       
        public void Invoke(){
            if(AddActionFun != null){
                AddActionFun(ActionName, this);
            }
        }

        /// <summary>
        /// 接收点击事件
        /// </summary>
        /// <param name="activeElement"></param>
        public abstract void ReceiveClick(IHTMLElement activeElement);
 
    }
}
