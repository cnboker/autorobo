using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Actions;
using AutoRobo.Makers.Views;
using mshtml;

namespace AutoRobo.Makers.Presentation
{
    public class BrowserPresenter : Presenter<IBrowserView>
    {        
        private MyBrowser editBrowser;
        private IHTMLElement EditableActiveElement;
        private Timer timerActiveElement = new Timer();
        private IHTMLElement KeyEntryElement;
        private DateTime LastKeyTime = DateTime.MinValue;
        private bool ControlKeyDown { get; set; }
        private bool ShiftKeyDown;        
        /// <summary>
        /// 键盘输入数据
        /// </summary>
        private StringBuilder SbKeys = new StringBuilder();
      
        public BrowserPresenter(IBrowserView view)
            : base(view)
        {            
        }
 
        protected override void OnViewInitialize(object sender, EventArgs e)
        {                      
            //this.timerActiveElement.Enabled = true;
            //this.timerActiveElement.Interval = 250;
            //this.timerActiveElement.Tick += new System.EventHandler(this.timerActiveElement_Tick);       
            base.OnViewInitialize(sender, e);
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
            editBrowser = Context.Browser as MyBrowser;            
            //editBrowser.WBKeyDown += new csExWB.WBKeyDownEventHandler(RoboBrowser_WBKeyDown);
            //editBrowser.WBKeyUp += new csExWB.WBKeyUpEventHandler(RoboBrowser_WBKeyUp);
            editBrowser.WBEvaluteNewWindow += new csExWB.EvaluateNewWindowEventHandler(editBrowser_WBEvaluteNewWindow);
            //editBrowser.WBLButtonUp += new csExWB.HTMLMouseEventHandler(editBrowser_WBLButtonUp);
            //使用WBLButtonDown事件是因为该事件有对e.Handled做处理,WBLButtonUp事件e.Handled=true无效
            //editBrowser.WBLButtonDown += new csExWB.HTMLMouseEventHandler(editBrowser_WBLButtonDown);
            Context.State.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(State_PropertyChanged);
            base.OnViewLoad(sender, e);
        }

        void State_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRecord")
            {
                timerActiveElement.Enabled = Context.State.IsRecord;
            }
        }

        //void editBrowser_WBLButtonDown(object sender, csExWB.HTMLMouseEventArgs e)
        //{
        //    ViewBase view = View as ViewBase;
        //    if (!Context.State.IsRecord) return;
        //    if (Context.State.IsRunning) return;
        //    //多步骤接收活动参数处理            
        //    MultiStepActionParameter parameter = Context.MultiStepActionParameter;
        //    IHTMLElement activeElement = e.SrcElement as mshtml.IHTMLElement;
        //    if (activeElement.tagName == "HTML") return;
           
        //    if (parameter != null)
        //    {
        //        if (activeElement == parameter.Element)
        //        {
        //            e.Handled = true;
        //            return;
        //        }
        //        e.Handled = true;
        //        parameter.ReceiveClick(activeElement);
        //        if (parameter.Eval())
        //        {
        //            Context.MultiStepActionParameter = null;
        //            parameter.Invoke();
        //        }               
        //        return;
        //    }
           
        //    OnMouseUp(activeElement);
        //}

   
        /// <summary>
        /// 增加新导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editBrowser_WBEvaluteNewWindow(object sender, csExWB.EvaluateNewWindowEventArgs e)
        {
            ViewBase view = View as ViewBase;
            if (Context.State.IsRecord)
            {                
                Model.AddAction(Context, ActionEnum.ActionNavigate.ToString(), new ActionNavigateParameter() { Url = e.url });              
            }          
        }

        //void RoboBrowser_WBKeyUp(object sender, csExWB.WBKeyUpEventArgs e)
        //{

        //    switch (e.keycode)
        //    {

        //        case Keys.ShiftKey:
        //            ShiftKeyDown = false;
        //            break;
        //        case Keys.ControlKey:
        //            ControlKeyDown = false;
        //            break;
        //        //case Keys.Alt:
        //        //    break;
        //        //g_mem_add Tab处理
        //        //case Keys.Tab:
        //        //    WatinContext context = WatinContextFactory.GetContext(this);
        //        //    WatinScript wscript = context.Wscript;
        //        //    BrowserWindow watinie = context.BrowserWindow;
        //        //    ActionDirectionKey akey = wscript.AddDirectionKey(watinie);
        //        //    akey.DirectionKey = "{TAB}";
        //        //    wscript.AddAction(akey);
        //        //    break;
        //        case Keys.Enter:
        //        case Keys.Up:
        //        case Keys.Down:
        //        case Keys.Left:
        //        case Keys.Right:
        //            //case Keys.Back:
        //            //case Keys.Delete:
        //            e.handled = true;
        //            WriteKeys();
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //void RoboBrowser_WBKeyDown(object sender, csExWB.WBKeyDownEventArgs e)
        //{        
        //    if (ControlKeyDown) return;

