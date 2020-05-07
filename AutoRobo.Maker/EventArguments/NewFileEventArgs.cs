using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Makers.Views;
using AutoRobo.Core.Models;

namespace AutoRobo.Makers.EventArguments
{
    public class NewFileEventArgs : EventArgs
    {
        public string ProjectName { get; set; }
        public string StartUrl { get; set; }
        public DataMethod Method { get; set; }

        public NewFileEventArgs(string projectName, string startUrl, DataMethod method)
        {
            this.StartUrl = startUrl;
            this.Method = method;
            this.ProjectName = projectName;
        }
    }
}
