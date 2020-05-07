using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.Models;
using AutoRobo.WebApi;

namespace AutoRobo.ClientLib.PageHooks.Models
{
    public class CustomScriptWriteRepository : IActionWriteRepository
    {
        private string userid;
        private string scriptId;
        /// <summary>
        /// 执行用户手动调整的脚本
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="scriptId"></param>
        public CustomScriptWriteRepository(string userid, string scriptId) {
            this.userid = userid;
            this.scriptId = scriptId;
        }
        public void Write(string actionxml)
        {
            ServerApiInvoker.PostRecordMark(userid, scriptId, actionxml);
        }
    }
}
