using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ScintillaNET;
using System.Diagnostics;
using AutoRobo.Core.Actions;
using System.Threading;
using Jint;
using AutoRobo.Core.ns;
using WatiN.Core;
using Util;

namespace AutoRobo.Core.UserControls
{
    public partial class codeRichEditor : UserControl
    {
        private ScintillaNET.Scintilla scintilla;              
        private JintEngine engine = null;
        // Indicates that calls to the StyleNeeded event
        // should use the custom INI lexer
        private bool _iniLexer;

        public bool IniLexer
        {
            get { return _iniLexer; }
            set { _iniLexer = value; }
        }
        public string Code {
            get {
                if (scintilla == null) return "";
                return scintilla.Text;
            }
            set {
                scintilla.Text = value;
            }
        }

        public AutoRobo.Core.Logger.ILog Logger { get; set; }

        public JintEngine JintEngine { get { return engine; } set { engine = value; } }
        public ICoreBrowser Browser { get; set; }

        public event EventHandler RunBefore = null;

        public codeRichEditor()
        {
            InitializeComponent();            
            CreateControls();
            toolStripComboBox.SelectedIndex = 0;
            
        }

        void codeRichEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            TmpSave();
        }

        protected override void OnLoad(EventArgs e)
        {
            FindForm().FormClosing += new FormClosingEventHandler(codeRichEditor_FormClosing);
            engine.Step += new EventHandler<Jint.Debugger.DebugInformation>(engine_Step);
            // Use a built-in lexer and configuration
            IniLexer = false;
            toolStripStopButton.Enabled = false;
            base.OnLoad(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.S | Keys.Control))
            {
                TmpSave();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CreateControls() {
            this.components = new System.ComponentModel.Container();
            this.scintilla = new ScintillaNET.Scintilla();
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.LineWrapping.VisualFlags = ScintillaNET.LineWrappingVisualFlags.End;
            this.scintilla.Location = new System.Drawing.Point(0, 0);
            this.scintilla.Margins.Margin1.AutoToggleMarkerNumber = 0;
            this.scintilla.Margins.Margin1.IsClickable = true;
            this.scintilla.Margins.Margin2.Width = 16;
            this.scintilla.Name = "_scintilla";
            this.scintilla.TabIndex = 0;
            this.scintilla.StyleNeeded += new System.EventHandler<ScintillaNET.StyleNeededEventArgs>(this.scintilla_StyleNeeded);
            this.scintilla.ModifiedChanged += new System.EventHandler(this.scintilla_ModifiedChanged);
            scintilla.ConfigurationManager.Language = "js";
            scintilla.Margins[0].Width = 18;
            editorPanel.Controls.Add(this.scintilla);            
            //scintilla.BringToFront();
        }

        void engine_Step(object sender, Jint.Debugger.DebugInformation e)
        {
            //触发异常，停止脚步运行
            if (stopClicked) {
                stopClicked = false;
                
                throw new StopJintException();
            }
        }

        private void scintilla_ModifiedChanged(object sender, EventArgs e)
        {
            AddOrRemoveAsteric();
        }
     
        private void scintilla_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            // Style the _text
            if (_iniLexer)
                SCide.IniLexer.StyleNeeded((Scintilla)sender, e.Range);
        }

        private void AddOrRemoveAsteric()
        {
            if (scintilla.Modified)
            {
                if (!Text.EndsWith(" *"))
                    Text += " *";
            }
            else
            {
                if (Text.EndsWith(" *"))
                    Text = Text.Substring(0, Text.Length - 2);
            }
        }


        private Thread runtimeThread = null;

        private void toolStripRunButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Code)) return;
            string code = Code;         
            toolStripRunButton.Enabled = false;
            toolStripStopButton.Enabled = true;            
            int selectIndex = toolStripComboBox.SelectedIndex;
         
            runtimeThread = new Thread(delegate()
            {
                try
                {
                    Browser browser = new BrowserWindow(Browser);
                    if (selectIndex == 0)
                    {
                        if (RunBefore != null)
                        {
                            RunBefore(this, EventArgs.Empty);
                        }
                        Log("开始执行脚本...");
                        engine.Run(code);
                        Log("脚本执行完成");
                    }                 
                }
                catch (StopJintException stopException) {
                    Log(stopException.Message);
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                }
                finally {
                    toolStripRunButton.Enabled = true;
                    toolStripStopButton.Enabled = false;
                }
            });
            runtimeThread.SetApartmentState(ApartmentState.STA);
            runtimeThread.Start();
        }

        private void Log(string message) {
            if (Logger != null)
            {
                Logger.Output(message);
            }
        }
        private void InjectJQuery(Browser browser)
        {
            try
            {
                //logger.Info("InjectJQuery");
                string jqueryScript = HttpRequestWapper.GetData(StringHelper.Domain + "/asserts/jqueryInject.js");
                browser.InjectScript(jqueryScript);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

      
        private void helpButton_Click(object sender, EventArgs e)
        {
            Process ieProcess = Process.Start("iexplore", @"http://www.ebotop.com/Page/javascriptApi");
        }

        private bool stopClicked = false;
        private void toolStripStopButton_Click(object sender, EventArgs e)
        {
            if (runtimeThread != null)
            {
                runtimeThread.Abort();
            }
            stopClicked = true;
            toolStripRunButton.Enabled = true;
            toolStripStopButton.Enabled = false;
        }


        public void TmpRead() {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path = System.IO.Path.Combine(path, "scriptCode.tmp");
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(path);
                    Code = sr.ReadToEnd();
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }
            }
        }

        public void TmpSave() {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path = System.IO.Path.Combine(path, "scriptCode.tmp");
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(path, false);
                sw.Write(Code);
            }
            finally
            {
                if(sw != null)
                sw.Close();
            }
        }
           
    
    }
}
