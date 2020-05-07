namespace AutoRobo.Core.UserControls
{
    partial class StringRegexForm
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
            this.javascriptTipLabel = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.testBtn = new System.Windows.Forms.Button();
            this.scriptRadio = new System.Windows.Forms.RadioButton();
            this.replaceRadioButton = new System.Windows.Forms.RadioButton();
            this.sourceTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.targetTextbox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.regxPage = new System.Windows.Forms.TabPage();
            this.replacePage = new System.Windows.Forms.TabPage();
            this.regexRadio = new System.Windows.Forms.RadioButton();
            this.regexHelper = new System.Windows.Forms.ComboBox();
            this.regexTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.javascriptPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.regxPage.SuspendLayout();
            this.replacePage.SuspendLayout();
            this.javascriptPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // javascriptTipLabel
            // 
            this.javascriptTipLabel.AutoSize = true;
            this.javascriptTipLabel.Location = new System.Drawing.Point(20, 37);
            this.javascriptTipLabel.Name = "javascriptTipLabel";
            this.javascriptTipLabel.Size = new System.Drawing.Size(257, 12);
            this.javascriptTipLabel.TabIndex = 0;
            this.javascriptTipLabel.Text = "javascript处理字符串，输入参数arguments[0]";
            this.javascriptTipLabel.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(22, 66);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(419, 230);
            this.textBox.TabIndex = 1;
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(119, 453);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(221, 453);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(17, 453);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(75, 23);
            this.testBtn.TabIndex = 4;
            this.testBtn.Text = "测试";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // scriptRadio
            // 
            this.scriptRadio.AutoSize = true;
            this.scriptRadio.Location = new System.Drawing.Point(208, 17);
            this.scriptRadio.Name = "scriptRadio";
            this.scriptRadio.Size = new System.Drawing.Size(83, 16);
            this.scriptRadio.TabIndex = 6;
            this.scriptRadio.Text = "javascript";
            this.scriptRadio.UseVisualStyleBackColor = true;
            // 
            // replaceRadioButton
            // 
            this.replaceRadioButton.AutoSize = true;
            this.replaceRadioButton.Location = new System.Drawing.Point(110, 17);
            this.replaceRadioButton.Name = "replaceRadioButton";
            this.replaceRadioButton.Size = new System.Drawing.Size(83, 16);
            this.replaceRadioButton.TabIndex = 8;
            this.replaceRadioButton.Text = "字符串替换";
            this.replaceRadioButton.UseVisualStyleBackColor = true;
            // 
            // sourceTextbox
            // 
            this.sourceTextbox.Location = new System.Drawing.Point(18, 47);
            this.sourceTextbox.Name = "sourceTextbox";
            this.sourceTextbox.Size = new System.Drawing.Size(141, 21);
            this.sourceTextbox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "到";
            // 
            // targetTextbox
            // 
            this.targetTextbox.Location = new System.Drawing.Point(202, 47);
            this.targetTextbox.Name = "targetTextbox";
            this.targetTextbox.Size = new System.Drawing.Size(179, 21);
            this.targetTextbox.TabIndex = 11;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(18, 94);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(409, 172);
            this.listBox1.TabIndex = 12;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(394, 45);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(31, 23);
            this.addButton.TabIndex = 13;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.regxPage);
            this.tabControl1.Controls.Add(this.replacePage);
            this.tabControl1.Controls.Add(this.javascriptPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 385);
            this.tabControl1.TabIndex = 14;
            // 
            // regxPage
            // 
            this.regxPage.Controls.Add(this.label3);
            this.regxPage.Controls.Add(this.label2);
            this.regxPage.Controls.Add(this.regexTextbox);
            this.regxPage.Controls.Add(this.regexHelper);
            this.regxPage.Location = new System.Drawing.Point(4, 22);
            this.regxPage.Name = "regxPage";
            this.regxPage.Padding = new System.Windows.Forms.Padding(3);
            this.regxPage.Size = new System.Drawing.Size(460, 359);
            this.regxPage.TabIndex = 0;
            this.regxPage.Text = "正则表达式";
            this.regxPage.UseVisualStyleBackColor = true;
            // 
            // replacePage
            // 
            this.replacePage.Controls.Add(this.listBox1);
            this.replacePage.Controls.Add(this.addButton);
            this.replacePage.Controls.Add(this.sourceTextbox);
            this.replacePage.Controls.Add(this.label1);
            this.replacePage.Controls.Add(this.targetTextbox);
            this.replacePage.Location = new System.Drawing.Point(4, 22);
            this.replacePage.Name = "replacePage";
            this.replacePage.Padding = new System.Windows.Forms.Padding(3);
            this.replacePage.Size = new System.Drawing.Size(460, 359);
            this.replacePage.TabIndex = 1;
            this.replacePage.Text = "字符串替换";
            this.replacePage.UseVisualStyleBackColor = true;
            // 
            // regexRadio
            // 
            this.regexRadio.AutoSize = true;
            this.regexRadio.Checked = true;
            this.regexRadio.Location = new System.Drawing.Point(12, 17);
            this.regexRadio.Name = "regexRadio";
            this.regexRadio.Size = new System.Drawing.Size(83, 16);
            this.regexRadio.TabIndex = 5;
            this.regexRadio.TabStop = true;
            this.regexRadio.Text = "正则表达式";
            this.regexRadio.UseVisualStyleBackColor = true;
            // 
            // regexHelper
            // 
            this.regexHelper.FormattingEnabled = true;
            this.regexHelper.Items.AddRange(new object[] {
            "提取手机号码 \\d{11}",
            "提取数字 \\d+",
            "金额 \\d+(.\\d{1,2})?",
            "提取电话号码 (([0-9]{11})|((400|800)([0-9\\\\-]{7,10})|(([0-9]{4}|[0-9]{3})(-| )?)?([0-9]" +
                "{7,8})((-| |转)*([0-9]{1,4}))?))"});
            this.regexHelper.Location = new System.Drawing.Point(118, 57);
            this.regexHelper.Name = "regexHelper";
            this.regexHelper.Size = new System.Drawing.Size(285, 20);
            this.regexHelper.TabIndex = 8;
            // 
            // regexTextbox
            // 
            this.regexTextbox.Location = new System.Drawing.Point(118, 100);
            this.regexTextbox.Multiline = true;
            this.regexTextbox.Name = "regexTextbox";
            this.regexTextbox.Size = new System.Drawing.Size(285, 207);
            this.regexTextbox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "常用表达式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "自定义表达式";
            // 
            // javascriptPage
            // 
            this.javascriptPage.Controls.Add(this.textBox);
            this.javascriptPage.Controls.Add(this.javascriptTipLabel);
            this.javascriptPage.Location = new System.Drawing.Point(4, 22);
            this.javascriptPage.Name = "javascriptPage";
            this.javascriptPage.Padding = new System.Windows.Forms.Padding(3);
            this.javascriptPage.Size = new System.Drawing.Size(460, 359);
            this.javascriptPage.TabIndex = 2;
            this.javascriptPage.Text = "javascript";
            this.javascriptPage.UseVisualStyleBackColor = true;
            // 
            // StringRegexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 496);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.replaceRadioButton);
            this.Controls.Add(this.scriptRadio);
            this.Controls.Add(this.regexRadio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StringRegexForm";
            this.Text = "字符串处理";
            this.tabControl1.ResumeLayout(false);
            this.regxPage.ResumeLayout(false);
            this.regxPage.PerformLayout();
            this.replacePage.ResumeLayout(false);
            this.replacePage.PerformLayout();
            this.javascriptPage.ResumeLayout(false);
            this.javascriptPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label javascriptTipLabel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.RadioButton scriptRadio;
        private System.Windows.Forms.RadioButton replaceRadioButton;
        private System.Windows.Forms.TextBox sourceTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox targetTextbox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage regxPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox regexTextbox;
        private System.Windows.Forms.ComboBox regexHelper;
        private System.Windows.Forms.TabPage replacePage;
        private System.Windows.Forms.TabPage javascriptPage;
        private System.Windows.Forms.RadioButton regexRadio;
    }
}