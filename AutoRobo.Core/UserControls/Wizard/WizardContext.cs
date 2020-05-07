using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using AutoRobo.Core.Actions.InOut.Configuration;

namespace AutoRobo.Core.UserControls.Wizard
{
    /// <summary>
    /// Wizard上下文信息
    /// </summary>
    public abstract class WizardContext
    {        
        public DataTable Data { get; set; }
        /// <summary>
        /// 导航页面
        /// </summary>
        public List<WizardPage> Pages
        {
            get;
            set;
        }
        /// <summary>
        /// 创建导航页面
        /// </summary>
        /// <returns></returns>        
        public abstract void InitailizeWizardPages();
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public abstract IOSettings ToSettings();
    }
}
