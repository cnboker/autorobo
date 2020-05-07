using System;
using System.IO;
using System.Windows.Forms;
using AutoRobo.Core;
using System.Linq;
using System.Security.Permissions;
using AutoRobo.Makers.Presentation;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace AutoRobo.Makers.Views
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class FileView : ViewBase, IFileView
    {
        FilePresenter presener = null;
        FileSystemWatcher watcher = null;

        public FileView()
        {
            InitializeComponent();
            this.TabText = "项目";
            treeView1.LabelEdit = true;
            presener = new FilePresenter(this);
            CloseButtonVisible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            RegisterEvent();
            WacherFile();
            TreeLoad();
            base.OnLoad(e);
        }

        private void WacherFile() {
            //监视外部文件改变，重新加载文件
            watcher = new FileSystemWatcher();
            watcher.Path = AppSettings.Instance.LibraryPath;
            /* Watch for changes in LastAccess and LastWrite times, and
           the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;
            // Only watch text files.
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            TreeLoad();
        }

        private void TreeLoad() {
            if (InvokeRequired) {
                Invoke(new Action(TreeLoad));
                return;
            }
            treeView1.Nodes.Clear();
            var rootDirectory = new DirectoryInfo(AppSettings.Instance.LibraryPath);         
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            treeView1.Nodes.Add(node);
            ListDirectory(node, rootDirectory);
            node.Expand();        
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            TreeLoad();
        }

        private void RegisterEvent()
        {
            treeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseDoubleClick);
            treeView1.BeforeExpand += new TreeViewCancelEventHandler(treeView1_BeforeExpand);            
            treeView1.AfterLabelEdit += new NodeLabelEditEventHandler(treeView1_AfterLabelEdit);
            toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);           
        }

        void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            Rename(e);
            watcher.EnableRaisingEvents = true;
        }

        void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            if (e.ClickedItem == createFileButton) {
                CreateFile();
            }
            else if (e.ClickedItem == createDirButton) {
                CreateDir();
            }
            else if (e.ClickedItem == renameButton) {
                TreeNode node = treeView1.SelectedNode;
                if (node == null) return;
                node.BeginEdit();
            }
            else if (e.ClickedItem == deleteButton) {
                if (treeView1.SelectedNode == null) return;
                if (MessageBox.Show("确定要删除此项吗?", "确认操作", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                    DirectoryInfo dir = treeView1.SelectedNode.Tag as DirectoryInfo;
                    try
                    {
                        if (dir != null)
                        {                           
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(dir.FullName,
                                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                                RecycleOption.SendToRecycleBin);
                        }
                        else
                        {
                            FileInfo file = treeView1.SelectedNode.Tag as FileInfo;
                            //file.Delete();
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file.FullName,
                                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                                RecycleOption.SendToRecycleBin);
                        }
                        treeView1.SelectedNode.Remove();
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            watcher.EnableRaisingEvents = true;
        }

        private void Rename(NodeLabelEditEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
            TreeNode parent = e.Node.Parent;
            if (parent == null) return;
            DirectoryInfo parentDir = parent.Tag as DirectoryInfo;
            //是目录
            if (e.Node.SelectedImageIndex == 0)
            {
                string editdir = "";
                //修改目录名称
                if (e.Node.Tag != null)
                {
                    //节点为修改，不处理
                    if (e.Label == null) return;
                    editdir = Path.Combine(parentDir.FullName + "\\" + e.Label);
                    DirectoryInfo cur = e.Node.Tag as DirectoryInfo;
                    if (cur.FullName != editdir)
                    {
                        try
                        {
                            Directory.Move(cur.FullName, editdir);
                            var dir  = new DirectoryInfo(editdir);
                            e.Node.Tag = dir;
                            UpdateDirectory(e.Node, dir);
                        }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message);                            
                            e.Node.BeginEdit();
                        }
                    }
                }
                else {
                    string name = e.Node.Text;
                    if (e.Label != null) {
                        name = e.Label;
                    }
                    editdir = Path.Combine(parentDir.FullName + "\\" + name);
                    if (!Directory.Exists(editdir))
                    {
                        e.Node.Tag = Directory.CreateDirectory(editdir);
                        e.Node.EndEdit(false);
                    }
                    else {
                        MessageBox.Show("当前目录已经存在");
                        e.CancelEdit = true;
                    }
                }               
            }//是文件
            else {
                string editfile ="";
                //修改文件名称
                if (e.Node.Tag != null)
                {
                    if (e.Label == null) return;
                    editfile = Path.Combine(parentDir.FullName + "\\" + e.Label);
                    FileInfo cur = e.Node.Tag as FileInfo;
                    
                    if (cur.FullName != editfile)
                    {
                        try
                        {                            
                            File.Move(cur.FullName, editfile);
                            e.Node.Tag = new FileInfo(editfile);
                        }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message);
                            e.CancelEdit = true;
                        }
                    }
                }
                else
                {
                    string name = e.Node.Text;
                    if (e.Label != null)
                    {
                        name = e.Label;
                    }
                    editfile = Path.Combine(parentDir.FullName + "\\" + name);
                    if (!File.Exists(editfile))
                    {
                        StreamWriter fs = null;
                        try
                        {
                            fs = new StreamWriter(editfile);
                            fs.Write(@"<?xml version=""1.0"" encoding=""utf-8\""?>
<AutoScript>
<MainAction>    
</MainAction>
<FunctionAction />
<VariableAction>   
</VariableAction>
</AutoScript>
                        ");
                        }
                        finally
                        {
                            fs.Close();
                        }
                        e.Node.Tag = new FileInfo(editfile);
                        e.Node.EndEdit(false);
                    }
                    else
                    {
                        MessageBox.Show("当前文件已经存在");                        
                        e.Node.BeginEdit();
                    }
                }               
            }
           
        }

        private void CreateDir()
        {
            if (treeView1.SelectedNode == null) return;
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.IsEditing) {
                treeView1.SelectedNode.EndEdit(false);
            }
            TreeNode node = treeView1.SelectedNode;
            if (node == null) return;
            DirectoryInfo dir = node.Tag as DirectoryInfo;
            if (dir == null) return;
            string newdir = GetNewName(false, dir.GetDirectories().Select(c => c.Name).ToArray());
            var newNode = new TreeNode(newdir);
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 0;
            
            node.Nodes.Add(newNode);
            treeView1.SelectedNode = newNode;
            newNode.BeginEdit();          
        }

        private string GetNewName(bool isFile, string[] existNames) {
            string prefixName = "新文件";
            if (!isFile) {
                prefixName = "新目录";
            }
            int index = 1;
            while (existNames.Any(c => c == (prefixName + index.ToString())))
            {
                index++;
            }
            return prefixName + index.ToString();
        }

        private void CreateFile()
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null) return;
            if (node.IsEditing)
            {
                node.EndEdit(false);
            }
            DirectoryInfo dir = node.Tag as DirectoryInfo;
            if (dir == null)
            {
                node = node.Parent;
                dir = node.Tag as DirectoryInfo;
            }

            NewFileDialogForm f = new NewFileDialogForm(dir.FullName);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK) {                              
                //string newFileName = GetNewName(true, dir.GetFiles().Select(c => c.Name.Substring(0, c.Name.IndexOf("."))).ToArray()) + ".bit";
                var newNode = new TreeNode(f.FileName);
                newNode.ImageIndex = 1;
                newNode.SelectedImageIndex = 1;
                node.Nodes.Add(newNode);
                treeView1.SelectedNode = newNode;
                newNode.BeginEdit();
                newNode.EndEdit(false);
            }
                                       
        }

        void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                ListDirectory(e.Node, (DirectoryInfo)e.Node.Tag);
            }
        }

        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DirectoryInfo cur = e.Node.Tag as DirectoryInfo;
            if (cur != null) {
                UpdateDirectory(e.Node, cur);
            }
            if (LoadFile != null) {
                LoadFile(e.Node, EventArgs.Empty);
            }
        }

        private void UpdateDirectory(TreeNode currentNode, DirectoryInfo directoryInfo) {
            currentNode.Nodes.Clear();
            ListDirectory(currentNode, directoryInfo);
        }

        class IconInfo {
            public string FileExt { get; set; }
            public int ImageIndex { get; set; }
        }
        private List<IconInfo> iconList = new List<IconInfo>();

        private void ListDirectory(TreeNode currentNode, DirectoryInfo directoryInfo)
        {            
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };                
                currentNode.Nodes.Add(childDirectoryNode);
                childDirectoryNode.Nodes.Add("*");
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Attributes == FileAttributes.Hidden) continue;
                FileInfo info = new FileInfo(file.FullName);
                IconInfo iconInfo = iconList.FirstOrDefault(c => c.FileExt == info.Extension);
                if (iconInfo == null)
                {
                    System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                    imageList1.Images.Add(icon);
                    iconInfo = new IconInfo() { FileExt = info.Extension, ImageIndex = imageList1.Images.Count - 1 };
                    iconList.Add(iconInfo);                    
                }
                currentNode.Nodes.Add(new TreeNode(file.Name) { ImageIndex = iconInfo.ImageIndex, SelectedImageIndex = iconInfo.ImageIndex, Tag = file });
            }
        }



        public event EventHandler LoadFile;

        private void openDirToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppSettings.Instance.LibraryPath);
        }

        
    }
}
