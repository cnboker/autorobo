namespace AutoRobo.Core.UserControls
{
    partial class ucFileReader
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
            this.varComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.fileBtn = new System.Windows.Forms.Button();
            this.includeHeaderCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.comboBoxFormat);
            this.tabPage2.Controls.Add(this.includeHeaderCheckBox);
            this.tabPage2.Controls.Add(this.fileBtn);
            this.tabPage2.Controls.Add(this.fileNameTextBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.varComboBox);
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
            this.label3.Location = new System.Drawing.Point(28, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "变量";
            // 
            // varComboBox
            // 
            this.varComboBox.FormattingEnabled = true;
            this.varComboBox.Location = new System.Drawing.Point(95, 46);
            this.varComboBox.Name = "varComboBox";
            this.varComboBox.Size = new System.Drawing.Size(201, 20);
            this.varComboBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "读文件";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(95, 99);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(201, 21);
            this.fileNameTextBox.TabIndex = 3;
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(302, 97);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(75, 23);
            this.fileBtn.TabIndex = 6;
            this.fileBtn.Text = "选择文件";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.fileBtn_Click);
            // 
            // includeHeaderCheckBox
            // 
            this.includeHeaderCheckBox.AutoSize = true;
            this.includeHeaderCheckBox.Location = new System.Drawing.Point(95, 135);
            this.includeHeaderCheckBox.Name = "includeHeaderCheckBox";
            this.includeHeaderCheckBox.Size = new System.Drawing.Size(96, 16);
            this.includeHeaderCheckBox.TabIndex = 7;
            this.includeHeaderCheckBox.Text = "是否读起始行";
            this.includeHeaderCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "输出格式";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
            "CSV  - 逗号分隔数据",
            "TSV - Tab分割数据",
            "XLS"});
            this.comboBoxFormat.Location = new System.Drawing.Point(95, 73);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(201, 20);
            this.comboBoxFormat.TabIndex = 14;
            // 
            // ucFileReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucFileReader";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox varComboBox;
        private System.Windows.Forms.CheckBox includeHeaderCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxFormat;
    }
}
