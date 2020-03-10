using System;
using System.Windows.Forms;

namespace MachineKeyGenerator
{
    public partial class GeneratorForm : Form
    {
        public GeneratorForm()
        {
            InitializeComponent();
            txtDecryption.Text = Properties.Settings.Default.DecryptionKeyLength.ToString();
            txtValidation.Text = Properties.Settings.Default.ValidationKeyLength.ToString();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int DecLength = Properties.Settings.Default.DecryptionKeyLength;
            int ValLength = Properties.Settings.Default.ValidationKeyLength;
            if (!Int32.TryParse(txtDecryption.Text, out DecLength) || !Int32.TryParse(txtValidation.Text, out ValLength))
            {
                txtOutput.Text += string.Format("No (or invalid) lengths specified for decryption key length or validation key length.\r\nUsing defaults of {0} bytes for decryption key and {1} bytes for validation key.\r\n", DecLength, ValLength);
            }
            txtOutput.Text += string.Format("<machineKey decryptionKey=\"{0}\" validationKey=\"{1}\" validation=\"HMACSHA256\"/>\r\n", KeyGenerator.CreateKey(DecLength), KeyGenerator.CreateKey(ValLength));
        }
    }
}
