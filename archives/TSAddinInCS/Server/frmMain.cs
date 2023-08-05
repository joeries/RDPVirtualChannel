using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Collections.Generic;

namespace TSAddinInCSServer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            Me = this;
            InitializeComponent();
        }
        IntPtr mHandle = IntPtr.Zero;

        private void btLoadAndSend_Click(object sender, EventArgs e)
        {
            if (ofdPickUpFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    GZipStream gs = new GZipStream(ms, CompressionMode.Compress, true);
                    FileStream fs = File.OpenRead(ofdPickUpFile.FileName);
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
                    int written = 0;
                    bool ret = WtsApi32.WTSVirtualChannelWrite(mHandle, gziped, gziped.Length, ref written);
                    if (!ret || written == gziped.Length)
                        MessageBox.Show("Sent!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Bumm! Somethings gone wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Somethings gone wrong:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mHandle = WtsApi32.WTSVirtualChannelOpen(IntPtr.Zero, -1, "TSCS");
            new System.Threading.Thread(new System.Threading.ThreadStart(InterpretLoop)).Start();
            new System.Threading.Thread(new System.Threading.ThreadStart(ReadLoop)).Start();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool ret = WtsApi32.WTSVirtualChannelClose(mHandle);
        }

        bool closing = false;
        static frmMain Me;
        List<byte> maindata = new List<byte>();

        static void ReadLoop()
        {
            while (!Me.closing)
            {
                byte[] data = new byte[1600];
                int readed = 0;
                if (WtsApi32.WTSVirtualChannelRead(Me.mHandle, 2000, data, data.Length, ref readed))
                {
                    if (readed > 0)
                    {
                        byte[] buff = new byte[readed];
                        Buffer.BlockCopy(data, 0, buff, 0, readed);
                        lock (Me.maindata)
                        {
                            Me.maindata.AddRange(buff);
                        }
                    }
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        static void InterpretLoop()
        {
            while (!Me.closing)
            {
                if (Me.maindata.Count > 4)
                {
                    int len = 0;
                    lock (Me.maindata)
                    {
                        len = BitConverter.ToInt32(Me.maindata.GetRange(0, 4).ToArray(), 0);
                        Me.maindata.RemoveRange(0, 4);
                    }
                    MemoryStream ms = new MemoryStream();
                    while (len > 0 && !Me.closing)
                    {
                        byte[] bytes;
                        int count = 0;
                        lock (Me.maindata)
                        {
                            count = len > Me.maindata.Count ? Me.maindata.Count : len;
                            bytes = Me.maindata.GetRange(0, count).ToArray();
                            Me.maindata.RemoveRange(0, count);
                        }
                        len -= count;
                        if (count > 0)
                            ms.Write(bytes, 0, bytes.Length);
                        System.Threading.Thread.Sleep(10);
                    }
                    ms.Position = 0;
                    string filename = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".txt");
                    FileStream fs = File.OpenWrite(filename);
                    byte[] buffer = new byte[1024];
                    GZipStream gs = new GZipStream(ms, CompressionMode.Decompress, false);
                    int bytesRead = 0;
                    while ((bytesRead = gs.Read(buffer, 0, 1024)) != 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                    gs.Close();
                    fs.Close();
                    System.Threading.Thread.Sleep(10);
                    System.Diagnostics.Process.Start(filename);
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }
    }
}