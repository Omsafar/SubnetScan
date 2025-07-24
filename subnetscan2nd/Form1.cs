using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using VncSharpExampleCS;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Data;
using System.Management;
using System.ComponentModel;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Data.OleDb;

namespace subnetscan2nd
{
    public partial class Form1 : Form
    {
        // Timer da 2 minuti per Scan Automatica
        readonly Timer t = new Timer(TimeSpan.FromMinutes(2).TotalMilliseconds);

        // Range Scan IP
        // From IP 192.168.1.130 (Before only Server Machines and others)
        readonly int firstIp = 130;
        readonly int lastIP = 254;

        // Countdown for Async Ping
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

        CountdownEvent countdown;

        // Creazione DataTable per info Utenti, Computers
        readonly DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();

            // Inizializzazione DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;

            // Tolte ScrollBar e aggiunto Handler Rotella del Mouse
            dataGridView1.MouseWheel += new MouseEventHandler(Mousewheel);

            // Orologio
            DigiClockTextBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            timer1.Enabled = true;
            timer1.Interval = 1000;

            // Initialize Status
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "Idle";
            Control.CheckForIllegalCrossThreadCalls = false;

            // Scan ogni 'Timer' minuti
            t.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Tick);
            t.Start();
        }

        [Obsolete]
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            backgroundWorker1.RunWorkerAsync(argument: true);

        }        // Scan Iniziale
        public void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            if (conMenuStripIP.Visible == false)
            {
                resetEvent.WaitOne();
                backgroundWorker1.RunWorkerAsync(argument: true);
            }
            t.Stop();
            t.Start();

        }   // Call Scan every 30 Minutes
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.ReportProgress(0);

            // Cambio Stato Bottoni inizio
            cmdScan.Enabled = false;
            button1.Enabled = false;

            // Reset Timer 30 Minuti iniziale (per escludere il caso in cui venga richiamata durante l'esecuzione)
            t.Stop();
            t.Start();

            // Salvo Argomento per scansione automatica si/no
            bool auto = (bool)e.Argument;
            int count = 0;

            // Messaggio di Stato
            lblStatus.ForeColor = System.Drawing.Color.Green;
            lblStatus.Text = "Scanning...";

            // Pulizia DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            Thread.Sleep(500);

            backgroundWorker.ReportProgress(25);

            // Connessione al DB
            _ = new Import.Utility();
            string connection = "Data Source=SRVOMGES;Initial Catalog=DATASTAT01;Connect Timeout=500;Persist Security Info=True;User ID=TipesStat;Password=stp";
            Import.DbUtility utDB = new Import.DbUtility(connection);
            utDB.getConnection().Open();

            // Query e Fill DataTable da Select
            string queryId = "SELECT IP, PCName, Username FROM tbDailyPCInfo";
            SqlCommand sqlId = utDB.createSqlCommand(queryId);
            dt.Load(sqlId.ExecuteReader());
            utDB.getConnection().Close();

            // Subnet
            string subnet = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    subnet = ip.ToString();
                }
            }
            subnet = subnet.Substring(0, subnet.LastIndexOf("."));

            countdown = new CountdownEvent(1);

            // Counters per percentuale
            backgroundWorker.ReportProgress(50);

            // Loop di tutti gli IP
            for (int i = firstIp; i < lastIP; i++)
            {
                if (backgroundWorker1.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                // Sintassi Indirizzo IP
                string subnetn = "." + i.ToString();
                Ping ping = new Ping();
                ping.PingCompleted += new PingCompletedEventHandler(Ping_PingCompleted);
                countdown.AddCount();

                ping.SendAsync(subnet + subnetn, 100);
            }

            countdown.Signal();
            countdown.Wait();

            dt.Dispose();
            

            // Ordino automaticamente Per Nome
            dataGridView1.Sort(dataGridView1.Columns["User"], ListSortDirection.Ascending);
            dataGridView1.Refresh();
            lblStatus.Text = "Done!";

            // Aggiorno numero di Hosts Trovati
            count = dataGridView1.Rows.Count;
            label5.Text = count.ToString() + " Hosts";

            // Reset Timer 30 Minuti finale
            t.Stop();
            t.Start();

            backgroundWorker.ReportProgress(100);

            if (!auto && !(this.WindowState == FormWindowState.Minimized))
            {
                // Message with Hosts count
                label4.Text = "Last Scan: " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                MessageBox.Show("Scanning done!\nFound " + count.ToString() + " hosts.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                // Aggiornamento Last Scan Automatico
                label4.Text = "Last Scan: " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " (Auto)";
            }
            
            // Fine Ricerca
            cmdScan.Enabled = true;
            button1.Enabled = true;

            resetEvent.Set();
        }
        private void Ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            Retry:
            // Se l'IP Risponde allora
            if (e.Reply.Status == IPStatus.Success)
            {
                string address = e.Reply.Address.ToString();
                try
                {
                    // Query per ricerca PCName e Username da GetDailyPCInfo
                    string pcname = (from DataRow dr in dt.Rows
                                     where (string)dr["IP"] == address
                                     select (string)dr["PCName"]).FirstOrDefault();
                    string username = (from DataRow dr in dt.Rows
                                       where (string)dr["IP"] == address
                                       select (string)dr["Username"]).FirstOrDefault();

                    // Aggiunta di informazioni estratte all'elenco
                    if (pcname != null && username != null)
                    {
                        dataGridView1.Refresh();
                        dataGridView1.Rows.Add(address, pcname, username);
                        dataGridView1.Refresh();
                    }
                }
                catch (Exception ex) { goto Retry; }
            }

            countdown.Signal();
        }
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        [Obsolete]
        private void CmdScan_Click(object sender, EventArgs e)
        {
            resetEvent.WaitOne();
            backgroundWorker1.RunWorkerAsync(argument: false);

        }      // Scan Button Click


        //  Controllo Computers
        private void Button1_Click(object sender, EventArgs e)
        {

            int c = 0;
            string host = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                host += row.Cells["IP"].Value.ToString() + ",";
                c++;
            }

            DialogResult dialogResult = MessageBox.Show("\nShutdown all Computers in 5 minutes \n\n" +
                "Are you sure?", "Shutdown All", MessageBoxButtons.YesNo);
            // If User is Sure
            if (dialogResult == DialogResult.Yes)
            {
                // Calculate Difference in Seconds between Time Insered and Actual Time
                var seconds = 300;
                // Creating Process to Restart
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    host = row.Cells["IP"].Value.ToString();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        FileName = "shutdown.exe",
                        Arguments = "/s /f /m \\\\" + host + " /t " + seconds + " "
                        + "/c \"The IT department has initiated a remote Forced Shutdown on your computer.\n"
                        + "Your PC will Shutdown in 5 Minutes.\""
                    };
                    _ = Process.Start(startInfo);
                    this.Close();
                }
            }

        }      // Shutdown All Option

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // Controllo se ho cliccato con tasto destro
                if (e.Button == MouseButtons.Right)
                {
                    // Gestisco il Click destro sull'Header (altrimenti programma Crasha)
                    if (e.RowIndex == -1) return;
                    else
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                        dataGridView1.Focus();
                        conMenuStripIP.Show(Cursor.Position);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }   // Click destro, seleziono riga

        private void DetectActiveProcesses()
        {
            ConnectionOptions connectOptions = new ConnectionOptions
            {
                Username = @"tipesom\administrator",
                Password = "lele98",
            };

            //IP Address of the remote machine
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string ipAddress = row.Cells["IP"].Value.ToString();

            try
            {
                ManagementScope scope = new ManagementScope(@"\\" + ipAddress + @"\root\cimv2", connectOptions);

                //Define the WMI query to be executed on the remote machine
                SelectQuery query = new SelectQuery("select * from Win32_PerfFormattedData_PerfProc_Process");

                List<Tuple<string, int>> processes = new List<Tuple<string, int>>();
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();

                    foreach (ManagementObject process in collection)
                    {
                        //(conMenuStripIP.Items[6] as ToolStripMenuItem).DropDownItems.Add(process["Name"].ToString());
                        processes.Add(new Tuple<string, int>(process["Name"].ToString(), Convert.ToInt32(process["PercentProcessorTime"].ToString())));
                    }
                }

                processes = processes.OrderBy(i => i.Item2).ToList();
                //processes = processes.Distinct().ToList();

                foreach (Tuple<string, int> s in processes)
                    (conMenuStripIP.Items[6] as ToolStripMenuItem).DropDownItems.Add(s.Item1.ToString() + " " + s.Item2.ToString());

                // Other code
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }   //  Detect Processi Aperti

        private void ShowInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string host = row.Cells["IP"].Value.ToString();
            Thread qThread = new Thread(() => Query(host));
            qThread.Start();
        }   // Show Info Option
        public void Query(string host)
        {

            // Connessione al DB
            _ = new Import.Utility();
            string connection = "Data Source=SRVOMGES;Initial Catalog=DATASTAT01;Connect Timeout=500;Persist Security Info=True;User ID=TipesStat;Password=stp";
            Import.DbUtility utDB = new Import.DbUtility(connection);
            utDB.getConnection().Open();

            lblStatus.ForeColor = System.Drawing.Color.Green;

            // Query per ricerca Informazioni Complete da DB
            string queryId = "SELECT * FROM tbDailyPCInfo WHERE IP = '" + host + "'";
            SqlCommand sqlId = utDB.createSqlCommand(queryId);
            SqlDataReader readerId = sqlId.ExecuteReader();

            readerId.Read();
            string text = "";

            // Per ogni campo del Reader leggo NomeColonna e Valore
            for (int i = 1; i < readerId.FieldCount; i++)
            {
                text = text + readerId.GetName(i) + ": " + readerId.GetValue(i) + "\n";
            }
            readerId.Close();

            //  Messaggio finale di Informazioni
            MessageBox.Show(text, "Hostinfo: " + host, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }                                             // Read All Infos from DB and shows a Message Box

        public static void ControlSys(string host, int flags)
        {
            #region 
            /*
             *Flags:
             *  0 (0x0)Log Off
             *  4 (0x4)Forced Log Off (0 + 4)
             *  1 (0x1)Shutdown
             *  5 (0x5)Forced Shutdown (1 + 4)
             *  2 (0x2)Reboot
             *  6 (0x6)Forced Reboot (2 + 4)
             *  8 (0x8)Power Off
             *  12 (0xC)Forced Power Off (8 + 4)
             */
            #endregion
            RebootSettings Reboot = null;
            try
            {
                // Connessione al DB
                _ = new Import.Utility();
                string connection = "Data Source=SRVOMGES;Initial Catalog=DATASTAT01;Connect Timeout=500;Persist Security Info=True;User ID=TipesStat;Password=stp";
                Import.DbUtility utDB = new Import.DbUtility(connection);
                utDB.getConnection().Open();
                // Query per ricerca PCName e Username da GetDailyPCInfo
                string queryId = "SELECT * FROM tbDailyPCInfo WHERE IP = '" + host + "'";
                SqlCommand sqlId = utDB.createSqlCommand(queryId);
                SqlDataReader readerId = sqlId.ExecuteReader();
                readerId.Read();
                string pcname = readerId["PCName"].ToString();
                string username = readerId["Username"].ToString();
                readerId.Close();
                string text = "PCName: " + pcname + "\nUsername " + username;

                Reboot = new RebootSettings(host, text, flags);
                Reboot.Show();
            }
            finally
            {
                if (!Application.OpenForms.OfType<RebootSettings>().Any()) { Reboot.Dispose(); }
            }

        }                             // Sends Reboot, Shutdown, Forced Shutdown command to PC
        private void ShutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string host = row.Cells["IP"].Value.ToString();
            ControlSys(host, 5);
        }   // Shutdown Option
        private void RebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string host = row.Cells["IP"].Value.ToString();
            ControlSys(host, 6);
        }     // Reboot Option
        private void PowerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string host = row.Cells["IP"].Value.ToString();
            ControlSys(host, 12);
        }   // Forced Shutdown Option
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string host = row.Cells["IP"].Value.ToString();
            string path = @"\\" + host + @"\c$\";
            System.Diagnostics.Process.Start(path);

        }          // Open C$
        private void VNCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.SelectedRows[0];
            string host = row.Cells["IP"].Value.ToString();
            form VNC = new form(host);
            VNC.Show();
            if (!Application.OpenForms.OfType<form>().Any())
                VNC.Dispose();


        }        // VNC
        private void ProcessKillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectActiveProcesses();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            DigiClockTextBox.Text = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
        }                       // Orologio
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }        // Force Exit Application
        private void Mousewheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && dataGridView1.FirstDisplayedScrollingRowIndex > 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex++;
            }
        }                   // Handler Rotella del Mouse

        // Altri Metodi
        // Nascondi Icona
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

    }
}
