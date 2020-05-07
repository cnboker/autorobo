using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Makers.Views.Controls
{
    public class LineControl : Label
    {
        public LineControl() {
            Text = "";
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            AutoSize = false;
            Height = 2;
        }
    }
}
