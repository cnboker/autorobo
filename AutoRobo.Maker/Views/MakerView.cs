using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Models;
using AutoRobo.Makers.EventArguments;
using AutoRobo.Makers.Models.Repositories;
using AutoRobo.Makers.Presentation;
using AutoRobo.Protocol;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;

namespace AutoRobo.Makers.Views
{
    public partial class MakerView : FormBase, IMakerView, INewWindow
    {
        private MakerPresenter presenter = null;       
        private bool offline = false;
      
        private IActionRepository repository;
        private DeserializeDockContent m_deserializeDockContent;

        private FileView fileTree = null;
        private ActivityWorkshopView aws = null;
        private BrowserView mainBrowser = null;
        private HtmlView htmlView;
        //private ScriptView scriptView;
        private OutputView outputView;
        private PreviewForm formPreview;
        private Dictionary<string, BrowserView> browserViews = new Dictionary<string, BrowserView>();

        public MakerView(IActionRepository repository)
        {
            this.repository = repository; 
            InitializeComponent();
            CreateStandardControls();

            AttachBrowser(mainBrowser.Browser);
            
            presenter = new MakerPresenter(this);

            LoopRowSelector.Instance.RegisterHandler(tsbFirst,
                tsbLast, tsbNext, tsbPrevious);

            //new MultiBrowserArea(mainContainer);
            //splitContainer1.Paint += new PaintEventHandler(splitContainer1_Paint);
            Context.State.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(State_PropertyChanged);
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            dockPanel.ActiveContentChanged += dockPanel_ActiveContentChanged;  
            
        }

        private IDockContent lastActiveForm = null;
        void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (lastActiveForm == dockPanel.ActiveContent) return;
            BrowserView view = dockPanel.ActiveContent as BrowserView;
            if (view == null) return;
            lastActiveForm = dockPanel.ActiveContent;
            AttachBrowser(view.Browser);
        }

        protected override void AttachBrowser(ICoreBrowser activeBrowser)
        {
            base.AttachBrowser(activeBrowser);
            Context.Browser = activeBrowser;
            htmlView.BrowserAttach();
        }

        public void InsertStripButton(int index, ToolStripButton value)
        {            
            if (index < 0) {
                index = toolStrip1.Items.Count + index;
            }
            toolStrip1.Items.Insert(index, value);
        }

