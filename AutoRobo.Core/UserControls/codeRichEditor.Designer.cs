namespace AutoRobo.Core.UserControls
{
    partial class codeRichEditor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

    

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(codeRichEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripRunButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripStopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRunButton,
            this.toolStripStopButton,
            this.toolStripSeparator1,
            this.toolStripComboBox,
            this.helpButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(490, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripRunButton
            // 
            this.toolStripRunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRunButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRunButton.Image")));
            this.toolStripRunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRunButton.Name = "toolStripRunButton";
            this.toolStripRunButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripRunButton.Text = "执行脚本";
            this.toolStripRunButton.Click += new System.EventHandler(this.toolStripRunButton_Click);
            // 
            // toolStripStopButton
            // 
            this.toolStripStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStopButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStopButton.Image")));
            this.toolStripStopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStopButton.Name = "toolStripStopButton";
            this.toolStripStopButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripStopButton.Text = "停止";
            this.toolStripStopButton.Click += new System.EventHandler(this.toolStripStopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBox
            // 
            this.toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBox.Items.AddRange(new object[] {
            "jint运行时"});
            this.toolStripComboBox.Name = "toolStripComboBox";
            this.toolStripComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // helpButton
            // 
            this.helpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpButton.Image = ((System.Drawing.Image)(resources.GetObject("helpButton.Image")));
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.Text = "脚本帮助";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // editorPanel
            // 
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorPanel.Location = new System.Drawing.Point(0, 25);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Size = new System.Drawing.Size(490, 393);
            this.editorPanel.TabIndex = 0;
            // 
            // codeRichEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editorPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "codeRichEditor";
            this.Size = new System.Drawing.Size(490, 418);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripRunButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton helpButton;
        private System.Windows.Forms.ToolStripButton toolStripStopButton;

        private System.Windows.Forms.Panel editorPanel;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox;

    }
}
