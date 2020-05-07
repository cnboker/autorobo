namespace AutoRobo.UserControls
{
    partial class ucDirectKey
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
            this.shiftCheckbox = new System.Windows.Forms.CheckBox();
          
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.keysList = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
        
            // shiftCheckbox
            // 
            this.shiftCheckbox.AutoSize = true;
            this.shiftCheckbox.Location = new System.Drawing.Point(37, 53);
            this.shiftCheckbox.Name = "shiftCheckbox";
            this.shiftCheckbox.Size = new System.Drawing.Size(54, 16);
            this.shiftCheckbox.TabIndex = 23;
            this.shiftCheckbox.Text = "SHIFT";
            this.shiftCheckbox.UseVisualStyleBackColor = true;
            this.shiftCheckbox.Visible = false;
       
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "键";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(97, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(42, 16);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "ALT";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CausesValidation = false;
            this.checkBox2.Location = new System.Drawing.Point(157, 53);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(66, 16);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.Text = "CONTROL";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // keysList
            // 
            this.keysList.FormattingEnabled = true;
            this.keysList.Location = new System.Drawing.Point(83, 16);
            this.keysList.Name = "keysList";
            this.keysList.Size = new System.Drawing.Size(121, 20);
            this.keysList.TabIndex = 26;
            // 
            // ucDirectKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.tabPage2.Controls.Add(this.keysList);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.shiftCheckbox);

            this.tabPage2.Controls.Add(this.label2);
            this.Name = "ucDirectKey";
        
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox shiftCheckbox;
      
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox keysList;
    }
}
