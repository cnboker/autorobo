namespace AutoRobo.Core.UserControls.DTS
{
    partial class CSVWizardPage
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
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.gpbSeparator = new System.Windows.Forms.GroupBox();
            this.txtSeparatorOtherChar = new System.Windows.Forms.TextBox();
            this.rdbSeparatorOther = new System.Windows.Forms.RadioButton();
            this.rdbTab = new System.Windows.Forms.RadioButton();
            this.rdbComma = new System.Windows.Forms.RadioButton();
            this.chkFirstRowColumnNames = new System.Windows.Forms.CheckBox();
            this.dataGridView_preView = new System.Windows.Forms.DataGridView();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFileToImport = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbSemicolon = new System.Windows.Forms.RadioButton();
            this.gpbSeparator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_preView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(11, 402);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(65, 12);
            this.lblProgress.TabIndex = 18;
            this.lblProgress.Text = "导入: 0 行";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(519, 116);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(94, 23);
            this.btnPreview.TabIndex = 16;
            this.btnPreview.Text = "预览(前500行)";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // gpbSeparator
            // 
            this.gpbSeparator.Controls.Add(this.rdbSemicolon);
            this.gpbSeparator.Controls.Add(this.txtSeparatorOtherChar);
            this.gpbSeparator.Controls.Add(this.rdbSeparatorOther);
            this.gpbSeparator.Controls.Add(this.rdbTab);
            this.gpbSeparator.Controls.Add(this.rdbComma);
            this.gpbSeparator.Location = new System.Drawing.Point(13, 60);
            this.gpbSeparator.Name = "gpbSeparator";
            this.gpbSeparator.Size = new System.Drawing.Size(600, 42);
            this.gpbSeparator.TabIndex = 15;
            this.gpbSeparator.TabStop = false;
            this.gpbSeparator.Text = "分隔符";
            // 
            // txtSeparatorOtherChar
            // 
            this.txtSeparatorOtherChar.Location = new System.Drawing.Point(379, 15);
            this.txtSeparatorOtherChar.MaxLength = 1;
            this.txtSeparatorOtherChar.Name = "txtSeparatorOtherChar";
            this.txtSeparatorOtherChar.Size = new System.Drawing.Size(24, 21);
            this.txtSeparatorOtherChar.TabIndex = 3;
            // 
            // rdbSeparatorOther
            // 
            this.rdbSeparatorOther.AutoSize = true;
            this.rdbSeparatorOther.Location = new System.Drawing.Point(302, 20);
            this.rdbSeparatorOther.Name = "rdbSeparatorOther";
            this.rdbSeparatorOther.Size = new System.Drawing.Size(71, 16);
            this.rdbSeparatorOther.TabIndex = 2;
            this.rdbSeparatorOther.Text = "其它符号";
            this.rdbSeparatorOther.UseVisualStyleBackColor = true;
            // 
            // rdbTab
            // 
            this.rdbTab.AutoSize = true;
            this.rdbTab.Location = new System.Drawing.Point(218, 20);
            this.rdbTab.Name = "rdbTab";
            this.rdbTab.Size = new System.Drawing.Size(41, 16);
            this.rdbTab.TabIndex = 1;
            this.rdbTab.Text = "TAB";
            this.rdbTab.UseVisualStyleBackColor = true;
            // 
            // rdbComma
            // 
            this.rdbComma.AutoSize = true;
            this.rdbComma.Checked = true;
            this.rdbComma.Location = new System.Drawing.Point(38, 20);
            this.rdbComma.Name = "rdbComma";
            this.rdbComma.Size = new System.Drawing.Size(47, 16);
            this.rdbComma.TabIndex = 0;
            this.rdbComma.TabStop = true;
            this.rdbComma.Text = "逗号";
            this.rdbComma.UseVisualStyleBackColor = true;
            // 
            // chkFirstRowColumnNames
            // 
            this.chkFirstRowColumnNames.AutoSize = true;
            this.chkFirstRowColumnNames.Checked = true;
            this.chkFirstRowColumnNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirstRowColumnNames.Location = new System.Drawing.Point(13, 116);
            this.chkFirstRowColumnNames.Name = "chkFirstRowColumnNames";
            this.chkFirstRowColumnNames.Size = new System.Drawing.Size(108, 16);
            this.chkFirstRowColumnNames.TabIndex = 14;
            this.chkFirstRowColumnNames.Text = "首行包含列名称";
            this.chkFirstRowColumnNames.UseVisualStyleBackColor = true;
            // 
            // dataGridView_preView
            // 
            this.dataGridView_preView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_preView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_preView.Location = new System.Drawing.Point(13, 145);
            this.dataGridView_preView.Name = "dataGridView_preView";
            this.dataGridView_preView.RowTemplate.Height = 23;
            this.dataGridView_preView.Size = new System.Drawing.Size(600, 254);
            this.dataGridView_preView.TabIndex = 13;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(345, 21);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(108, 20);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFileToImport
            // 
            this.txtFileToImport.Location = new System.Drawing.Point(36, 20);
            this.txtFileToImport.Name = "txtFileToImport";
            this.txtFileToImport.Size = new System.Drawing.Size(292, 21);
            this.txtFileToImport.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFileToImport);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Location = new System.Drawing.Point(13, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 48);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择CSV文件";
            // 
            // rdbSemicolon
            // 
            this.rdbSemicolon.AutoSize = true;
            this.rdbSemicolon.Location = new System.Drawing.Point(128, 20);
            this.rdbSemicolon.Name = "rdbSemicolon";
            this.rdbSemicolon.Size = new System.Drawing.Size(47, 16);
            this.rdbSemicolon.TabIndex = 4;
            this.rdbSemicolon.Text = "分号";
            this.rdbSemicolon.UseVisualStyleBackColor = true;
            // 
            // ImportCSVFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.chkFirstRowColumnNames);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.gpbSeparator);
            this.Controls.Add(this.dataGridView_preView);
            this.Name = "ImportCSVFile";
            this.gpbSeparator.ResumeLayout(false);
            this.gpbSeparator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_preView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.GroupBox gpbSeparator;
        private System.Windows.Forms.TextBox txtSeparatorOtherChar;
        private System.Windows.Forms.RadioButton rdbSeparatorOther;
        private System.Windows.Forms.RadioButton rdbTab;
        private System.Windows.Forms.RadioButton rdbComma;
        private System.Windows.Forms.CheckBox chkFirstRowColumnNames;
        private System.Windows.Forms.DataGridView dataGridView_preView;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFileToImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbSemicolon;
    }
}
