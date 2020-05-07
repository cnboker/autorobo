namespace AutoRobo.Core.UserControls
{
    partial class ucVariable
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
            this.label3 = new System.Windows.Forms.Label();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.defaultLabel = new System.Windows.Forms.Label();
            this.valueTextbox = new System.Windows.Forms.TextBox();
            this.dataBtn = new System.Windows.Forms.Button();
            this.dataSourceBtn = new System.Windows.Forms.Button();
            this.varTypeLabel = new System.Windows.Forms.Label();
            this.varTypeList = new System.Windows.Forms.ComboBox();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.varTypeList);
            this.tabPage2.Controls.Add(this.varTypeLabel);
            this.tabPage2.Controls.Add(this.dataSourceBtn);
            this.tabPage2.Controls.Add(this.dataBtn);
            this.tabPage2.Controls.Add(this.valueTextbox);
            this.tabPage2.Controls.Add(this.defaultLabel);
            this.tabPage2.Controls.Add(this.nameTextbox);
            this.tabPage2.Controls.Add(this.label3);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "变量名称";
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(119, 72);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(182, 21);
            this.nameTextbox.TabIndex = 1;
            // 
            // defaultLabel
            // 
            this.defaultLabel.AutoSize = true;
            this.defaultLabel.Location = new System.Drawing.Point(52, 114);
            this.defaultLabel.Name = "defaultLabel";
            this.defaultLabel.Size = new System.Drawing.Size(17, 12);
            this.defaultLabel.TabIndex = 2;
            this.defaultLabel.Text = "值";
            this.defaultLabel.Visible = false;
            // 
            // valueTextbox
            // 
            this.valueTextbox.Location = new System.Drawing.Point(119, 114);
            this.valueTextbox.Name = "valueTextbox";
            this.valueTextbox.Size = new System.Drawing.Size(182, 21);
            this.valueTextbox.TabIndex = 3;
            this.valueTextbox.Text = "0";
            this.valueTextbox.Visible = false;
            // 
            // dataBtn
            // 
            this.dataBtn.Location = new System.Drawing.Point(226, 211);
            this.dataBtn.Name = "dataBtn";
            this.dataBtn.Size = new System.Drawing.Size(75, 23);
            this.dataBtn.TabIndex = 4;
            this.dataBtn.Text = "数据预览";
            this.dataBtn.UseVisualStyleBackColor = true;
            this.dataBtn.Visible = false;
            this.dataBtn.Click += new System.EventHandler(this.dataBtn_Click);
            // 
            // dataSourceBtn
            // 
            this.dataSourceBtn.Location = new System.Drawing.Point(119, 211);
            this.dataSourceBtn.Name = "dataSourceBtn";
            this.dataSourceBtn.Size = new System.Drawing.Size(75, 23);
            this.dataSourceBtn.TabIndex = 5;
            this.dataSourceBtn.Text = "数据源";
            this.dataSourceBtn.UseVisualStyleBackColor = true;
            this.dataSourceBtn.Visible = false;
            this.dataSourceBtn.Click += new System.EventHandler(this.dataSourceBtn_Click);
            // 
            // varTypeLabel
            // 
            this.varTypeLabel.AutoSize = true;
            this.varTypeLabel.Location = new System.Drawing.Point(52, 156);
            this.varTypeLabel.Name = "varTypeLabel";
            this.varTypeLabel.Size = new System.Drawing.Size(53, 12);
            this.varTypeLabel.TabIndex = 6;
            this.varTypeLabel.Text = "变量方向";
            this.varTypeLabel.Visible = false;
            // 
            // varTypeList
            // 
            this.varTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.varTypeList.FormattingEnabled = true;
            this.varTypeList.Items.AddRange(new object[] {
            "输出",
            "输入",
            "输入输出"});
            this.varTypeList.Location = new System.Drawing.Point(119, 148);
            this.varTypeList.Name = "varTypeList";
            this.varTypeList.Size = new System.Drawing.Size(182, 20);
            this.varTypeList.TabIndex = 7;
            this.varTypeList.Visible = false;
            // 
            // ucVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucVariable";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox valueTextbox;
        private System.Windows.Forms.Label defaultLabel;
        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button dataBtn;
        private System.Windows.Forms.Button dataSourceBtn;
        private System.Windows.Forms.ComboBox varTypeList;
        private System.Windows.Forms.Label varTypeLabel;
    }
}
