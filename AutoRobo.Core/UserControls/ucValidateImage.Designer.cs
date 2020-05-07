namespace AutoRobo.Core.UserControls
{
    partial class ucValidateImage
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
            this.groupNameBox = new System.Windows.Forms.TextBox();
            this.imageDisplayStatue = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.imageDisplayStatue);
            this.tabPage2.Controls.Add(this.groupNameBox);
            this.tabPage2.Controls.Add(this.label1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "组名称";
            // 
            // groupNameBox
            // 
            this.groupNameBox.Location = new System.Drawing.Point(33, 41);
            this.groupNameBox.Name = "groupNameBox";
            this.groupNameBox.Size = new System.Drawing.Size(242, 21);
            this.groupNameBox.TabIndex = 4;
            // 
            // imageDisplayStatue
            // 
            this.imageDisplayStatue.AutoSize = true;
            this.imageDisplayStatue.Location = new System.Drawing.Point(33, 84);
            this.imageDisplayStatue.Name = "imageDisplayStatue";
            this.imageDisplayStatue.Size = new System.Drawing.Size(168, 16);
            this.imageDisplayStatue.TabIndex = 5;
            this.imageDisplayStatue.Text = "验证码输入框聚焦后才显示";
            this.imageDisplayStatue.UseVisualStyleBackColor = true;
            // 
            // ucValidateImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucValidateImage";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox groupNameBox;
        private System.Windows.Forms.CheckBox imageDisplayStatue;
    }
}
