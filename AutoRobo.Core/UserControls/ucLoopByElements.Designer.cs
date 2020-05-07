namespace AutoRobo.Core.UserControls
{
    partial class ucLoopByElements
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
            this.loopProperty1 = new AutoRobo.Core.UserControls.LoopProperty();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.loopProperty1);
            // 
            // loopProperty1
            // 
            this.loopProperty1.FirstNumber = 0;
            this.loopProperty1.LastNumber = 0;
            this.loopProperty1.Location = new System.Drawing.Point(29, 61);
            this.loopProperty1.Name = "loopProperty1";
            this.loopProperty1.Size = new System.Drawing.Size(322, 112);
            this.loopProperty1.StepNumber = 1;
            this.loopProperty1.TabIndex = 5;
            // 
            // ucLoopBySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucLoopBySelector";
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LoopProperty loopProperty1;
    }
}
