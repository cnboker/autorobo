using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.UserControls
{
    public partial class ucBrowser : ucBaseEditor
    {
        public ucBrowser(IEditorAction Caller)
            : base(Caller)
        {
            InitializeComponent();            
        }

        public override ActionBase Action
        {
            get
            {
                if (base.Action == null) return null;
                var action = (ActionBrowser)base.Action;
                action.DownloadImages = downloadImages.Checked;
                action.DownloadSounds = downloadSounds.Checked;
                action.DownloadVideo = downloadVideo.Checked;
                action.DownloadActivex = downloadActivex.Checked;
                action.DownloadFlash = downloadFlash.Checked;
                action.Visibility = visibilityBox.Checked;
                action.DownloadScript = downLoadScriptBox.Checked;
                return base.Action;
            }
            set
            {
                var action = (ActionBrowser)value;
                downloadImages.Checked = action.DownloadImages;
                downloadSounds.Checked = action.DownloadSounds;
                downloadVideo.Checked = action.DownloadVideo;
                downloadActivex.Checked =action.DownloadActivex;
                downloadFlash.Checked = action.DownloadFlash; 
                visibilityBox.Checked = action.Visibility;
                downLoadScriptBox.Checked = action.DownloadScript;
                base.Action = action;
            }
        }

    }
}
