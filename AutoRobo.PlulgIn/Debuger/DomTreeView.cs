using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using mshtml;
using WatiN.Core;

namespace AutoRobo.PlulgIn.Debuger
{
    public partial class DomTreeView : MultiSelectTreeview
    {
        private const string Textnode = "#text";
        private const string Commentnode = "#comment";
        private const string Framenode = "FRAME";
        private const string Iframenode = "IFRAME";
        private Hashtable nodeHash = new Hashtable();
                
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DomTreeView));
        ICoreBrowser browser = null;
        //展开2级
        private const int expandDeep = 2;
        private int intervalTimes = 0;
        private Selector Selector = null;
        //private Browser watinBrowser = null;

        public DomTreeView() {            
            NodeMouseClick += new TreeNodeMouseClickEventHandler(DomTreeView_NodeMouseClick);
            LabelEdit = true;           
            
        }

    
        /// <summary>
        /// Starting point to walk the DOM
        /// </summary>
        /// <param name="documentObject">Webbrowser.Document object</param>
        public void LoadDOM(ICoreBrowser browser, object documentObject)
        {       
            this.browser = browser;
            Selector = browser.Selector;
           
              //watinBrowser = new BrowserWindow((MyBrowser)browser);
            nodeHash.Clear();
            Nodes.Clear();

            if (documentObject == null) return;
            IHTMLDOMNode domnode = null;
            try
            {
                var doc3 = documentObject as IHTMLDocument3;
                if (doc3 == null) return;
                domnode = (IHTMLDOMNode)doc3.documentElement;              
            }
            catch 
            {
                try
                {
                    var doc = documentObject as mshtml.IHTMLDocument3;
                    domnode = doc.documentElement as IHTMLDOMNode;
                }
                catch (Exception ex1) {
                    logger.Fatal(ex1);
                }       
            }

            //Start walking
            if (domnode == null) return;
            var top = NewDomNode(domnode);
            Nodes.Add(top);
            //top.IsLoadData = true;
            //ParseNodes(domnode, top, expandDeep);
            //top.Expand();
        }

    
        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="domnode"></param>
        /// <returns></returns>
        private DomTreeNode NewDomNode(IHTMLDOMNode domnode)
        {
            string text = "";
            if (Commentnode == domnode.nodeName)
            {
                if (domnode.nodeValue != null)
                {
                    text = "<!" + domnode.nodeValue + ">";
                }
            }
            else {
                text = string.Format("<{0}{1}>", domnode.nodeName.ToLower(), ToAttributeString(domnode));            
            }
            DomTreeNode treeNode = new DomTreeNode();
            treeNode.Text = text;
            treeNode.Tag = domnode;

            if (!nodeHash.Contains(domnode))
            {
                nodeHash.Add(domnode, treeNode);
            }
            return treeNode;
        }
        /// <summary>
        /// Recursive method to walk the DOM, acounts for frames
        /// </summary>
        /// <param name="node">Parent DOM node to walk</param>
        /// <param name="deep">tree deep</param>
        /// <returns></returns>
        private void ParseNodes(IHTMLDOMNode nd, TreeNode node, int deep)
        {
            deep--;
            try
            {
                intervalTimes++;
                if (intervalTimes > 20)
                {
                    Application.DoEvents();
                    intervalTimes = 0;
                }
                if (nd.nodeName == Framenode || nd.nodeName == Iframenode)
                {
                    mshtml.DispHTMLDocument doc3 = (mshtml.DispHTMLDocument)((SHDocVw.IWebBrowser2)nd).Document;
              
                    var tempnode = (IHTMLDOMNode)doc3.documentElement;
                    //get the comments for this node, if any
                    var framends = (IHTMLDOMChildrenCollection)doc3.childNodes;
                    foreach (IHTMLDOMNode tmpnd in framends)
                    {
                        IHTMLElement el = tmpnd as IHTMLElement;                        
                        var subTreeNode = NewDomNode(tmpnd);                        
                        node.Nodes.Add(subTreeNode);                      
                        subTreeNode.IsLoadData = true;
                        ParseNodes(tmpnd, subTreeNode, expandDeep);      
                    }
                    return;
                }

                //Get the DOM collection
                bool hasElement = false;
                foreach (IHTMLDOMNode childNode in nd.childNodes)
                {
                    if (childNode.nodeName == Textnode)
                    {                        
                        node.Text += string.Format("{0}", childNode.nodeValue);
                    }
                    else if (childNode.nodeName == Commentnode) { }
                    else{
                        hasElement = true;   
                        var subNode = NewDomNode(childNode);
                        BeginUpdate();
                        node.Nodes.Add(subNode);
                        EndUpdate();                 
                        if (deep > 0)
                        {
                            subNode.IsLoadData = true;
                            ParseNodes(childNode, subNode, deep);
                        }
                    }
                }
                if (!hasElement)
                {
                    node.Text += string.Format("</{0}>", nd.nodeName.ToLower());
                }

            }
            catch (Exception ex) {
                logger.Fatal(ex);
            }
        }

        private string ToAttributeString(IHTMLDOMNode nd)
        {
            if (nd.attributes == null) return "";
            StringBuilder sb = new StringBuilder();
            foreach (IHTMLDOMAttribute att in nd.attributes)
            {
                if (att.nodeName == "style") continue;
                if (!att.specified) continue;
                if (att.nodeValue == null) continue;
                TreeNode childItem = new TreeNode();
                sb.Append(" ");
                sb.Append(att.nodeName);
                sb.Append("=");
                sb.Append("\"");
                sb.Append(att.nodeValue);
                sb.Append("\"");
            }
            return sb.ToString();
        }

        void DomTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DomTreeNode node = e.Node as DomTreeNode;
            if (!node.IsExpanded && !node.IsLoadData)
            {
                node.IsLoadData = true;
                ParseNodes((IHTMLDOMNode)node.Tag, node, expandDeep);
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                TreeViewHitTestInfo info = this.HitTest(e.X, e.Y);
                this.ContextMenuStrip = CreateContextMenu(node);
                this.ContextMenuStrip.Show();
            }
        }

        private System.Windows.Forms.ContextMenuStrip CreateContextMenu(DomTreeNode node)
        {
            IHTMLElement element = node.Tag as IHTMLElement;

            ContextMenuStrip strip = new ContextMenuStrip();


            ToolStripMenuItem item = new ToolStripMenuItem("复制HTML");
            item.Name = "copyHTML";
            item.Tag = element;
            strip.Items.Add(item);

            item = new ToolStripMenuItem("复制innerHTML");            
            item.Name = "copyInnerHTML";
            item.Tag = element;
            strip.Items.Add(item);

            item = new ToolStripMenuItem("复制XPATH");            
            item.Name = "copyXPATH";
            item.Tag = element;
            strip.Items.Add(item);

            item = new ToolStripMenuItem("删除");
            item.Name = "delete";
            item.Tag = element;
            strip.Items.Add(item);


            strip.ItemClicked += new ToolStripItemClickedEventHandler(strip_ItemClicked);
            return strip;
        }

        void strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem item = e.ClickedItem as ToolStripMenuItem ;
            IHTMLElement element = item.Tag as IHTMLElement;
           switch (item.Name) { 
               case "copyHTML":
                   Clipboard.SetText(element.outerHTML);
                   break;
               case "copyInnerHTML":
                   Clipboard.SetText(element.innerHTML);
                   break;
               case "copyXPATH":
                   Clipboard.SetText(XPathFinder.GetXPath(element, true));
                   break;
               case "delete":
                   TreeNode node = this.SelectedNode;
                   if (node != null) {
                       IHTMLDOMNode domNode = node.Tag as IHTMLDOMNode;
                       domNode.parentNode.removeChild(domNode);
                       node.Remove();
                   }
                   break;          
               default:
                   break;
           }
        }
         

     
        /// <summary>
        /// 通过元素选择节点
        /// </summary>
        /// <param name="el"></param>
        public void SelectNode(IHTMLElement el)
        {      
            PopulateNode((IHTMLDOMNode)el);
            TreeNode treeNode = nodeHash[el] as TreeNode;
            //log(treeNode);
            SelectedNode = treeNode;
            treeNode.EnsureVisible();
        }
        private void log(TreeNode treeNode)
        {
            List<string> log = new List<string>();
            log.Add(((IHTMLDOMNode)treeNode.Tag).nodeName);
            TreeNode pn = treeNode.Parent;
            while (pn != null)
            {
                IHTMLDOMNode n = (IHTMLDOMNode)pn.Tag;
                string prefix = "";
                if (n.attributes != null)
                {
                    IHTMLDOMAttribute id = n.attributes.item("id") as IHTMLDOMAttribute;
                    if (id != null && id.nodeValue !=null)
                    {
                        prefix = "[" + id.nodeValue.ToString() + "]";
                    }                   
                }
                log.Add(n.nodeName + prefix);
                pn = pn.Parent;
            }
            log.Reverse();
            logger.Info(string.Join("/", log.ToArray()));
        }
        
        public void SelectNodes(IHTMLElement[] elements) {
            List<TreeNode> treeNodes = new List<TreeNode>();
            foreach (var el in elements)
            {
                PopulateNode((IHTMLDOMNode)el);
                DomTreeNode node = nodeHash[el] as DomTreeNode;
                if (node != null)
                {
                    treeNodes.Add(node);
                }
            }
            foreach (var node in treeNodes)
            {
                //node.BeginEdit();
                node.EnsureVisible();
                //node.EndEdit(false);
            }

            SelectedNodes = treeNodes;
        }

      
        /// <summary>
        /// 获取当前节点的父树节点和DOM父节点，如果不存在则创建它
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parentTreeNode"></param>
        /// <param name="parentNode"></param>
        private void GetOrCreateParent(IHTMLDOMNode node, out TreeNode parentTreeNode, out IHTMLDOMNode parentNode)
        {
            parentTreeNode = null;
            parentNode = null;
            if (node == null) return ;
            IHTMLDOMNode parent = node.parentNode;
            if ((parent.nodeName != "#document"))
            {  
                parentTreeNode = GetTreeNode(parent);
                if (parentTreeNode == null)
                {
                    parentTreeNode = NewDomNode(parent);
                }
                parentNode = parent;
            }
            else
            {
                //iframe node
                parent = BrowserExtensions.GetFrame((mshtml.IHTMLDocument2)parent) as IHTMLDOMNode;
                if (parent == null) return;
                //获取iframe树节点，如果不存在则创建它
                parentTreeNode = GetTreeNode(parent);
                if (parentTreeNode == null)
                {
                    parentTreeNode = NewDomNode(parent);                   
                }
                //检查当前节点是否是iframe的子节点，如果不是怎增加为子节点
                TreeNode curNode = GetTreeNode(node);
                if (!parentTreeNode.Nodes.Contains(curNode))
                {
                    parentTreeNode.Nodes.Add(curNode);
                }
                GetOrCreateParent(parent, out parentTreeNode, out parentNode);
            }
        }

        private void PopulateNode(IHTMLDOMNode node)
        {
            if (node == null) return;         
            TreeNode parentTreeNode;
            IHTMLDOMNode parent;
            GetOrCreateParent(node, out parentTreeNode, out parent);
            if (parent == null) return;
            foreach (IHTMLDOMNode childNode in parent.childNodes)
            {
                if (childNode.nodeName == Commentnode) continue;
                if (childNode.nodeName == Textnode)
                {
                    var val = childNode.nodeValue.ToString().Trim().Replace("\n", "").Replace("\t", "");
                    if (!string.IsNullOrEmpty(val))
                    {
                        parentTreeNode.Text += string.Format("{0}", childNode.nodeValue);
                    }
                }
                else
                {
                    AddNewNode(parentTreeNode, childNode);                 
                }
            }
            PopulateNode(parent);
        }

        private void AddNewNode(TreeNode parentTreeNode, IHTMLDOMNode node) {
            TreeNode treeNode = GetTreeNode(node);
            if (treeNode == null)
            {
                treeNode = NewDomNode(node);
                
            }
            if (!parentTreeNode.Nodes.Contains(treeNode) && treeNode.Parent == null) {
                parentTreeNode.Nodes.Add(treeNode);
            }
        }

        private TreeNode GetTreeNode(IHTMLDOMNode node)
        {
            if (nodeHash.Contains(node))
            {
                return nodeHash[node] as TreeNode;
            }
            return null;
        }    

        /// <summary>
        /// 加载单个树节点
        /// </summary>
        /// <param name="el"></param>
        /// <param name="dataDict"></param>
        private void PopulateNode(IHTMLElement el, List<IHTMLElement> dataDict)
        {
            if (el == null) return;
            DomTreeNode node = null;
            if (nodeHash.Contains(el))
            {
                node = nodeHash[el] as DomTreeNode;
                
                dataDict.Reverse();
                foreach (var o in dataDict)
                {
                    if (node == null) continue;
                    //logger.Info("add treenode:" + XPathFinder.GetXPath(o, true));

                    IHTMLDOMNode parent = ((IHTMLDOMNode)o).parentNode;
                    if (parent == null)
                    {
                        parent = (IHTMLDOMNode)BrowserExtensions.GetFrame((IHTMLElement)o);
                    }
                    ParseNodes(parent, node, 1);
                    node.IsLoadData = true;
                    node = nodeHash[o] as DomTreeNode;
                }
            }
            else
            {
                dataDict.Add(el);
                IHTMLDOMNode cnode = el as IHTMLDOMNode;
                IHTMLElement parentEl = cnode.parentNode as IHTMLElement;
                if (parentEl == null)
                {
                    IHTMLElement frame = BrowserExtensions.GetFrame((IHTMLElement)el);
                    PopulateNode(frame, dataDict);
                }
                else
                {
                    PopulateNode(parentEl, dataDict);
                }
            }                           
        }
   
    }
}
