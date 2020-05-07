using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AForge.Imaging.Filters;
using System.Drawing.Drawing2D;

namespace AutoRobo.PlulgIn.Croper
{
   
    /// <summary>
    /// 截图类
    /// </summary>
    public class ImageCroper 
    {
        // Events
        public delegate void SelectionEventHandler(object sender, SelectionEventArgs e);
        private log4net.ILog logger = log4net.LogManager.GetLogger("ImageCroper");
        public event SelectionEventHandler MouseImagePosition;
        public event SelectionEventHandler SelectionChanged;
        public event EventHandler SelectionFinished;

        private int width;
        private int height;
        private float zoom = 1;
   
        private bool cropping = false;
        private bool dragging = false;
        private Point start, end, startW, endW;
   
        private System.Drawing.Bitmap image = null;
        private MyBrowser myBrowser = null;

        public Image CropImage {
            get {
                return image;
            }
        }

        public ImageCroper(MyBrowser myBrowser)
        {
            this.myBrowser = myBrowser;

            
            myBrowser.WBMouseMove += new csExWB.HTMLMouseEventHandler(myBrowser_WBMouseMove);
            myBrowser.WBLButtonDown += new csExWB.HTMLMouseEventHandler(myBrowser_WBLButtonDown);
            myBrowser.WBLButtonUp += new csExWB.HTMLMouseEventHandler(myBrowser_WBLButtonUp);
   
            myBrowser.Paint += new PaintEventHandler(myBrowser_Paint);
        }

        void myBrowser_WBMouseMove(object sender, csExWB.HTMLMouseEventArgs e)
        {            
            if (dragging)
            {
                //e.Handled = true;
                System.Diagnostics.Debug.WriteLine("myBrowser_WBMouseMove");
                Graphics g = myBrowser.CreateGraphics();

                // erase frame
                DrawSelectionFrame(g);

                // get selection end point
                GetImageAndScreenPoints(new Point(e.ClientX, e.ClientY), out end, out endW);

                // draw frame
                DrawSelectionFrame(g);

                g.Dispose();

                if (SelectionChanged != null)
                {
                    Point sp = start;
                    Point ep = end;

                    // normalize start and end points
                    NormalizePoints(ref sp, ref ep);

                    SelectionChanged(this, new SelectionEventArgs(
                        sp, new Size(ep.X - sp.X + 1, ep.Y - sp.Y + 1)));
                }
            }
            else
            {
                if (MouseImagePosition != null)
                {
                    Rectangle rc = myBrowser.ClientRectangle;
                    int width = (int)(this.width * zoom);
                    int height = (int)(this.height * zoom);
                    Point scrollPos = myBrowser.GetScroll();
                    int x = (rc.Width < width) ? scrollPos.X : (rc.Width - width) / 2;
                    int y = (rc.Height < height) ? scrollPos.Y : (rc.Height - height) / 2;

                    if ((e.ClientX >= x) && (e.ClientY >= y) &&
                        (e.ClientX < x + width) && (e.ClientY < y + height))
                    {
                        // mouse is over the image
                        MouseImagePosition(this, new SelectionEventArgs(
                            new Point((int)((e.ClientX - x) / zoom), (int)((e.ClientY - y) / zoom))));
                    }
                    else
                    {
                        // mouse is outside image region
                        MouseImagePosition(this, new SelectionEventArgs(new Point(-1, -1)));
                    }
                }
            }
        }

        void myBrowser_WBLButtonUp(object sender, csExWB.HTMLMouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("myBrowser_WBLButtonUp");
            if (dragging)
            {
                e.Handled = true;
                // stop dragging and cropping
                dragging = cropping = false;
                // release capture
                //myBrowser.Capture = false;
                // set default mouse pointer
                myBrowser.Cursor = Cursors.Default;
                e.Handled = true;
                // erase frame
                Graphics g = myBrowser.CreateGraphics();
                DrawSelectionFrame(g);
                g.Dispose();

                // normalize start and end points
                NormalizePoints(ref start, ref end);

                // crop tge image
                ApplyFilter(new Crop(new Rectangle(start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1)));
                if (SelectionFinished != null) {
                    SelectionFinished(this, EventArgs.Empty);
                }
            }
        }

        void myBrowser_WBLButtonDown(object sender, csExWB.HTMLMouseEventArgs e)
        {
            if (cropping)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("myBrowser_WBLButtonDown");
                    // load image
                    image = (Bitmap)myBrowser.DrawThumb(myBrowser.ClientSize.Width, myBrowser.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                    // format image
                    AForge.Imaging.Image.FormatImage(ref image);
                    UpdateSize();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                e.Handled = true;
                // start dragging
                dragging = true;
                // set mouse capture
                //myBrowser.Capture = true;

                // get selection start point
                GetImageAndScreenPoints(new Point(e.ClientX, e.ClientY), out start, out startW);

                // end point is the same as start
                end = start;
                endW = startW;

                // draw frame
                Graphics g = myBrowser.CreateGraphics();
                DrawSelectionFrame(g);
                g.Dispose();
            }
        }

