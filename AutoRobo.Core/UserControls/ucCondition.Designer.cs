namespace AutoRobo.Core.UserControls
{
    partial class ucCondition
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
            this.label4 = new System.Windows.Forms.Label();
            this.ddlFormat = new System.Windows.Forms.ComboBox();
            this.patternTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ignorecaseCheckbox = new System.Windows.Forms.CheckBox();
            this.helpBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.matchModeList = new System.Windows.Forms.ComboBox();
            this.gb = new System.Windows.Forms.GroupBox();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gb);
            this.tabPage2.Controls.Add(this.matchModeList);
            this.tabPage2.Controls.Add(this.label6);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "表达式匹配";
            // 
            // ddlFormat
            // 
            this.ddlFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlFormat.FormattingEnabled = true;
            this.ddlFormat.Items.AddRange(new object[] {
            "Text",
            "Html"});
            this.ddlFormat.Location = new System.Drawing.Point(94, 66);
            this.ddlFormat.Name = "ddlFormat";
            this.ddlFormat.Size = new System.Drawing.Size(180, 20);
            this.ddlFormat.TabIndex = 19;
            // 
            // patternTextbox
            // 
            this.patternTextbox.Location = new System.Drawing.Point(94, 30);
            this.patternTextbox.Name = "patternTextbox";
            this.patternTextbox.Size = new System.Drawing.Size(180, 21);
            this.patternTextbox.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "匹配内容";
            // 
            // ignorecaseCheckbox
            // 
            this.ignorecaseCheckbox.AutoSize = true;
            this.ignorecaseCheckbox.Location = new System.Drawing.Point(94, 109);
            this.ignorecaseCheckbox.Name = "ignorecaseCheckbox";
            this.ignorecaseCheckbox.Size = new System.Drawing.Size(84, 16);
            this.ignorecaseCheckbox.TabIndex = 22;
            this.ignorecaseCheckbox.Text = "区分大小写";
            this.ignorecaseCheckbox.UseVisualStyleBackColor = true;
            // 
            // helpBtn
            // 
            this.helpBtn.Location = new System.Drawing.Point(280, 28);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(75, 23);
            this.helpBtn.TabIndex = 23;
            this.helpBtn.Text = "帮助";
            this.helpBtn.UseVisualStyleBackColor = true;
            this.helpBtn.Click += new System.EventHandler(this.helpBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "匹配模式";
            // 
            // matchModeList
            // 
            this.matchModeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.matchModeList.FormattingEnabled = true;
            this.matchModeList.Items.AddRange(new object[] {
            "当前元素存在",
            "元素内容匹配"});
            this.matchModeList.Location = new System.Drawing.Point(103, 35);
            this.matchModeList.Name = "matchModeList";
            this.matchModeList.Size = new System.Drawing.Size(247, 20);
            this.matchModeList.TabIndex = 25;
            this.matchModeList.SelectedIndexChanged += new System.EventHandler(this.matchModeList_SelectedIndexChanged);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.ddlFormat);
            this.gb.Controls.Add(this.patternTextbox);
            this.gb.Controls.Add(this.label4);
            this.gb.Controls.Add(this.helpBtn);
            this.gb.Controls.Add(this.label3);
            this.gb.Controls.Add(this.ignorecaseCheckbox);
            this.gb.Location = new System.Drawing.Point(27, 73);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(370, 152);
            this.gb.TabIndex = 26;
            this.gb.TabStop = false;
            this.gb.Visible = false;
            // 
            // ucCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucCondition";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlFormat;
        private System.Windows.Forms.TextBox patternTextbox;
        private System.Windows.Forms.CheckBox ignorecaseCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button helpBtn;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.ComboBox matchModeList;
        private System.Windows.Forms.Label label6;
    }
}
