namespace AutoRobo.Makers.Views
{
    partial class FileView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileView));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.createFileButton = new System.Windows.Forms.ToolStripButton();
            this.createDirButton = new System.Windows.Forms.ToolStripButton();
            this.renameButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.openDirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(324, 317);
            this.treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "inode-directory.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFileButton,
            this.createDirButton,
            this.renameButton,
            this.deleteButton,
            this.openDirToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(324, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // createFileButton
            // 
            this.createFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createFileButton.Image = ((System.Drawing.Image)(resources.GetObject("createFileButton.Image")));
            this.createFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createFileButton.Name = "createFileButton";
            this.createFileButton.Size = new System.Drawing.Size(23, 22);
            this.createFileButton.Text = "创建脚本文件";
            // 
            // createDirButton
            // 
            this.createDirButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createDirButton.Image = ((System.Drawing.Image)(resources.GetObject("createDirButton.Image")));
            this.createDirButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createDirButton.Name = "createDirButton";
            this.createDirButton.Size = new System.Drawing.Size(23, 22);
            this.createDirButton.Text = "创建目录";
            this.createDirButton.Visible = false;
            // 
            // renameButton
            // 
            this.renameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renameButton.Image = ((System.Drawing.Image)(resources.GetObject("renameButton.Image")));
            this.renameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(23, 22);
            this.renameButton.Text = "重命名";
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(23, 22);
            this.deleteButton.Text = "删除文件或目录";
            // 
            // openDirToolStripButton
            // 
            this.openDirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openDirToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openDirToolStripButton.Image")));
            this.openDirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openDirToolStripButton.Name = "openDirToolStripButton";
            this.openDirToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openDirToolStripButton.Text = "打开文件夹";
            this.openDirToolStripButton.Click += new System.EventHandler(this.openDirToolStripButton_Click);
            // 
            // FileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(324, 342);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton createFileButton;
        private System.Windows.Forms.ToolStripButton createDirButton;
        private System.Windows.Forms.ToolStripButton renameButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton openDirToolStripButton;
    }
}
