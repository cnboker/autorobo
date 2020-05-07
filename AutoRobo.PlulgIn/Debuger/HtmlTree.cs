using System;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.Core;
using WatiN.Core;
using System.Linq;
using WatiN.Core.Native.InternetExplorer;
using mshtml;
using csExWB;
using Util;
using AutoRobo.WebHelper;

namespace AutoRobo.PlulgIn.Debuger
{
    public partial class HtmlTree : UserControl
    {
        private DomTreeView treeDom;
        private MyBrowser browser = null;
        private Selector Selector = null;
        log4net.ILog logger = log4net.LogManager.GetLogger("HtmlTree");
        private IHTMLElement firstSelectedElement;
        /// <summary>
        /// 鼠标选择元素后触发事件
        /// </summary>
        public event HTMLMouseEventHandler SelectedEventHandler;

        /// <summary>
        /// 选择某个区域按钮
        /// </summary>
        public ToolStripButton SelectorAreaStripButton {
            get {
                return selectAreaToolStripButton;
            }
        }

        public ToolStripButton SelectorStripButton {
            get {
                return selectorStripButton;
            }
        }

        LineRectangle lineRect = null;

        public HtmlTree()
        {           
            InitializeComponent();
                     
            typeComboBox.SelectedIndex = 1;     
            selectorStripButton.CheckedChanged +=selectorStripButton_CheckedChanged;
            selectAreaToolStripButton.CheckedChanged += selectAreaToolStripButton_CheckedChanged;
        }

        void selectAreaToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (selectAreaToolStripButton.Checked)
            {
                IHTMLElement el = this.Selector.SelectorElement;
                if (el == null)
                {
                    selectAreaToolStripButton.Checked = false;
                    return;
                }
                this.Selector.Restore();
                this.Selector.HighlightArea(el);
            }
            else
            {
                this.Selector.Restore();
                this.Selector.AreaRestore();
            }
            //if (selectAreaToolStripButton.Checked && lineRect == null)
            //{
            //    TransparentPanel tra = new TransparentPanel();
            //    tra.Dock = DockStyle.Fill;
                
            //    browser.Parent.Controls.Add(tra);
            //    tra.BringToFront();
            //    lineRect = new LineRectangle(tra);
            //    lineRect.OnFinished += lineRect_OnFinished;
            //}
            //else { 
            
            //}
        }

        void lineRect_OnFinished(object sender, RectangleArgs args)
        {
            
        }

      
        private bool SelectorOn {
            get {
                return selectorStripButton.Checked;
            }
            set {
                selectorStripButton.Checked = value;                
            }
        }

        private bool SelectAreaOn
        {
            get
            {
                return selectAreaToolStripButton.Checked;
            }
            set
            {
                selectAreaToolStripButton.Checked = value;
            }
        }

        void browser_WBMouseMove(object sender, csExWB.HTMLMouseEventArgs e)
        {
            if (SelectorOn)
            {               
                Selector.Highlight((mshtml.IHTMLElement)e.SrcElement);
            }       
        }

