using System;
using System.Windows.Forms;
using AutoRobo.Core;
using Util;
using System.Diagnostics;
using AutoRobo.WebHelper;
using System.IO;
using AutoRobo.WebHelper.Winform;
using System.Threading;
using System.Collections;

namespace AutoRobo.ClientLib.Ants
{
    public partial class BrowserSettings : Form
    {
        private TestProxyForm testProxyForm = new TestProxyForm();
        public bool DownloadImages {
            get {
                return downloadImages.Checked;
            }
            set {
                downloadImages.Checked = value;
            }
        }
        public bool DownloadVideo
        {
            get
            {
                return downloadVideo.Checked;
            }
            set
            {
                downloadVideo.Checked = value;
            }
        }
        public bool DownloadSounds
        {
            get
            {
                return downloadSounds.Checked;
            }
            set {
                downloadSounds.Checked = value;
            }
        }
        public bool DownloadActivex
        {
            get
            {
                return downloadActivex.Checked;
            }
            set
            {
                downloadActivex.Checked = value;
            }
        }

        public bool DownloadFlash {
            get {
                return downloadFlash.Checked;
            }
            set
            {
                downloadFlash.Checked = value;
            }
        }

        public bool AllowScriptError
        {
            get { return this.enableScriptError.Checked; }
            set { enableScriptError.Checked = value; }
        }

        public bool AllowAlert
        {
            get { return allowAlert.Checked; }
            set { allowAlert.Checked = value; }
        }
        public event EventHandler ClearCache;

        public BrowserSettings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.listBoxProxy.Columns.Add("代理服务器列表");
            this.listBoxProxy.Columns[0].Width = 200;

            var setting = AppSettings.Instance;
            debugCheckBox.Checked = setting.Debug;
            DownloadImages = setting.DownloadImages;
            DownloadSounds = setting.DownloadSounds;
            DownloadVideo = setting.DownloadVideo;
            DownloadActivex = setting.DownloadActiveX;
            DownloadFlash = setting.DownloadFlash;
            AllowAlert = setting.AllowAlert;
            AllowScriptError = setting.AllowScriptError;
            offlineCheckBox.Checked = setting.OfflineMode;
                    
            defaultPathTextBox.Text = setting.LibraryPath;
            //加载优化器配置
            LoadEngineSettings();
            useProxyCheckBox_CheckedChanged(checkBoxEnableProxy, EventArgs.Empty);
            LoadProxyPage();
            tabControl1.Controls.Remove(tabPage4);
            base.OnLoad(e);
        }

     
        private void LoadEngineSettings() {
            var setting = AppSettings.Instance;
           
            adslGroupBox.Enabled = false;
            if (setting.EngineSettings == null) return;
            switch (setting.EngineSettings.Networking) { 
                case EngineNetWorkEnum.None:
                    noneRadioButton.Checked = true;
                    break;
                case EngineNetWorkEnum.System:
                    systemRadioButton.Checked = true;
                    break;
                case EngineNetWorkEnum.ADSL:
                    adslGroupBox.Enabled = true;
                    adslRadioButton.Checked = true;
                    break;
                default:
                    break;
            }
            adslNameBox.Text = setting.EngineSettings.ADSLName;
            dialTimeBox.Text = setting.EngineSettings.DialTime.ToString();
            adslRadioButton.CheckedChanged += new EventHandler(adslRadioButton_CheckedChanged);
        }

        private void SaveEngineSettings() {
            var setting = AppSettings.Instance;
            if (setting.EngineSettings == null) {
                setting.EngineSettings = new EngineSettings();
            }
            EngineNetWorkEnum enw = EngineNetWorkEnum.None;
            if (systemRadioButton.Checked) {
                enw = EngineNetWorkEnum.System;
            }
            else if (adslRadioButton.Checked) {
                enw = EngineNetWorkEnum.ADSL;
            }
            
            if (enw == EngineNetWorkEnum.ADSL) {              
                    AdslDataValidate();                
            }
            setting.EngineSettings.Networking = enw;
            setting.EngineSettings.ADSLName = adslNameBox.Text;
            setting.EngineSettings.DialTime = Convert.ToInt32(dialTimeBox.Text);
          
        }

        private void AdslDataValidate() {
            int dialTime = 300;
            if (string.IsNullOrEmpty(adslNameBox.Text))
            {
                throw new ApplicationException("请输入ADSL名称");
            }
            if (string.IsNullOrEmpty(dialTimeBox.Text))
            {
                throw new ApplicationException("请输入ADSL重新拨号周期");
            }
            bool result = Int32.TryParse(dialTimeBox.Text, out dialTime);
            if (!result)
            {
                throw new ApplicationException("ADSL拨号周期不是有效数据");
            }
            if (dialTime < 30)
            {
                throw new ApplicationException("ADSL拨号周期不能低于30秒");
            }
        }
        void adslRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            adslGroupBox.Enabled = adslRadioButton.Checked;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            var setting = AppSettings.Instance;
            setting.Debug = debugCheckBox.Checked;
            setting.DownloadImages = DownloadImages;
            setting.DownloadSounds = DownloadSounds;
            setting.DownloadVideo = DownloadVideo;
            setting.DownloadActiveX = DownloadActivex;
            setting.DownloadFlash = DownloadFlash;
            setting.AllowAlert = AllowAlert;
            setting.OfflineMode = offlineCheckBox.Checked;
            setting.AllowScriptError = AllowScriptError;
           
