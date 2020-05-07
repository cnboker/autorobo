using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.ActionBuilder
{
    public class ActionTypingParameter : ActionParameter
    {
        private string keys = "";
        public string SbKeys{
            get{
                return keys;
            }
            set{
                keys = value.Replace("processkey", ""); 
            }
        }
             
    }
}
