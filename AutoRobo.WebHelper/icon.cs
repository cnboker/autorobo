using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace Util
{
    public enum DrawAction
    {
        Line,
        Rectangle
    }

    public class RectangleArgs:EventArgs{
        public Rectangle Rectangle {get;set;}
    }

    public delegate void FinishedDelegate(object sender, RectangleArgs args);

    public class LineRectangle
    {
        Rectangle SelectRect = new Rectangle();
        Point ps = new Point();
        Point pe = new Point();
        Control target;
        DrawAction drawAction = DrawAction.Rectangle;
        public event FinishedDelegate OnFinished;
 
        public LineRectangle(Control control)
            : this(control, DrawAction.Rectangle)
        {

        }

        public LineRectangle(Control control, DrawAction action)
        {
            this.target = control;
            this.drawAction = action;
            target.MouseDown += new MouseEventHandler(OnMouseDown);
            target.MouseMove += new MouseEventHandler(OnMouseMove);
            target.MouseUp += new MouseEventHandler(OnMouseUp);
        }

        private void OnMouseDown(Object sender, MouseEventArgs e)
        {
            SelectRect.Width = 0;
            SelectRect.Height = 0;
            SelectRect.X = e.X;
            SelectRect.Y = e.Y;

            ps.X = e.X;
            ps.Y = e.Y;
            pe = ps;
        }

        private void OnMouseMove(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                var thisform = this.target;
                // First DrawReversible to toggle to the background color
                // Second DrawReversible to toggle to the specified color
                if (drawAction == DrawAction.Line)
                {
                    ControlPaint.DrawReversibleLine(thisform.PointToScreen(ps), thisform.PointToScreen(pe), Color.Black);
                    pe = new Point(e.X, e.Y);
                    ControlPaint.DrawReversibleLine(thisform.PointToScreen(ps), thisform.PointToScreen(pe), Color.Black);
                }
                else
                {
                    ControlPaint.DrawReversibleFrame(thisform.RectangleToScreen(SelectRect), Color.Black, FrameStyle.Dashed);
                    SelectRect.Width = e.X - SelectRect.X;
                    SelectRect.Height = e.Y - SelectRect.Y;
                    ControlPaint.DrawReversibleFrame(thisform.RectangleToScreen(SelectRect), Color.Black, FrameStyle.Dashed);
                }
            }
        }

        private void OnMouseUp(Object sender, MouseEventArgs e)
        {
            var thisform = this.target;
            Graphics g = thisform.CreateGraphics();
            Pen p = new Pen(Color.Blue, 2);
            if (drawAction == DrawAction.Line)
            {
                ControlPaint.DrawReversibleLine(thisform.PointToScreen(ps), thisform.PointToScreen(pe), Color.Black);
                g.DrawLine(p, ps, pe);
            }
            else
            {
                ControlPaint.DrawReversibleFrame(thisform.RectangleToScreen(SelectRect), Color.Black, FrameStyle.Dashed);
                g.DrawRectangle(p, SelectRect);
            }
            g.Dispose();
            if (OnFinished != null) {
                OnFinished(this, new RectangleArgs() { Rectangle = SelectRect });
            }
        }
    }

}
