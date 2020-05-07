namespace AutoRobo.UserControls
{
    partial class ucClick
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkNoWait = new System.Windows.Forms.CheckBox();
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
            // panel1
            // 
            this.panel1.Controls.Add(this.chkNoWait);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 348);
            this.panel1.TabIndex = 10;
            // 
            // chkNoWait
            // 
            this.chkNoWait.AutoSize = true;
            this.chkNoWait.Location = new System.Drawing.Point(27, 87);
            this.chkNoWait.Name = "chkNoWait";
            this.chkNoWait.Size = new System.Drawing.Size(72, 16);
            this.chkNoWait.TabIndex = 5;
            this.chkNoWait.Text = "不用等待";
            this.chkNoWait.UseVisualStyleBackColor = true;
            // 
            // ucClick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucClick";
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkNoWait;
      
        protected System.Windows.Forms.Panel panel1;

    }
}