            setting.LibraryPath = defaultPathTextBox.Text;

            SaveEngineSettings();
            SaveProxyPage();
            AppSettings.Instance.Save();
            Close();
        }

        private void LoadProxyPage()
        {
            try
            {
                var setting = AppSettings.Instance;
                this.checkBoxEnableProxy.Checked = setting.ProxyEnabled;
                if (setting.ProxyDataList != null)
                {
                    foreach (string str in setting.ProxyDataList)
                    {
                        PROXY_DATA proxyData = GetProxyData(str);
                        this.listBoxProxy.Items.Add(proxyData.address);
                        this.listBoxProxy.Items[this.listBoxProxy.Items.Count - 1].Tag = str;
                    }
                }
                this.checkBoxRotateProxy.Checked = setting.ProxyRotate;
                this.textBoxInterval.Text = setting.ProxyRotateInterval.ToString();
            }
            catch (Exception)
            {
            }
        }
        private bool SaveProxyPage()
        {
            bool flag = true;
            var setting = AppSettings.Instance;
            if (!this.checkBoxEnableProxy.Checked)
            {
                setting.ProxyEnabled = this.checkBoxEnableProxy.Checked;
                return flag;
            }
            if (this.listBoxProxy.Items.Count == 0)
            {
                MessageBox.Show("无代理服务器数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (setting.ProxyDataList == null)
            {
                setting.ProxyDataList = new ArrayList();
            }
            setting.ProxyRotate = this.checkBoxRotateProxy.Checked;
            try
            {
                setting.ProxyRotateInterval = byte.Parse(this.textBoxInterval.Text);
            }
            catch (Exception ex)
            {
                if (this.checkBoxRotateProxy.Checked)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    flag = false;
                }
            }
            if (setting.ProxyRotateInterval == 0)
            {
                MessageBox.Show("轮询时间必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                setting.ProxyRotateInterval = 1;
                flag = false;
            }
            setting.ProxyDataList.Clear();
            foreach (ListViewItem item in this.listBoxProxy.Items)
            {
                setting.ProxyDataList.Add(item.Tag);
            }
            setting.ProxyEnabled = this.checkBoxEnableProxy.Checked;
            return flag;
        }

        private void clrCacheBtn_Click(object sender, EventArgs e)
        {
            if (ClearCache != null)
            {
                ClearCache(sender, EventArgs.Empty);
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            Process ieProcess = Process.Start("iexplore", string.Format("{0}/swiki/page/adsl settings", StringHelper.Domain)); 
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AdslDataValidate();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            string adslName = adslNameBox.Text;
            RASDisplay ras = new RASDisplay();
            try
            {
                if (!ras.IsConnected)
                {
                    ras.Connect(adslName);
                    return;
                }
                ras.Disconnect();//断开连接
                //Thread.Sleep(2000);
                ras.Connect(adslName);//重新拨号
                MessageBox.Show("测试成功");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void g1_Enter(object sender, EventArgs e)
        {

        }

        private void selectFolderBtn_Click(object sender, EventArgs e)
        {
            string _folderName = "c:/";
            _folderName = (System.IO.Directory.Exists(defaultPathTextBox.Text)) ? _folderName : "";
            var dlg1 = new FolderBrowserDialogEx
            {
                Description = "选择库目录:",
                ShowNewFolderButton = true,
                ShowEditBox = true,
                //NewStyle = false,
                SelectedPath = _folderName,
                ShowFullPathInEditBox = false,
            };
            dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;

            var result = dlg1.ShowDialog();

            if (result == DialogResult.OK)
            {
                defaultPathTextBox.Text = dlg1.SelectedPath;               
            }
        }

        private void useProxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {            
            foreach (Control c in tabPage2.Controls)
            {
                c.Enabled = checkBoxEnableProxy.Checked;
            }
            textBoxUserName.Enabled = checkBoxRequiredAuth.Checked;
            textBoxPassword.Enabled = checkBoxRequiredAuth.Checked;
            textBoxInterval.Enabled = checkBoxRotateProxy.Checked;
            checkBoxEnableProxy.Enabled = true;
        }

        #region 代理操作
        private void proxyAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.addressBox.Text.Trim() == "")
                {
                    MessageBox.Show("地址不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (this.portBox.Text.Trim() == "")
                {
                    MessageBox.Show("端口不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (this.checkBoxRequiredAuth.Checked && ((this.textBoxUserName.Text.Trim() == "") || (this.textBoxPassword.Text.Trim() == "")))
                {
                    MessageBox.Show("用户或密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string text = this.addressBox.Text + ":" + this.portBox.Text;
                    //if (!text.ToLower().StartsWith("http"))
                    //{
                    //    text = "http://" + text;
                    //}
                    this.listBoxProxy.Items.Add(text);
                    string str2 = text;
                    if (this.checkBoxRequiredAuth.Checked)
                    {
                        str2 = (str2 + " " + this.textBoxUserName.Text) + " " + this.textBoxPassword.Text;
                    }
                    this.listBoxProxy.Items[this.listBoxProxy.Items.Count - 1].Tag = str2;
                    this.addressBox.Text = "";
                    this.portBox.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = "txt",
                Filter = "TXT file (*.txt)|*.txt|CSV file (*.csv)|*.csv| All files (*.*)|*.*",
                AddExtension = true,
                RestoreDirectory = true
            };
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string str2 in File.ReadAllText(dialog.FileName).Replace("\r", "").Split(new char[] { ',', ';', '\n',' ' }))
                    {
                        if (str2 != "")
                        {
                            PROXY_DATA proxyData = GetProxyData(str2);
                            this.listBoxProxy.Items.Add(proxyData.address);
                            this.listBoxProxy.Items[this.listBoxProxy.Items.Count - 1].Tag = str2;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

       

        public static PROXY_DATA GetProxyData(string proxyDataString)
        {
            PROXY_DATA proxy_data = new PROXY_DATA();
            string[] strArray = proxyDataString.Split(new char[] { ' ', '\t' });
            if (strArray.Length > 1)
            {
                proxy_data.address = strArray[0] + ":" + strArray[1];
            }
            //if (strArray.Length > 1)
            //{
            //    proxy_data.username = strArray[1];
            //}
            //if (strArray.Length > 2)
            //{
            //    proxy_data.password = strArray[2];
            //}
            return proxy_data;
        }


        private void proxyTestBtn_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(this.TestProxies));
            thread.Start();
            this.testProxyForm.StartPosition = FormStartPosition.CenterParent;
            this.testProxyForm.ShowDialog();
            thread.Abort();
        }

        private void TestProxies()
        {
            int num = 1;
            int count = this.listBoxProxy.Items.Count;
            for (int i = this.listBoxProxy.Items.Count - 1; i >= 0; i--)
            {
                string status = "(" + num.ToString() + " of " + count.ToString() + ")";
                this.testProxyForm.SetStatus(status);
                num++;
                if (!ProxyManager.CheckProxy(GetProxyData(this.GetProxyInfoString(i))))
                {
                    this.RemoveProxyAt(i);
                }
            }
            this.CloseTestProxyForm();
        }

        private void CloseTestProxyForm()
        {
            if (this.testProxyForm.InvokeRequired)
            {
                CloseTestProxyFormCallback method = new CloseTestProxyFormCallback(this.CloseTestProxyForm);
                base.Invoke(method, null);
            }
            else
            {
                this.testProxyForm.Close();
            }
        }

        private void RemoveProxyAt(int i)
        {
            if (this.listBoxProxy.InvokeRequired)
            {
                RemoveProxyAtCallback method = new RemoveProxyAtCallback(this.RemoveProxyAt);
                base.Invoke(method, new object[] { i });
            }
            else
            {
                this.listBoxProxy.Items.RemoveAt(i);
            }
        }

        private delegate void CloseTestProxyFormCallback();

        private delegate string GetProxyInfoStringCallback(int i);

        private delegate void RemoveProxyAtCallback(int i);

        private string GetProxyInfoString(int i)
        {
            if (this.listBoxProxy.InvokeRequired)
            {
                GetProxyInfoStringCallback method = new GetProxyInfoStringCallback(this.GetProxyInfoString);
                return (string)base.Invoke(method, new object[] { i });
            }
            return (string)this.listBoxProxy.Items[i].Tag;
        }

        private void proxyDeleteBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listBoxProxy.SelectedItems)
            {
                this.listBoxProxy.Items.Remove(item);
            }
        }

        private void proxyUpBtn_Click(object sender, EventArgs e)
        {
            if ((this.listBoxProxy.SelectedItems.Count > 0) && (this.listBoxProxy.Items.Count > 1))
            {
                ListViewItem item = this.listBoxProxy.SelectedItems[0];
                int index = this.listBoxProxy.Items.IndexOf(item);
                if (index != 0)
                {
                    this.listBoxProxy.Items.Remove(item);
                    this.listBoxProxy.Items.Insert(index - 1, item);
                    this.listBoxProxy.Items[index - 1].Selected = true;
                }
            }
        }

        private void proxyDownBtn_Click(object sender, EventArgs e)
        {
            if ((this.listBoxProxy.SelectedItems.Count > 0) && (this.listBoxProxy.Items.Count > 1))
            {
                ListViewItem item = this.listBoxProxy.SelectedItems[0];
                int index = this.listBoxProxy.Items.IndexOf(item);
                if (index != (this.listBoxProxy.Items.Count - 1))
                {
                    this.listBoxProxy.Items.Remove(item);
                    this.listBoxProxy.Items.Insert(index + 1, item);
                    this.listBoxProxy.Items[index + 1].Selected = true;
                }
            }
        }

      

        #endregion

        private void clrBtn_Click(object sender, EventArgs e)
        {
            listBoxProxy.Items.Clear();
        }
    }
}
