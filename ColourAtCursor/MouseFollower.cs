using System;
using System.Drawing;
using System.Windows.Forms;

namespace SwatchThis
{
    public partial class MouseFollower : Form
    {
        private const int Offset = 16;
        private const int ScanningInterval = 20; // milliseconds

        public MouseFollower()
        {
            InitializeComponent();

            // Timer to get mouse position every few milliseconds.
            Timer UpdateTimer = new Timer();
            UpdateTimer.Tick += T_Tick;
            UpdateTimer.Interval = ScanningInterval;
            UpdateTimer.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            var Coords = Cursor.Position;

            // Current colour - update the labels etc.
            Color C = CoreFunctions.GetColorAt(Coords.X, Coords.Y);
            pbColour.BackColor = C;
            lblCurrent.Text = ColorTranslator.ToHtml(C);
            lblRGB.Text = string.Format("R:{0} G:{1} B:{2}", C.R, C.G, C.B);


            // Update position. 
            // Check X and stop it going off the end.
            if (Coords.X + Offset + this.Width > Screen.GetWorkingArea(Coords).Width)
            {
                Coords.X = Coords.X - Offset - this.Width;
            }
            else
            {
                Coords.X += Offset;
            }

            // Check Y and stop it going off the bottom.
            if (Coords.Y + Offset + this.Height > Screen.GetWorkingArea(Coords).Height)
            {
                Coords.Y = Coords.Y - Offset - this.Height;
            }
            else
            {
                Coords.Y += Offset;
            }
            this.Location = Coords;            
        }
    }
}
