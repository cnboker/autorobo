using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core;

namespace AutoRobo.Makers.Views
{
    public enum CollectEventType { 
        MouseClick,
        CollectionContent
    }
    public partial class CollectDialog : MyDialog
    {
        public CollectEventType CollectEventType { get; set; }

        public CollectDialog()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.ListToListAllowPrompt = !checkBox1.Checked;
            Close();
        }
    }
}
