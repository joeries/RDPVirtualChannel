using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading;
using System.Collections.Generic;
using Win32.WtsApi32;

namespace TSAddinInCS
{
    public partial class frmMain : Form
    {
        public static frmMain MainForm
        {
            get { return mainForm; }
        }
        static frmMain mainForm;

        public frmMain()
        {
            mainForm = this;
            InitializeComponent();
            mnuAuto.Checked = Properties.Settings.Default.AutoOpen;
            mnuNICAuto.Checked = mnuAuto.Checked;
        }

        ChannelEntryPoints EntryPoints;
        int OpenChannel;

        public frmMain(ChannelEntryPoints entryPoints, int openChannel)
            : this()
        {
            OpenChannel = openChannel;
            EntryPoints = entryPoints;
        }

        public bool RealClosing = false;

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!RealClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
        }

        public void ShowMe(object sender, EventArgs e)
        {
            if (Visible)
                Visible = false;
            else
                Visible = true;
        }

        public delegate void Void();

        public void NewFile(string file)
        {
            Invoke(new Void(delegate() { lvMain.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), file }, 0)); }));
            System.Media.SystemSounds.Exclamation.Play();
            niMain.BalloonTipText = file;
            if (mnuAuto.Checked)
                System.Diagnostics.Process.Start(file);
            else
            {
                Application.DoEvents();
                niMain.ShowBalloonTip(1000);
                Application.DoEvents();
            }
        }

        private void niMain_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(niMain.BalloonTipText);
        }

        private void frmMain_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                mnuNICShow.Text = "Hide";
            else
                mnuNICShow.Text = "Show";

        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            (new frmAbout()).ShowDialog(this);
        }

        private void niMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMe(null, null);
        }

        private void lvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMain.SelectedItems.Count > 0)
            {
                Visible = false;
                System.Diagnostics.Process.Start(lvMain.SelectedItems[0].SubItems[1].Text);
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                niMain.Text = value;
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (ListViewItem lvi in lvMain.Items)
            {
                try
                {
                    System.IO.File.Delete(lvi.SubItems[1].Text);
                }
                catch
                {
                }
            }
        }

        private void mnuLVCSaveAs_Click(object sender, EventArgs e)
        {
            if (lvMain.SelectedItems.Count > 0)
            {
                string fileSavePath = string.Empty;
                Thread newThread = new Thread(delegate()
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Text files|*.txt";
                        saveFileDialog.Title = "Save As...";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            fileSavePath = saveFileDialog.FileName;
                        }
                    }
                });
                newThread.SetApartmentState(ApartmentState.STA);
                newThread.Start();
                newThread.Join();
                try
                {
                    System.IO.File.Copy(lvMain.SelectedItems[0].SubItems[1].Text, fileSavePath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mnuLVCOpen_Click(object sender, EventArgs e)
        {
            lvMain_MouseDoubleClick(null, null);
        }

        private void mnuAuto_Click(object sender, EventArgs e)
        {
            mnuAuto.Checked = !mnuAuto.Checked;
            mnuNICAuto.Checked = mnuAuto.Checked;
            Properties.Settings.Default.AutoOpen = mnuAuto.Checked;
            Properties.Settings.Default.Save();
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            Bounds = new System.Drawing.Rectangle(Screen.PrimaryScreen.WorkingArea.Right - Width, Screen.PrimaryScreen.WorkingArea.Bottom - Height, Width, Height);
        }

        private void mnuSendFile_Click(object sender, EventArgs e)
        {
            string fileOpenPath = string.Empty;
            Thread newThread = new Thread(delegate()
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text files|*.txt";
                    openFileDialog.Title = "Save As...";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        fileOpenPath = openFileDialog.FileName;
                    }
                }
            });
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
            newThread.Join();
            MemoryStream ms = new MemoryStream();
            GZipStream gs = new GZipStream(ms, CompressionMode.Compress, true);
            FileStream fs = File.OpenRead(fileOpenPath);
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = fs.Read(buffer, 0, 1024)) != 0)
            {
                gs.Write(buffer, 0, bytesRead);
            }
            gs.Close();
            byte[] gziped = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(gziped, 0, (int)ms.Length);
            List<byte> dat = new List<byte>(gziped);
            dat.InsertRange(0, BitConverter.GetBytes(dat.Count));
            gziped = dat.ToArray();
            ChannelReturnCodes ret = EntryPoints.VirtualChannelWrite(OpenChannel, gziped, (uint)gziped.Length, null);
        }
    }
}
