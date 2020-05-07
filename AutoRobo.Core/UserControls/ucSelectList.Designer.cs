namespace AutoRobo.UserControls
{
    partial class ucSelectList
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
            this.chkByValue = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSelection = new System.Windows.Forms.TextBox();
            this.chkIsRegex = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
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
            // chkByValue
            // 
            this.chkByValue.AutoSize = true;
            this.chkByValue.Location = new System.Drawing.Point(16, 74);
            this.chkByValue.Name = "chkByValue";
            this.chkByValue.Size = new System.Drawing.Size(84, 16);
            this.chkByValue.TabIndex = 5;
            this.chkByValue.Text = "选择的为值";
            this.chkByValue.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "选择";
            // 
            // txtSelection
            // 
            this.txtSelection.Location = new System.Drawing.Point(16, 34);
            this.txtSelection.Name = "txtSelection";
            this.txtSelection.Size = new System.Drawing.Size(247, 21);
            this.txtSelection.TabIndex = 7;
            // 
            // chkIsRegex
            // 
            this.chkIsRegex.AutoSize = true;
            this.chkIsRegex.Location = new System.Drawing.Point(16, 106);
            this.chkIsRegex.Name = "chkIsRegex";
            this.chkIsRegex.Size = new System.Drawing.Size(108, 16);
            this.chkIsRegex.TabIndex = 8;
            this.chkIsRegex.Text = "正则表达式匹配";
            this.chkIsRegex.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkByValue);
            this.panel1.Controls.Add(this.txtSelection);
            this.panel1.Controls.Add(this.chkIsRegex);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 348);
            this.panel1.TabIndex = 11;
            // 
            // ucSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucSelectList";
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkByValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSelection;
        private System.Windows.Forms.CheckBox chkIsRegex;
        private System.Windows.Forms.Panel panel1;
    }
}
