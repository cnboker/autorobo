using System;
using System.Windows.Forms;
using AutoRobo.Core.ns;
using AutoRobo.Core.Plugins;
using AutoRobo.Core.UserControls;
using AutoRobo.PlulgIn.Properties;
using AutoRobo.Core;
using AutoRobo.Core.Actions;
using System.Data;
using mshtml;
using System.Runtime.InteropServices;

namespace AutoRobo.PlulgIn.Debuger
{
    
    public partial class DebugerUI : UserControl, IPlugin
    {
        private HtmlTree htmlTree = null;
       // private codeRichEditor codeEditor = null;
        private MyBrowser browser = null;
        private log4net.ILog logger = log4net.LogManager.GetLogger("DebugerUI");

        public DebugerUI()
        {
            InitializeComponent();             
        }

        void browser_DocumentFullComplete(object sender, EventArgs e)
        {
            htmlTree.RefreshTreeView();
        }
   
        protected override void OnLoad(EventArgs e)
        {
            this.browser = Host.Browser as MyBrowser;                  
            browser.DocumentFullComplete += new EventHandler(browser_DocumentFullComplete);
            htmlTree = new HtmlTree();
            htmlTree.Attach(browser);
            htmlTree.Dock = DockStyle.Fill;
            htmlPage.Controls.Add(htmlTree);
            
            OutputPanel output = new OutputPanel();
            output.Dock = DockStyle.Fill;
            outputPage.Controls.Add(output);

            //codeEditor = JintCreator.CreateEditor(browser);
            //scirptPage.Controls.Add(codeEditor);
            //codeEditor.Logger = output;                   
            base.OnLoad(e);
        }

        void ResultSet_DataUpdate(object sender, EventArgs e)
        {
            //UpdateResultSet((DataTable)sender);
            ResultPanel rp = outputPage.Controls[0] as ResultPanel;
            rp.Bind((DataTable)sender);
        }

     
        private void UpdateResultSet(DataTable table) {
            if (InvokeRequired)
            {
                this.Invoke(new Action<DataTable>(UpdateResultSet), table);
                return;
            }
            
            Control resultTabPanel = GetControlByName(table.TableName);
            ResultPanel rp = null;
            if (resultTabPanel == null)
            {
                TabPage page = new TabPage("输出[" + table.TableName + "]");
                page.Name = table.TableName;
                resultTabPanel = page;
                tabControl.Controls.Add(page);               
                rp = new ResultPanel();
                rp.Dock = DockStyle.Fill;
                page.Controls.Add(rp);
            }
            else
            {
                rp = resultTabPanel.Controls[0] as ResultPanel;
            }
            rp.Bind(table);
        }

        private Control GetControlByName(string Name)
        {
            foreach (Control c in tabControl.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }

        public IPluginHost Host
        {
            get;
            set;
        }

        public new void Show() {
            Host.Show(this);
            if (this.Parent != null) {
                htmlTree.UpdateView(browser);
            }
            base.Show();
        }

        public Keys DataKey
        {
            get { return Keys.F12; }
        }


        public string StripItemText
        {
            get { return "调试器[F12]"; }
        }

        public System.Drawing.Bitmap StripItemImage
        {
            get { return Resources.debugBtn; }
        }
    }
}
