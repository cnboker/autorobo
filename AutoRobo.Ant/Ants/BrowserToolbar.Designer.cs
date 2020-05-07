using AutoRobo.Core.Controls;

namespace AutoRobo.ClientLib.Ants
{
    partial class BrowserToolbar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserToolbar));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.urlTextbox = new AutoRobo.Core.Controls.ToolStripSpringTextBox();
            this.homeBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.configButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.refreshButton,
            this.urlTextbox,
            this.homeBtn,
            this.toolStripSeparator1,
            this.configButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(854, 31);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // backButton
            // 
            this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(28, 28);
            this.backButton.Text = "后退";
            this.backButton.Click += new System.EventHandler(this.GoSearchToolStripButtonClickHandler);
            // 
            // forwardButton
            // 
            this.forwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
            this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(28, 28);
            this.forwardButton.Text = "前进";
            this.forwardButton.Click += new System.EventHandler(this.GoSearchToolStripButtonClickHandler);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(28, 28);
            this.refreshButton.Text = "刷新";
            this.refreshButton.Click += new System.EventHandler(this.GoSearchToolStripButtonClickHandler);
            // 
            // urlTextbox
            // 
            this.urlTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.urlTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.urlTextbox.Name = "urlTextbox";
            this.urlTextbox.Size = new System.Drawing.Size(665, 31);
            this.urlTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyClickHandler);
            this.urlTextbox.Click += new System.EventHandler(this.urlTextbox_Click);
            // 
            // homeBtn
            // 
            this.homeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.homeBtn.Image = ((System.Drawing.Image)(resources.GetObject("homeBtn.Image")));
            this.homeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(28, 28);
            this.homeBtn.Text = "主页";
            this.homeBtn.Click += new System.EventHandler(this.GoSearchToolStripButtonClickHandler);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // configButton
            // 
            this.configButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.configButton.Image = ((System.Drawing.Image)(resources.GetObject("configButton.Image")));
            this.configButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(28, 28);
            this.configButton.Text = "参数设定";
            this.configButton.Click += new System.EventHandler(this.GoSearchToolStripButtonClickHandler);
            // 
            // BrowserToolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.toolStrip1);
            this.Name = "BrowserToolbar";
            this.Size = new System.Drawing.Size(854, 80);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton backButton;
        private System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private ToolStripSpringTextBox urlTextbox;
        private System.Windows.Forms.ToolStripButton homeBtn;
        private System.Windows.Forms.ToolStripButton configButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
