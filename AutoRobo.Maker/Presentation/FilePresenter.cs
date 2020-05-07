using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Makers.Views;
using System.Windows.Forms;
using System.IO;
using AutoRobo.Makers.Models.Repositories;
using AutoRobo.Core.Models;

namespace AutoRobo.Makers.Presentation
{
    public class FilePresenter:Presenter<IFileView>
    {
        public FilePresenter(IFileView view):base(view) {
            view.Initialize += new EventHandler(view_Initialize);
            view.Load += new EventHandler(view_Load);
            view.LoadFile += new EventHandler(view_LoadFile);
        }

        void view_LoadFile(object sender, EventArgs e)
        {
            TreeNode node = sender as TreeNode;
            FileInfo file = node.Tag as FileInfo;
            if (file == null) return;
            if (file.Extension == ".csv") {
                System.Diagnostics.Process.Start("notepad.exe", file.FullName);
                return;
            }
            if (file.Extension != ".bit") {
                try
                {
                    System.Diagnostics.Process.Start(file.FullName);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                return;
            }            
            ModelSet model = new ModelSet(new FileActionRepository(file.FullName));
            Context.ActionModel = model;
            
            Context.State.DataPrepared = true;
        }

        void view_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void view_Initialize(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
