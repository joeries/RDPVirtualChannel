using System;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.IO.Compression;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TSAddinInCS
{
    public class Unpacker
    {
        byte[] bytes;
        int offset = 0;

        public Unpacker(uint totalLength)
        {
            bytes = new byte[(long)totalLength];
        }

        public void DataRecived(byte[] data)
        {
            Buffer.BlockCopy(data, 0, bytes, offset, data.Length);
            offset += data.Length;
        }


        public void Unpack()
        {
            Thread thread = new Thread(new ThreadStart(ThreadUnpack));
            thread.Start();
        }

        void ThreadUnpack()
        {
            try
            {
                string filename = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".txt");
                FileStream fs = File.OpenWrite(filename);
                byte[] buffer = new byte[1024];
                MemoryStream ms = new MemoryStream();
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                GZipStream gs = new GZipStream(ms, CompressionMode.Decompress, false);
                int bytesRead = 0;
                while ((bytesRead = gs.Read(buffer, 0, 1024)) != 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
                gs.Close();
                fs.Close();
                frmMain.MainForm.NewFile(filename);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
