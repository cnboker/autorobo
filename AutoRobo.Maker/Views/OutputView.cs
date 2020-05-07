using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.PlulgIn.Debuger;

namespace AutoRobo.Makers.Views
{
    public partial class OutputView : ViewBase
    {
        /// <summary>
        /// 日志输出控制台
        /// </summary>
        public AutoRobo.Core.Logger.ILog Logger { get; set; }

        public OutputView()
        {
            InitializeComponent();
            TabText = "输出";
            Logger = new OutputPanel() { Dock = DockStyle.Fill };
            Controls.Add((Control)Logger);
            CloseButtonVisible = false;
        }
       
    }
}
