using System;
using System.Drawing;
using System.Windows.Forms;

namespace Measure
{
    class Overlay : Form
    {
        Point start;
        Point end;
        bool drawing;

        Pen linePen = new Pen(new SolidBrush(Color.FromArgb(255, Color.Magenta)), 1);
        Font Segoe = new Font("Segoe UI", 11, FontStyle.Regular);

        Bitmap screenshot = null;

        public Overlay()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            this.MouseDown += Overlay_MouseDown;
            this.MouseUp += Overlay_MouseUp;
            this.MouseMove += Overlay_MouseMove;
            this.Paint += Overlay_Paint;
            this.KeyUp += Overlay_KeyUp;

        }

        private void InitializeComponent()
        {
            this.Name = "Measure";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Set start point as we initially click
        /// </summary>
        private void Overlay_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            drawing = true;
        }

        /// <summary>
        /// Stop drawing as we release the mouse.
        /// </summary>
        private void Overlay_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        /// <summary>
        /// If we're drawing and moving the mouse, we set the end point to the current cursor location.
        /// </summary>
        private void Overlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                end = e.Location;
                // Cause active area of form to be redrawn
                this.Invalidate();
            }
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            // Vertical line
            e.Graphics.DrawLine(linePen, start.X, start.Y, start.X, end.Y);

            // Horizontal line
            e.Graphics.DrawLine(linePen, start.X, end.Y, end.X, end.Y);

            // Diagonal line
            e.Graphics.DrawLine(linePen, start.X, start.Y, end.X, end.Y);

            // Boxes to show end points
            e.Graphics.FillPolygon(Brushes.LimeGreen, PointToBox(start));
            e.Graphics.FillPolygon(Brushes.LimeGreen, PointToBox(end));

            // Text coordinates of start- and endpoints.
            e.Graphics.DrawString(SimpleCoords(start), Segoe, Brushes.White, start);
            e.Graphics.DrawString(SimpleCoords(end), Segoe, Brushes.White, end);

            // Line lengths
            // Width
            int dx = end.X - start.X;
            e.Graphics.DrawString(Math.Abs(dx) + "px", Segoe, Brushes.White, start.X + dx / 2, end.Y);

            // Height
            int dy = end.Y - start.Y;
            e.Graphics.DrawString(Math.Abs(dy) + "px", Segoe, Brushes.White, start.X, start.Y + dy / 2);

            // Hypotenuse length
            double hyp = Math.Round(Math.Sqrt(dx * dx + dy * dy), 0, MidpointRounding.AwayFromZero);
            e.Graphics.DrawString(hyp + "px", Segoe, Brushes.White, start.X + dx / 2, start.Y + dy / 2);

        }

        /// <summary>
        /// Capture the escape or Q keys to exit.
        /// </summary>
        private void Overlay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Q)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Convert a single point into a 5x5 pixel box
        /// </summary>
        /// <param name="point">Point to centre box on.</param>
        /// <returns>An array of points making a 5x5 rectangle centred on a specified point.</returns>
        private Point[] PointToBox(Point point)
        {
            return new Point[] {
                new Point(point.X - 2, point.Y - 2 ),
                new Point(point.X + 2, point.Y - 2 ),
                new Point(point.X + 2, point.Y + 2 ),
                new Point(point.X - 2, point.Y + 2 )
            };
        }

        /// <summary>
        /// Override the background painting to fake a translucent form background.
        /// Draw instructions at this point to avoid redrawing every frame.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Take screenshot the first time. Won't update the screen, but otherwise it fades to black every frame as we take successively darker screenshots.
            if (screenshot == null)
            {
                using (Image image = new Bitmap(this.Width, this.Height))
                {
                    using (Graphics Surface = Graphics.FromImage(image))
                    {
                        var BackgroundBrush = new SolidBrush(Color.FromArgb(128, Color.Black));
                        var DarkerBrush = new SolidBrush(Color.FromArgb(192, Color.Black));

                        Surface.CopyFromScreen(0, 0, 0, 0, this.Size);
                        Surface.FillRectangle(BackgroundBrush, this.DisplayRectangle);

                        // Measure and draw instructions. Draw a darker rectangle behind them.
                        Size TextSize = ExpandSize(Size.Round(e.Graphics.MeasureString(Properties.Strings.Instructions, Segoe)), 5);
                        Point TextOrigin = Point.Round(new PointF(this.Width / 2 - TextSize.Width / 2, this.Height - TextSize.Height - 10));
                        var TextBox = new Rectangle(TextOrigin, TextSize);
                        Surface.FillRectangle(DarkerBrush, TextBox);
                        Surface.DrawString(Properties.Strings.Instructions, Segoe, Brushes.White, TextBox);

                    }
                    screenshot = new Bitmap(image);
                }
            }
            e.Graphics.DrawImage(screenshot, 0, 0);
        }
        
        /// <summary>
        /// Get shorter version of the coordinates of a Point (or PointF).
        /// </summary>
        /// <param name="point">Point to return coords of.</param>
        /// <returns>Coordinates in the format "X,Y".</returns>
        public string SimpleCoords(PointF point)
        {
            return point.X + "," + point.Y;
        }

        /// <summary>
        /// Expand a given size by a given amount.
        /// </summary>
        /// <param name="size">Size to base our new size on.</param>
        /// <param name="expandBy">Amount to change dimensions by.</param>
        public Size ExpandSize(Size size, int expandBy)
        {
            return new Size(size.Width + expandBy, size.Height + expandBy);
        }

    }
}
