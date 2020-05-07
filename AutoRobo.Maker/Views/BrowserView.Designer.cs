namespace AutoRobo.Makers.Views
{
    partial class BrowserView
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
            this.webBrowser = new AutoRobo.MyBrowser();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.AllowAlert = false;
            this.webBrowser.Border3DEnabled = false;
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.DocumentSource = "<HTML><HEAD></HEAD>\r\n<BODY></BODY></HTML>";
            this.webBrowser.DocumentTitle = "";
            this.webBrowser.DownloadActiveX = true;
            this.webBrowser.DownloadFrames = true;
            this.webBrowser.DownloadImages = true;
            this.webBrowser.DownloadJava = true;
            this.webBrowser.DownloadScripts = true;
            this.webBrowser.DownloadSounds = true;
            this.webBrowser.DownloadVideo = true;                     
            this.webBrowser.FileDownloadDirectory = "C:\\Users\\THINK\\Documents\\";            
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.LocationUrl = "about:blank";
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.NewWin = null;
            this.webBrowser.ObjectForScripting = null;
            this.webBrowser.OffLine = false;
            this.webBrowser.RegisterAsBrowser = false;
            this.webBrowser.RegisterAsDropTarget = false;
            this.webBrowser.RegisterForInternalDragDrop = true;
            this.webBrowser.ScrollBarsEnabled = true;            
            this.webBrowser.SendSourceOnDocumentCompleteWBEx = true;
            this.webBrowser.Silent = false;
            this.webBrowser.Size = new System.Drawing.Size(150, 150);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.TextSize = IfacesEnumsStructsClasses.TextSizeWB.Medium;
            this.webBrowser.UseInternalDownloadManager = true;
            this.webBrowser.WBDOCDOWNLOADCTLFLAG = 112;
            this.webBrowser.WBDOCHOSTUIDBLCLK = IfacesEnumsStructsClasses.DOCHOSTUIDBLCLK.DEFAULT;
            this.webBrowser.WBDOCHOSTUIFLAG = 262276;
            // 
            // BrowserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser);
            this.Name = "BrowserView";
            this.ResumeLayout(false);

        }

        #endregion

        private MyBrowser webBrowser;

    }
}
