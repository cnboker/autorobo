using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using AutoRobo.ClientLib.Ants;
using AutoRobo.Core;

namespace AutoRobo.Makers.Views
{
    public partial class FormBase : FireBrowser
    {
        public AppContext Context { get { return AppContext.Current; } }
       
        public FormBase()
        {
            this.IsMdiContainer = true;      
            InitializeComponent();
            
        }
   
        //protected void PanelDock( DockPanel dockPanel, ViewBase panel, DockState state) {
        //    panel.Context = Context;            
        //    panel.Show(dockPanel, state);
        //}

     
    }
}
