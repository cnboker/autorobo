using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.UserControls.Wizard;
using AutoRobo.Core.Models;
using Util;
using AutoRobo.Core;
using System.IO;

namespace AutoRobo.Makers.Views
{
    public partial class StartView : Form
    {
   
        /// <summary>
        /// 脚本用途
        /// </summary>
        public DataMethod Method
        {
            get
            {
                if (collectRadioButton.Checked)
                {
                    return DataMethod.Collect;
                }
                else
                {
                    return DataMethod.Publish;
                }
            }
        }

        public string StartUrl {
            get {
                string url = startUrlBox.Text;
                if (url.IndexOf("http") == -1)
                {
                    url = "http://" + url;
                }
                return url;
            }
        }

        public string ProjectName {
            get {
                return projectNameBox.Text;
            }
        }

        public StartView()
        {
            InitializeComponent();
            startUrlBox.KeyDown += new KeyEventHandler(startUrlBox_KeyDown);
            projectNameBox.Focus();
        }

        public StartView(DataMethod method, string startUrl):this() {
            this.startUrlBox.Text = startUrl;
            collectRadioButton.Checked = (method == DataMethod.Collect);
            publishRadioButton.Checked = (method == DataMethod.Publish);
        }

        void startUrlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OK();
                e.Handled = true;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void OK() {
            if (string.IsNullOrEmpty(projectNameBox.Text)) {
                MessageBox.Show("项目名称不能为空");
                projectNameBox.Focus();
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            string newDir = Path.Combine(AppSettings.Instance.LibraryPath, projectNameBox.Text);
            if (Directory.Exists(newDir)) {
                MessageBox.Show(string.Format("{0}已经存在,请重新命名", projectNameBox.Text));
                projectNameBox.Focus();
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            bool result = HttpPing.Ping(StartUrl);
            if (result)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else {
                MessageBox.Show("无效的地址");
                startUrlBox.Focus();
                DialogResult = System.Windows.Forms.DialogResult.None;
            }            
        }
    }
}
