namespace AutoRobo.Makers.Views
{
    partial class StartView
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startUrlBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.publishRadioButton = new System.Windows.Forms.RadioButton();
            this.collectRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.projectNameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(383, 350);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(491, 350);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.projectNameBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.startUrlBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.publishRadioButton);
            this.groupBox1.Controls.Add(this.collectRadioButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 319);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新建项目";
            // 
            // startUrlBox
            // 
            this.startUrlBox.Location = new System.Drawing.Point(50, 238);
            this.startUrlBox.Name = "startUrlBox";
            this.startUrlBox.Size = new System.Drawing.Size(398, 21);
            this.startUrlBox.TabIndex = 9;
            this.startUrlBox.Text = "http://";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "请输入脚网站地址";
            // 
            // publishRadioButton
            // 
            this.publishRadioButton.AutoSize = true;
            this.publishRadioButton.Location = new System.Drawing.Point(52, 161);
            this.publishRadioButton.Name = "publishRadioButton";
            this.publishRadioButton.Size = new System.Drawing.Size(71, 16);
            this.publishRadioButton.TabIndex = 7;
            this.publishRadioButton.Text = "发布数据";
            this.publishRadioButton.UseVisualStyleBackColor = true;
            // 
            // collectRadioButton
            // 
            this.collectRadioButton.AutoSize = true;
            this.collectRadioButton.Checked = true;
            this.collectRadioButton.Location = new System.Drawing.Point(52, 132);
            this.collectRadioButton.Name = "collectRadioButton";
            this.collectRadioButton.Size = new System.Drawing.Size(71, 16);
            this.collectRadioButton.TabIndex = 6;
            this.collectRadioButton.TabStop = true;
            this.collectRadioButton.Text = "收集数据";
            this.collectRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择您要创建的脚本用途";
            // 
            // projectNameBox
            // 
            this.projectNameBox.Location = new System.Drawing.Point(50, 68);
            this.projectNameBox.Name = "projectNameBox";
            this.projectNameBox.Size = new System.Drawing.Size(323, 21);
            this.projectNameBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "请输入项目名称";
            // 
            // StartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 389);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartView";
            this.Text = "新建项目";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox startUrlBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton publishRadioButton;
        private System.Windows.Forms.RadioButton collectRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox projectNameBox;
        private System.Windows.Forms.Label label3;

    }
}