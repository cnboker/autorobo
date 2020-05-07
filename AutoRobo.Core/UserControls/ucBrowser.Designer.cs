namespace AutoRobo.Core.UserControls
{
    partial class ucBrowser
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
            this.downloadFlash = new System.Windows.Forms.CheckBox();
            this.downloadActivex = new System.Windows.Forms.CheckBox();
            this.downloadSounds = new System.Windows.Forms.CheckBox();
            this.downloadVideo = new System.Windows.Forms.CheckBox();
            this.downloadImages = new System.Windows.Forms.CheckBox();
            this.downLoadScriptBox = new System.Windows.Forms.CheckBox();
            this.visibilityBox = new System.Windows.Forms.CheckBox();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.visibilityBox);
            this.tabPage2.Controls.Add(this.downLoadScriptBox);
            this.tabPage2.Controls.Add(this.downloadFlash);
            this.tabPage2.Controls.Add(this.downloadActivex);
            this.tabPage2.Controls.Add(this.downloadSounds);
            this.tabPage2.Controls.Add(this.downloadVideo);
            this.tabPage2.Controls.Add(this.downloadImages);
            this.tabPage2.Size = new System.Drawing.Size(422, 353);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(422, 353);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(430, 379);
            // 
            // downloadFlash
            // 
            this.downloadFlash.AutoSize = true;
            this.downloadFlash.Checked = true;
            this.downloadFlash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadFlash.Location = new System.Drawing.Point(39, 114);
            this.downloadFlash.Name = "downloadFlash";
            this.downloadFlash.Size = new System.Drawing.Size(78, 16);
            this.downloadFlash.TabIndex = 30;
            this.downloadFlash.Text = "下载Flash";
            this.downloadFlash.UseVisualStyleBackColor = true;
            // 
            // downloadActivex
            // 
            this.downloadActivex.AutoSize = true;
            this.downloadActivex.Checked = true;
            this.downloadActivex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadActivex.Location = new System.Drawing.Point(199, 44);
            this.downloadActivex.Name = "downloadActivex";
            this.downloadActivex.Size = new System.Drawing.Size(90, 16);
            this.downloadActivex.TabIndex = 29;
            this.downloadActivex.Text = "下载Activex";
            this.downloadActivex.UseVisualStyleBackColor = true;
            // 
            // downloadSounds
            // 
            this.downloadSounds.AutoSize = true;
            this.downloadSounds.Checked = true;
            this.downloadSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadSounds.Location = new System.Drawing.Point(39, 149);
            this.downloadSounds.Name = "downloadSounds";
            this.downloadSounds.Size = new System.Drawing.Size(72, 16);
            this.downloadSounds.TabIndex = 28;
            this.downloadSounds.Text = "下载声音";
            this.downloadSounds.UseVisualStyleBackColor = true;
            // 
            // downloadVideo
            // 
            this.downloadVideo.AutoSize = true;
            this.downloadVideo.Checked = true;
            this.downloadVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadVideo.Location = new System.Drawing.Point(39, 79);
            this.downloadVideo.Name = "downloadVideo";
            this.downloadVideo.Size = new System.Drawing.Size(72, 16);
            this.downloadVideo.TabIndex = 27;
            this.downloadVideo.Text = "下载视频";
            this.downloadVideo.UseVisualStyleBackColor = true;
            // 
            // downloadImages
            // 
            this.downloadImages.AutoSize = true;
            this.downloadImages.Checked = true;
            this.downloadImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadImages.Location = new System.Drawing.Point(39, 44);
            this.downloadImages.Name = "downloadImages";
            this.downloadImages.Size = new System.Drawing.Size(72, 16);
            this.downloadImages.TabIndex = 26;
            this.downloadImages.Text = "下载图片";
            this.downloadImages.UseVisualStyleBackColor = true;
            // 
            // downLoadScriptBox
            // 
            this.downLoadScriptBox.AutoSize = true;
            this.downLoadScriptBox.Checked = true;
            this.downLoadScriptBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downLoadScriptBox.Location = new System.Drawing.Point(199, 77);
            this.downLoadScriptBox.Name = "downLoadScriptBox";
            this.downLoadScriptBox.Size = new System.Drawing.Size(72, 16);
            this.downLoadScriptBox.TabIndex = 31;
            this.downLoadScriptBox.Text = "下载脚本";
            this.downLoadScriptBox.UseVisualStyleBackColor = true;
            // 
            // visibilityBox
            // 
            this.visibilityBox.AutoSize = true;
            this.visibilityBox.Checked = true;
            this.visibilityBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visibilityBox.Location = new System.Drawing.Point(199, 109);
            this.visibilityBox.Name = "visibilityBox";
            this.visibilityBox.Size = new System.Drawing.Size(72, 16);
            this.visibilityBox.TabIndex = 32;
            this.visibilityBox.Text = "是否显示";
            this.visibilityBox.UseVisualStyleBackColor = true;
            // 
            // ucBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucBrowser";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

     
        private System.Windows.Forms.CheckBox downloadFlash;
        private System.Windows.Forms.CheckBox downloadActivex;
        private System.Windows.Forms.CheckBox downloadSounds;
        private System.Windows.Forms.CheckBox downloadVideo;
        private System.Windows.Forms.CheckBox downloadImages;
        private System.Windows.Forms.CheckBox downLoadScriptBox;
        private System.Windows.Forms.CheckBox visibilityBox;
    }
}
