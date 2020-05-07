namespace AutoRobo.Core.UserControls
{
    partial class ucActionCall
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
            this.scriptListBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scriptListBox);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Size = new System.Drawing.Size(405, 353);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(405, 353);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(413, 379);
            // 
            // scriptListBox
            // 
            this.scriptListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scriptListBox.FormattingEnabled = true;
            this.scriptListBox.Location = new System.Drawing.Point(96, 56);
            this.scriptListBox.Name = "scriptListBox";
            this.scriptListBox.Size = new System.Drawing.Size(207, 20);
            this.scriptListBox.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "调用";
            // 
            // ucActionCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucActionCall";
            this.Size = new System.Drawing.Size(413, 442);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

 
        private System.Windows.Forms.ComboBox scriptListBox;
        private System.Windows.Forms.Label label1;
    }
}
