using AutoRobo.Core.UserControls;
namespace AutoRobo.UserControls
{
    partial class ucTypeText
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
            this.txtTextToType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.testBtn = new System.Windows.Forms.Button();
            this.lenNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.randomCheckBox = new System.Windows.Forms.CheckBox();
            this.chkValueOnly = new System.Windows.Forms.CheckBox();
            this.inputVariableControl = new AutoRobo.Core.UserControls.ActionBindingBridge();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lenNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            // 
            // txtTextToType
            // 
            this.txtTextToType.Location = new System.Drawing.Point(25, 54);
            this.txtTextToType.Multiline = true;
            this.txtTextToType.Name = "txtTextToType";
            this.txtTextToType.Size = new System.Drawing.Size(351, 67);
            this.txtTextToType.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "显示的文本";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.chkValueOnly);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTextToType);
            this.panel1.Controls.Add(this.inputVariableControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 348);
            this.panel1.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.testBtn);
            this.groupBox1.Controls.Add(this.lenNumericUpDown);
            this.groupBox1.Controls.Add(this.typeComboBox);
            this.groupBox1.Controls.Add(this.randomCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(23, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 75);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(270, 30);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(59, 23);
            this.testBtn.TabIndex = 20;
            this.testBtn.Text = "测试";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // lenNumericUpDown
            // 
            this.lenNumericUpDown.Location = new System.Drawing.Point(227, 30);
            this.lenNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.lenNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lenNumericUpDown.Name = "lenNumericUpDown";
            this.lenNumericUpDown.Size = new System.Drawing.Size(33, 21);
            this.lenNumericUpDown.TabIndex = 19;
            this.lenNumericUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "手机号",
            "公司名称",
            "地址",
            "姓名",
            "数字",
            "字符"});
            this.typeComboBox.Location = new System.Drawing.Point(99, 31);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(113, 20);
            this.typeComboBox.TabIndex = 18;
            // 
            // randomCheckBox
            // 
            this.randomCheckBox.AutoSize = true;
            this.randomCheckBox.Location = new System.Drawing.Point(6, 35);
            this.randomCheckBox.Name = "randomCheckBox";
            this.randomCheckBox.Size = new System.Drawing.Size(96, 16);
            this.randomCheckBox.TabIndex = 17;
            this.randomCheckBox.Text = "随机生成文本";
            this.randomCheckBox.UseVisualStyleBackColor = true;
            // 
            // chkValueOnly
            // 
            this.chkValueOnly.AutoSize = true;
            this.chkValueOnly.Location = new System.Drawing.Point(25, 127);
            this.chkValueOnly.Name = "chkValueOnly";
            this.chkValueOnly.Size = new System.Drawing.Size(72, 16);
            this.chkValueOnly.TabIndex = 15;
            this.chkValueOnly.Text = "只作为值";
            this.chkValueOnly.UseVisualStyleBackColor = true;
            // 
            // inputVariableControl
            // 
            this.inputVariableControl.Location = new System.Drawing.Point(25, 241);
            this.inputVariableControl.Name = "inputVariableControl";
            this.inputVariableControl.Size = new System.Drawing.Size(353, 66);
            this.inputVariableControl.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "绑定名称";
            // 
            // ucTypeText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucTypeText";
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lenNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown lenNumericUpDown;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.CheckBox randomCheckBox;
        private System.Windows.Forms.Button testBtn;
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTextToType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkValueOnly;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private ActionBindingBridge inputVariableControl = null;

       
    }
}
