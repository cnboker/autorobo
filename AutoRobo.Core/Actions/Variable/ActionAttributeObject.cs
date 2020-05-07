using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.DataMapping;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// NameValue活动变量
    /// </summary>
    public class ActionAttributeObject : ActionVariable
    {
        private IMapAttribute attributes = null;

        public override string GetTypeName()
        {
            return "自定义对象";
        }

        public ActionAttributeObject() {
            Direction = Models.VariableDirection.Input;
        }

        public ActionAttributeObject(IMapAttribute attr, VariableDirection direct)
        {
            this.attributes = attr;
            Name = attr.Name;
            Direction = direct;
        }

        public override object Data
        {
            get { return attributes; }
            set { attributes = (IMapAttribute)value; }
        }

        public override string GetDescription()
        {
            return "自定义数据对象";
        }
    }
}
