using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using Timer=System.Windows.Forms.Timer;
using System.Timers;
using System.Runtime.InteropServices;
using mshtml;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionFileDialog : ActionElementBase
    {       
        public string Filename { get; set; }

        public IHTMLElement ActiveElement { get; set; }
        private System.Timers.Timer timerFileDialog;
        private bool blnFileDialogFound;
        public event EventHandler FileDialogFoundHandler;

        public ActionFileDialog()
        {
            ElementType = ElementTypes.FileUpload;            
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            WatchFileUploadBox(Window, parameter.Element);
            return base.Parse(parameter);
        }

        //ActionFileDialog fileDialog = null;
        /// <summary>
        /// Sets a timer to watch for a file dialog box
        /// </summary>
        /// <param name="browser">Browser running this dialog</param>
        /// <param name="activeElement">Element to check after dialog is found</param>
        public void WatchFileUploadBox(BrowserWindow browser, IHTMLElement activeElement)
        {
            var fileDialog = new ActionFileDialog { ActiveElement = activeElement };
            fileDialog.SetFindMethod(activeElement);
            fileDialog.WaitForFileDialog(activeElement);
            fileDialog.FileDialogFoundHandler += new EventHandler(fileDialog_FileDialogFoundHandler);
        }

        void fileDialog_FileDialogFoundHandler(object sender, EventArgs e)
        {
            ActionFileDialog fileDialog = sender as ActionFileDialog;
            //AddAction(fileDialog);
            AppContext.ActionModel.AddAction(fileDialog);
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucFileUpload(editorAction);
        }

        public void WaitForFileDialog(IHTMLElement activeElement)
        {
            ActiveElement = activeElement;
            blnFileDialogFound = false;
            timerFileDialog = new System.Timers.Timer(1000);
            timerFileDialog.Elapsed += TimerFileDialogElapsed;
            timerFileDialog.Enabled = true;
        }

        /// <summary>
        /// Timer event for the file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimerFileDialogElapsed(object sender, ElapsedEventArgs e)
        {
            if (!blnFileDialogFound)
            {
                if (FileDialogFound())
                {
                    blnFileDialogFound = true;
                }
                else
                {
                    return;
                }
            }

            if (FileDialogFound())
            {
                return;
            }

            timerFileDialog.Enabled = false;

            Filename = ActiveElementAttribute(ActiveElement, "value");
            if (Filename == "") return;
            blnFileDialogFound = false;

            if (FileDialogFoundHandler != null)
            {
                FileDialogFoundHandler(this, EventArgs.Empty);
            }
 
        }
        public override string ActionDisplayName
        {
            get { return "上传文件对话框"; }
        }
        /// <summary>
        /// Uses Win32 calls to find whether the foreground window is a file dialog
        /// </summary>
        /// <returns>true if it is a file dialog</returns>
        public bool FileDialogFound()
        {
            IntPtr win = GetForegroundWindow();
            long lstyle = GetWindowStyle(win);

            if (lstyle.ToString("X") == "96CC20C4" || lstyle.ToString("X") == "96CC02C4")
            {
                return true;
            }

            return false;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        internal static Int64 GetWindowStyle(IntPtr hwnd)
        {
            var info = new WINDOWINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            GetWindowInfo(hwnd, ref info);

            return Convert.ToInt64(info.dwStyle);
        }


        public override Bitmap GetIcon()
        {
            return GetIconFromFile("Upload.bmp");
        }

       
        public class ClickDialogThread
        {
            public Element element;
            public bool Error { get; set; }
            public void Run()
            {
                try
                {
                    element.ClickNoWait();
                }
                catch {
                    Error = true;
                }
            }
        }

        private Timer EnterFilenameTimer;
        private ClickDialogThread clickDialog;
        public override void Perform()
        {
            var element = GetFileElement();
            if (element != null)
            {
                clickDialog = new ClickDialogThread { element = element };

                var thread = new Thread(clickDialog.Run) { Priority = ThreadPriority.Highest };
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                EnterFilenameTimer = new Timer { Interval = 2000, Enabled = true };
                EnterFilenameTimer.Tick += EnterFilenameTimer_Tick;

                while (EnterFilenameTimer.Enabled) Application.DoEvents();
            }
        }

        void EnterFilenameTimer_Tick(object sender, EventArgs e)
        {
            EnterFilenameTimer.Enabled = false;
            if (!clickDialog.Error)
            {
                SendKeys.SendWait(Filename + "{ENTER}");
            }
        }


        //public override CodeLine ToCode(ICodeFormatter Formatter)
        //{
        //    var line = new CodeLine();
        //    line.Attributes = FindMechanism;            
        //    var builder = new StringBuilder();
        //    line.Frames = FrameList;
        //    line.ModelPath = GetDocumentPath(Formatter);
        //    builder.Append(line.ModelPath);
        //    builder.Append(Formatter.MethodSeparator);
        //    builder.Append(ElementType);
        //    builder.Append("(" + FindMechanism.ToString() + ")");
        //    line.ModelLocalProperty = builder.ToString();
        //    builder.Append(Formatter.MethodSeparator);
        //    string localname = PerformReplacement(Filename);
        //    line.ModelFunction = "Set(\"" + localname + "\")" + Formatter.LineEnding;
        //    line.FullLine = builder.ToString();
        //    return line;
        //}

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            Filename = node.Attributes["Filename"].Value;            
        }

  

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("Filename", Filename);
            base.WriterAddAttribute(writer);
        }
    }
}
