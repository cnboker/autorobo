using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.ClientLib.PageHooks
{
    public interface IFunStripItem {
        object Model { get; set; }
        /// <summary>
        /// click 处理程序
        /// </summary>
        string ProcessHandler { get; set; }    
    }

    public class FunToolStripButton : ToolStripButton, IFunStripItem
    {
        public object Model
        {
            get;
            set;
        }

        public string ProcessHandler
        {
            get;
            set;
        }
    }

    public class FunCheckboxItem : ToolStripCheckBox, IFunStripItem
    {
        public object Model
        {
            get;
            set;
        }

        public string ProcessHandler
        {
            get;
            set;
        }
    }

}
