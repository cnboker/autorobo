namespace AutoRobo.Core.UserControls
{
    partial class ucThread
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
        
            this.threadCountBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.threadCountBox)).BeginInit();
            this.SuspendLayout();
         
            // 
            // threadCountBox
            // 
            this.threadCountBox.Location = new System.Drawing.Point(106, 34);
            this.threadCountBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.threadCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadCountBox.Name = "threadCountBox";
            this.threadCountBox.Size = new System.Drawing.Size(54, 21);
            this.threadCountBox.TabIndex = 18;
            this.threadCountBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "线程数";
            // 
            // ucThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.tabPage2.Controls.Add(this.threadCountBox);
            this.tabPage2.Controls.Add(this.label2);
            this.Name = "ucThread";
            ((System.ComponentModel.ISupportInitialize)(this.threadCountBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

      
        private System.Windows.Forms.NumericUpDown threadCountBox;
        private System.Windows.Forms.Label label2;
    }
}
