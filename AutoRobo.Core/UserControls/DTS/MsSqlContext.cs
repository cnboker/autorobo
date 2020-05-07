
using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls.Wizard;
using AutoRobo.Core.Actions.InOut.Configuration;

namespace AutoRobo.Core.UserControls.DTS
{
    public class MsSqlContext : WizardContext
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SqlText { get; set; }
     
        public override void InitailizeWizardPages()
        {
            Pages = new List<WizardPage>();
            Pages.Add(new MSSqlWizardPage());
            Pages.Add(new MSSqlWizardQueryPage());
        }

        public override Actions.InOut.Configuration.IOSettings ToSettings()
        {
            string sqlconnection = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", Server, Database, UserName, Password);
            return new MssqlSettings()
            {
                ConnectionString = sqlconnection,
                SqlText = SqlText
            };
        }
    }
}
