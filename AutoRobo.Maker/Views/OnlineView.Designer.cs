namespace AutoRobo.Makers.Views
{
    partial class OnlineView
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
            this.mytab = new System.Windows.Forms.CustomTabControl();
            this.SuspendLayout();
            // 
            // mytab
            // 
            this.mytab.DisplayStyle = System.Windows.Forms.TabStyle.Chrome;
            // 
            // 
            // 
            this.mytab.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.mytab.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.mytab.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.mytab.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.mytab.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.White;
            this.mytab.DisplayStyleProvider.FocusTrack = false;
            this.mytab.DisplayStyleProvider.HotTrack = true;
            this.mytab.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mytab.DisplayStyleProvider.Opacity = 1F;
            this.mytab.DisplayStyleProvider.Overlap = 16;
            this.mytab.DisplayStyleProvider.Padding = new System.Drawing.Point(7, 5);
            this.mytab.DisplayStyleProvider.Radius = 16;
            this.mytab.DisplayStyleProvider.ShowTabCloser = true;
            this.mytab.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.mytab.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.mytab.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.mytab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mytab.HotTrack = true;
            this.mytab.Location = new System.Drawing.Point(0, 0);
            this.mytab.Name = "mytab";
            this.mytab.SelectedIndex = 0;
            this.mytab.Size = new System.Drawing.Size(284, 262);
            this.mytab.TabIndex = 0;
            // 
            // OnlineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.mytab);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "OnlineView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomTabControl mytab;


    }
}
