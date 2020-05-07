namespace AutoRobo.Core.UserControls
{
    partial class ucValidateCode
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.mobileVaildateCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupNameBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(414, 412);
       
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
        
            // 
            // tabPage3
            // 
            this.tabPage4.Controls.Add(this.mobileVaildateCheckBox);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.groupNameBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
        
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "验证码属性";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // mobileVaildateCheckBox
            // 
            this.mobileVaildateCheckBox.AutoSize = true;
            this.mobileVaildateCheckBox.Location = new System.Drawing.Point(29, 82);
            this.mobileVaildateCheckBox.Name = "mobileVaildateCheckBox";
            this.mobileVaildateCheckBox.Size = new System.Drawing.Size(84, 16);
            this.mobileVaildateCheckBox.TabIndex = 26;
            this.mobileVaildateCheckBox.Text = "手机验证码";
            this.mobileVaildateCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "组名称";
            // 
            // groupNameBox
            // 
            this.groupNameBox.Location = new System.Drawing.Point(29, 45);
            this.groupNameBox.Name = "groupNameBox";
            this.groupNameBox.Size = new System.Drawing.Size(320, 21);
            this.groupNameBox.TabIndex = 24;
            // 
            // ucValidateCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucValidateCode";
            
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox groupNameBox;
        private System.Windows.Forms.CheckBox mobileVaildateCheckBox;

    }
}
