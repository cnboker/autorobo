using System.Windows.Forms;
using System.Diagnostics;

namespace AutoRobo.Core.Logger
{
    public partial class frmException : Form
    {
        public frmException()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, System.EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}