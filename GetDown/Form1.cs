using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Media;

namespace GetDown
{
    public partial class Form1 : Form
    {
        BindingList<QueryLogEntry> QueryLog = new BindingList<QueryLogEntry>();
        BackgroundWorker QueryWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        public Form1()
        {
            InitializeComponent();

            // Data-bind options box
            var RetryOptions = new Dictionary<string, int>
            {
                {"1", 1},
                {"5", 5},
                {"10", 10},
                {"50", 50 },
                {"100", 100 },
                {"Until response received",-1 }
            };
            cmbRetry.DataSource = new BindingSource(RetryOptions, null);
            cmbRetry.DisplayMember = "Key";
            cmbRetry.ValueMember = "Value";
            cmbRetry.SelectedIndex = 5;

            // Data-bind 
            dgvStatus.DataSource = QueryLog;
            dgvStatus.Columns["MessageColour"].Visible = false;
            dgvStatus.DataMemberChanged += DgvStatus_DataMemberChanged;

        }

        private void DgvStatus_DataMemberChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            // Are we already running?
            if (QueryWorker.IsBusy)
            {
                QueryWorker.CancelAsync();
            }
            else
            {
                // Check URI
                if (Uri.IsWellFormedUriString(txtUri.Text, UriKind.Absolute))
                {
                    string Uri = txtUri.Text;
                    int RetryLimit = 0;

                    // Test combo box - user can input anytrhing so make sure it's an int or our preferred "until response received" string.
                    if (cmbRetry.Text == "Until response received")
                    {
                        // Go forever or until it's up
                        RetryLimit = -1;
                    }
                    else if (int.TryParse(cmbRetry.Text, out RetryLimit) && RetryLimit > 0)
                    {
                        // We're good to go and we've set RetryLimit.
                    }
                    else
                    {
                        MessageBox.Show("\"" + cmbRetry.Text + "\" is not a valid number of times to retry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Either RetryLimit is -1 (hence, set by us) or it's > 0 (hence, set by user). Go-time.
                    if (RetryLimit != 0)
                    {
                        // Let's make a background process do this so our UI is still responsive.
                        QueryWorker.DoWork += QueryLoopJob;
                        QueryWorker.ProgressChanged += QueryWorker_ProgressChanged;
                        QueryWorker.RunWorkerCompleted += QueryWorker_RunWorkerCompleted;
                        QueryWorker.RunWorkerAsync(argument: new Tuple<string, int>(Uri, RetryLimit));
                        btnGo.Text = "Stop";
                    }
                }
                else
                {
                    MessageBox.Show("\"" + txtUri.Text + "\" is not a valid URI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void QueryLoopJob(object sender, DoWorkEventArgs e)
        {
            var WorkerProcess = ((BackgroundWorker)sender);
            using (var Client = new WebClient())
            {
                if (chkUseProxy.Checked)
                {
                    Client.Proxy = WebRequest.GetSystemWebProxy();
                    Client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                string Uri = ((Tuple<string, int>)e.Argument).Item1;
                string Response = null;
                int LoopCount = 0;
                int MaxLoop = ((Tuple<string, int>)e.Argument).Item2;

                do
                {
                    // Make sure we're supposed to be doing this.
                    if (WorkerProcess.CancellationPending == false)
                    {
                        LoopCount++;
                        var LogEntry = new QueryLogEntry(Uri, "Querying...");
                        WorkerProcess.ReportProgress(LoopCount, LogEntry);
                        try
                        {
                            Response = Client.DownloadString(Uri);
                            LogEntry.Message = "Success";
                            LogEntry.MessageColour = Color.DarkGreen;
                            WorkerProcess.ReportProgress(LoopCount, LogEntry);
                            if (chkPlaySound.Checked) { SystemSounds.Beep.Play(); }
                            // Manually set our exit condition to stop looping.
                            LoopCount = MaxLoop;
                            WorkerProcess.CancelAsync();
                        }
                        catch (WebException wex)
                        {
                            LogEntry.Message = wex.Message;
                            WorkerProcess.ReportProgress(LoopCount, LogEntry);
                            Wait(60);
                        }
                    }
                    else
                    {
                        // Quick & dirty exit.
                        LoopCount = MaxLoop;
                    }
                } while (LoopCount != MaxLoop);


            }
        }


        private void Wait(int seconds)
        {
            var Timer1 = new Timer();
            if (seconds == 0 || seconds < 0) { return; }
            Timer1.Interval = 1000;
            Timer1.Enabled = true;
            Timer1.Start();
            Timer1.Tick += (s, e) =>
            {
                toolStripStatusLabel1.Text = string.Format("Waiting. {0} seconds remaining.", seconds);
                seconds--;
                if (seconds == 0)
                {
                    toolStripStatusLabel1.Text = "";
                    Timer1.Enabled = false;
                    Timer1.Stop();
                }
            };
            while (Timer1.Enabled)
            {
                // Stop timer if user clicks "stop".
                if (QueryWorker.CancellationPending)
                {
                    toolStripStatusLabel1.Text = "";
                    Timer1.Enabled = false;
                    Timer1.Stop();
                }
                Application.DoEvents();
            }
        }

        private void QueryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnGo.Text = "Go";
        }

        private void QueryWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            QueryLogEntry LogEntry = e.UserState as QueryLogEntry;
            if (QueryLog.Count > 0 && QueryLog[QueryLog.Count - 1].QueryDate == LogEntry.QueryDate)
            {
                QueryLog[QueryLog.Count - 1].Message = LogEntry.Message;
                QueryLog[QueryLog.Count - 1].MessageColour = LogEntry.MessageColour;
            }
            else
            {
                QueryLog.Add(LogEntry);
            }
        }


        class QueryLogEntry
        {
            public DateTime QueryDate { get; set; }
            public string URI { get; set; }
            public string Message { get; set; }
            public Color? MessageColour { get; set; }

            public QueryLogEntry(string uri, string message, Color? colour = null)
            {
                QueryDate = DateTime.Now;
                URI = uri;
                Message = message;
                MessageColour = colour ?? SystemColors.ControlText;
            }
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            Button Btn = sender as Button;
            // Expanded?
            if (Btn.Top > 200)
            {
                dgvStatus.Visible = false;
                Btn.Top = 101;
                Btn.Text = "&Show More";
                Btn.FindForm().Height = 193;
            }
            else
            {
                dgvStatus.Visible = true;
                Btn.Top = 385;
                Btn.Text = "&Show Less";
                Btn.FindForm().Height = 480;
            }
        }
    }
}
