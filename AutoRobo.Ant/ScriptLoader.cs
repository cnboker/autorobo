using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.Properties;

namespace AutoRobo.ClientLib
{
    public class ScriptLoaderExtend
    {
        static private string AjaxMoniterInstallScript;
  
        public static string GetAjaxMoniterInstallScript()
        {
            if (!string.IsNullOrEmpty(AjaxMoniterInstallScript))
            {
                return AjaxMoniterInstallScript;
            }

            var result = new StringBuilder();

            result.Append("with(window) { if (typeof ajaxWatin == 'undefined') {");
            result.Append(Resources.ajaxMoniter);
            result.Append("};};");
            result.AppendLine();
            //result.Append(Resources.iframeMoniter);
            AjaxMoniterInstallScript = result.ToString();
            return AjaxMoniterInstallScript;
        }

      

    }
}
