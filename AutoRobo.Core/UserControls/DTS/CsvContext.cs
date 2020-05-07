using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls.Wizard;
using AutoRobo.Core.Actions.InOut.Configuration;

namespace AutoRobo.Core.UserControls.DTS
{
    public class CsvContext : WizardContext
    {
        public string CSVFile { get; set; }
        public bool IncludeHeader { get; set; }
        public char SpliterChar { get; set; }

        public override void InitailizeWizardPages()
        {
            Pages = new List<WizardPage>();
            Pages.Add(new CSVWizardPage());
        }

        public override Actions.InOut.Configuration.IOSettings ToSettings()
        {
            return new CSVSettings()
            {
                FileName = CSVFile,
                Spilter = SpliterChar,
                IncludeHeader = IncludeHeader
            };
        }
    }
}
