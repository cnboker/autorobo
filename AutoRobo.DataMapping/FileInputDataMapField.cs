using System;
using System.IO;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class FileInputDataMapField : InputDataMapField
    {
        /// <summary>
        /// 上传文件控件文件路径，即_ImageUploader.cshtml 文件路径
        /// </summary>
        static public string UploadFilePagePath { get; set; }
       
        public string Name {
            get {
                return "tempFiles";
            }
        }

        public override void TableHtmlChildRender(StringBuilder sb)
        {
            StreamReader sr = new StreamReader(UploadFilePagePath);
            string template = "";
            try
            {
                template = sr.ReadToEnd();
                //sb.Append(Razor.Parse(template));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                sr.Close();
            }
            sb.AppendFormat("<input id=\"{0}\" type=\"file\" class=\"text {1}\" name=\"{0}\" value=\"{2}\"/>",
                this.Name, Required ? "required" : "", System.Web.HttpUtility.UrlDecode(this.Value));
        }

      
    }
}
