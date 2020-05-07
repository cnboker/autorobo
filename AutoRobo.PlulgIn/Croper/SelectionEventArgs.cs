using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AutoRobo.PlulgIn.Croper
{
    // Selection arguments
    public class SelectionEventArgs : EventArgs
    {

        private Point location;
        private Size size;

        // Constructors
        public SelectionEventArgs(Point location)
        {
            this.location = location;
        }
        public SelectionEventArgs(Point location, Size size)
        {
            this.location = location;
            this.size = size;
        }

        // Location property
        public Point Location
        {
            get { return location; }
        }
        // Size property
        public Size Size
        {
            get { return size; }
        }
    }
}
