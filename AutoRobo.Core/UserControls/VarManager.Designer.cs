namespace AutoRobo.Core.UserControls
{
    partial class VarManager
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
            this.leftPanel = new System.Windows.Forms.Panel();
            this.leftTopPanel = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.leftBottomPanel = new System.Windows.Forms.Panel();
            this.delBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.okBtn = new System.Windows.Forms.Button();
            this.directComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.defaultValueTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.namebox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.leftPanel.SuspendLayout();
            this.leftTopPanel.SuspendLayout();
            this.leftBottomPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.leftTopPanel);
            this.leftPanel.Controls.Add(this.leftBottomPanel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(234, 451);
            this.leftPanel.TabIndex = 0;
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Controls.Add(this.listView1);
            this.leftTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTopPanel.Location = new System.Drawing.Point(0, 0);
            this.leftTopPanel.Name = "leftTopPanel";
            this.leftTopPanel.Size = new System.Drawing.Size(234, 390);
            this.leftTopPanel.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(234, 390);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // leftBottomPanel
            // 
            this.leftBottomPanel.Controls.Add(this.delBtn);
            this.leftBottomPanel.Controls.Add(this.addBtn);
            this.leftBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.leftBottomPanel.Location = new System.Drawing.Point(0, 390);
            this.leftBottomPanel.Name = "leftBottomPanel";
            this.leftBottomPanel.Size = new System.Drawing.Size(234, 61);
            this.leftBottomPanel.TabIndex = 0;
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(126, 17);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(75, 23);
            this.delBtn.TabIndex = 6;
            this.delBtn.Text = "删除";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(29, 17);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 0;
            this.addBtn.Text = "增加";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.okBtn);
            this.rightPanel.Controls.Add(this.directComboBox);
            this.rightPanel.Controls.Add(this.label4);
            this.rightPanel.Controls.Add(this.typeComboBox);
            this.rightPanel.Controls.Add(this.label3);
            this.rightPanel.Controls.Add(this.defaultValueTextBox);
            this.rightPanel.Controls.Add(this.label2);
            this.rightPanel.Controls.Add(this.namebox);
            this.rightPanel.Controls.Add(this.label1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(234, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(444, 451);
            this.rightPanel.TabIndex = 1;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(125, 274);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // directComboBox
            // 
            this.directComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.directComboBox.FormattingEnabled = true;
            this.directComboBox.Items.AddRange(new object[] {
            "输出",
            "输入",
            "输入输出"});
            this.directComboBox.Location = new System.Drawing.Point(125, 154);
            this.directComboBox.Name = "directComboBox";
            this.directComboBox.Size = new System.Drawing.Size(196, 20);
            this.directComboBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "方向";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "整数",
            "字符串",
            "数组",
            "表格"});
            this.typeComboBox.Location = new System.Drawing.Point(125, 111);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(196, 20);
            this.typeComboBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "变量类型";
            // 
            // defaultValueTextBox
            // 
            this.defaultValueTextBox.Location = new System.Drawing.Point(125, 197);
            this.defaultValueTextBox.Name = "defaultValueTextBox";
            this.defaultValueTextBox.Size = new System.Drawing.Size(196, 21);
            this.defaultValueTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "缺省值";
            // 
            // namebox
            // 
            this.namebox.Location = new System.Drawing.Point(125, 67);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(196, 21);
            this.namebox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "变量名称";
            // 
            // VarManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 451);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VarManager";
            this.Text = "变量管理器";
            this.leftPanel.ResumeLayout(false);
            this.leftTopPanel.ResumeLayout(false);
            this.leftBottomPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel leftTopPanel;
        private System.Windows.Forms.Panel leftBottomPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.ComboBox directComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox defaultValueTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okBtn;
    }
}