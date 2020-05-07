using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;
using Jint;
using mshtml;
using System.Threading;
using System.Diagnostics;

namespace AutoRobo.Core.UserControls
{
    public partial class ucJavascriptInterpreter : ucBaseEditor
    {
        private codeRichEditor textEditor = null;
        public ucJavascriptInterpreter(IEditorAction editorAction):base(editorAction)
        {
            InitializeComponent();
           
        }      

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var act = (ActionJavascriptInterpreter)base.Action;
                act.Script = @codeBox.Text;                
                return act;
            }
            set
            {
                var action = (ActionJavascriptInterpreter)value;                 
                codeBox.Text = @action.Script;
                base.Action = action;
            }
        }
     
    
        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            codeBox.Text = textEditor.Code;
        }
        /// <summary>
        /// 粘帖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            codeBox.Text = Clipboard.GetData(DataFormats.UnicodeText).ToString();
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            codeBox.SelectAll();
            codeBox.Copy();
            codeBox.DeselectAll();
        }       

    }
}
