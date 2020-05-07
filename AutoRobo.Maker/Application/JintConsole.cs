using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.UserControls;
using AutoRobo.Core.ns;
using Jint;
using AutoRobo.Core;
using Jint.Delegates;
using System.IO;
using AutoRobo.ClientLib.Ants;


namespace AutoRobo.Makers
{
    public class JintConsole
    {
        private static codeRichEditor textEditor = null;
        private static MyBrowser ie = null;

        static public Form CreateForm(MyBrowser ie)
        {
            JintConsole.ie = ie;
            Form form = new Form();
            form.Text = "代码编辑器";
            form.Size = new System.Drawing.Size(800, 600);
           // textEditor = JintCreator.CreateEditor(ie);
            form.FormClosed += new FormClosedEventHandler(form_FormClosed);
            textEditor.RunBefore += new EventHandler(textEditor_RunBefore);
            return form;
        }

        static void StartMyIE() {
            if (textEditor.InvokeRequired) {
                textEditor.Invoke(new System.Action(() => {
                    Form ieHost = new Form();
                    AutoBrowser ieControl = new AutoBrowser();
                    ieControl.Dock = DockStyle.Fill;
                    ieHost.Controls.Add(ieControl);
                    ieControl.Browser.AllowAlert = true;
                    ie = ieControl.Browser as MyBrowser;
                    ieHost.Show();
                    ieHost.Size = new System.Drawing.Size(800, 640);
                    ieHost.Activate();
                }));
            }          
        }

        static void textEditor_RunBefore(object sender, EventArgs e)
        {
            StartMyIE();           
        }

        static void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            textEditor.TmpSave();           
        }
    }
}
