namespace AutoRobo.Core.UserControls
{
    partial class ucAddDataToList
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
            this.label4 = new System.Windows.Forms.Label();
            this.ruleButton = new System.Windows.Forms.Button();
            this.propertyTextbox = new System.Windows.Forms.TextBox();
            this.arrayCombox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.attrCombox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fliterEmptyStringCheckBox = new System.Windows.Forms.CheckBox();
            this.varBtn = new System.Windows.Forms.Button();
            this.methodComboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.Size = new System.Drawing.Size(424, 370);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.methodComboBox1);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.varBtn);
            this.tabPage2.Controls.Add(this.fliterEmptyStringCheckBox);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.ruleButton);
            this.tabPage2.Controls.Add(this.attrCombox);
            this.tabPage2.Controls.Add(this.propertyTextbox);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.arrayCombox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Size = new System.Drawing.Size(424, 370);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(424, 370);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(432, 396);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "描述";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "选择变量";
            // 
            // ruleButton
            // 
            this.ruleButton.Location = new System.Drawing.Point(102, 198);
            this.ruleButton.Name = "ruleButton";
            this.ruleButton.Size = new System.Drawing.Size(75, 23);
            this.ruleButton.TabIndex = 5;
            this.ruleButton.Text = "字符串规则";
            this.ruleButton.UseVisualStyleBackColor = true;
            this.ruleButton.Click += new System.EventHandler(this.ruleButton_Click);
            // 
            // propertyTextbox
            // 
            this.propertyTextbox.Enabled = false;
            this.propertyTextbox.Location = new System.Drawing.Point(98, 108);
            this.propertyTextbox.Name = "propertyTextbox";
            this.propertyTextbox.Size = new System.Drawing.Size(200, 21);
            this.propertyTextbox.TabIndex = 4;
            // 
            // arrayCombox
            // 
            this.arrayCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.arrayCombox.FormattingEnabled = true;
            this.arrayCombox.Location = new System.Drawing.Point(98, 32);
            this.arrayCombox.Name = "arrayCombox";
            this.arrayCombox.Size = new System.Drawing.Size(200, 20);
            this.arrayCombox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "元素内容";
            // 
            // attrCombox
            // 
            this.attrCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attrCombox.FormattingEnabled = true;
            this.attrCombox.Items.AddRange(new object[] {
            "Text",
            "Html",
            "属性值"});
            this.attrCombox.Location = new System.Drawing.Point(98, 67);
            this.attrCombox.Name = "attrCombox";
            this.attrCombox.Size = new System.Drawing.Size(200, 20);
            this.attrCombox.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "元素属性";
            // 
            // fliterEmptyStringCheckBox
            // 
            this.fliterEmptyStringCheckBox.AutoSize = true;
            this.fliterEmptyStringCheckBox.Location = new System.Drawing.Point(102, 176);
            this.fliterEmptyStringCheckBox.Name = "fliterEmptyStringCheckBox";
            this.fliterEmptyStringCheckBox.Size = new System.Drawing.Size(96, 16);
            this.fliterEmptyStringCheckBox.TabIndex = 20;
            this.fliterEmptyStringCheckBox.Text = "过滤空字符串";
            this.fliterEmptyStringCheckBox.UseVisualStyleBackColor = true;
            // 
            // varBtn
            // 
            this.varBtn.Location = new System.Drawing.Point(305, 29);
            this.varBtn.Name = "varBtn";
            this.varBtn.Size = new System.Drawing.Size(75, 23);
            this.varBtn.TabIndex = 21;
            this.varBtn.Text = "变量定义";
            this.varBtn.UseVisualStyleBackColor = true;
            this.varBtn.Click += new System.EventHandler(this.varBtn_Click);
            // 
            // methodComboBox1
            // 
            this.methodComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodComboBox1.FormattingEnabled = true;
            this.methodComboBox1.Items.AddRange(new object[] {
            "无",
            "增加多项到表列",
            "增加多项到表行"});
            this.methodComboBox1.Location = new System.Drawing.Point(100, 142);
            this.methodComboBox1.Name = "methodComboBox1";
            this.methodComboBox1.Size = new System.Drawing.Size(200, 20);
            this.methodComboBox1.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "操作";
            // 
            // ucAddDataToList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucAddDataToList";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ruleButton;
        private System.Windows.Forms.TextBox propertyTextbox;
        private System.Windows.Forms.ComboBox arrayCombox;
        private System.Windows.Forms.ComboBox attrCombox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox fliterEmptyStringCheckBox;
        private System.Windows.Forms.Button varBtn;
        private System.Windows.Forms.ComboBox methodComboBox1;
        private System.Windows.Forms.Label label8;

    }
}
