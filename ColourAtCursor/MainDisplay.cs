using System;
using System.Drawing;
using System.Windows.Forms;

namespace SwatchThis
{
    public partial class MainDisplay : Form
    {
        KeyboardHook hook = new KeyboardHook();

        public MainDisplay()
        {
            InitializeComponent();
            chkShowFollower.Checked = Properties.Settings.Default.DisplayMouseFollower;
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(SwatchThis.ModifierKeys.Control | SwatchThis.ModifierKeys.Alt, Keys.C);
            ShowOrHideFollower();
        }

        private static void ShowOrHideFollower()
        {
            bool FollowerExists = false;
            foreach (Form F in Application.OpenForms)
            {
                if (F.Name == "MouseFollower") { FollowerExists = true; }
            }

            // Spawn one if it's required and doesn't exist.
            if (Properties.Settings.Default.DisplayMouseFollower && FollowerExists == false)
            {
                var F = new MouseFollower();
                F.Show();
            }

            // Close it if it's there and not supposed to be.
            if (Properties.Settings.Default.DisplayMouseFollower == false && FollowerExists == true)
            {
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].GetType() == typeof(MouseFollower))
                    {
                        Application.OpenForms[i].Close();
                    }
                }
            }
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Color C = CoreFunctions.GetColorAt(Cursor.Position.X, Cursor.Position.Y);
            lblColour.Text = string.Format("{0} (R:{1}, G:{2}, B:{3})", ColorTranslator.ToHtml(C), C.R, C.G, C.B);
            pbColour.BackColor = C;
        }

        private void chkShowFollower_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisplayMouseFollower = ((CheckBox)sender).Checked;
            Properties.Settings.Default.Save();
            ShowOrHideFollower();
        }
    }
}
