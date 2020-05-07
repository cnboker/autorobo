namespace AutoRobo.Core.UserControls
{
    partial class ucPagination
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
            this.pageCountTextbox = new System.Windows.Forms.TextBox();
            this.countRadioButton = new System.Windows.Forms.RadioButton();
            this.existRadioButton = new System.Windows.Forms.RadioButton();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pageCountTextbox);
            this.tabPage2.Controls.Add(this.countRadioButton);
            this.tabPage2.Controls.Add(this.existRadioButton);
            // 
            // pageCountTextbox
            // 
            this.pageCountTextbox.Location = new System.Drawing.Point(64, 95);
            this.pageCountTextbox.Name = "pageCountTextbox";
            this.pageCountTextbox.Size = new System.Drawing.Size(133, 21);
            this.pageCountTextbox.TabIndex = 9;
            // 
            // countRadioButton
            // 
            this.countRadioButton.AutoSize = true;
            this.countRadioButton.Location = new System.Drawing.Point(64, 63);
            this.countRadioButton.Name = "countRadioButton";
            this.countRadioButton.Size = new System.Drawing.Size(47, 16);
            this.countRadioButton.TabIndex = 8;
            this.countRadioButton.Text = "页数";
            this.countRadioButton.UseVisualStyleBackColor = true;
            // 
            // existRadioButton
            // 
            this.existRadioButton.AutoSize = true;
            this.existRadioButton.Checked = true;
            this.existRadioButton.Location = new System.Drawing.Point(64, 31);
            this.existRadioButton.Name = "existRadioButton";
            this.existRadioButton.Size = new System.Drawing.Size(155, 16);
            this.existRadioButton.TabIndex = 6;
            this.existRadioButton.TabStop = true;
            this.existRadioButton.Text = "\"下一步\"按钮存在则继续";
            this.existRadioButton.UseVisualStyleBackColor = true;
            // 
            // ucPagination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucPagination";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox pageCountTextbox;
        private System.Windows.Forms.RadioButton countRadioButton;
        private System.Windows.Forms.RadioButton existRadioButton;
    }
}
