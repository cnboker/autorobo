namespace AutoRobo.Makers.Views
{
    partial class ActivityWorkshopView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityWorkshopView));
            this.tabControl = new System.Windows.Forms.CustomTabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.container = new System.Windows.Forms.Panel();
            this.gridSource = new AutoRobo.Makers.Views.ActionGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.backToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbScriptMoveTop = new System.Windows.Forms.ToolStripButton();
            this.tsbScriptMovePervious = new System.Windows.Forms.ToolStripButton();
            this.tsbScriptMoveNext = new System.Windows.Forms.ToolStripButton();
            this.tsbScriptMoveBottom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteAction = new System.Windows.Forms.ToolStripButton();
            this.tsbClearTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.functionTabPage = new System.Windows.Forms.TabPage();
            this.varTabPage = new System.Windows.Forms.TabPage();
            this.varActionGrid = new AutoRobo.Makers.Views.VariableActionGrid();
            this.varToolStrip = new System.Windows.Forms.ToolStrip();
            this.varEditStripBtn = new System.Windows.Forms.ToolStripButton();
            this.tabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.container.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.varTabPage.SuspendLayout();
            this.varToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.mainTabPage);
            this.tabControl.Controls.Add(this.functionTabPage);
            this.tabControl.Controls.Add(this.varTabPage);
            this.tabControl.DisplayStyle = System.Windows.Forms.TabStyle.VisualStudio;
            // 
            // 
            // 
            this.tabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.tabControl.DisplayStyleProvider.FocusTrack = false;
            this.tabControl.DisplayStyleProvider.HotTrack = true;
            this.tabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tabControl.DisplayStyleProvider.Opacity = 1F;
            this.tabControl.DisplayStyleProvider.Overlap = 7;
            this.tabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(14, 1);
            this.tabControl.DisplayStyleProvider.ShowTabCloser = false;
            this.tabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(400, 422);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.container);
            this.mainTabPage.Location = new System.Drawing.Point(4, 21);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Size = new System.Drawing.Size(392, 397);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "活动列表";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // container
            // 
            this.container.Controls.Add(this.gridSource);
            this.container.Controls.Add(this.toolStrip2);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(392, 397);
            this.container.TabIndex = 0;
            // 
            // gridSource
            // 
            this.gridSource.AutoSize = true;
            this.gridSource.BackColor = System.Drawing.Color.White;
            this.gridSource.ColumnsCount = 3;
            this.gridSource.CustomSort = true;
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSource.EnableSort = true;
            this.gridSource.FixedRows = 1;
            this.gridSource.Location = new System.Drawing.Point(0, 26);
            this.gridSource.Margin = new System.Windows.Forms.Padding(2);
            this.gridSource.Method = AutoRobo.Core.Models.DataMethod.Collect;
            this.gridSource.Name = "gridSource";
            this.gridSource.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridSource.RowsCount = 6;
            this.gridSource.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridSource.Size = new System.Drawing.Size(392, 371);
            this.gridSource.TabIndex = 15;
            this.gridSource.TabStop = true;
            this.gridSource.ToolTipText = "";
            this.gridSource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridSource_MouseClick);
            this.gridSource.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridSource_MouseDoubleClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripButton,
            this.toolStripSeparator1,
            this.tsbScriptMoveTop,
            this.tsbScriptMovePervious,
            this.tsbScriptMoveNext,
            this.tsbScriptMoveBottom,
            this.toolStripSeparator12,
            this.editToolStripButton,
            this.tsbDeleteAction,
            this.tsbClearTest,
            this.toolStripSeparator4,
            this.refreshButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(392, 26);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // backToolStripButton
            // 
            this.backToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("backToolStripButton.Image")));
            this.backToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backToolStripButton.Name = "backToolStripButton";
            this.backToolStripButton.Size = new System.Drawing.Size(23, 23);
            this.backToolStripButton.Text = "返回";
            this.backToolStripButton.Click += new System.EventHandler(this.backToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbScriptMoveTop
            // 
            this.tsbScriptMoveTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScriptMoveTop.Image = ((System.Drawing.Image)(resources.GetObject("tsbScriptMoveTop.Image")));
            this.tsbScriptMoveTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptMoveTop.Name = "tsbScriptMoveTop";
            this.tsbScriptMoveTop.Size = new System.Drawing.Size(23, 23);
            this.tsbScriptMoveTop.Text = "到顶";
            this.tsbScriptMoveTop.Click += new System.EventHandler(this.tsbScriptMoveTop_Click);
            // 
            // tsbScriptMovePervious
            // 
            this.tsbScriptMovePervious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScriptMovePervious.Image = ((System.Drawing.Image)(resources.GetObject("tsbScriptMovePervious.Image")));
            this.tsbScriptMovePervious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptMovePervious.Name = "tsbScriptMovePervious";
            this.tsbScriptMovePervious.Size = new System.Drawing.Size(23, 23);
            this.tsbScriptMovePervious.Text = "上移";
            this.tsbScriptMovePervious.Click += new System.EventHandler(this.tsbScriptMovePervious_Click);
            // 
            // tsbScriptMoveNext
            // 
            this.tsbScriptMoveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScriptMoveNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbScriptMoveNext.Image")));
            this.tsbScriptMoveNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptMoveNext.Name = "tsbScriptMoveNext";
            this.tsbScriptMoveNext.Size = new System.Drawing.Size(23, 23);
            this.tsbScriptMoveNext.Text = "下移";
            this.tsbScriptMoveNext.Click += new System.EventHandler(this.tsbScriptMoveNext_Click);
            // 
            // tsbScriptMoveBottom
            // 
            this.tsbScriptMoveBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScriptMoveBottom.Image = ((System.Drawing.Image)(resources.GetObject("tsbScriptMoveBottom.Image")));
            this.tsbScriptMoveBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptMoveBottom.Name = "tsbScriptMoveBottom";
            this.tsbScriptMoveBottom.Size = new System.Drawing.Size(23, 23);
            this.tsbScriptMoveBottom.Text = "到低";
            this.tsbScriptMoveBottom.Click += new System.EventHandler(this.tsbScriptMoveBottom_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 26);
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripButton.Image")));
            this.editToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Size = new System.Drawing.Size(23, 23);
            this.editToolStripButton.Text = "编辑";
            this.editToolStripButton.Click += new System.EventHandler(this.editToolStripButton_Click);
            // 
            // tsbDeleteAction
            // 
            this.tsbDeleteAction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteAction.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteAction.Image")));
            this.tsbDeleteAction.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDeleteAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteAction.Name = "tsbDeleteAction";
            this.tsbDeleteAction.Size = new System.Drawing.Size(23, 23);
            this.tsbDeleteAction.Text = "删除";
            this.tsbDeleteAction.ToolTipText = "删除步骤";
            this.tsbDeleteAction.Click += new System.EventHandler(this.tsbDeleteAction_Click);
            // 
            // tsbClearTest
            // 
            this.tsbClearTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClearTest.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearTest.Image")));
            this.tsbClearTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearTest.Name = "tsbClearTest";
            this.tsbClearTest.Size = new System.Drawing.Size(23, 23);
            this.tsbClearTest.Text = "清除";
            this.tsbClearTest.Click += new System.EventHandler(this.tsbClearTest_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 23);
            this.refreshButton.Text = "排序";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // functionTabPage
            // 
            this.functionTabPage.Location = new System.Drawing.Point(4, 21);
            this.functionTabPage.Name = "functionTabPage";
            this.functionTabPage.Size = new System.Drawing.Size(392, 397);
            this.functionTabPage.TabIndex = 1;
            this.functionTabPage.Text = "过程";
            this.functionTabPage.UseVisualStyleBackColor = true;
            // 
            // varTabPage
            // 
            this.varTabPage.Controls.Add(this.varActionGrid);
            this.varTabPage.Controls.Add(this.varToolStrip);
            this.varTabPage.Location = new System.Drawing.Point(4, 21);
            this.varTabPage.Name = "varTabPage";
            this.varTabPage.Size = new System.Drawing.Size(392, 397);
            this.varTabPage.TabIndex = 2;
            this.varTabPage.Text = "变量";
            this.varTabPage.UseVisualStyleBackColor = true;
            // 
            // varActionGrid
            // 
            this.varActionGrid.AutoSize = true;
            this.varActionGrid.BackColor = System.Drawing.Color.White;
            this.varActionGrid.ColumnsCount = 3;
            this.varActionGrid.CustomSort = true;
            this.varActionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varActionGrid.EnableSort = true;
            this.varActionGrid.FixedRows = 1;
            this.varActionGrid.Location = new System.Drawing.Point(0, 31);
            this.varActionGrid.Margin = new System.Windows.Forms.Padding(2);
            this.varActionGrid.Method = AutoRobo.Core.Models.DataMethod.Collect;
            this.varActionGrid.Name = "varActionGrid";
            this.varActionGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.varActionGrid.RowsCount = 6;
            this.varActionGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.varActionGrid.Size = new System.Drawing.Size(392, 366);
            this.varActionGrid.TabIndex = 17;
            this.varActionGrid.TabStop = true;
            this.varActionGrid.ToolTipText = "";
            this.varActionGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridSource_MouseClick);
            this.varActionGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridSource_MouseDoubleClick);
            // 
            // varToolStrip
            // 
            this.varToolStrip.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.varToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.varEditStripBtn});
            this.varToolStrip.Location = new System.Drawing.Point(0, 0);
            this.varToolStrip.Name = "varToolStrip";
            this.varToolStrip.Size = new System.Drawing.Size(392, 31);
            this.varToolStrip.TabIndex = 16;
            this.varToolStrip.Text = "toolStrip1";
            // 
            // varEditStripBtn
            // 
            this.varEditStripBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.varEditStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("varEditStripBtn.Image")));
            this.varEditStripBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.varEditStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.varEditStripBtn.Name = "varEditStripBtn";
            this.varEditStripBtn.Size = new System.Drawing.Size(28, 28);
            this.varEditStripBtn.Text = "编辑";
            this.varEditStripBtn.Click += new System.EventHandler(this.varEditStripBtn_Click);
            // 
            // ActivityWorkshopView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 422);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivityWorkshopView";
            this.tabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.varTabPage.ResumeLayout(false);
            this.varTabPage.PerformLayout();
            this.varToolStrip.ResumeLayout(false);
            this.varToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomTabControl tabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.TabPage functionTabPage;
        private System.Windows.Forms.TabPage varTabPage;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbScriptMoveTop;
        private System.Windows.Forms.ToolStripButton tsbScriptMovePervious;
        private System.Windows.Forms.ToolStripButton tsbScriptMoveNext;
        private System.Windows.Forms.ToolStripButton tsbScriptMoveBottom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton tsbDeleteAction;
        private System.Windows.Forms.ToolStripButton tsbClearTest;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private AutoRobo.Makers.Views.ActionGrid gridSource;
        private System.Windows.Forms.ToolStripButton backToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private VariableActionGrid varActionGrid;
        private System.Windows.Forms.ToolStrip varToolStrip;
        private System.Windows.Forms.ToolStripButton varEditStripBtn;
    }
}
