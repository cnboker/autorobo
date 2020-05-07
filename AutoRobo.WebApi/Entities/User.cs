using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebApi.Entities
{

    public class User
    {
        public string UserName { get; set; }
        /// <summary>
        /// 网站注册用户USERID
        /// </summary>
        public string UserId { get; set; }
    }
}
