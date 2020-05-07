namespace AutoRobo.Core.UserControls
{
    partial class ucLoopByArray
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
            this.label3 = new System.Windows.Forms.Label();
            this.arrayComboBox = new System.Windows.Forms.ComboBox();
            this.reverseCheckBox = new System.Windows.Forms.CheckBox();
            this.topTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.topTextBox);
            this.tabPage2.Controls.Add(this.reverseCheckBox);
            this.tabPage2.Controls.Add(this.arrayComboBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Size = new System.Drawing.Size(422, 353);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(422, 353);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(430, 379);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "数组列表";
            // 
            // arrayComboBox
            // 
            this.arrayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.arrayComboBox.FormattingEnabled = true;
            this.arrayComboBox.Location = new System.Drawing.Point(93, 43);
            this.arrayComboBox.Name = "arrayComboBox";
            this.arrayComboBox.Size = new System.Drawing.Size(188, 20);
            this.arrayComboBox.TabIndex = 2;
            // 
            // reverseCheckBox
            // 
            this.reverseCheckBox.AutoSize = true;
            this.reverseCheckBox.Location = new System.Drawing.Point(93, 86);
            this.reverseCheckBox.Name = "reverseCheckBox";
            this.reverseCheckBox.Size = new System.Drawing.Size(72, 16);
            this.reverseCheckBox.TabIndex = 4;
            this.reverseCheckBox.Text = "数据倒序";
            this.reverseCheckBox.UseVisualStyleBackColor = true;
            // 
            // topTextBox
            // 
            this.topTextBox.Location = new System.Drawing.Point(93, 114);
            this.topTextBox.Name = "topTextBox";
            this.topTextBox.Size = new System.Drawing.Size(60, 21);
            this.topTextBox.TabIndex = 6;
            this.topTextBox.Text = "-1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "前N行";
            // 
            // ucLoopByArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucLoopByArray";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox arrayComboBox;
        private System.Windows.Forms.CheckBox reverseCheckBox;
        private System.Windows.Forms.TextBox topTextBox;
        private System.Windows.Forms.Label label4;
    }
}
