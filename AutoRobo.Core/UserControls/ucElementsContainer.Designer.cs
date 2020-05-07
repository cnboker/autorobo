namespace AutoRobo.Core.UserControls
{
    partial class ucElementsContainer
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
            this.gbFindElement = new System.Windows.Forms.GroupBox();
            this.gridElement = new SourceGrid.Grid();
            this.tabControl1.SuspendLayout();
            this.gbFindElement.SuspendLayout();
            this.SuspendLayout();
         
            // 
            // gbFindElement
            // 
            this.gbFindElement.Controls.Add(this.gridElement);
            this.gbFindElement.Location = new System.Drawing.Point(0, 0);
            this.gbFindElement.Margin = new System.Windows.Forms.Padding(2, 7, 2, 2);
            this.gbFindElement.Name = "gbFindElement";
            this.gbFindElement.Padding = new System.Windows.Forms.Padding(7, 7, 7, 15);
            this.gbFindElement.Size = new System.Drawing.Size(420, 340);
            this.gbFindElement.TabIndex = 20;
            this.gbFindElement.TabStop = false;
            // 
            // gridElement
            // 
            this.gridElement.AutoSize = true;
            this.gridElement.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.gridElement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridElement.ColumnsCount = 1;
            this.gridElement.CustomSort = true;
            this.gridElement.DefaultWidth = 40;
            this.gridElement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridElement.EnableSort = true;
            this.gridElement.FixedRows = 3;
            this.gridElement.Location = new System.Drawing.Point(7, 21);
            this.gridElement.Margin = new System.Windows.Forms.Padding(1);
            this.gridElement.Name = "gridElement";
            this.gridElement.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridElement.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridElement.Size = new System.Drawing.Size(406, 304);
            this.gridElement.TabIndex = 15;
            this.gridElement.TabStop = true;
            this.gridElement.ToolTipText = "";
            // 
            // ucElementsContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.tabPage2.Controls.Add(this.gbFindElement);
            this.Name = "ucElementsContainer";

            this.tabControl1.ResumeLayout(false);
            this.gbFindElement.ResumeLayout(false);
            this.gbFindElement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gbFindElement;
        internal SourceGrid.Grid gridElement;
    }
}
