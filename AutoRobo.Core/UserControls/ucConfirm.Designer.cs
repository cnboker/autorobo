namespace AutoRobo.UserControls
{
    partial class ucConfirm
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
            this.rbOK = new System.Windows.Forms.RadioButton();
            this.rbCancel = new System.Windows.Forms.RadioButton();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rbCancel);
            this.tabPage2.Controls.Add(this.rbOK);
          
            // 
            // rbOK
            // 
            this.rbOK.AutoSize = true;
            this.rbOK.Location = new System.Drawing.Point(17, 25);
            this.rbOK.Name = "rbOK";
            this.rbOK.Size = new System.Drawing.Size(47, 16);
            this.rbOK.TabIndex = 21;
            this.rbOK.TabStop = true;
            this.rbOK.Text = "确认";
            this.rbOK.UseVisualStyleBackColor = true;
            // 
            // rbCancel
            // 
            this.rbCancel.AutoSize = true;
            this.rbCancel.Location = new System.Drawing.Point(82, 25);
            this.rbCancel.Name = "rbCancel";
            this.rbCancel.Size = new System.Drawing.Size(47, 16);
            this.rbCancel.TabIndex = 22;
            this.rbCancel.TabStop = true;
            this.rbCancel.Text = "取消";
            this.rbCancel.UseVisualStyleBackColor = true;
            // 
            // ucConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucConfirm";
           
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

      
        private System.Windows.Forms.RadioButton rbOK;
        private System.Windows.Forms.RadioButton rbCancel;

    }
}
