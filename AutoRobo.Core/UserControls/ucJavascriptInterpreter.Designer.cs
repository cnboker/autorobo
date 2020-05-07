namespace AutoRobo.Core.UserControls
{
    partial class ucJavascriptInterpreter
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.editBtn = new System.Windows.Forms.Button();
            this.copyBtn = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Size = new System.Drawing.Size(405, 353);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(388, 353);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(396, 379);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.codeBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(399, 347);
            this.panel2.TabIndex = 1;
            // 
            // codeBox
            // 
            this.codeBox.AcceptsReturn = true;
            this.codeBox.AcceptsTab = true;
            this.codeBox.BackColor = System.Drawing.Color.White;
            this.codeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Multiline = true;
            this.codeBox.Name = "codeBox";
            this.codeBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.codeBox.Size = new System.Drawing.Size(399, 347);
            this.codeBox.TabIndex = 0;
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(216, 402);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 15;
            this.editBtn.Text = "粘帖";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // copyBtn
            // 
            this.copyBtn.Location = new System.Drawing.Point(306, 402);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(75, 21);
            this.copyBtn.TabIndex = 16;
            this.copyBtn.Text = "拷贝";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // ucJavascriptInterpreter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.editBtn);
            this.Name = "ucJavascriptInterpreter";
            this.Size = new System.Drawing.Size(396, 442);
            this.Controls.SetChildIndex(this.editBtn, 0);
            this.Controls.SetChildIndex(this.copyBtn, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

      
     
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button copyBtn;
    }
}
