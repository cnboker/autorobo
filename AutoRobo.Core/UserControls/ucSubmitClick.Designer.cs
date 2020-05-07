namespace AutoRobo.UserControls
{
    partial class ucSubmitClick
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
            this.submitCheckbox = new System.Windows.Forms.CheckBox();
            this.recordMarkCheckbox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.submitCheckbox);
            this.panel1.Controls.Add(this.recordMarkCheckbox);
            this.panel1.Controls.SetChildIndex(this.recordMarkCheckbox, 0);
            this.panel1.Controls.SetChildIndex(this.submitCheckbox, 0);
            // 
            // submitCheckbox
            // 
            this.submitCheckbox.AutoSize = true;
            this.submitCheckbox.Location = new System.Drawing.Point(27, 32);
            this.submitCheckbox.Name = "submitCheckbox";
            this.submitCheckbox.Size = new System.Drawing.Size(72, 16);
            this.submitCheckbox.TabIndex = 10;
            this.submitCheckbox.Text = "自动提交";
            this.submitCheckbox.UseVisualStyleBackColor = true;
            // 
            // recordMarkCheckbox
            // 
            this.recordMarkCheckbox.AutoSize = true;
            this.recordMarkCheckbox.Location = new System.Drawing.Point(27, 60);
            this.recordMarkCheckbox.Name = "recordMarkCheckbox";
            this.recordMarkCheckbox.Size = new System.Drawing.Size(120, 16);
            this.recordMarkCheckbox.TabIndex = 11;
            this.recordMarkCheckbox.Text = "记录用户数据设置";
            this.recordMarkCheckbox.UseVisualStyleBackColor = true;
            // 
            // ucSubmitClick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucSubmitClick";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox submitCheckbox;
        private System.Windows.Forms.CheckBox recordMarkCheckbox;
    }
}
