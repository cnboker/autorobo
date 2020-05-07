using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core
{
    public partial class MyDialog : Form
    {
        public MyDialog()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 1, 1));
            Color c = System.Drawing.ColorTranslator.FromHtml("#9a9a9a");
            Color c1 = System.Drawing.ColorTranslator.FromHtml("#6d6d6d");
            Color c2 = System.Drawing.ColorTranslator.FromHtml("  #4f4f4f");
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    c1, 1, ButtonBorderStyle.Solid,
                    c1, 1, ButtonBorderStyle.Solid,
                    c, 1, ButtonBorderStyle.Solid,
                    c, 1, ButtonBorderStyle.Solid);
            //Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            //ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            Rectangle rc = new Rectangle(0, 0, this.ClientSize.Width, 5);
            c = System.Drawing.ColorTranslator.FromHtml("#1b9fdf");
            SolidBrush sb = new SolidBrush(c);
            e.Graphics.FillRectangle(sb, rc);

            base.OnPaint(e);
        }

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 205;   // Caption bar height;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }

            base.WndProc(ref m);
        }

    }
}
