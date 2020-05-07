namespace AutoRobo.Core.UserControls
{
    partial class ucSelectVariable
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
            this.var = new System.Windows.Forms.Label();
            this.varComboBox = new System.Windows.Forms.ComboBox();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.varComboBox);
            this.tabPage2.Controls.Add(this.var);
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
            // var
            // 
            this.var.AutoSize = true;
            this.var.Location = new System.Drawing.Point(19, 63);
            this.var.Name = "var";
            this.var.Size = new System.Drawing.Size(53, 12);
            this.var.TabIndex = 0;
            this.var.Text = "变量列表";
            // 
            // varComboBox
            // 
            this.varComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.varComboBox.FormattingEnabled = true;
            this.varComboBox.Location = new System.Drawing.Point(102, 63);
            this.varComboBox.Name = "varComboBox";
            this.varComboBox.Size = new System.Drawing.Size(214, 20);
            this.varComboBox.TabIndex = 1;
            // 
            // ucSelectVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucSelectVariable";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label var;
        private System.Windows.Forms.ComboBox varComboBox;
    }
}
