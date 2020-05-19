using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColourAtCursor
{
    public partial class MainDisplay : Form
    {
        KeyboardHook hook = new KeyboardHook();

        public MainDisplay()
        {
            InitializeComponent();
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(ColourAtCursor.ModifierKeys.Control | ColourAtCursor.ModifierKeys.Alt, Keys.C);
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Color C = GetColorAt(Cursor.Position.X, Cursor.Position.Y);
            lblColour.Text = string.Format("{0} (R:{1}, G:{2}, B:{3})", ColorTranslator.ToHtml(C), C.R, C.G, C.B);
            pbColour.BackColor = C;
        }

        Color GetColorAt(int x, int y)
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                Rectangle bounds = new Rectangle(x, y, 1, 1);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                }
                return bmp.GetPixel(0, 0);
            }
        }
    }
}
