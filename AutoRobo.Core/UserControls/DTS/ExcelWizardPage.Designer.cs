namespace AutoRobo.Core.UserControls.DTS
{
    partial class ExcelWizardPage
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFileToImport = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.chkFirstRowColumnNames = new System.Windows.Forms.CheckBox();
            this.dataGridView_preView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_preView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(472, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(108, 20);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFileToImport
            // 
            this.txtFileToImport.Location = new System.Drawing.Point(41, 20);
            this.txtFileToImport.Name = "txtFileToImport";
            this.txtFileToImport.Size = new System.Drawing.Size(396, 21);
            this.txtFileToImport.TabIndex = 14;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(26, 402);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(65, 12);
            this.lblProgress.TabIndex = 22;
            this.lblProgress.Text = "导入: 0 行";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(500, 54);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(111, 23);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "预览 (前500行)";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // chkFirstRowColumnNames
            // 
            this.chkFirstRowColumnNames.AutoSize = true;
            this.chkFirstRowColumnNames.Checked = true;
            this.chkFirstRowColumnNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirstRowColumnNames.Location = new System.Drawing.Point(28, 58);
            this.chkFirstRowColumnNames.Name = "chkFirstRowColumnNames";
            this.chkFirstRowColumnNames.Size = new System.Drawing.Size(108, 16);
            this.chkFirstRowColumnNames.TabIndex = 20;
            this.chkFirstRowColumnNames.Text = "首行包含列名称";
            this.chkFirstRowColumnNames.UseVisualStyleBackColor = true;
            // 
            // dataGridView_preView
            // 
            this.dataGridView_preView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_preView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_preView.Location = new System.Drawing.Point(28, 83);
            this.dataGridView_preView.Name = "dataGridView_preView";
            this.dataGridView_preView.RowTemplate.Height = 23;
            this.dataGridView_preView.Size = new System.Drawing.Size(597, 307);
            this.dataGridView_preView.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFileToImport);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Location = new System.Drawing.Point(28, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 49);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择EXCEL文件";
            // 
            // linkBtn
            // 
            this.linkBtn.Location = new System.Drawing.Point(383, 55);
            this.linkBtn.Name = "linkBtn";
            this.linkBtn.Size = new System.Drawing.Size(111, 23);
            this.linkBtn.TabIndex = 24;
            this.linkBtn.Text = "文档链接预览";
            this.linkBtn.UseVisualStyleBackColor = true;
            this.linkBtn.Click += new System.EventHandler(this.linkBtn_Click);
            // 
            // ImportExcelFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.chkFirstRowColumnNames);
            this.Controls.Add(this.dataGridView_preView);
            this.Name = "ImportExcelFile";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_preView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFileToImport;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.CheckBox chkFirstRowColumnNames;
        private System.Windows.Forms.DataGridView dataGridView_preView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button linkBtn;
    }
}
