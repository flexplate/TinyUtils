using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FirstBits
{
    public partial class MainForm : Form
    {
        const int Limit = 1024;
        byte[] Buffer = new byte[Limit];
        int BytesRead;
        Encoding SelectedEncoding = Encoding.UTF8;


        public MainForm()
        {
            InitializeComponent();

            // Drag/drop bits
            AllowDrop = true;
            rtfHex.AllowDrop = true;
            rtfString.AllowDrop = true;

            DragEnter += ShowDragCursor;
            rtfHex.DragEnter += ShowDragCursor;
            rtfString.DragEnter += ShowDragCursor;

            DragDrop += ProcessDragDrop;
            rtfHex.DragDrop += ProcessDragDrop;
            rtfString.DragDrop += ProcessDragDrop;
        }

        /// <summary>
        /// Display the appropriate cursor to show if the user can drop what they're dragging into the app.
        /// </summary>
        private void ShowDragCursor(object sender, DragEventArgs e)
        {
            // Is user dragging a file?
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// User has dropped what they're dragging. If it's a file, load it.
        /// </summary>
        private void ProcessDragDrop(object sender, DragEventArgs e)
        {
            // Check dropped data is a file.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                // Sanity check.
                if (filePaths.Length > 0)
                {
                    // We only handle one file at a time in this program.
                    if (File.Exists(filePaths[0]))
                    {
                        LoadFile(filePaths[0]);
                    }
                }
            }
        }

        /// <summary>
        /// Open a file, put it in the buffer.
        /// </summary>
        /// <param name="path">Path to the file to open.</param>
        public void LoadFile(string path)
        {
            // Clear outputs
            rtfHex.Text = "";
            rtfString.Text = "";

            // Read
            using (FileStream S = new FileStream(path, FileMode.Open))
            {
                S.Position = 0;
                BytesRead = 0;
                do
                {
                    BytesRead += S.Read(Buffer, BytesRead, Limit - BytesRead);
                } while (BytesRead != Limit && S.Position < S.Length);
            }
            RefreshByteDisplay ();
        }

        /// <summary>
        /// User has clicked an item on the encoding menu. Check it and uncheck the others.
        /// </summary>
        /// <param name="selectedMenuItem">Menu item to keep checked.</param>
        private void DeselectOtherEncodingOptions(ToolStripMenuItem selectedMenuItem)
        {
            selectedMenuItem.Checked = true;
            foreach (var item in selectedMenuItem.Owner.Items.OfType<ToolStripMenuItem>().Where(i => i != selectedMenuItem))
            {
                item.Checked = false;
            }
        }

        /// <summary>
        /// Either the bytes in the buffer or the selected encoding has changed. Refresh the contents of the text displays.
        /// </summary>
        private void RefreshByteDisplay()
        {
            rtfHex.Text = "";
            rtfString.Text = "";
            for (int i = 0; i < BytesRead; i++)
            {
                rtfHex.Text += Buffer[i].ToString("x2") + "\u00b7" ;
                rtfString.Text += SelectedEncoding.GetString(new[] { Buffer[i] }) + "\u00b7";
            }            
        }

        /// <summary>
        /// User has clicked Open. Show a file picker and pass it to LoadFile.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LoadFile(dlg.FileName);
                }
            }
        }

        /// <summary>
        /// User has selected ASCII encoding.
        /// </summary>
        private void aSCIIToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DeselectOtherEncodingOptions(sender as ToolStripMenuItem);
            SelectedEncoding = Encoding.ASCII;
            RefreshByteDisplay();
        }

        /// <summary>
        /// User has selected Unicode encoding.
        /// </summary>
        private void unicodeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DeselectOtherEncodingOptions(sender as ToolStripMenuItem);
            SelectedEncoding = Encoding.Unicode;
            RefreshByteDisplay();
        }

        /// <summary>
        /// User has selected big-endian Unicode encoding.
        /// </summary>
        private void unicodeBigEndianToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DeselectOtherEncodingOptions(sender as ToolStripMenuItem);
            SelectedEncoding = Encoding.BigEndianUnicode;
            RefreshByteDisplay();
                    }

        /// <summary>
        /// User has selected UTF8 encoding.
        /// </summary>
        private void uTF8ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DeselectOtherEncodingOptions(sender as ToolStripMenuItem);
            SelectedEncoding = Encoding.UTF8;
            RefreshByteDisplay();
        }

        /// <summary>
        /// User has selected text in the hex display. Select the same text in the string display.
        /// </summary>
        private void rtfHex_SelectionChanged(object sender, System.EventArgs e)
        {
            var txt = sender as RichTextBox;

            // TODO: Figure out a more robust way of converting indices between strings and hex bytes
            int start = (int)Math.Ceiling(txt.SelectionStart / 1.5);
            int selectionLength = (int)Math.Ceiling(txt.SelectionLength / 1.5);

            rtfString.Select(start, selectionLength);
        }

        /// <summary>
        /// User has selected text in the string display. Select the appropriate bytes.
        /// </summary>
        private void rtfString_SelectionChanged(object sender, System.EventArgs e)
        {
            var txt = sender as RichTextBox;

            // TODO: Figure out a more robust way of converting indices between strings and hex bytes
            int start = (int)Math.Ceiling(txt.SelectionStart*1.5);
            int selectionLength = (int)Math.Ceiling(txt.SelectionLength*1.5);

            rtfHex.Select(start, selectionLength);
        }
    }
}
