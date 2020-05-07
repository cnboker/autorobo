using AutoRobo.Core;
using System;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;

namespace AutoRobo.Makers.Views
{
    public partial class ViewBase : DockContent
    {
        [ToolboxItem(false)]        
        public AppContext Context
        {
            get {
                FormBase fb = DockPanel.FindForm() as FormBase;
                if (fb == null) return null;
                return fb.Context;
            }
        }

        public ViewBase()
        {
            InitializeComponent();
            HideOnClose = true;            
            CloseButton = true;
            CloseButtonVisible = true;
            AllowEndUserDocking = true;
        }
      
        protected override void OnLoad(EventArgs e)
        {            
            if (Initialize != null)
            {
                Initialize(this, EventArgs.Empty);
            }
            if (Initialized != null) {
                Initialized(this, EventArgs.Empty);
            }
            base.OnLoad(e);
        }

        public event EventHandler Initialize;
        public event EventHandler Initialized;
    }
}
