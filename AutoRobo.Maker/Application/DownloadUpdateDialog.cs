using AutoRobo.Makers.AutoUpdate.Framework;
using System;
using System.Windows.Forms;

namespace AutoRobo.Makers
{
    public partial class DownloadUpdateDialog : Form
    {
       
        private readonly UpdateManager _updateManager;

        public Update NewUpdate
        {
            get { return _updateManager.NewUpdate; }
        }

        public int DownloadProgress
        {
            get { return progressBar.Value; }
            set
            {
                progressBar.Value = value;
            }
        }

        public DownloadUpdateDialog(UpdateManager updateManager)
        {
            InitializeComponent();
             _updateManager = updateManager;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _updateManager.CleanUp();
            base.OnClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            _updateManager.DownloadUpdateAsync(finished =>
                                                    {
                                                        if (finished)
                                                        {
                                                            try
                                                            {
                                                                _updateManager.ApplyUpdate();                                                                
                                                                Application.Exit();
                                                            }
                                                            catch {
                                                                Close();
                                                            }
                                                           
                                                        }
                                                    },
                                                progressPercent =>
                                                {
                                                    this.DownloadProgress = progressPercent;
                                                });

            base.OnLoad(e);
        }

    }
}
