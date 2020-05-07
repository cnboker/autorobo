using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutoRobo.PlulgIn.Debuger
{
    public partial class OutputPanel : UserControl, AutoRobo.Core.Logger.ILog
    {
        private StringBuilder sb = new StringBuilder();

        public OutputPanel()
        {
            InitializeComponent();            
            Console.SetOut(new StringWriter(sb));
        }

        public void Output(string text)
        {            
            sb.Append(text);
            sb.AppendLine();
            outputTextbox.Text = sb.ToString();
            outputTextbox.SelectionStart = sb.Length;
            outputTextbox.ScrollToCaret();
        }

        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            outputTextbox.Text = "";
            sb.Length = 0;
        }
    }
}