        void MyBrowser_WBLButtonUp(object sender, HTMLMouseEventArgs e)
        {
            if (SelectorOn)
            {                
                if (ModifierKeys == Keys.Control)
                {
                    if (firstSelectedElement == null)
                    {
                        firstSelectedElement = e.SrcElement;
                        e.Handled = true;
                    }
                    else
                    {
                        try
                        {
                            Selector.HighlightPareElements(firstSelectedElement, e.SrcElement);
                            treeDom.SelectNodes(Selector.SelectorElements);
                        }                      
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            e.Handled = true;
                            return;
                        }
                      
                        e.Handled = true;
                        SelectorOn = false;
                        firstSelectedElement = null;
                        xpathBox.Text = Selector.CssSelector;
                        if (SelectedEventHandler != null)
                        {
                            SelectedEventHandler(this, e);
                        }
                    }
                }
                else
                {
                   
                    if (SelectorOn)
                    {
                        SelectorOn = false;
                        firstSelectedElement = null;
                        try
                        {
                            Selector.Highlight(e.SrcElement);
                            treeDom.SelectNodes(Selector.SelectorElements);
                        }
                       
                        catch (Exception ex) {
                            e.Handled = true;
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                    xpathBox.Text = Selector.CssSelector;
                   
                    e.Handled = true;
                    if (SelectedEventHandler != null)
                    {
                        SelectedEventHandler(this, e);
                    }
                }
               
            }
           
        }

        void MyBrowser_WBLButtonDown(object sender, HTMLMouseEventArgs e)
        {
            if (SelectorOn)
            {                
                e.Handled = true;
            }
        }
  
        void selectorStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectorOn)
            {
                browser.WBMouseMove += new csExWB.HTMLMouseEventHandler(browser_WBMouseMove);
            }
            else {
                browser.WBMouseMove -= new csExWB.HTMLMouseEventHandler(browser_WBMouseMove);
                firstSelectedElement = null;
            }
        }

        void browser_WBKeyUp(object sender, csExWB.WBKeyUpEventArgs e)
        {
            switch (e.keycode)
            {
                case Keys.Escape:
                    Selector.Restore();
                    Selector.AreaRestore();
                    selectorStripButton.Checked = false;
                    selectAreaToolStripButton.Checked = false;
                    break;
                default: break;
            }
        }

        #region 元素定位
        /// <summary>
        /// 检查工具栏元素定位按钮状态
        /// </summary>
        /// <param name="iHTMLElement"></param>
        private void CheckSelectorButton(mshtml.IHTMLElement el)
        {
            parentStripButton.Enabled = el != null;
            childStripButton.Enabled = el != null;
            leftStripButton.Enabled = el != null;
            rightStripButton.Enabled = (Selector.SelectorElement != null);
            selectorStripButton.Enabled = true;
            var children = (mshtml.IHTMLElementCollection)el.children;
            if (el.parentElement == null)
            {
                this.parentStripButton.Enabled = false;
            }
            else if (children.length == 0)
            {
                childStripButton.Enabled = false;
            }
        }

        private void parentElementButton_Click(object sender, EventArgs e)
        {
            if (Selector.SelectorElement == null)
            {
                this.parentStripButton.Enabled = false;
                return;
            }
            var parent = Selector.SelectorElement.parentElement;
            if (parent != null)
            {
                SelectTreeNode(parent);
                Selector.Highlight(parent);                
                this.childStripButton.Enabled = true;
            }
            else
            {
                this.parentStripButton.Enabled = false;
            }
        }


        private void childElementButton_Click(object sender, EventArgs e)
        {
            if (Selector.SelectorElement == null) return;
            var children = (IHTMLElementCollection)Selector.SelectorElement.children;
            if (children.length > 0)
            {
                IHTMLElement el = children.item(null, 0) as IHTMLElement;                
                SelectTreeNode(el);
                Selector.Highlight(el);                
                parentStripButton.Enabled = true;
            }
            else
            {
                childStripButton.Enabled = false;
            }
        }

        private void leftStripButton_Click(object sender, EventArgs e)
        {
            if (Selector.SelectorElement == null) return;
            var parent = Selector.SelectorElement.parentElement;
            if (parent == null)
            {
                return;
            }
            var children = (IHTMLElementCollection)parent.children;
            if (children.length > 0)
            {
                IHTMLElement elLeft = null;
                foreach (IHTMLElement el in children)
                {

                    if (el != Selector.SelectorElement)
                    {
                        elLeft = el;
                        continue;
                    }
                    else
                    {
                        //rightStripButton.Enabled = true;
                        break;
                    }
                }
                
                if (elLeft != null)
                {
                    SelectTreeNode(elLeft);
                    Selector.Highlight(elLeft);                    
                }

            }
        }

        private void rightStripButton_Click(object sender, EventArgs e)
        {
            if (Selector.SelectorElement == null) return;
            var parent = Selector.SelectorElement.parentElement;
            if (parent == null)
            {
                return;
            }
            var children = (IHTMLElementCollection)parent.children;
            if (children.length > 0)
            {
                IHTMLElement rightEl = null;
                bool isfind = false;
                foreach (IHTMLElement el in children)
                {
                    if (isfind)
                    {
                        //leftStripButton.Enabled = true;
                        rightEl = el;
                        isfind = false;
                        break;
                    }
                    if (el != Selector.SelectorElement) continue;
                    isfind = true;
                }
                //rightStripButton.Enabled = rightEl != null;
                if (rightEl != null)
                {
                    SelectTreeNode(rightEl);
                    Selector.Highlight(rightEl);                    
                }
            }
        }

  
        #endregion
   
        public event EventHandler XPathChanged;
        /// <summary>
        /// 当前选择元素路径
        /// </summary>
        public string CurrentXPath
        {
            get
            {
                return xpathBox.Text;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            treeDom = new DomTreeView();
            treeDom.Dock = DockStyle.Fill;
            treeDom.LabelEdit = true;
            contentPanel.Controls.Add(treeDom);
            xpathBox.KeyUp += new KeyEventHandler(xpathBox_KeyUp);
            base.OnLoad(e);
        }
     

        void xpathBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(xpathBox.Text))
                {
                    Selector.HighlithBySelector(xpathBox.Text);
                    treeDom.SelectNodes(Selector.SelectorElements);
                }
            }
        }

        void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "toolStripLocationButton")
            {
                if (!string.IsNullOrEmpty(xpathBox.Text))
                {
                    Selector.HighlithBySelector(xpathBox.Text);
                    treeDom.SelectNodes(Selector.SelectorElements);
                }
            }
            else if (e.ClickedItem.Name == "refreshStripButton") {
                RefreshTreeView();
            }
        }
         

        private int pageHashCode = 0;
        /// <summary>
        /// 更新整个树信息，当浏览器数据加载完成后
        /// </summary>
        /// <param name="data"></param>
        public void UpdateView(MyBrowser browser)
        {
            //避免重复加载数据
            int hashCode = browser.LocationUrl.GetHashCode();
            if (pageHashCode == hashCode) return;
            pageHashCode = hashCode;
            RefreshTreeView();         
        }

        void DomTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {         
            var el = e.Node.Tag as IHTMLElement;
            Selector.Highlight(el);                     
            UpdateXPath(el);            
        }

        /// <summary>
        /// 挂载浏览器
        /// </summary>
        public void Attach(MyBrowser browser) {
            
            this.browser = browser;
            this.Selector = browser.Selector;
            if (treeDom == null) return;
             
            browser.WBKeyUp -= new csExWB.WBKeyUpEventHandler(browser_WBKeyUp);
            selectorStripButton.CheckedChanged -= new EventHandler(selectorStripButton_CheckedChanged);
            browser.WBLButtonDown -= new HTMLMouseEventHandler(MyBrowser_WBLButtonDown);
            browser.WBLButtonUp -= new HTMLMouseEventHandler(MyBrowser_WBLButtonUp);

            browser.WBKeyUp += new csExWB.WBKeyUpEventHandler(browser_WBKeyUp);
            selectorStripButton.CheckedChanged += new EventHandler(selectorStripButton_CheckedChanged);
            browser.WBLButtonDown += new HTMLMouseEventHandler(MyBrowser_WBLButtonDown);
            browser.WBLButtonUp += new HTMLMouseEventHandler(MyBrowser_WBLButtonUp);

            
            treeDom.AfterSelect -= new TreeViewEventHandler(DomTreeView_AfterSelect);
            treeDom.AfterSelect += new TreeViewEventHandler(DomTreeView_AfterSelect);
            browser.DocumentFullComplete += browser_DocumentFullComplete;
            RefreshTreeView();
        }

     
        void browser_DocumentFullComplete(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        public void RefreshTreeView() {
            if (browser == null) return;
            treeDom.LoadDOM(browser, browser.WebbrowserObject.Document);
        }
        /// <summary>
        /// 构建鼠标点击事件，同时遍历iframe
        /// </summary>
        private void InspectMouseEvent()
        {
            mshtml.HTMLDocument htmlDoc = browser.WebbrowserObject.Document as mshtml.HTMLDocument;
            mshtml.DispHTMLDocument disp = htmlDoc as mshtml.DispHTMLDocument;
            DHTMLEventHandler onmousedownhandler = new DHTMLEventHandler(htmlDoc);
            onmousedownhandler.Handler += new DHTMLEvent(Mouse_Down);
            disp.onmousedown = onmousedownhandler;
            IHTMLElementCollection col = BrowserExtensions.GetFrames((IHTMLDocument2)htmlDoc);
            InspectFrameMouseEvent(col);
        }

        private void InspectFrameMouseEvent(mshtml.IHTMLElementCollection fc)
        {
            if (fc == null) return;
            if (fc.length > 0)
            {
                for (int i = 0; i < fc.length; i++)
                {
                    object id = (object)i;
                    IHTMLWindow2 frameWindow = (IHTMLWindow2)fc.item(id,0);
                    mshtml.HTMLDocument frameDoc = (mshtml.HTMLDocument)frameWindow.document;
                    mshtml.DispHTMLDocument frameDispDoc = (mshtml.DispHTMLDocument)frameDoc;
                    DHTMLEventHandler onmousedownhand = new DHTMLEventHandler(frameDoc);
                    onmousedownhand.Handler += new DHTMLEvent(Mouse_Down);
                    frameDispDoc.onmousedown = onmousedownhand;
                    IHTMLElementCollection col = BrowserExtensions.GetFrames((IHTMLDocument2)frameDoc);
                    InspectFrameMouseEvent(col);
                }
            }
        }
      
        public void Mouse_Down(mshtml.IHTMLEventObj e)
        {
            if (selectorStripButton.Checked)
            {
                e.returnValue = true;
                selectorStripButton.Checked = false;
                IHTMLElement src = (IHTMLElement)e.srcElement;
                Selector.Highlight(src);
                SelectTreeNode(src);
                //logger.Info(e.srcElement.innerText);
            }
        }


        private void UpdateXPath(IHTMLElement element)
        {
            string path = "";
            if (typeComboBox.SelectedIndex == 0)
            {
                path = XPathFinder.GetXPath(element, true);                
            }
            else
            {
                path = this.Selector.GetCssPath(element, false);
            }
            this.xpathBox.Text = path;
        }

        /// <summary>
        /// 选择树节点, 单选择器选中某个元素的时候触发该函数
        /// </summary>
        /// <param name="element"></param>
        private void SelectTreeNode(IHTMLElement element)
        {
            UpdateXPath(element);
            if (treeDom != null)
            {
                treeDom.SelectNode((IHTMLElement)element);
            }
        }

   
    }
}
