using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.UserControls
{
    public partial class LoopProperty : UserControl
    {
        public int FirstNumber
        {
            get
            {
                return Convert.ToInt16(firstNumberTextBox.Text);
            }
            set
            {
                firstNumberTextBox.Text = value.ToString();
            }
        }

        public int LastNumber { get { return Convert.ToInt16(this.lastNumberTextBox.Text); } set{lastNumberTextBox.Text = value.ToString();}}
        public int StepNumber { get { return Convert.ToInt16(this.stepTextBox.Text); } set{stepTextBox.Text = value.ToString();}}
        
        public LoopProperty()
        {
            InitializeComponent();
        }

        public new void Validate() { 
            int result = 0;
            if (!int.TryParse(firstNumberTextBox.Text, out result)) {
                throw new ApplicationException("第一行行数必须是数字");
            }
            if (!int.TryParse(lastNumberTextBox.Text, out result))
            {
                throw new ApplicationException("最后一行行数必须是数字");
            }
            if (!int.TryParse(stepTextBox.Text, out result))
            {
                throw new ApplicationException("步长必须是数字");
            }
            else {
                if (result == 0) {
                    throw new ApplicationException("步长必须是大于0的数");
                }
            }

        }
    }
}
