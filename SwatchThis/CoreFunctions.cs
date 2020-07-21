using System.Drawing;

namespace SwatchThis
{
    public static class CoreFunctions
    {
        public static Color GetColorAt(int x, int y)
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
