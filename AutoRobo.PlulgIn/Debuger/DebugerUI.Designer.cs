namespace AutoRobo.PlulgIn.Debuger
{
    partial class DebugerUI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.CustomTabControl();
            this.htmlPage = new System.Windows.Forms.TabPage();
            this.scirptPage = new System.Windows.Forms.TabPage();
            this.outputPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.htmlPage);
            this.tabControl.Controls.Add(this.scirptPage);
            this.tabControl.Controls.Add(this.outputPage);
            this.tabControl.DisplayStyle = System.Windows.Forms.TabStyle.VisualStudio;
            // 
            // 
            // 
            this.tabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.tabControl.DisplayStyleProvider.FocusTrack = false;
            this.tabControl.DisplayStyleProvider.HotTrack = true;
            this.tabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tabControl.DisplayStyleProvider.Opacity = 1F;
            this.tabControl.DisplayStyleProvider.Overlap = 7;
            this.tabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(14, 1);
            this.tabControl.DisplayStyleProvider.ShowTabCloser = false;
            this.tabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(683, 389);
            this.tabControl.TabIndex = 0;
            // 
            // htmlPage
            // 
            this.htmlPage.Location = new System.Drawing.Point(4, 4);
            this.htmlPage.Name = "htmlPage";
            this.htmlPage.Padding = new System.Windows.Forms.Padding(3);
            this.htmlPage.Size = new System.Drawing.Size(675, 364);
            this.htmlPage.TabIndex = 0;
            this.htmlPage.Text = "HTML";
            this.htmlPage.UseVisualStyleBackColor = true;
            // 
            // scirptPage
            // 
            this.scirptPage.Location = new System.Drawing.Point(4, 4);
            this.scirptPage.Name = "scirptPage";
            this.scirptPage.Padding = new System.Windows.Forms.Padding(3);
            this.scirptPage.Size = new System.Drawing.Size(675, 364);
            this.scirptPage.TabIndex = 1;
            this.scirptPage.Text = "脚本";
            this.scirptPage.UseVisualStyleBackColor = true;
            // 
            // outputPage
            // 
            this.outputPage.Location = new System.Drawing.Point(4, 4);
            this.outputPage.Name = "outputPage";
            this.outputPage.Size = new System.Drawing.Size(675, 364);
            this.outputPage.TabIndex = 2;
            this.outputPage.Text = "输出";
            this.outputPage.UseVisualStyleBackColor = true;
            // 
            // DebugerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.Name = "DebugerUI";
            this.Size = new System.Drawing.Size(683, 389);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomTabControl tabControl;
        private System.Windows.Forms.TabPage htmlPage;
        private System.Windows.Forms.TabPage scirptPage;
        private System.Windows.Forms.TabPage outputPage;
    }
}
