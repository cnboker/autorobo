using System.Windows.Forms;
namespace AutoRobo.ClientLib.Ants
{
    partial class FireBrowser
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
        private void InitializeComponent()
        {
            this.browserToolbar = new AutoRobo.ClientLib.Ants.BrowserToolbar();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.stripStatusLMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.xyLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // browserToolbar
            // 
            this.browserToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.browserToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.browserToolbar.Host = null;
            this.browserToolbar.Location = new System.Drawing.Point(0, 0);
            this.browserToolbar.Name = "browserToolbar";
            this.browserToolbar.Size = new System.Drawing.Size(525, 32);
            this.browserToolbar.TabIndex = 0;
            this.browserToolbar.WebBrowser = null;
            // 
            // tsStatus
            // 
            this.tsStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsStatus.ImageTransparentColor = System.Drawing.Color.White;
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(377, 20);
            this.tsStatus.Spring = true;
            this.tsStatus.Text = "Ready...";
            this.tsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(117, 19);
            this.tsProgress.Step = 1;
            this.tsProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus,
            this.stripStatusLMessage,
            this.xyLabel,
            this.tsProgress});
            this.statusStripMain.Location = new System.Drawing.Point(0, 323);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStripMain.Size = new System.Drawing.Size(525, 25);
            this.statusStripMain.TabIndex = 20;
            // 
            // stripStatusLMessage
            // 
            this.stripStatusLMessage.Name = "stripStatusLMessage";
            this.stripStatusLMessage.Size = new System.Drawing.Size(0, 20);
            // 
            // xyLabel
            // 
            this.xyLabel.Name = "xyLabel";
            this.xyLabel.Size = new System.Drawing.Size(12, 20);
            this.xyLabel.Text = " ";
            // 
            // FireBrowser
            // 
            this.ClientSize = new System.Drawing.Size(525, 348);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.browserToolbar);
            this.Name = "FireBrowser";
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected BrowserToolbar browserToolbar;
        protected ToolStripStatusLabel tsStatus;
        protected ToolStripProgressBar tsProgress;
        protected StatusStrip statusStripMain;

        #endregion

        private ToolStripStatusLabel stripStatusLMessage;
        private ToolStripStatusLabel xyLabel;

    }
}
