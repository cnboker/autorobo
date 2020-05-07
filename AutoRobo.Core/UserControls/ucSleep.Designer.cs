namespace AutoRobo.UserControls
{
    partial class ucSleep
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
            this.numSleep = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
           
            this.label1 = new System.Windows.Forms.Label();
            this.waitdataAvailableBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSleep)).BeginInit();
            this.SuspendLayout();
            // 
            // numSleep
            // 
            this.numSleep.Location = new System.Drawing.Point(88, 22);
            this.numSleep.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSleep.Name = "numSleep";
            this.numSleep.Size = new System.Drawing.Size(54, 21);
            this.numSleep.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "等待时间";
          
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "毫秒";
            // 
            // waitdataAvailableBox
            // 
            this.waitdataAvailableBox.AutoSize = true;
            this.waitdataAvailableBox.Location = new System.Drawing.Point(28, 58);
            this.waitdataAvailableBox.Name = "waitdataAvailableBox";
            this.waitdataAvailableBox.Size = new System.Drawing.Size(108, 16);
            this.waitdataAvailableBox.TabIndex = 17;
            this.waitdataAvailableBox.Text = "等待数据可利用";
            this.waitdataAvailableBox.UseVisualStyleBackColor = true;
            // 
            // ucSleep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.waitdataAvailableBox);
            this.tabPage2.Controls.Add(this.label1);

            this.tabPage2.Controls.Add(this.numSleep);
            this.tabPage2.Controls.Add(this.label2);
            this.Name = "ucSleep";
            
            ((System.ComponentModel.ISupportInitialize)(this.numSleep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numSleep;
        private System.Windows.Forms.Label label2;
   
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox waitdataAvailableBox;
    }
}
