namespace AutoRobo.Core.UserControls
{
    partial class LoopProperty
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
            this.label1 = new System.Windows.Forms.Label();
            this.firstNumberTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lastNumberTextBox = new System.Windows.Forms.TextBox();
            this.stepTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "第一行行数";
            // 
            // firstNumberTextBox
            // 
            this.firstNumberTextBox.Location = new System.Drawing.Point(110, 14);
            this.firstNumberTextBox.Name = "firstNumberTextBox";
            this.firstNumberTextBox.Size = new System.Drawing.Size(154, 21);
            this.firstNumberTextBox.TabIndex = 1;
            this.firstNumberTextBox.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "最后一行行数";
            // 
            // lastNumberTextBox
            // 
            this.lastNumberTextBox.Location = new System.Drawing.Point(110, 40);
            this.lastNumberTextBox.Name = "lastNumberTextBox";
            this.lastNumberTextBox.Size = new System.Drawing.Size(154, 21);
            this.lastNumberTextBox.TabIndex = 3;
            this.lastNumberTextBox.Text = "0";
            // 
            // stepTextBox
            // 
            this.stepTextBox.Location = new System.Drawing.Point(110, 67);
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size(154, 21);
            this.stepTextBox.TabIndex = 4;
            this.stepTextBox.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "步长";
            // 
            // LoopProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stepTextBox);
            this.Controls.Add(this.lastNumberTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.firstNumberTextBox);
            this.Controls.Add(this.label1);
            this.Name = "LoopProperty";
            this.Size = new System.Drawing.Size(322, 112);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox firstNumberTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lastNumberTextBox;
        private System.Windows.Forms.TextBox stepTextBox;
        private System.Windows.Forms.Label label3;
    }
}
