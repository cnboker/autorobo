using AutoRobo.Core;
using AutoRobo.Makers.Presentation;
using AutoRobo.Robo.Application;

namespace AutoRobo.Makers.Views
{
    public partial class BrowserView : ViewBase, IBrowserView
    {
        private BrowserPresenter presenter = null;
       
        public MyBrowser Browser {
            get {
                return webBrowser;
            }
        }

        public BrowserView(string title="开始")
        {
            InitializeComponent();
            TabText = title;
            CloseButtonVisible = false;
            presenter = new BrowserPresenter(this);            
        }

        protected override void OnLoad(System.EventArgs e)
        {
            webBrowser.ObjectForScripting = new COMContext(Context);
            base.OnLoad(e);
        }

       
    }
}