        void myBrowser_Paint(object sender, PaintEventArgs e)
        {
            if (image != null)
            {
                Graphics g = e.Graphics;
                Rectangle rc = myBrowser.ClientRectangle;
                Pen pen = new Pen(Color.FromArgb(0, 0, 0));

                int width = (int)(this.width * zoom);
                int height = (int)(this.height * zoom);
                Point scrollPos = myBrowser.GetScroll();
                int x = (rc.Width < width) ? scrollPos.X : (rc.Width - width) / 2;
                int y = (rc.Height < height) ? scrollPos.Y : (rc.Height - height) / 2;

                // draw rectangle around the image
                g.DrawRectangle(pen, x - 1, y - 1, width + 1, height + 1);

                // set nearest neighbor interpolation to avoid image smoothing
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                // draw image
                g.DrawImage(image, x, y, width, height);

                pen.Dispose();
                System.Diagnostics.Debug.WriteLine("myBrowser_Paint");
            }
        }

     

        public void Crop()
        {
            if (!cropping)
            {
                // turn on
                cropping = true;
                myBrowser.Cursor = Cursors.Cross;
            }
            else
            {
                // turn off
                cropping = false;
                myBrowser.Cursor = Cursors.Default;
            }
        }
        // Apply filter on the image
        private void ApplyFilter(IFilter filter)
        {
            try
            {
                // set wait cursor
                myBrowser.Cursor = Cursors.WaitCursor;

                // apply filter to the image
                Bitmap newImage = filter.Apply(image);           
                // release current image
                image.Dispose();
                image = newImage;

                // update
                UpdateNewImage();
                   
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Selected filter can not be applied to the image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // restore cursor
                myBrowser.Cursor = Cursors.Default;
            }
        }


        // Update document and notify client about changes
        private void UpdateNewImage()
        {
            // update size
            UpdateSize();
            // repaint
            myBrowser.Invalidate();
        }     

        // Update document size 
        private void UpdateSize()
        {
            // image dimension
            width = image.Width;
            height = image.Height;

            // scroll bar size
            //AutoScrollMinSize = new Size((int)(width * zoom), (int)(height * zoom));
        }

       

        // Normalize points so, that pt1 becomes top-left point of rectangle
        // and pt2 becomes right-bottom
        private void NormalizePoints(ref Point pt1, ref Point pt2)
        {
            Point t1 = pt1;
            Point t2 = pt2;

            pt1.X = Math.Min(t1.X, t2.X);
            pt1.Y = Math.Min(t1.Y, t2.Y);
            pt2.X = Math.Max(t1.X, t2.X);
            pt2.Y = Math.Max(t1.Y, t2.Y);
        }


        // Draw selection rectangle
        private void DrawSelectionFrame(Graphics g)
        {
            Point sp = startW;
            Point ep = endW;

            // Normalize points
            NormalizePoints(ref sp, ref ep);
            // Draw reversible frame
            ControlPaint.DrawReversibleFrame(new Rectangle(sp.X, sp.Y, ep.X - sp.X + 1, ep.Y - sp.Y + 1), Color.White, FrameStyle.Thick);
        }

        // On mouse leave
        private void ImageDoc_MouseLeave(object sender, System.EventArgs e)
        {
            if ((!dragging) && (MouseImagePosition != null))
            {
                MouseImagePosition(this, new SelectionEventArgs(new Point(-1, -1)));
            }
        }

        // Calculate image and screen coordinates of the point
        private void GetImageAndScreenPoints(Point point, out Point imgPoint, out Point screenPoint)
        {
            Rectangle rc = myBrowser.ClientRectangle;
            int width = (int)(this.width * zoom);
            int height = (int)(this.height * zoom);
            Point scrollPosition = myBrowser.GetScroll();
            int x = (rc.Width < width) ? scrollPosition.X : (rc.Width - width) / 2;
            int y = (rc.Height < height) ? scrollPosition.Y : (rc.Height - height) / 2;

            int ix = Math.Min(Math.Max(x, point.X), x + width - 1);
            int iy = Math.Min(Math.Max(y, point.Y), y + height - 1);

            ix = (int)((ix - x) / zoom);
            iy = (int)((iy - y) / zoom);

            // image point
            imgPoint = new Point(ix, iy);
            // screen point
            screenPoint = myBrowser.PointToScreen(new Point((int)(ix * zoom + x), (int)(iy * zoom + y)));
        }

 
    }

}
