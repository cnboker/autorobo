using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 验证码分组接口
    /// </summary>
    public interface IValidateGroup
    {
        string GroupName { get; set; }
    }
}
