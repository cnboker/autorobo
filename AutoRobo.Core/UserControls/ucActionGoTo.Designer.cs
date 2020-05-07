namespace AutoRobo.Core
{
    partial class ucActionGoTo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.skipElementList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.skipElementList);
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
            // skipElementList
            // 
            this.skipElementList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skipElementList.FormattingEnabled = true;
            this.skipElementList.Location = new System.Drawing.Point(91, 60);
            this.skipElementList.Name = "skipElementList";
            this.skipElementList.Size = new System.Drawing.Size(274, 20);
            this.skipElementList.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "跳转到";
            // 
            // ConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ConditionForm";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox skipElementList;
        private System.Windows.Forms.Label label3;

    }
}