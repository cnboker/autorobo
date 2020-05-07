namespace AutoRobo.UserControls
{
    partial class ucBaseElement
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.descBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.validateButton = new System.Windows.Forms.Button();
            this.regxCheckBox = new System.Windows.Forms.CheckBox();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Size = new System.Drawing.Size(424, 354);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(424, 354);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Size = new System.Drawing.Size(432, 380);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.Controls.SetChildIndex(this.tabPage3, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // descBox
            // 
            this.descBox.Location = new System.Drawing.Point(0, 0);
            this.descBox.Name = "descBox";
            this.descBox.Size = new System.Drawing.Size(100, 21);
            this.descBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(100, 21);
            this.textBoxLabel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.validateButton);
            this.tabPage3.Controls.Add(this.regxCheckBox);
            this.tabPage3.Controls.Add(this.valueTextBox);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.methodComboBox);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.contentTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(424, 354);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "元素定位";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(215, 132);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(75, 23);
            this.validateButton.TabIndex = 26;
            this.validateButton.Text = "验证";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // regxCheckBox
            // 
            this.regxCheckBox.AutoSize = true;
            this.regxCheckBox.Location = new System.Drawing.Point(82, 131);
            this.regxCheckBox.Name = "regxCheckBox";
            this.regxCheckBox.Size = new System.Drawing.Size(108, 16);
            this.regxCheckBox.TabIndex = 25;
            this.regxCheckBox.Text = "正则表达式匹配";
            this.regxCheckBox.UseVisualStyleBackColor = true;
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(82, 53);
            this.valueTextBox.Multiline = true;
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(289, 72);
            this.valueTextBox.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "值";
            // 
            // methodComboBox
            // 
            this.methodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Location = new System.Drawing.Point(82, 24);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(289, 20);
            this.methodComboBox.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "方式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "元素内容";
            // 
            // contentTextBox
            // 
            this.contentTextBox.Location = new System.Drawing.Point(82, 182);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.Size = new System.Drawing.Size(289, 133);
            this.contentTextBox.TabIndex = 19;
            // 
            // ucBaseElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Name = "ucBaseElement";
            this.Size = new System.Drawing.Size(432, 453);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabPage tabPage3;
       
        private System.Windows.Forms.TextBox descBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox contentTextBox;
        private System.Windows.Forms.CheckBox regxCheckBox;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button validateButton;
    }
}
