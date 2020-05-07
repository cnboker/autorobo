namespace AutoRobo.ClientLib.Ants
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class TestProxyForm : Form
    {
        private Button buttonCancel;
        private IContainer components;
        private Label labelInfo;
        private Label labelProgress;

        public TestProxyForm()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(25, 25);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(183, 13);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "正在测试代理,请稍候 ..";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(84, 56);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 31);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(191, 25);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(59, 12);
            this.labelProgress.TabIndex = 2;
            this.labelProgress.Text = "(1 of 10)";
            // 
            // TestProxyForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 99);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestProxyForm";
            this.ShowIcon = false;
            this.Text = "测试代理服务器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void SetStatus(string status)
        {
            if (this.labelProgress.InvokeRequired)
            {
                SetStatusCallback method = new SetStatusCallback(this.SetStatus);
                base.Invoke(method, new object[] { status });
            }
            else
            {
                this.labelProgress.Text = status;
            }
        }

        private delegate void SetStatusCallback(string status);
    }
}

