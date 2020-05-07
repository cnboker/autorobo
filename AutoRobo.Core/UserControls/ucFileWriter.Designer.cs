namespace AutoRobo.Core.UserControls
{
    partial class ucFileWriter
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
            this.var = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fileTextbox = new System.Windows.Forms.TextBox();
            this.browserBtn = new System.Windows.Forms.Button();
            this.headerCheckbox = new System.Windows.Forms.CheckBox();
            this.sourceCheckedListBox = new AutoRobo.Core.UserControls.TableVariableListBox();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.comboBoxFormat);
            this.tabPage2.Controls.Add(this.sourceCheckedListBox);
            this.tabPage2.Controls.Add(this.headerCheckbox);
            this.tabPage2.Controls.Add(this.browserBtn);
            this.tabPage2.Controls.Add(this.fileTextbox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.var);
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
            // var
            // 
            this.var.AutoSize = true;
            this.var.Location = new System.Drawing.Point(25, 51);
            this.var.Name = "var";
            this.var.Size = new System.Drawing.Size(53, 12);
            this.var.TabIndex = 3;
            this.var.Text = "保存内容";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "文件名";
            // 
            // fileTextbox
            // 
            this.fileTextbox.Location = new System.Drawing.Point(84, 203);
            this.fileTextbox.Name = "fileTextbox";
            this.fileTextbox.Size = new System.Drawing.Size(214, 21);
            this.fileTextbox.TabIndex = 5;
            // 
            // browserBtn
            // 
            this.browserBtn.Location = new System.Drawing.Point(304, 199);
            this.browserBtn.Name = "browserBtn";
            this.browserBtn.Size = new System.Drawing.Size(75, 23);
            this.browserBtn.TabIndex = 6;
            this.browserBtn.Text = "浏览";
            this.browserBtn.UseVisualStyleBackColor = true;
            this.browserBtn.Visible = false;
            this.browserBtn.Click += new System.EventHandler(this.browserBtn_Click);
            // 
            // headerCheckbox
            // 
            this.headerCheckbox.AutoSize = true;
            this.headerCheckbox.Location = new System.Drawing.Point(84, 248);
            this.headerCheckbox.Name = "headerCheckbox";
            this.headerCheckbox.Size = new System.Drawing.Size(96, 16);
            this.headerCheckbox.TabIndex = 7;
            this.headerCheckbox.Text = "是否写起始行";
            this.headerCheckbox.UseVisualStyleBackColor = true;
            this.headerCheckbox.Visible = false;
            // 
            // sourceCheckedListBox
            // 
            this.sourceCheckedListBox.FormattingEnabled = true;
            this.sourceCheckedListBox.Location = new System.Drawing.Point(84, 51);
            this.sourceCheckedListBox.Name = "sourceCheckedListBox";
            this.sourceCheckedListBox.Size = new System.Drawing.Size(214, 116);
            this.sourceCheckedListBox.TabIndex = 8;
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
            "CSV  - 逗号分隔数据",
            "TSV - Tab分割数据",
            "XLS",
            "XML",
            "JSON"});
            this.comboBoxFormat.Location = new System.Drawing.Point(84, 175);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(214, 20);
            this.comboBoxFormat.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "输出格式";
            // 
            // ucFileWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucFileWriter";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox headerCheckbox;
        private System.Windows.Forms.Button browserBtn;
        private System.Windows.Forms.TextBox fileTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label var;
        private TableVariableListBox sourceCheckedListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxFormat;
    }
}
