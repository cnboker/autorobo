namespace AutoRobo.ClientLib.Ants
{
    partial class BrowserSettings
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
            this.components = new System.ComponentModel.Container();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.g1 = new System.Windows.Forms.GroupBox();
            this.enableScriptError = new System.Windows.Forms.CheckBox();
            this.allowAlert = new System.Windows.Forms.CheckBox();
            this.downloadFlash = new System.Windows.Forms.CheckBox();
            this.downloadActivex = new System.Windows.Forms.CheckBox();
            this.downloadSounds = new System.Windows.Forms.CheckBox();
            this.downloadVideo = new System.Windows.Forms.CheckBox();
            this.downloadImages = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.clrCacheBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.adslGroupBox = new System.Windows.Forms.GroupBox();
            this.TestBtn = new System.Windows.Forms.Button();
            this.dialTimeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.adslNameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.adslRadioButton = new System.Windows.Forms.RadioButton();
            this.systemRadioButton = new System.Windows.Forms.RadioButton();
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.selectFolderBtn = new System.Windows.Forms.Button();
            this.defaultPathTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBoxProxy = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxInterval = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxRotateProxy = new System.Windows.Forms.CheckBox();
            this.proxyDownBtn = new System.Windows.Forms.Button();
            this.proxyUpBtn = new System.Windows.Forms.Button();
            this.proxyDeleteBtn = new System.Windows.Forms.Button();
            this.proxyTestBtn = new System.Windows.Forms.Button();
            this.importBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.proxyAddBtn = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.checkBoxRequiredAuth = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.checkBoxEnableProxy = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.offlineCheckBox = new System.Windows.Forms.CheckBox();
            this.clrBtn = new System.Windows.Forms.Button();
            this.g1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.adslGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(411, 399);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(308, 399);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // g1
            // 
            this.g1.Controls.Add(this.enableScriptError);
            this.g1.Controls.Add(this.allowAlert);
            this.g1.Controls.Add(this.downloadFlash);
            this.g1.Controls.Add(this.downloadActivex);
            this.g1.Controls.Add(this.downloadSounds);
            this.g1.Controls.Add(this.downloadVideo);
            this.g1.Controls.Add(this.downloadImages);
            this.g1.Dock = System.Windows.Forms.DockStyle.Top;
            this.g1.Location = new System.Drawing.Point(3, 3);
            this.g1.Name = "g1";
            this.g1.Size = new System.Drawing.Size(499, 243);
            this.g1.TabIndex = 3;
            this.g1.TabStop = false;
            this.g1.Enter += new System.EventHandler(this.g1_Enter);
            // 
            // enableScriptError
            // 
            this.enableScriptError.AutoSize = true;
            this.enableScriptError.Location = new System.Drawing.Point(23, 152);
            this.enableScriptError.Name = "enableScriptError";
            this.enableScriptError.Size = new System.Drawing.Size(120, 16);
            this.enableScriptError.TabIndex = 6;
            this.enableScriptError.Text = "允许弹出脚本错误";
            this.enableScriptError.UseVisualStyleBackColor = true;
            // 
            // allowAlert
            // 
            this.allowAlert.AutoSize = true;
            this.allowAlert.Location = new System.Drawing.Point(23, 115);
            this.allowAlert.Name = "allowAlert";
            this.allowAlert.Size = new System.Drawing.Size(108, 16);
            this.allowAlert.TabIndex = 5;
            this.allowAlert.Text = "允许弹出提示框";
            this.allowAlert.UseVisualStyleBackColor = true;
            // 
            // downloadFlash
            // 
            this.downloadFlash.AutoSize = true;
            this.downloadFlash.Checked = true;
            this.downloadFlash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadFlash.Location = new System.Drawing.Point(138, 31);
            this.downloadFlash.Name = "downloadFlash";
            this.downloadFlash.Size = new System.Drawing.Size(78, 16);
            this.downloadFlash.TabIndex = 4;
            this.downloadFlash.Text = "下载Flash";
            this.downloadFlash.UseVisualStyleBackColor = true;
            // 
            // downloadActivex
            // 
            this.downloadActivex.AutoSize = true;
            this.downloadActivex.Checked = true;
            this.downloadActivex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadActivex.Location = new System.Drawing.Point(259, 31);
            this.downloadActivex.Name = "downloadActivex";
            this.downloadActivex.Size = new System.Drawing.Size(90, 16);
            this.downloadActivex.TabIndex = 3;
            this.downloadActivex.Text = "下载Activex";
            this.downloadActivex.UseVisualStyleBackColor = true;
            // 
            // downloadSounds
            // 
            this.downloadSounds.AutoSize = true;
            this.downloadSounds.Checked = true;
            this.downloadSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadSounds.Location = new System.Drawing.Point(138, 69);
            this.downloadSounds.Name = "downloadSounds";
            this.downloadSounds.Size = new System.Drawing.Size(72, 16);
            this.downloadSounds.TabIndex = 2;
            this.downloadSounds.Text = "下载声音";
            this.downloadSounds.UseVisualStyleBackColor = true;
            // 
            // downloadVideo
            // 
            this.downloadVideo.AutoSize = true;
            this.downloadVideo.Checked = true;
            this.downloadVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadVideo.Location = new System.Drawing.Point(23, 69);
            this.downloadVideo.Name = "downloadVideo";
            this.downloadVideo.Size = new System.Drawing.Size(72, 16);
            this.downloadVideo.TabIndex = 1;
            this.downloadVideo.Text = "下载视频";
            this.downloadVideo.UseVisualStyleBackColor = true;
            // 
            // downloadImages
            // 
            this.downloadImages.AutoSize = true;
            this.downloadImages.Checked = true;
            this.downloadImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.downloadImages.Location = new System.Drawing.Point(23, 31);
            this.downloadImages.Name = "downloadImages";
            this.downloadImages.Size = new System.Drawing.Size(72, 16);
            this.downloadImages.TabIndex = 0;
            this.downloadImages.Text = "下载图片";
            this.downloadImages.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // clrCacheBtn
            // 
            this.clrCacheBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.clrCacheBtn.Location = new System.Drawing.Point(38, 79);
            this.clrCacheBtn.Name = "clrCacheBtn";
            this.clrCacheBtn.Size = new System.Drawing.Size(123, 23);
            this.clrCacheBtn.TabIndex = 7;
            this.clrCacheBtn.Text = "清除缓存";
            this.clrCacheBtn.UseVisualStyleBackColor = true;
            this.clrCacheBtn.Click += new System.EventHandler(this.clrCacheBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clrCacheBtn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 367);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 367);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.adslGroupBox);
            this.groupBox5.Controls.Add(this.adslRadioButton);
            this.groupBox5.Controls.Add(this.systemRadioButton);
            this.groupBox5.Controls.Add(this.noneRadioButton);
            this.groupBox5.Location = new System.Drawing.Point(10, 29);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(489, 203);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "网络连接设置";
            // 
            // adslGroupBox
            // 
            this.adslGroupBox.Controls.Add(this.TestBtn);
            this.adslGroupBox.Controls.Add(this.dialTimeBox);
            this.adslGroupBox.Controls.Add(this.label4);
            this.adslGroupBox.Controls.Add(this.button1);
            this.adslGroupBox.Controls.Add(this.adslNameBox);
            this.adslGroupBox.Controls.Add(this.label3);
            this.adslGroupBox.Location = new System.Drawing.Point(24, 95);
            this.adslGroupBox.Name = "adslGroupBox";
            this.adslGroupBox.Size = new System.Drawing.Size(440, 92);
            this.adslGroupBox.TabIndex = 10;
            this.adslGroupBox.TabStop = false;
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(366, 55);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(68, 23);
            this.TestBtn.TabIndex = 15;
            this.TestBtn.Text = "测试";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // dialTimeBox
            // 
            this.dialTimeBox.Location = new System.Drawing.Point(129, 57);
            this.dialTimeBox.Name = "dialTimeBox";
            this.dialTimeBox.Size = new System.Drawing.Size(115, 21);
            this.dialTimeBox.TabIndex = 14;
            this.dialTimeBox.Text = "300";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "重新拨号周期(秒)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(366, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "帮助";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // adslNameBox
            // 
            this.adslNameBox.Location = new System.Drawing.Point(129, 21);
            this.adslNameBox.Name = "adslNameBox";
            this.adslNameBox.Size = new System.Drawing.Size(159, 21);
            this.adslNameBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "ADSL名称";
            // 
            // adslRadioButton
            // 
            this.adslRadioButton.AutoSize = true;
            this.adslRadioButton.Location = new System.Drawing.Point(24, 73);
            this.adslRadioButton.Name = "adslRadioButton";
            this.adslRadioButton.Size = new System.Drawing.Size(119, 16);
            this.adslRadioButton.TabIndex = 9;
            this.adslRadioButton.TabStop = true;
            this.adslRadioButton.Text = "ADSL自动拨号设置";
            this.adslRadioButton.UseVisualStyleBackColor = true;
            // 
            // systemRadioButton
            // 
            this.systemRadioButton.AutoSize = true;
            this.systemRadioButton.Location = new System.Drawing.Point(24, 51);
            this.systemRadioButton.Name = "systemRadioButton";
            this.systemRadioButton.Size = new System.Drawing.Size(95, 16);
            this.systemRadioButton.TabIndex = 8;
            this.systemRadioButton.TabStop = true;
            this.systemRadioButton.Text = "使用系统代理";
            this.systemRadioButton.UseVisualStyleBackColor = true;
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Checked = true;
            this.noneRadioButton.Location = new System.Drawing.Point(24, 29);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(59, 16);
            this.noneRadioButton.TabIndex = 7;
            this.noneRadioButton.TabStop = true;
            this.noneRadioButton.Text = "无代理";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(513, 393);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.g1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(505, 367);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数设定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.selectFolderBtn);
            this.groupBox4.Controls.Add(this.defaultPathTextBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 246);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(499, 118);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // selectFolderBtn
            // 
            this.selectFolderBtn.Location = new System.Drawing.Point(383, 47);
            this.selectFolderBtn.Name = "selectFolderBtn";
            this.selectFolderBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFolderBtn.TabIndex = 2;
            this.selectFolderBtn.Text = "浏览";
            this.selectFolderBtn.UseVisualStyleBackColor = true;
            this.selectFolderBtn.Visible = false;
            this.selectFolderBtn.Click += new System.EventHandler(this.selectFolderBtn_Click);
            // 
            // defaultPathTextBox
            // 
            this.defaultPathTextBox.Location = new System.Drawing.Point(78, 47);
            this.defaultPathTextBox.Name = "defaultPathTextBox";
            this.defaultPathTextBox.ReadOnly = true;
            this.defaultPathTextBox.Size = new System.Drawing.Size(298, 21);
            this.defaultPathTextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "库路径";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.clrBtn);
            this.tabPage2.Controls.Add(this.listBoxProxy);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.textBoxInterval);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.checkBoxRotateProxy);
            this.tabPage2.Controls.Add(this.proxyDownBtn);
            this.tabPage2.Controls.Add(this.proxyUpBtn);
            this.tabPage2.Controls.Add(this.proxyDeleteBtn);
            this.tabPage2.Controls.Add(this.proxyTestBtn);
            this.tabPage2.Controls.Add(this.importBtn);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.proxyAddBtn);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.checkBoxEnableProxy);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(505, 367);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置代理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBoxProxy
            // 
            this.listBoxProxy.FullRowSelect = true;
            this.listBoxProxy.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listBoxProxy.Location = new System.Drawing.Point(43, 165);
            this.listBoxProxy.Name = "listBoxProxy";
            this.listBoxProxy.Size = new System.Drawing.Size(312, 144);
            this.listBoxProxy.TabIndex = 21;
            this.listBoxProxy.UseCompatibleStateImageBehavior = false;
            this.listBoxProxy.View = System.Windows.Forms.View.Details;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(217, 328);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "分钟";
            // 
            // textBoxInterval
            // 
            this.textBoxInterval.Location = new System.Drawing.Point(176, 322);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new System.Drawing.Size(35, 21);
            this.textBoxInterval.TabIndex = 19;
            this.textBoxInterval.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 328);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "每";
            // 
            // checkBoxRotateProxy
            // 
            this.checkBoxRotateProxy.AutoSize = true;
            this.checkBoxRotateProxy.Location = new System.Drawing.Point(33, 324);
            this.checkBoxRotateProxy.Name = "checkBoxRotateProxy";
            this.checkBoxRotateProxy.Size = new System.Drawing.Size(96, 16);
            this.checkBoxRotateProxy.TabIndex = 17;
            this.checkBoxRotateProxy.Text = "动态切换代理";
            this.checkBoxRotateProxy.UseVisualStyleBackColor = true;
            this.checkBoxRotateProxy.CheckedChanged += new System.EventHandler(this.useProxyCheckBox_CheckedChanged);
            // 
            // proxyDownBtn
            // 
            this.proxyDownBtn.Location = new System.Drawing.Point(375, 273);
            this.proxyDownBtn.Name = "proxyDownBtn";
            this.proxyDownBtn.Size = new System.Drawing.Size(75, 23);
            this.proxyDownBtn.TabIndex = 16;
            this.proxyDownBtn.Text = "下移";
            this.proxyDownBtn.UseVisualStyleBackColor = true;
            this.proxyDownBtn.Click += new System.EventHandler(this.proxyDownBtn_Click);
            // 
            // proxyUpBtn
            // 
            this.proxyUpBtn.Location = new System.Drawing.Point(375, 246);
            this.proxyUpBtn.Name = "proxyUpBtn";
            this.proxyUpBtn.Size = new System.Drawing.Size(75, 23);
            this.proxyUpBtn.TabIndex = 15;
            this.proxyUpBtn.Text = "上移";
            this.proxyUpBtn.UseVisualStyleBackColor = true;
            this.proxyUpBtn.Click += new System.EventHandler(this.proxyUpBtn_Click);
            // 
            // proxyDeleteBtn
            // 
            this.proxyDeleteBtn.Location = new System.Drawing.Point(375, 219);
            this.proxyDeleteBtn.Name = "proxyDeleteBtn";
            this.proxyDeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.proxyDeleteBtn.TabIndex = 14;
            this.proxyDeleteBtn.Text = "删除";
            this.proxyDeleteBtn.UseVisualStyleBackColor = true;
            this.proxyDeleteBtn.Click += new System.EventHandler(this.proxyDeleteBtn_Click);
            // 
            // proxyTestBtn
            // 
            this.proxyTestBtn.Location = new System.Drawing.Point(375, 192);
            this.proxyTestBtn.Name = "proxyTestBtn";
            this.proxyTestBtn.Size = new System.Drawing.Size(75, 23);
            this.proxyTestBtn.TabIndex = 13;
            this.proxyTestBtn.Text = "代理测试";
            this.proxyTestBtn.UseVisualStyleBackColor = true;
            this.proxyTestBtn.Click += new System.EventHandler(this.proxyTestBtn_Click);
            // 
            // importBtn
            // 
            this.importBtn.Location = new System.Drawing.Point(375, 165);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(75, 23);
            this.importBtn.TabIndex = 12;
            this.importBtn.Text = "导入";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "代理列表";
            // 
            // proxyAddBtn
            // 
            this.proxyAddBtn.Location = new System.Drawing.Point(375, 66);
            this.proxyAddBtn.Name = "proxyAddBtn";
            this.proxyAddBtn.Size = new System.Drawing.Size(96, 38);
            this.proxyAddBtn.TabIndex = 9;
            this.proxyAddBtn.Text = "增加到列表";
            this.proxyAddBtn.UseVisualStyleBackColor = true;
            this.proxyAddBtn.Click += new System.EventHandler(this.proxyAddBtn_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.textBoxPassword);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.textBoxUserName);
            this.groupBox6.Controls.Add(this.addressBox);
            this.groupBox6.Controls.Add(this.checkBoxRequiredAuth);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.portBox);
            this.groupBox6.Location = new System.Drawing.Point(33, 40);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(322, 106);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "用户名";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(237, 73);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(59, 21);
            this.textBoxPassword.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(74, 73);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(104, 21);
            this.textBoxUserName.TabIndex = 7;
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(74, 20);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(104, 21);
            this.addressBox.TabIndex = 1;
            // 
            // checkBoxRequiredAuth
            // 
            this.checkBoxRequiredAuth.AutoSize = true;
            this.checkBoxRequiredAuth.Location = new System.Drawing.Point(74, 50);
            this.checkBoxRequiredAuth.Name = "checkBoxRequiredAuth";
            this.checkBoxRequiredAuth.Size = new System.Drawing.Size(72, 16);
            this.checkBoxRequiredAuth.TabIndex = 6;
            this.checkBoxRequiredAuth.Text = "需要验证";
            this.checkBoxRequiredAuth.UseVisualStyleBackColor = true;
            this.checkBoxRequiredAuth.CheckedChanged += new System.EventHandler(this.useProxyCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(237, 20);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(59, 21);
            this.portBox.TabIndex = 3;
            // 
            // checkBoxEnableProxy
            // 
            this.checkBoxEnableProxy.AutoSize = true;
            this.checkBoxEnableProxy.Location = new System.Drawing.Point(33, 18);
            this.checkBoxEnableProxy.Name = "checkBoxEnableProxy";
            this.checkBoxEnableProxy.Size = new System.Drawing.Size(120, 16);
            this.checkBoxEnableProxy.TabIndex = 7;
            this.checkBoxEnableProxy.Text = "启动代理访问网络";
            this.checkBoxEnableProxy.UseVisualStyleBackColor = true;
            this.checkBoxEnableProxy.CheckedChanged += new System.EventHandler(this.useProxyCheckBox_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(505, 367);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "缓冲管理";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(505, 367);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "网络代理设定";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.debugCheckBox);
            this.tabPage5.Controls.Add(this.offlineCheckBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(505, 367);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "模式";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(40, 96);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(72, 16);
            this.debugCheckBox.TabIndex = 1;
            this.debugCheckBox.Text = "调试模式";
            this.debugCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.debugCheckBox.UseVisualStyleBackColor = true;
            // 
            // offlineCheckBox
            // 
            this.offlineCheckBox.AutoSize = true;
            this.offlineCheckBox.Location = new System.Drawing.Point(40, 56);
            this.offlineCheckBox.Name = "offlineCheckBox";
            this.offlineCheckBox.Size = new System.Drawing.Size(72, 16);
            this.offlineCheckBox.TabIndex = 0;
            this.offlineCheckBox.Text = "离线模式";
            this.offlineCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.offlineCheckBox.UseVisualStyleBackColor = true;
            this.offlineCheckBox.Visible = false;
            // 
            // clrBtn
            // 
            this.clrBtn.Location = new System.Drawing.Point(375, 300);
            this.clrBtn.Name = "clrBtn";
            this.clrBtn.Size = new System.Drawing.Size(75, 23);
            this.clrBtn.TabIndex = 22;
            this.clrBtn.Text = "清除";
            this.clrBtn.UseVisualStyleBackColor = true;
            this.clrBtn.Click += new System.EventHandler(this.clrBtn_Click);
            // 
            // BrowserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 432);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "BrowserSettings";
            this.Text = "选项";
            this.g1.ResumeLayout(false);
            this.g1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.adslGroupBox.ResumeLayout(false);
            this.adslGroupBox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.GroupBox g1;
        private System.Windows.Forms.CheckBox downloadVideo;
        private System.Windows.Forms.CheckBox downloadImages;
        private System.Windows.Forms.CheckBox downloadActivex;
        private System.Windows.Forms.CheckBox downloadSounds;
        private System.Windows.Forms.CheckBox downloadFlash;
        private System.Windows.Forms.CheckBox allowAlert;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button clrCacheBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton adslRadioButton;
        private System.Windows.Forms.RadioButton systemRadioButton;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox adslGroupBox;
        private System.Windows.Forms.TextBox adslNameBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox dialTimeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.CheckBox enableScriptError;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox offlineCheckBox;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button selectFolderBtn;
        private System.Windows.Forms.TextBox defaultPathTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button proxyUpBtn;
        private System.Windows.Forms.Button proxyDeleteBtn;
        private System.Windows.Forms.Button proxyTestBtn;
        private System.Windows.Forms.Button importBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button proxyAddBtn;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.CheckBox checkBoxRequiredAuth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.CheckBox checkBoxEnableProxy;
        private System.Windows.Forms.Button proxyDownBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxInterval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxRotateProxy;
        private System.Windows.Forms.ListView listBoxProxy;
        private System.Windows.Forms.Button clrBtn;
    }
}