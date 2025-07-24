using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace subnetscan2nd
{
    public partial class RebootSettings : Form
    {
        private readonly string host;
        private readonly string text;
        private readonly string action;
        private readonly string arg;
        public RebootSettings(string host, string text, int flags)
        {
            this.host = host;
            this.text = text;
            InitializeComponent();

            switch (flags)
            {
                case 6:
                    action = "Reboot";
                    arg = "/r /f";
                    break;
                case 5:
                    action = "Shutdown";
                    arg = "/s";
                    break;
                case 12:
                    action = "-F Shutdown";
                    arg = "/s /f";
                    break;
            }

            this.Text = action + " Settings";
            label1.Text = action + " at:";
            button2.Text = action + " at Time";
            button1.Text = action + " in 5 Min";

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(text + "\n\n" + action + " at \n"
                + dateTimePicker1.Value + "\n\nAre you sure?", action, MessageBoxButtons.YesNo);
            // If User is Sure
            if (dialogResult == DialogResult.Yes)
            {
                // Calculate Difference in Seconds between Time Insered and Actual Time
                var seconds = (int)Math.Floor((dateTimePicker1.Value - DateTime.Now).TotalSeconds);
                // Creating Process to Restart
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "shutdown.exe",
                    Arguments = arg + " /m \\\\" + host + " /t " + seconds + " "
                    + "/c \"The IT department has initiated a remote " + action + " on your computer.\n"
                    + "Your PC will " + action + " at " + dateTimePicker1.Value + ".\""
                };
                _ = Process.Start(startInfo);
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }

        }   // Reboot At Time
        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(text + "\n\n" + action + " in 5 minutes\n\nAre you sure?", action, MessageBoxButtons.YesNo);
            // If User is Sure
            if (dialogResult == DialogResult.Yes)
            {
                // Set seconds at 300 (5 minutes, let User have time to close apps and save documents)
                var seconds = 300;
                // Creating Process to Restart
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "shutdown.exe",
                    Arguments = "/r /m \\\\" + host + " /t " + seconds + " "
                    + "/c \"The IT department has initiated a remote " + action + " on your computer.\n"
                    + "Your PC will " + action + " in 5 minutes, close your applications and save documents.\""
                };
                _ = Process.Start(startInfo);
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }

        }   // Reboot in 5 Min
    }
}
