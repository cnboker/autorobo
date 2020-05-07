namespace AutoRobo.UserControls
{
    partial class ucKey
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
            this.ddlKeyFunction = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKeyCharacter = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
         
            this.tabPage2.Controls.Add(this.panel1);
          
         
            // 
            // ddlKeyFunction
            // 
            this.ddlKeyFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlKeyFunction.FormattingEnabled = true;
            this.ddlKeyFunction.Items.AddRange(new object[] {
            "Down",
            "Press",
            "Up"});
            this.ddlKeyFunction.Location = new System.Drawing.Point(94, 18);
            this.ddlKeyFunction.Name = "ddlKeyFunction";
            this.ddlKeyFunction.Size = new System.Drawing.Size(116, 20);
            this.ddlKeyFunction.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "按键方式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "按键字符";
            // 
            // txtKeyCharacter
            // 
            this.txtKeyCharacter.Location = new System.Drawing.Point(94, 58);
            this.txtKeyCharacter.MaxLength = 1;
            this.txtKeyCharacter.Name = "txtKeyCharacter";
            this.txtKeyCharacter.Size = new System.Drawing.Size(24, 21);
            this.txtKeyCharacter.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtKeyCharacter);
            this.panel1.Controls.Add(this.ddlKeyFunction);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 407);
            this.panel1.TabIndex = 11;
            // 
            // ucKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucKey";
           
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlKeyFunction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKeyCharacter;
        private System.Windows.Forms.Panel panel1;
    }
}
