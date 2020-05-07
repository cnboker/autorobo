using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.ns
{
    public partial class PromptDialog : Form
    {
        public string LableText
        {
            get
            {
                return labelText.Text;
            }
            set { labelText.Text = value; }
        }

        public string InputText
        {
            get
            {
                return richTextBox.Text;
            
            }
            set { richTextBox.Text = value; }
        }

        public PromptDialog()
        {
            InitializeComponent();
        }
    }
}
