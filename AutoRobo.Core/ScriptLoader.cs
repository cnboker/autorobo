using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Properties;

namespace AutoRobo.Core
{
    public class ScriptLoader
    {
        static private string JqueryInstallScript;
        static private string SelectChangeEventTrigger;
        //static private string JquerySelector;

        /// <summary>
        /// 下拉列表选择改变事件触发
        /// </summary>
        /// <returns></returns>
        public static string GetSelectChangeEventTrigger() {
            if (!string.IsNullOrEmpty(SelectChangeEventTrigger))
            {
                return SelectChangeEventTrigger;
            }
            var result = new StringBuilder();           
            result.Append("with(window) { if (typeof selectonchange == 'undefined') {");
            result.AppendLine();
            result.Append("selectonchange = '';");
            result.AppendLine();
            result.Append(Resources.SelectChange);
            result.Append("}}");
            SelectChangeEventTrigger = result.ToString();
            return SelectChangeEventTrigger;
        }

        public static string GetJqueryInstallScript()
        {
            if (!string.IsNullOrEmpty(JqueryInstallScript)) {
                return JqueryInstallScript;
            }
            var result = new StringBuilder();

            result.Append("with(window) { if (typeof $jq == 'undefined') {");
            result.Append(Resources.Jquery);
            result.Append(Resources.jqDeserialize);
            result.Append("$jq = jQuery.noConflict();");

            result.Append("$jq.fn.any = function () {");
            result.Append(" return this.length !== 0;");
            result.Append("}");

            //result.Append("$jq.fn.isChildOf  = function (b) {");
            //result.Append(" return (this.parents(b).length > 0); ");
            //result.Append("}");

            //追加form提交触发事件
           // result.Append("$jq(document).ready(function(){");
            //result.AppendLine();
            //result.Append(" $jq('form').submit(function (el) { window.external.Call('FormSubmitHandler', {data:$jq(this).find(':input:not(:hidden)').serialize(), containPassword:$jq(this).find('input:password').length>0} ); });");
           // result.Append("});");
            //append formDeserialize
            //result.AppendLine();
            //result.Append(Resources.formDeserialize);
            result.AppendLine();
            result.Append("};};");

            JqueryInstallScript = result.ToString();
            return JqueryInstallScript;
        }

        /// <summary>
        /// 获取点击元素select path
        /// <param name="parentCssSelector">父selector</param>
        /// </summary>
        /// <returns></returns>
        public static string GetJquerySelector(string parentCssSelector)
        {          
            var result = new StringBuilder();
            result.Append(GetJqueryInstallScript());
            result.AppendLine();
            result.Append("var __selector='" + parentCssSelector + "';");
            result.AppendLine();
            result.Append("with(window) { if (typeof getJquerySelectorIsRegister == 'undefined') {");
            result.AppendLine();
            result.Append(Resources.JquerySelector);
            result.AppendLine();
            result.Append("getJquerySelectorIsRegister = true;");
            result.Append("}};");
            result.AppendLine();
            return result.ToString();            
        }


    }
}
