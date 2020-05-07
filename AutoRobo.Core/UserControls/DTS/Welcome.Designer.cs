namespace AutoRobo.Core.UserControls.DTS
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.csvRadioBtn = new System.Windows.Forms.RadioButton();
            this.sqlserverRadioBtn = new System.Windows.Forms.RadioButton();
            this.excelRadioBtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(94, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "欢迎使用数据导入导出向导";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::AutoRobo.Core.Properties.Resources._1369654724_wizard;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(514, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(122, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // csvRadioBtn
            // 
            this.csvRadioBtn.AutoSize = true;
            this.csvRadioBtn.Location = new System.Drawing.Point(98, 184);
            this.csvRadioBtn.Name = "csvRadioBtn";
            this.csvRadioBtn.Size = new System.Drawing.Size(65, 16);
            this.csvRadioBtn.TabIndex = 13;
            this.csvRadioBtn.Tag = "FromCSV";
            this.csvRadioBtn.Text = "CSV文件";
            this.csvRadioBtn.UseVisualStyleBackColor = true;
            // 
            // sqlserverRadioBtn
            // 
            this.sqlserverRadioBtn.AutoSize = true;
            this.sqlserverRadioBtn.Location = new System.Drawing.Point(98, 218);
            this.sqlserverRadioBtn.Name = "sqlserverRadioBtn";
            this.sqlserverRadioBtn.Size = new System.Drawing.Size(101, 16);
            this.sqlserverRadioBtn.TabIndex = 12;
            this.sqlserverRadioBtn.Tag = "FromMsSql";
            this.sqlserverRadioBtn.Text = "MS SQL Server";
            this.sqlserverRadioBtn.UseVisualStyleBackColor = true;
            // 
            // excelRadioBtn
            // 
            this.excelRadioBtn.AutoSize = true;
            this.excelRadioBtn.Checked = true;
            this.excelRadioBtn.Location = new System.Drawing.Point(98, 150);
            this.excelRadioBtn.Name = "excelRadioBtn";
            this.excelRadioBtn.Size = new System.Drawing.Size(77, 16);
            this.excelRadioBtn.TabIndex = 11;
            this.excelRadioBtn.TabStop = true;
            this.excelRadioBtn.Tag = "FromExcel";
            this.excelRadioBtn.Text = "EXCEL文件";
            this.excelRadioBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(95, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "导入导出数据格式";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(1, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(892, 2);
            this.label3.TabIndex = 14;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.csvRadioBtn);
            this.Controls.Add(this.sqlserverRadioBtn);
            this.Controls.Add(this.excelRadioBtn);
            this.Controls.Add(this.label1);
            this.Name = "Welcome";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton csvRadioBtn;
        private System.Windows.Forms.RadioButton sqlserverRadioBtn;
        private System.Windows.Forms.RadioButton excelRadioBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