        //    if (e.virtualkey == Keys.Back || e.virtualkey == Keys.Delete
        //  || e.virtualkey == Keys.Up || e.virtualkey == Keys.Down
        //  || e.virtualkey == Keys.Left || e.virtualkey == Keys.Right)
        //    {
        //        return;
        //    }

        //    if (editBrowser.GetActiveElement() != EditableActiveElement)
        //    {
        //        WriteKeys();
        //    }
        //    EditableActiveElement = editBrowser.GetActiveElement() as mshtml.IHTMLElement;
        //    CloneActiveElementForKeys();

        //    if (e.keycode == Keys.ControlKey)
        //    {
        //        ControlKeyDown = true;
        //    }
        //    else if (e.keycode == Keys.ShiftKey)
        //    {
        //        ShiftKeyDown = true;
        //    }
        //    else
        //    {
        //        if (e.keycode != Keys.Tab
        //            && e.keycode != Keys.Back
        //            && e.keycode != Keys.Delete
        //            && e.keycode != Keys.Up
        //            && e.keycode != Keys.Down
        //            && e.keycode != Keys.Left
        //            && e.keycode != Keys.Right && e.keycode.ToString() != "Escape")
        //        {
        //            AddKeys(ShiftKeyDown, e.keycode);
        //            LastKeyTime = DateTime.Now;
        //        }
        //    }

