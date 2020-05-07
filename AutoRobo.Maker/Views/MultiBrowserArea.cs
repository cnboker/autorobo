using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.Interface;
using System.Windows.Forms;

namespace AutoRobo.Makers.Views
{
    public class MultiBrowserArea : IViewArea
    {        
        private TableLayoutPanel panel = null;
        SplitContainer splitContainer1 = null;

        public MultiBrowserArea(SplitContainer splitContainer1)
        {            
            this.splitContainer1 = splitContainer1;
            panel = new TableLayoutPanel() { Dock = DockStyle.Fill };
            this.splitContainer1.Panel2.Controls.Add(panel);
            this.splitContainer1.Panel2Collapsed = true;
        }

        public void AddControl(Control child)
        {
            child.Dock = DockStyle.Fill;
            Show();
            int rowIndex = 0;
            if (panel.Controls.Count > 0)
            {
                rowIndex = panel.RowCount++;
            }
            panel.Controls.Add(child, 0, rowIndex);
            Layout();
        }

        public void Clear()
        {
            if (splitContainer1.InvokeRequired)
            {
                splitContainer1.Invoke(new MethodInvoker(Clear));
                return;
            }
            if (panel.Controls.Count == 0)
            {
                panel.RowCount = 1;
            }
            Layout();
        }

        private void Layout()
        {
            panel.RowStyles.Clear();
            int count = GetVisibleControlCount();
            for (int i = 0; i < count; i++)
            {
                RowStyle style = new RowStyle(SizeType.Percent, 100 / count);
                panel.RowStyles.Add(style);
            }
            if (panel.RowStyles.Count > 0)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private int GetVisibleControlCount()
        {
            int count = 0;
            foreach (Control c in panel.Controls)
            {
                if (c.Visible)
                {
                    count++;
                }
            }
            return count;
        }

        public void Show()
        {
            splitContainer1.Panel2Collapsed = false;
        }

        public void Hide()
        {
            splitContainer1.Panel2Collapsed = true;
        }


        public int Width
        {
            get { return splitContainer1.Panel2.Width; }
        }
    }

}
