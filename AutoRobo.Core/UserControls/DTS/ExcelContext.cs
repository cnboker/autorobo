using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls.Wizard;
using AutoRobo.Core.Actions.InOut.Configuration;

namespace AutoRobo.Core.UserControls.DTS
{
    public class ExcelContext : WizardContext
    {
        public string ExcelFile { get; set; }
        public bool IncludeHeader { get; set; }
        /// <summary>
        /// 如果excel文档有链接内容，则提取出来
        /// </summary>        
        public List<string> Links { get; set; }
    
        public override void InitailizeWizardPages()
        {
            Pages = new List<WizardPage>();
            Pages.Add(new ExcelWizardPage());
        }

        public override Actions.InOut.Configuration.IOSettings ToSettings()
        {
            return new ExcelSettings()
            {
                FileName = ExcelFile,
                IncludeHeader = IncludeHeader
            };
        }
    }
}
