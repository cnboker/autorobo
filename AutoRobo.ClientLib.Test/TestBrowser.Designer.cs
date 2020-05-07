namespace AutoRobo.ClientLib.Test
{
    partial class TestBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myBrowser1 = new MyBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // myBrowser1
            // 
            this.myBrowser1.AllowAlert = false;
            this.myBrowser1.Border3DEnabled = false;
            this.myBrowser1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myBrowser1.DocumentSource = "<HTML><HEAD></HEAD>\r\n<BODY></BODY></HTML>";
            this.myBrowser1.DocumentTitle = "";
            this.myBrowser1.DownloadActiveX = false;
            this.myBrowser1.DownloadFrames = true;
            this.myBrowser1.DownloadImages = false;
            this.myBrowser1.DownloadJava = false;
            this.myBrowser1.DownloadScripts = true;
            this.myBrowser1.DownloadSounds = false;
            this.myBrowser1.DownloadVideo = false;            
            this.myBrowser1.FileDownloadDirectory = "C:\\Users\\THINK\\Documents\\";            
            
            this.myBrowser1.Location = new System.Drawing.Point(0, 0);
            this.myBrowser1.LocationUrl = "about:blank";
            this.myBrowser1.Name = "myBrowser1";
            this.myBrowser1.ObjectForScripting = null;
            this.myBrowser1.OffLine = false;
            this.myBrowser1.RegisterAsBrowser = true;
            this.myBrowser1.RegisterAsDropTarget = false;
            this.myBrowser1.RegisterForInternalDragDrop = true;
            this.myBrowser1.ScrollBarsEnabled = true;            
            this.myBrowser1.SendSourceOnDocumentCompleteWBEx = true;
            this.myBrowser1.Silent = false;
            this.myBrowser1.Size = new System.Drawing.Size(809, 396);
            this.myBrowser1.TabIndex = 0;
            this.myBrowser1.Text = "myBrowser1";
            this.myBrowser1.TextSize = IfacesEnumsStructsClasses.TextSizeWB.Medium;
            this.myBrowser1.UseInternalDownloadManager = true;
            this.myBrowser1.WBDOCDOWNLOADCTLFLAG = 1280;
            this.myBrowser1.WBDOCHOSTUIDBLCLK = IfacesEnumsStructsClasses.DOCHOSTUIDBLCLK.DEFAULT;
            this.myBrowser1.WBDOCHOSTUIFLAG = 262276;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 426);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 475);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.myBrowser1);
            this.Name = "TestBrowser";
            this.ResumeLayout(false);

        }

        #endregion

        private MyBrowser myBrowser1;
        private System.Windows.Forms.Button button1;
    }
}