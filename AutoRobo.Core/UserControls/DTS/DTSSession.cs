using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AutoRobo.Core.UserControls.DTS
{
    public class DTSSession
    {
        public DTSDirection Direction { get; set; }
        public DataTable DataSource { get; set; }
    }
}
