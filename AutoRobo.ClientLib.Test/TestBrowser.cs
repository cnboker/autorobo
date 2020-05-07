using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.PlulgIn.Croper;

namespace AutoRobo.ClientLib.Test
{
    public partial class TestBrowser : Form
    {
        public TestBrowser()
        {
            InitializeComponent();
        }

        public MyBrowser Browser {
            get {
                return myBrowser1;
            }
        }

        public void Run() {
            Application.Run(this);
        }
        public string InnerHtml {
            get { return myBrowser1.DocumentSource; }
            set { myBrowser1.DocumentSource = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageCroper target = new ImageCroper(TestHelper.WB);
            target.Crop();
        }
    }
}
