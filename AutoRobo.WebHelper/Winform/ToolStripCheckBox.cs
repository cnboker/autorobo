using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public partial class ToolStripCheckBox : ToolStripControlHost
    {
        public ToolStripCheckBox()
            : base(new CheckBox())
        {

        }

        public CheckBox CheckBoxControl
        {
            get { return Control as CheckBox; }
        }

        public bool Checked
        {
            get { return CheckBoxControl.Checked; }
            set { CheckBoxControl.Checked = value; }
        }

        public event EventHandler CheckedChanged;

        public void OnCheckedChanged(object sender, EventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(sender, e);
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            (control as CheckBox).CheckedChanged += OnCheckedChanged;
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            (control as CheckBox).CheckedChanged -= OnCheckedChanged;
        }
    }
}
