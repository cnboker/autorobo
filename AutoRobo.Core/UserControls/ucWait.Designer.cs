namespace AutoRobo.UserControls
{
    partial class ucWait
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
            this.chkRegex = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlWaitType = new System.Windows.Forms.ComboBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.Size = new System.Drawing.Size(424, 354);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Size = new System.Drawing.Size(424, 354);
            // 
            // chkRegex
            // 
            this.chkRegex.AutoSize = true;
            this.chkRegex.Location = new System.Drawing.Point(88, 135);
            this.chkRegex.Name = "chkRegex";
            this.chkRegex.Size = new System.Drawing.Size(108, 16);
            this.chkRegex.TabIndex = 3;
            this.chkRegex.Text = "正则表达式匹配";
            this.chkRegex.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "等待操作";
            // 
            // ddlWaitType
            // 
            this.ddlWaitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlWaitType.FormattingEnabled = true;
            this.ddlWaitType.Items.AddRange(new object[] {
            "等待元素存在",
            "等待元素移除",
            "等待元素属性值匹配"});
            this.ddlWaitType.Location = new System.Drawing.Point(88, 17);
            this.ddlWaitType.Name = "ddlWaitType";
            this.ddlWaitType.Size = new System.Drawing.Size(121, 20);
            this.ddlWaitType.TabIndex = 5;
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(88, 43);
            this.numTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(54, 21);
            this.numTimeout.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "等待时间";
            // 
            // txtAttribute
            // 
            this.txtAttribute.Location = new System.Drawing.Point(88, 70);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(121, 21);
            this.txtAttribute.TabIndex = 8;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(88, 97);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(121, 21);
            this.txtValue.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "属性";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "值";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtAttribute);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkRegex);
            this.panel1.Controls.Add(this.ddlWaitType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.numTimeout);
            this.panel1.Controls.Add(this.txtValue);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 348);
            this.panel1.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "秒";
            // 
            // ucWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucWait";
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRegex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlWaitType;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label4;
    
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
    }
}
