using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
namespace AutoRobo.Makers.Views
{
    partial class MakerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakerView));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin2 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient4 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient8 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient9 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient10 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient11 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient12 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient6 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient13 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient14 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.fileDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbRecord = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRunCurrent = new System.Windows.Forms.ToolStripButton();
            this.stepRunBtn = new System.Windows.Forms.ToolStripButton();
            this.tsbRunStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddAction = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbFirst = new System.Windows.Forms.ToolStripButton();
            this.tsbPrevious = new System.Windows.Forms.ToolStripButton();
            this.tsbNext = new System.Windows.Forms.ToolStripButton();
            this.tsbLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.helpStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tsbBreakPoint = new System.Windows.Forms.ToolStripButton();
            this.feedbackButton = new System.Windows.Forms.ToolStripButton();
            this.updateBtn = new System.Windows.Forms.ToolStripButton();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // browserToolbar
            // 
            this.browserToolbar.Size = new System.Drawing.Size(725, 32);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // fileDropDownButton
            // 
            this.fileDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fileDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStripMenuItem,
            this.openStripMenuItem,
            this.saveStripMenuItem,
            this.saveAsStripMenuItem,
            this.toolStripSeparator2,
            this.aboutStripMenuItem,
            this.exitStripMenuItem});
            this.fileDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("fileDropDownButton.Image")));
            this.fileDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileDropDownButton.Name = "fileDropDownButton";
            this.fileDropDownButton.Size = new System.Drawing.Size(34, 25);
            this.fileDropDownButton.Text = "文件";
            this.fileDropDownButton.Visible = false;
            // 
            // newStripMenuItem
            // 
            this.newStripMenuItem.Name = "newStripMenuItem";
            this.newStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.newStripMenuItem.Text = "新建";
            this.newStripMenuItem.Click += new System.EventHandler(this.newStripMenuItem_Click);
            // 
            // openStripMenuItem
            // 
            this.openStripMenuItem.Name = "openStripMenuItem";
            this.openStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openStripMenuItem.Text = "打开";
            this.openStripMenuItem.Click += new System.EventHandler(this.openStripMenuItem_Click);
            // 
            // saveStripMenuItem
            // 
            this.saveStripMenuItem.Name = "saveStripMenuItem";
            this.saveStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveStripMenuItem.Text = "保存";
            this.saveStripMenuItem.Click += new System.EventHandler(this.saveStripMenuItem_Click);
            // 
            // saveAsStripMenuItem
            // 
            this.saveAsStripMenuItem.Name = "saveAsStripMenuItem";
            this.saveAsStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsStripMenuItem.Text = "另存为...";
            this.saveAsStripMenuItem.Click += new System.EventHandler(this.saveAsStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(118, 6);
            // 
            // aboutStripMenuItem
            // 
            this.aboutStripMenuItem.Name = "aboutStripMenuItem";
            this.aboutStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.aboutStripMenuItem.Text = "关于";
            this.aboutStripMenuItem.Visible = false;
            this.aboutStripMenuItem.Click += new System.EventHandler(this.aboutStripMenuItem_Click);
            // 
            // exitStripMenuItem
            // 
            this.exitStripMenuItem.Name = "exitStripMenuItem";
            this.exitStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitStripMenuItem.Text = "退出";
            this.exitStripMenuItem.Click += new System.EventHandler(this.exitStripMenuItem_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(25, 25);
            this.tsbSave.Text = "保存脚本";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbRecord
            // 
            this.tsbRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRecord.Enabled = false;
            this.tsbRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsbRecord.Image")));
            this.tsbRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRecord.Name = "tsbRecord";
            this.tsbRecord.Size = new System.Drawing.Size(25, 25);
            this.tsbRecord.Text = "开始录制";
            this.tsbRecord.Click += new System.EventHandler(this.tsbRecord_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Enabled = false;
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(25, 25);
            this.tsbStop.Text = "停止录制";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbRunCurrent
            // 
            this.tsbRunCurrent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRunCurrent.Enabled = false;
            this.tsbRunCurrent.Image = ((System.Drawing.Image)(resources.GetObject("tsbRunCurrent.Image")));
            this.tsbRunCurrent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunCurrent.Name = "tsbRunCurrent";
            this.tsbRunCurrent.Size = new System.Drawing.Size(25, 25);
            this.tsbRunCurrent.Text = "运行";
            this.tsbRunCurrent.Click += new System.EventHandler(this.tsbRunCurrent_Click);
            // 
            // stepRunBtn
            // 
            this.stepRunBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stepRunBtn.Enabled = false;
            this.stepRunBtn.Image = ((System.Drawing.Image)(resources.GetObject("stepRunBtn.Image")));
            this.stepRunBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stepRunBtn.Name = "stepRunBtn";
            this.stepRunBtn.Size = new System.Drawing.Size(25, 25);
            this.stepRunBtn.Text = "单步执行";
            this.stepRunBtn.Click += new System.EventHandler(this.stepRunBtn_Click);
            // 
            // tsbRunStop
            // 
            this.tsbRunStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRunStop.Enabled = false;
            this.tsbRunStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbRunStop.Image")));
            this.tsbRunStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunStop.Name = "tsbRunStop";
            this.tsbRunStop.Size = new System.Drawing.Size(25, 25);
            this.tsbRunStop.ToolTipText = "停止运行";
            this.tsbRunStop.Click += new System.EventHandler(this.tsbRunStop_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbAddAction
            // 
            this.tsbAddAction.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAddAction.BackColor = System.Drawing.SystemColors.Control;
            this.tsbAddAction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddAction.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddAction.Image")));
            this.tsbAddAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddAction.Name = "tsbAddAction";
            this.tsbAddAction.Size = new System.Drawing.Size(37, 25);
            this.tsbAddAction.Text = "添加活动";
            // 
            // tsbFirst
            // 
            this.tsbFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFirst.Image = ((System.Drawing.Image)(resources.GetObject("tsbFirst.Image")));
            this.tsbFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFirst.Name = "tsbFirst";
            this.tsbFirst.Size = new System.Drawing.Size(25, 25);
            this.tsbFirst.Text = "第一行";
            // 
            // tsbPrevious
            // 
            this.tsbPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrevious.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrevious.Image")));
            this.tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevious.Name = "tsbPrevious";
            this.tsbPrevious.Size = new System.Drawing.Size(25, 25);
            this.tsbPrevious.Text = "上一行";
            // 
            // tsbNext
            // 
            this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbNext.Image")));
            this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new System.Drawing.Size(25, 25);
            this.tsbNext.Text = "下一行";
            // 
            // tsbLast
            // 
            this.tsbLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLast.Image = ((System.Drawing.Image)(resources.GetObject("tsbLast.Image")));
            this.tsbLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLast.Name = "tsbLast";
            this.tsbLast.Size = new System.Drawing.Size(25, 25);
            this.tsbLast.Text = "最后一行";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // helpStripButton
            // 
            this.helpStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpStripButton.Image")));
            this.helpStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpStripButton.Name = "helpStripButton";
            this.helpStripButton.Size = new System.Drawing.Size(25, 25);
            this.helpStripButton.ToolTipText = "关于";
            this.helpStripButton.Click += new System.EventHandler(this.helpStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(21, 21);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileDropDownButton,
            this.newToolStripButton,
            this.tsbSave,
            this.tsbRecord,
            this.tsbStop,
            this.toolStripSeparator,
            this.tsbRunCurrent,
            this.stepRunBtn,
            this.tsbBreakPoint,
            this.tsbRunStop,
            this.toolStripSeparator9,
            this.tsbAddAction,
            this.tsbFirst,
            this.tsbPrevious,
            this.tsbNext,
            this.tsbLast,
            this.toolStripSeparator3,
            this.toolStripSeparator1,
            this.feedbackButton,
            this.helpStripButton,
            this.updateBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(725, 28);
            this.toolStrip1.TabIndex = 18;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(25, 25);
            this.newToolStripButton.Text = "新建项目";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // tsbBreakPoint
            // 
            this.tsbBreakPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBreakPoint.Image = ((System.Drawing.Image)(resources.GetObject("tsbBreakPoint.Image")));
            this.tsbBreakPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBreakPoint.Name = "tsbBreakPoint";
            this.tsbBreakPoint.Size = new System.Drawing.Size(25, 25);
            this.tsbBreakPoint.Text = "设置断点";
            this.tsbBreakPoint.Click += new System.EventHandler(this.breakPoint_Click);
            // 
            // feedbackButton
            // 
            this.feedbackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.feedbackButton.Image = ((System.Drawing.Image)(resources.GetObject("feedbackButton.Image")));
            this.feedbackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.feedbackButton.Name = "feedbackButton";
            this.feedbackButton.Size = new System.Drawing.Size(25, 25);
            this.feedbackButton.Text = "意见反馈";
            this.feedbackButton.Click += new System.EventHandler(this.feedbackButton_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateBtn.Image = ((System.Drawing.Image)(resources.GetObject("updateBtn.Image")));
            this.updateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(25, 25);
            this.updateBtn.Text = "系统升级";
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.dockPanel.DockBottomPortion = 150D;
            this.dockPanel.DockLeftPortion = 400D;
            this.dockPanel.DockRightPortion = 200D;
            this.dockPanel.DockTopPortion = 150D;
            this.dockPanel.Location = new System.Drawing.Point(0, 60);
            this.dockPanel.Margin = new System.Windows.Forms.Padding(7);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(725, 284);
            dockPanelGradient4.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient4.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            autoHideStripSkin2.DockStripGradient = dockPanelGradient4;
            tabGradient8.EndColor = System.Drawing.SystemColors.Control;
            tabGradient8.StartColor = System.Drawing.SystemColors.Control;
            tabGradient8.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin2.TabGradient = tabGradient8;
            autoHideStripSkin2.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            dockPanelSkin2.AutoHideStripSkin = autoHideStripSkin2;
            tabGradient9.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            tabGradient9.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            tabGradient9.TextColor = System.Drawing.Color.White;
            dockPaneStripGradient2.ActiveTabGradient = tabGradient9;
            dockPanelGradient5.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient5.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient2.DockStripGradient = dockPanelGradient5;
            tabGradient10.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient10.StartColor = System.Drawing.SystemColors.Control;
            tabGradient10.TextColor = System.Drawing.SystemColors.GrayText;
            dockPaneStripGradient2.InactiveTabGradient = tabGradient10;
            dockPaneStripSkin2.DocumentGradient = dockPaneStripGradient2;
            dockPaneStripSkin2.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            tabGradient11.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(170)))), ((int)(((byte)(220)))));
            tabGradient11.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient11.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            tabGradient11.TextColor = System.Drawing.Color.White;
            dockPaneStripToolWindowGradient2.ActiveCaptionGradient = tabGradient11;
            tabGradient12.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient12.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient12.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dockPaneStripToolWindowGradient2.ActiveTabGradient = tabGradient12;
            dockPanelGradient6.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient6.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripToolWindowGradient2.DockStripGradient = dockPanelGradient6;
            tabGradient13.EndColor = System.Drawing.SystemColors.ControlDark;
            tabGradient13.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient13.StartColor = System.Drawing.SystemColors.Control;
            tabGradient13.TextColor = System.Drawing.SystemColors.GrayText;
            dockPaneStripToolWindowGradient2.InactiveCaptionGradient = tabGradient13;
            tabGradient14.EndColor = System.Drawing.SystemColors.Control;
            tabGradient14.StartColor = System.Drawing.SystemColors.Control;
            tabGradient14.TextColor = System.Drawing.SystemColors.GrayText;
            dockPaneStripToolWindowGradient2.InactiveTabGradient = tabGradient14;
            dockPaneStripSkin2.ToolWindowGradient = dockPaneStripToolWindowGradient2;
            dockPanelSkin2.DockPaneStripSkin = dockPaneStripSkin2;
            this.dockPanel.Skin = dockPanelSkin2;
            this.dockPanel.TabIndex = 20;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // MakerView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(725, 369);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MakerView";
            this.Text = "Bitrun";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.browserToolbar, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.dockPanel, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private ToolStripDropDownButton fileDropDownButton;
        private ToolStripMenuItem newStripMenuItem;
        private ToolStripMenuItem openStripMenuItem;
        private ToolStripMenuItem saveStripMenuItem;
        private ToolStripMenuItem saveAsStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem aboutStripMenuItem;
        private ToolStripMenuItem exitStripMenuItem;
        private ToolStripButton tsbSave;
        private ToolStripButton tsbRecord;
        private ToolStripButton tsbStop;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton tsbRunCurrent;
        private ToolStripButton stepRunBtn;
        private ToolStripButton tsbRunStop;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripSplitButton tsbAddAction;
        private ToolStripButton tsbFirst;
        private ToolStripButton tsbPrevious;
        private ToolStripButton tsbNext;
        private ToolStripButton tsbLast;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton helpStripButton;
        private ToolStrip toolStrip1;
        private DockPanel dockPanel;
        private ToolStripButton feedbackButton;
        private ToolStripButton newToolStripButton;
        private ToolStripButton tsbBreakPoint;
        private ToolStripButton updateBtn;
        private ToolStripSeparator toolStripSeparator1;
    }
}