        private void CreateStandardControls()
        {
            fileTree = new FileView();
            aws = new ActivityWorkshopView();
            mainBrowser = new BrowserView() { Name = "MainBrowser" };
            AppContext.Current.Browser = mainBrowser.Browser;
            browserViews.Add(mainBrowser.Name, mainBrowser);
            //构造脚本模型
            var model = new ModelSet(repository);
            htmlView = new HtmlView(this);
            
            //scriptView = new ScriptView();
            outputView = new OutputView();
            //scriptView.Editor.Logger = outputView.Logger;
            AppContext.Current.Logger = outputView.Logger;
            formPreview = new PreviewForm();
            AppContext.Current.MainWindow = this;
            mainBrowser.Browser.BeforeNavigate2 += new csExWB.BeforeNavigate2EventHandler(Browser_BeforeNavigate2);
        }

        
        void Browser_BeforeNavigate2(object sender, csExWB.BeforeNavigate2EventArgs e)
        {
            //内置协议,打开在线脚本
            if (e.url.IndexOf("autorobo://") == 0) {
                e.Cancel = true;
                ProtocolObject protocol = ProtocolObject.DeserializeObject(e.url);
                ActionRepository repository = new ActionRepository((RecorderProtocol)protocol);
                var model = new ModelSet(repository);
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ActivityWorkshopView).ToString())
                return aws;
            else if (persistString == typeof(FileView).ToString())
                return fileTree;
            else if (persistString == typeof(BrowserView).ToString())
                return mainBrowser;
            else if (persistString == typeof(HtmlView).ToString())
                return htmlView;
            //else if (persistString == typeof(ScriptView).ToString())
            //    return scriptView;
            else if (persistString == typeof(OutputView).ToString())
                return outputView;
            else if (persistString == typeof(PreviewForm).ToString()) {
                return formPreview;
            }
            else
                return null;
        }

      
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("确定要退出程序吗", "提示",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            e.Cancel = (result == DialogResult.No);
            
            if (!e.Cancel)
            {
                if (AppContext.Current != null && AppContext.Current.CurrentWorker != null)
                {
                    AppContext.Current.CurrentWorker.Stop();
                }
                
            }
            //dockPanel.SaveAsXml("d:/1.xml");
            base.OnClosing(e);
        }
    

        void State_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRecord") {
                UpdateRecord(AppContext.Current.State.IsRecord);
            }
            else if (e.PropertyName == "IsRunning" && !AppContext.Current.State.IsRunning)
            {
                RunComplete();
            }
            else if (e.PropertyName == "BreakReached")
            {
                stepRunBtn.Enabled = AppContext.Current.State.BreakReached;
                tsbRunCurrent.Enabled = AppContext.Current.State.BreakReached;
            }
            else if (e.PropertyName == "DataPrepared")
            {
                tsbRunCurrent.Enabled = AppContext.Current.State.DataPrepared;
                tsbSave.Enabled = AppContext.Current.State.DataPrepared;
                tsbRecord.Enabled = AppContext.Current.State.DataPrepared;
                tsbBreakPoint.Enabled = AppContext.Current.State.DataPrepared;
            }
        }

        protected override void OnLoad(EventArgs e)
        {            
            Assembly assembly = Assembly.GetAssembly(typeof(MakerView));
            Stream xmlStream = assembly.GetManifestResourceStream("AutoRobo.Makers.dockConfig.xml");
            dockPanel.LoadFromXml(xmlStream, m_deserializeDockContent);
            xmlStream.Close();
            //formPreview.Show(this.dockPanel, DockState.DockRight);
            MyBrowser browser = AppContext.Current.Browser as MyBrowser;
            browser.WBContextMenu += new csExWB.ContextMenuEventHandler(MakerView_WBContextMenu);
            this.tsbAddAction.DropDownItems.AddRange(GetItems());
            htmlView.BrowserAttach();
            base.OnLoad(e);           
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {            
            if (keyData == (Keys.Control | Keys.S)) { //保存脚本
                saveStripMenuItem_Click(saveStripMenuItem, EventArgs.Empty);
            }
            else if (keyData == Keys.F8) {
                tsbRunCurrent_Click(tsbRunCurrent, EventArgs.Empty); //启动脚本
            }
            else if (keyData == Keys.F11) {
                stepRunBtn_Click(stepRunBtn, EventArgs.Empty); //单步跟踪脚本
            }
            else if (keyData == Keys.F6) { //启动录制或停止录制               
                if (AppContext.Current.State.IsRecord)
                {
                    tsbStop_Click(tsbStop, EventArgs.Empty);
                }
                else
                {
                    tsbRecord_Click(tsbRecord, EventArgs.Empty);
                }
            }
            //ctrl+shift+T
            //贴标
            if (keyData == (Keys.T | Keys.Control | Keys.Shift))
            {
                if (TagLabelEvent != null)
                {
                    TagLabelEvent(this, EventArgs.Empty);
                }
            }
            //else if (keyData == Keys.F10) {
            //    if (MessageBox.Show("确定要切换到在线模式?", "信息确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) {
            //        System.Diagnostics.Process.Start(Application.ExecutablePath, "-mode online"); // to start new instance of application
            //        this.Close();
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);            
        }

        void MakerView_WBContextMenu(object sender, csExWB.ContextMenuEventArgs e)
        {
            //非录制状态不显示上下文菜单
            if (!AppContext.Current.State.IsRecord) return;
            MyBrowser browser = AppContext.Current.Browser as MyBrowser;
            browser.Selector.Restore();
            browser.Selector.Highlight((mshtml.IHTMLElement)e.ctxmenuelem);
            e.displaydefault = false;
            if (contextMenuStrip.Items.Count == 0)
            {
                contextMenuStrip.Items.AddRange(GetItems());
            }
            e.pt.Offset(15, 5);
            contextMenuStrip.Show(e.pt);
           
        }

        public void ShowContextMenu() {
            if (contextMenuStrip.Items.Count == 0)
            {
                contextMenuStrip.Items.AddRange(GetItems());
            }
            Point p = MousePosition;
            p.Offset(15, 5);
            contextMenuStrip.Show(p);
        }

        private ToolStripItem[] GetItems() {
            ToolStripItem[] items = MenuBuilder.CreateMenu();
            foreach (var item in items)
            {
                ToolStripItemEventRegister(item);
            }
            return items;
        }

        private void ToolStripItemEventRegister(ToolStripItem item)
        {
            ToolStripDropDownItem dd = item as ToolStripDropDownItem;
            if (dd == null) return;
            if (item.Tag != null && !string.IsNullOrEmpty(item.Tag.ToString()))
            {
                item.Click += new EventHandler(item_Click);
            }
            foreach (ToolStripItem subItem in dd.DropDownItems)
            {
                ToolStripItemEventRegister(subItem);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            if (AddAction != null) { AddAction(sender, EventArgs.Empty); }
        }

    
        void RunComplete()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RunComplete));
                return;
            }
            tsbRecord.Enabled = true;
            tsbStop.Enabled = false;
            tsbRunCurrent.Enabled = true;
            tsbRunStop.Enabled = false;
        }

        public event EventHandler Record;

        public event EventHandler StopRecord;

        public event EventHandler StopRun;

        public event EventHandler AddAction;
      
        public event EventHandler BreakPoint;

        public event EventHandler StepRun;

        public event EventHandler Run;

        public event EventHandler New;

        public event EventHandler Open;

        public event EventHandler Save;

        public event EventHandler SaveAs;

        public event EventHandler Exit;
      
        private void tsbRecord_Click(object sender, EventArgs e)
        {
            if (Context.State.IsRunning)
            {
                if (MessageBox.Show("启动录制前需要停止脚本运行,要停止脚本运行吗?", "提示",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tsbRunStop_Click(tsbRunStop, EventArgs.Empty);
                }
                else {
                    return;
                }
            }
            UpdateRecord(true);     
            if (Record != null) { Record(sender, e); }
        }

        private void UpdateRecord(bool isRecord) {
            tsbRecord.Enabled = !isRecord;
            tsbStop.Enabled = isRecord;

        }
        /// <summary>
        /// 停止录制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStop_Click(object sender, EventArgs e)
        {
            UpdateRecord(false);                              
            if (StopRecord != null) { StopRecord(sender, e); }
        }

        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRunCurrent_Click(object sender, EventArgs e)
        {
            Context.State.SelectItemIndex = 1;
            tsbRunCurrent.Enabled = false;
            tsbRunStop.Enabled = true;                
            if (Run != null) { Run(sender, e); }
        }

        /// <summary>
        /// 单步跟踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stepRunBtn_Click(object sender, EventArgs e)
        {           
            if (StepRun != null) { StepRun(sender, e); }
        }

        /// <summary>
        /// 断点设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakPoint_Click(object sender, EventArgs e)
        {
            if (BreakPoint != null) { BreakPoint(sender, e); }
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRunStop_Click(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                Invoke(new Action<object, EventArgs>(tsbRunStop_Click), sender, e);
                return;
            }
            tsbRecord.Enabled = true;
            tsbStop.Enabled = false;          
            tsbRunCurrent.Enabled = true;
            tsbRunStop.Enabled = false;
            if (StopRun != null) { StopRun(sender, e); }
        }  

        private void newStripMenuItem_Click(object sender, EventArgs e)
        {
            StartView view = new StartView();
            if (view.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                NewFileEventArgs args = new NewFileEventArgs(view.ProjectName,view.StartUrl, view.Method);
                if (New != null) { New(sender, args); }
            }            
        }

        private void openStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Open != null) { Open(sender, e); } 
        }

        private void saveStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Save != null) { Save(sender, e); }
        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveAs != null) { SaveAs(sender, e); }
        }

        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Exit != null) { Exit(sender, e); }
        }

        private void tsbAddAction_Click(object sender, EventArgs e)
        {
            if (AddAction != null) { AddAction(sender, e); }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (Save != null) { Save(sender, e); }
        }

        public bool Offline
        {
            get
            {
                return offline;
            }
            set
            {
                offline = value;
                tsbSave.Visible = !offline;
                fileDropDownButton.Visible = offline;
            }
        }

        public event EventHandler TagLabelEvent;

        void helpStripButton_Click(object sender, System.EventArgs e)
        {
            Process ieProcess = Process.Start("iexplore", @"http://www.ebotop.com/page");
        }


        private void aboutStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void feedbackButton_Click(object sender, EventArgs e)
        {
            Process.Start("iexplore.exe", "http://www.ebotop.com/qa");
        }

        public event EventHandler Initialize;

        //public void AssignBrowserObject(ref object obj)
        //{
        //    throw new NotImplementedException();
        //}

        public ICoreBrowser Switch(string browserName)
        {
            ICoreBrowser my = null;
            if (browserViews.ContainsKey(browserName))
            {
                browserViews[browserName].Activate();
                my = browserViews[browserName].Browser;             
            }
            else
            {
                if (InvokeRequired)
                {
                    my = Invoke(new Func<ICoreBrowser>(() =>
                    {
                        return CreateBrowser(browserName);
                    })) as ICoreBrowser;
                }
                else
                {
                    my = CreateBrowser(browserName);
                }
            }         
            return my;
        }

        private ICoreBrowser CreateBrowser(string browserName)
        {
            var view = new BrowserView("新页面") { Name = browserName};
            view.Show(dockPanel);
            view.DockTo(dockPanel, DockStyle.None);
            view.Browser.Name = browserName;
            view.CloseButtonVisible = true;
            browserViews.Add(browserName, view);
            view.Browser.WBContextMenu += MakerView_WBContextMenu;
            return view.Browser;
        }

        //public void Goto(ICoreBrowser parent, string url)
        //{
        //    ICoreBrowser current = null;
        //    foreach (BrowserView view in childBrowsers) {
        //        if (view.Browser == parent) {
        //            current = view.Browser;
        //        }
        //    }
        //    if (current == null) {
        //        BrowserView newView = new BrowserView();
        //        //newView.Context = new AppContext()
        //        //{
        //        //    ActionModel = Context.ActionModel,
        //        //    CurrentWorker = Context.CurrentWorker,
        //        //    Logger = Context.Logger,
        //        //    State = Context.State
        //        //};

        //    }
        //}

        private void updateBtn_Click(object sender, EventArgs e)
        {
            (new Updater()).CheckForUpdate();
        }

    }
}