        //    //Consume keys here, if needed
        //    try
        //    {
        //        if (e.virtualkey == Keys.ControlKey)
        //        {
        //            switch (e.keycode)
        //            {
        //                case Keys.F:
        //                    //m_frmFind.Show(this);
        //                    e.handled = true;
        //                    break;
        //                case Keys.N:
        //                    //AddNewBrowser(Blank, AboutBlank, string.Empty, true);
        //                    e.handled = true;
        //                    break;
        //                case Keys.O:
        //                    //AddNewBrowser(Blank, AboutBlank, string.Empty, true);
        //                    e.handled = true;
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception eex)
        //    {
        //        logger.Fatal(eex);
        //    }
        //}
        //private void CloneActiveElementForKeys()
        //{
        //    if (EditableActiveElement == null) return;
        //    var domnode = (mshtml.IHTMLDOMNode)EditableActiveElement;

        //    KeyEntryElement = (IHTMLElement)domnode.cloneNode(true);
        //}

        //public void AddKeys(bool shifted, Keys keycode)
        //{
        //    ViewBase view = View as ViewBase;            
        //    if (!Context.State.IsRecord) return;

        //    string strKey = keycode.ToString();

        //    switch (keycode)
        //    {
        //        case Keys.Space: strKey = " "; break;
        //        case Keys.Enter:
        //        case Keys.Tab:
        //        case Keys.Up:
        //        case Keys.Down:
        //        case Keys.Left:
        //        case Keys.Right:
        //        case Keys.Back:
        //        case Keys.Delete: strKey = ""; break;
        //        case Keys.F1:
        //        case Keys.F2:
        //        case Keys.F3:
        //        case Keys.F4:
        //        case Keys.F5:
        //        case Keys.F6:
        //        case Keys.F7:
        //        case Keys.F8:
        //        case Keys.F9:
        //        case Keys.F10:
        //        case Keys.F11:
        //        case Keys.F12: 
        //            strKey = ""; break;
        //    }


        //    if (shifted && Regex.IsMatch(strKey, @"\AD\d\z"))
        //    {
        //        strKey = Convert.ToChar(keycode).ToString();
        //        switch (strKey)
        //        {
        //            case "1": strKey = "!"; break;
        //            case "2": strKey = "@"; break;
        //            case "3": strKey = "#"; break;
        //            case "4": strKey = "$"; break;
        //            case "5": strKey = "%"; break;
        //            case "6": strKey = "^"; break;
        //            case "7": strKey = "&"; break;
        //            case "8": strKey = "*"; break;
        //            case "9": strKey = "("; break;
        //            case "0": strKey = ")"; break;
        //        }
        //    }
        //    else if (!shifted && Regex.IsMatch(strKey, @"\AD\d\z"))
        //    {
        //        strKey = Convert.ToChar(keycode).ToString();
        //    }
        //    else if (Regex.IsMatch(strKey, @"\ANumPad\d\z"))
        //    {
        //        strKey = strKey.Replace("NumPad", "");
        //    }
        //    else if (!shifted && strKey == @"\")
        //    {
        //        strKey = "\\";
        //    }
        //    else if (!shifted && Regex.IsMatch(strKey, @"\AOem\w+\z"))
        //    {
        //        switch (strKey)
        //        {
        //            case "Oemtilde": strKey = "`"; break;
        //            case "OemMinus": strKey = "-"; break;
        //            case "Oemplus": strKey = "="; break;
        //            case "OemOpenBrackets": strKey = "["; break;
        //            case "Oem6": strKey = "]"; break;
        //            case "Oem1": strKey = ";"; break;
        //            case "Oem7": strKey = "'"; break;
        //            case "Oemcomma": strKey = ","; break;
        //            case "OemPeriod": strKey = "."; break;
        //            case "OemQuestion": strKey = "/"; break;
        //            case "Oem5": strKey = @"\"; break;
        //        }
        //    }
        //    else if (shifted && Regex.IsMatch(strKey, @"\AOem\w+\z"))
        //    {
        //        switch (strKey)
        //        {
        //            case "Oemtilde": strKey = "~"; break;
        //            case "OemMinus": strKey = "_"; break;
        //            case "Oemplus": strKey = "+"; break;
        //            case "OemOpenBrackets": strKey = "{"; break;
        //            case "Oem6": strKey = "}"; break;
        //            case "Oem1": strKey = ":"; break;
        //            case "Oem7": strKey = "\\\""; break;
        //            case "Oemcomma": strKey = "<"; break;
        //            case "OemPeriod": strKey = ">"; break;
        //            case "OemQuestion": strKey = "?"; break;
        //            case "Oem5": strKey = "|"; break;
        //        }
        //    }

        //    if (shifted)
        //    {
        //        SbKeys.Append(strKey);
        //    }
        //    else
        //    {
        //        SbKeys.Append(strKey.ToLower());
        //    }
        //}


        /// <summary>
        /// This function is called by the timer to figure out what has been "added"
        /// in text. Try to change this to realtime on certain keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void timerActiveElement_Tick(object sender, EventArgs e)
        //{                       
        //    if (!Context.State.IsRecord) return;
           
        //    if (LastKeyTime < DateTime.Now.AddMilliseconds(-1 * AppSettings.Instance.TypingTime) && SbKeys.Length > 0)
        //    {
        //        Model.AddAction(Context, ActionEnum.ActionTypeText.ToString(),
        //                    new ActionTypingParameter()
        //                    {
        //                        Element = EditableActiveElement == null ? KeyEntryElement : EditableActiveElement,
        //                        XPathFinder = false,                                                                
        //                        SbKeys = SbKeys.ToString()
        //                    });
        //        SbKeys.Length = 0;                
        //    }
        //}

        //private void OnMouseUp(IHTMLElement activeElement)
        //{                 
        //    if (activeElement != EditableActiveElement)
        //    {
        //        WriteKeys();
        //    }      
        //    EditableActiveElement = activeElement;
        //    //选择器启动状态， 屏蔽自动添加活动功能
        //    //if (View.Browser.Selector.Highlighting) return;
        //    string ActionString = ActionEnum.ActionClick.ToString();
        //    ElementTypes elementType = ActionElementBase.TagStringToElementType(activeElement);
        //    switch (elementType)
        //    {
        //        case ElementTypes.CheckBox:
        //            ActionString = ActionEnum.ActionCheckbox.ToString();
        //            break;
        //        case ElementTypes.RadioButton:
        //            ActionString = ActionEnum.ActionRadio.ToString();
        //            break;
        //        case ElementTypes.FileUpload:
        //            ActionString = ActionEnum.ActionFileDialog.ToString();
        //            break;
        //    }
        //    Model.AddAction(Context, ActionString, new ActionClickParameter()
        //    {                
        //        Element = activeElement,                                
        //        Filter = ActionFliter.High
        //    });
 
        //}

        /// <summary>
        /// 控件间焦点切换的时候触发增加输入框Action
        /// </summary>
        //private void WriteKeys()
        //{         
        //    if (EditableActiveElement == null) return; 
        //    if (SbKeys.Length > 0)
        //    {
        //        Model.AddAction(Context, ActionEnum.ActionTypeText.ToString(),
        //                        new ActionTypingParameter()
        //                        {                                    
        //                            Element = EditableActiveElement,
        //                            XPathFinder = false,                                    
        //                            SbKeys = SbKeys.ToString()
        //                        });
        //        SbKeys.Length = 0;
        //    }
        //}

    }
}
