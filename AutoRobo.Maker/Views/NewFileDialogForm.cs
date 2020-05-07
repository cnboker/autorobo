using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core;

namespace AutoRobo.Makers.Views
{
    public partial class NewFileDialogForm : Form
    {
        
        public string FileName {
            get {
                return fileName;
            }
        }
        private string fileName;
        private string dir;
        public NewFileDialogForm(string dir)
        {
            this.dir = dir;
            InitializeComponent();
            okBtn.Click +=okBtn_Click;
        }


        private void okBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileNameBox.Text))
            {
                MessageBox.Show("文件名不能为空");
                fileNameBox.Focus();
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            fileName = fileNameBox.Text;
            if (fileName.IndexOf(".bit") == -1)
            {
                fileName += ".bit";
            }
            string newFile = Path.Combine(dir, fileName);
            if (File.Exists(newFile))
            {
                MessageBox.Show(string.Format("{0}已经存在,请重新命名", fileName));
                fileNameBox.Focus();
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
        }
    }
}
