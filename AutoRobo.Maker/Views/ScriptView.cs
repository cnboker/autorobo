using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.UserControls;
using AutoRobo.Core.ns;
using AutoRobo.Core;

namespace AutoRobo.Makers.Views
{
    public partial class ScriptView : ViewBase
    {
        public codeRichEditor Editor { get; set; }
        public ScriptView()
        {
            InitializeComponent();
            TabText = "脚本";
            Editor = JintCreator.CreateEditor(AppContext.Current);
            this.Controls.Add(Editor);
        }
     
    }
}
