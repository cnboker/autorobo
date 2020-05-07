

using AutoRobo.Core.Controls;
namespace AutoRobo.PlulgIn.Debuger
{
    partial class HtmlTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlTree));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.selectAreaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.selectorStripButton = new System.Windows.Forms.ToolStripButton();
            this.refreshStripButton = new System.Windows.Forms.ToolStripButton();
            this.parentStripButton = new System.Windows.Forms.ToolStripButton();
            this.childStripButton = new System.Windows.Forms.ToolStripButton();
            this.leftStripButton = new System.Windows.Forms.ToolStripButton();
            this.rightStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.typeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.xpathBox = new AutoRobo.Core.Controls.ToolStripSpringTextBox();
            this.toolStripLocationButton = new System.Windows.Forms.ToolStripButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAreaToolStripButton,
            this.selectorStripButton,
            this.refreshStripButton,
            this.parentStripButton,
            this.childStripButton,
            this.leftStripButton,
            this.rightStripButton,
            this.toolStripSeparator1,
            this.typeComboBox,
            this.xpathBox,
            this.toolStripLocationButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(782, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // selectAreaToolStripButton
            // 
            this.selectAreaToolStripButton.CheckOnClick = true;
            this.selectAreaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectAreaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("selectAreaToolStripButton.Image")));
            this.selectAreaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAreaToolStripButton.Name = "selectAreaToolStripButton";
            this.selectAreaToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.selectAreaToolStripButton.Text = "选择区域";
            // 
            // selectorStripButton
            // 
            this.selectorStripButton.CheckOnClick = true;
            this.selectorStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectorStripButton.Image = ((System.Drawing.Image)(resources.GetObject("selectorStripButton.Image")));
            this.selectorStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectorStripButton.Name = "selectorStripButton";
            this.selectorStripButton.Size = new System.Drawing.Size(23, 22);
            this.selectorStripButton.Text = "元素定位器";
            // 
            // refreshStripButton
            // 
            this.refreshStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshStripButton.Image")));
            this.refreshStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshStripButton.Name = "refreshStripButton";
            this.refreshStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshStripButton.Text = "刷新";
            // 
            // parentStripButton
            // 
            this.parentStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.parentStripButton.Enabled = false;
            this.parentStripButton.Image = ((System.Drawing.Image)(resources.GetObject("parentStripButton.Image")));
            this.parentStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parentStripButton.Name = "parentStripButton";
            this.parentStripButton.Size = new System.Drawing.Size(23, 22);
            this.parentStripButton.ToolTipText = "父元素";
            this.parentStripButton.Visible = false;
            this.parentStripButton.Click += new System.EventHandler(this.parentElementButton_Click);
            // 
            // childStripButton
            // 
            this.childStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.childStripButton.Enabled = false;
            this.childStripButton.Image = ((System.Drawing.Image)(resources.GetObject("childStripButton.Image")));
            this.childStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.childStripButton.Name = "childStripButton";
            this.childStripButton.Size = new System.Drawing.Size(23, 22);
            this.childStripButton.ToolTipText = "子元素";
            this.childStripButton.Visible = false;
            this.childStripButton.Click += new System.EventHandler(this.childElementButton_Click);
            // 
            // leftStripButton
            // 
            this.leftStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.leftStripButton.Enabled = false;
            this.leftStripButton.Image = ((System.Drawing.Image)(resources.GetObject("leftStripButton.Image")));
            this.leftStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.leftStripButton.Name = "leftStripButton";
            this.leftStripButton.Size = new System.Drawing.Size(23, 22);
            this.leftStripButton.ToolTipText = "左元素";
            this.leftStripButton.Visible = false;
            this.leftStripButton.Click += new System.EventHandler(this.leftStripButton_Click);
            // 
            // rightStripButton
            // 
            this.rightStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rightStripButton.Enabled = false;
            this.rightStripButton.Image = ((System.Drawing.Image)(resources.GetObject("rightStripButton.Image")));
            this.rightStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rightStripButton.Name = "rightStripButton";
            this.rightStripButton.Size = new System.Drawing.Size(23, 22);
            this.rightStripButton.ToolTipText = "右元素";
            this.rightStripButton.Visible = false;
            this.rightStripButton.Click += new System.EventHandler(this.rightStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.Items.AddRange(new object[] {
            "xpath",
            "css selector"});
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 25);
            this.typeComboBox.Visible = false;
            // 
            // xpathBox
            // 
            this.xpathBox.Name = "xpathBox";
            this.xpathBox.Size = new System.Drawing.Size(413, 25);
            // 
            // toolStripLocationButton
            // 
            this.toolStripLocationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLocationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLocationButton.Name = "toolStripLocationButton";
            this.toolStripLocationButton.Size = new System.Drawing.Size(36, 22);
            this.toolStripLocationButton.Text = "定位";
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 25);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(782, 155);
            this.contentPanel.TabIndex = 14;
            // 
            // HtmlTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "HtmlTree";
            this.Size = new System.Drawing.Size(782, 180);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripSpringTextBox xpathBox;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.ToolStripButton toolStripLocationButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton selectorStripButton;
        private System.Windows.Forms.ToolStripButton parentStripButton;
        private System.Windows.Forms.ToolStripButton childStripButton;
        private System.Windows.Forms.ToolStripButton leftStripButton;
        private System.Windows.Forms.ToolStripButton rightStripButton;
        private System.Windows.Forms.ToolStripButton refreshStripButton;
        private System.Windows.Forms.ToolStripComboBox typeComboBox;
        private System.Windows.Forms.ToolStripButton selectAreaToolStripButton;

    }
}
