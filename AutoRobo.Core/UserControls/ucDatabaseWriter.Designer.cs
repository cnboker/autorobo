namespace AutoRobo.Core.UserControls
{
    partial class ucDatabaseWriter
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
            this.comboBoxServerType = new System.Windows.Forms.ComboBox();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.labelPwd = new System.Windows.Forms.Label();
            this.labelUName = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.checkBoxWinAuth = new System.Windows.Forms.CheckBox();
            this.tableNameList = new AutoRobo.Core.UserControls.TableVariableListBox();
            this.labelTableName = new System.Windows.Forms.Label();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.labelDBName = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.connectionTestBtn = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.groupBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.connectionTestBtn);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.comboBoxServerType);
            this.tabPage2.Controls.Add(this.groupBoxLogin);
            this.tabPage2.Controls.Add(this.tableNameList);
            this.tabPage2.Controls.Add(this.labelTableName);
            this.tabPage2.Controls.Add(this.textBoxDBName);
            this.tabPage2.Controls.Add(this.labelDBName);
            this.tabPage2.Controls.Add(this.labelAddress);
            this.tabPage2.Controls.Add(this.textBoxServerAddress);
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
            this.label3.Location = new System.Drawing.Point(25, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "数据库类型";
            // 
            // comboBoxServerType
            // 
            this.comboBoxServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServerType.FormattingEnabled = true;
            this.comboBoxServerType.Items.AddRange(new object[] {
            "MS SQL Server",
            "MySQL"});
            this.comboBoxServerType.Location = new System.Drawing.Point(113, 41);
            this.comboBoxServerType.Name = "comboBoxServerType";
            this.comboBoxServerType.Size = new System.Drawing.Size(192, 20);
            this.comboBoxServerType.TabIndex = 15;
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.labelPwd);
            this.groupBoxLogin.Controls.Add(this.labelUName);
            this.groupBoxLogin.Controls.Add(this.textBoxPassword);
            this.groupBoxLogin.Controls.Add(this.textBoxUserName);
            this.groupBoxLogin.Controls.Add(this.checkBoxWinAuth);
            this.groupBoxLogin.Location = new System.Drawing.Point(27, 67);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(278, 110);
            this.groupBoxLogin.TabIndex = 10;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "登录到服务器";
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Location = new System.Drawing.Point(35, 81);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(29, 12);
            this.labelPwd.TabIndex = 7;
            this.labelPwd.Text = "密码";
            // 
            // labelUName
            // 
            this.labelUName.AutoSize = true;
            this.labelUName.Location = new System.Drawing.Point(35, 55);
            this.labelUName.Name = "labelUName";
            this.labelUName.Size = new System.Drawing.Size(41, 12);
            this.labelUName.TabIndex = 6;
            this.labelUName.Text = "用户名";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(101, 78);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(154, 21);
            this.textBoxPassword.TabIndex = 2;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Enabled = false;
            this.textBoxUserName.Location = new System.Drawing.Point(101, 52);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(154, 21);
            this.textBoxUserName.TabIndex = 1;
            // 
            // checkBoxWinAuth
            // 
            this.checkBoxWinAuth.AutoSize = true;
            this.checkBoxWinAuth.Location = new System.Drawing.Point(37, 20);
            this.checkBoxWinAuth.Name = "checkBoxWinAuth";
            this.checkBoxWinAuth.Size = new System.Drawing.Size(90, 16);
            this.checkBoxWinAuth.TabIndex = 0;
            this.checkBoxWinAuth.Text = "windows验证";
            this.checkBoxWinAuth.UseVisualStyleBackColor = true;
            // 
            // tableNameList
            // 
            this.tableNameList.Location = new System.Drawing.Point(113, 227);
            this.tableNameList.Name = "tableNameList";
            this.tableNameList.Size = new System.Drawing.Size(192, 84);
            this.tableNameList.TabIndex = 12;
            // 
            // labelTableName
            // 
            this.labelTableName.AutoSize = true;
            this.labelTableName.Location = new System.Drawing.Point(25, 227);
            this.labelTableName.Name = "labelTableName";
            this.labelTableName.Size = new System.Drawing.Size(41, 12);
            this.labelTableName.TabIndex = 14;
            this.labelTableName.Text = "表名称";
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(113, 183);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(192, 21);
            this.textBoxDBName.TabIndex = 11;
            // 
            // labelDBName
            // 
            this.labelDBName.AutoSize = true;
            this.labelDBName.Location = new System.Drawing.Point(27, 186);
            this.labelDBName.Name = "labelDBName";
            this.labelDBName.Size = new System.Drawing.Size(65, 12);
            this.labelDBName.TabIndex = 13;
            this.labelDBName.Text = "数据库名称";
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(25, 19);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(77, 12);
            this.labelAddress.TabIndex = 8;
            this.labelAddress.Text = "数据库服务器";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(113, 16);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(192, 21);
            this.textBoxServerAddress.TabIndex = 9;
            // 
            // connectionTestBtn
            // 
            this.connectionTestBtn.Location = new System.Drawing.Point(113, 317);
            this.connectionTestBtn.Name = "connectionTestBtn";
            this.connectionTestBtn.Size = new System.Drawing.Size(75, 23);
            this.connectionTestBtn.TabIndex = 17;
            this.connectionTestBtn.Text = "测试连接";
            this.connectionTestBtn.UseVisualStyleBackColor = true;
            this.connectionTestBtn.Click += new System.EventHandler(this.connectionTestBtn_Click);
            // 
            // ucDatabaseWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDatabaseWriter";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxServerType;
        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.Label labelUName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.CheckBox checkBoxWinAuth;
        private TableVariableListBox tableNameList;
        private System.Windows.Forms.Label labelTableName;
        private System.Windows.Forms.TextBox textBoxDBName;
        private System.Windows.Forms.Label labelDBName;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Button connectionTestBtn;
    }
}
