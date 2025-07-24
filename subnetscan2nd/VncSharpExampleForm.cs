using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VncSharp;

namespace VncSharpExampleCS
{
    public partial class form : Form
    {
        private readonly string host;
        private static string GetPassword()
        {
            //Password Generale
            string password = "paratori";
            return password;
        }

        public form(string host)
        {

            this.host = host;
            InitializeComponent();
            rd.GetPassword = new AuthenticateDelegate(GetPassword);
            Connection();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // If the user tries to close the window without doing a clean
            // shutdown of the remote connection, do it for them.
            if (rd.IsConnected)
                rd.Disconnect();

            base.OnClosing(e);
        }

        private void FlipMenuOptions()
        {
            connectToolStripMenuItem.Enabled = !rd.IsConnected;

            disconnectToolStripMenuItem.Enabled = rd.IsConnected;
            cTRLALTDELToolStripMenuItem.Enabled = rd.IsConnected;
            fullScreenRefreshToolStripMenuItem.Enabled = rd.IsConnected;
            aLTF4ToolStripMenuItem.Enabled = rd.IsConnected;
            cTRLESCToolStripMenuItem.Enabled = rd.IsConnected;
            cTRLToolStripMenuItem.Enabled = rd.IsConnected;
            aLTToolStripMenuItem.Enabled = rd.IsConnected;
            pasteToolStripMenuItem.Enabled = rd.IsConnected;
        }

        private void ConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connection();
        }

        private void DisconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rd.IsConnected)
               rd.Disconnect();
        }

        private void CTRLALTDELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rd.IsConnected)
                rd.SendSpecialKeys(SpecialKeys.CtrlAltDel);
        }

        private void ALTF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rd.IsConnected)
                rd.SendSpecialKeys(SpecialKeys.AltF4);
        }

        private void CTRLESCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rd.IsConnected)
                rd.SendSpecialKeys(SpecialKeys.CtrlEsc);
        }

        private void CTRLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rd.IsConnected)
                rd.SendSpecialKeys(SpecialKeys.Ctrl, false /* don't release CTRL */);
        }

        private void ALTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rd.IsConnected)
                rd.SendSpecialKeys(SpecialKeys.Alt, false /* don't release ALT */);
        }

        private void ClippedViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clippedViewToolStripMenuItem.Checked = true;
            scaledViewToolStripMenuItem.Checked = false;

            // Turn-off scaling and use clipping
            if (rd.IsConnected)
                rd.SetScalingMode(false);
        }

        private void ScaledViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clippedViewToolStripMenuItem.Checked = false;
            scaledViewToolStripMenuItem.Checked = true;

            // Turn-off clipping and use scaling
            if (rd.IsConnected)
                rd.SetScalingMode(true);
        }

        private void FullScreenRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Request a fullscreen update (normally incremental updates are sent)
            if (rd.IsConnected)
                rd.FullScreenUpdate();
        }

        private void ViewOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewOnlyToolStripMenuItem.Checked = !viewOnlyToolStripMenuItem.Checked;

            // Turn view-only mode (no mouse/keyboard events sent) on or off
            if (rd.IsConnected)
                rd.SetInputMode(viewOnlyToolStripMenuItem.Checked);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Copy the contents of the local clipboard into the server's clipboard
            // so that it can be pasted.  Only works with text.
            if (rd.IsConnected) {
                rd.FillServerClipboard();

                MessageBox.Show(this,
                                "Your clipboard's text was copied to the remote host.",
                                "Clipboard Copied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
  
            }
        }

        private void Rd_ConnectComplete(object sender, ConnectEventArgs e)
        {
            // Update the Form to match the geometry of remote desktop (including the height of the menu bar in this form).
            ClientSize = new Size(e.DesktopWidth, e.DesktopHeight + menuStrip1.Height);

            // Change the Form's title to match the remote desktop name
            Text = e.DesktopName;

            FlipMenuOptions();

            // Give the remote desktop focus now that it's connected
            rd.Focus();
        }

        private void Rd_ConnectionLost(object sender, EventArgs e)
        {
            // Let the user know of the lost connection     MESSAGGIO TOLTO PERCHE' INVADENTE

            /*MessageBox.Show(this,
                        "Lost Connection to Host.",
                        "Connection Lost",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            FlipMenuOptions();
            */
        }

        private void Rd_ClipboardChanged(object sender, EventArgs e)
        {
            // You normally wouldn't do this (i.e., you might show something in the status bar),
            // but as a demo, let the user know that there is new data in the local clipboard
            MessageBox.Show(this,
                            "Remote clipboard copied to local host.",
                            "Clipboard Changed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void Rd_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form_Load(object sender, EventArgs e)
        {

        }

        private void VncSharpExampleCS_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Connection()
        {
            // As long as they didn't click Cancel, try to connect
            if (host != null) {
                try
                {
                    rd.Connect(host, viewOnlyToolStripMenuItem.Checked, scaledViewToolStripMenuItem.Checked);
                }
                catch (VncProtocolException vex)
                {
                    MessageBox.Show(this,
                                    string.Format("Unable to connect to VNC host:\n\n{0}.\n\nCheck that a VNC host is running there.", vex.Message),
                                    string.Format("Unable to Connect to {0}", host),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                                    string.Format("Unable to connect to host.  Error was: {0}", ex.Message),
                                    string.Format("Unable to Connect to {0}", host),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}