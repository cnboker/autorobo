using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 数值变量
    /// </summary>
    public class ActionIntegerVariable : ActionVariable
    {
        private int Value { get; set; }

        public override string GetTypeName()
        {
            return "整数";
        }
    
 
        public override string GetDescription()
        {
            return "数值变量";
        }

        /// <summary>
        /// 变量减1
        /// </summary>
        /// <param name="variableName"></param>
        public void Decrement()
        {
            Value--;
        }

        /// <summary>
        /// 变量加1
        /// </summary>
        /// <param name="variableName"></param>
        public void Increment()
        {
            Value++;
        }

        public override void Reset()
        {
            Value = 0;
        }

   
        public override object Data
        {
            get { return Value; }
            set { Value = Convert.ToInt32(value); }
        }


        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
