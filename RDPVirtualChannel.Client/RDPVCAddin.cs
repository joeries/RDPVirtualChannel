using FieldEffect.VCL.Client;
using FieldEffect.VCL.Client.Interfaces;
using FieldEffect.VCL.Client.WtsApi32;
using FieldEffect.VCL.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RDPVirtualChannel.Client
{
    public static class RDPVCAddin
    {
        private static IRdpClientVirtualChannel client;
        private static string data = string.Empty;
        private static WebClient wc = new WebClient();

        [DllExport("VirtualChannelEntry", CallingConvention.StdCall)]
        public static bool VirtualChannelEntry(ref ChannelEntryPoints entry)
        {
            try
            {
                client = new RdpClientVirtualChannel("RDPVC");
                client.EntryPoints = entry;
                client.Initialize();
                client.DataChannelEvent += RdpClientVirtualChannel_DataChannelEvent;
                //MessageBox.Show("RDPVC Client Initialized");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return false;
            }
        }

        private static void RdpClientVirtualChannel_DataChannelEvent(object sender, DataChannelEventArgs e)
        {
            //MessageBox.Show($"Data Length: {e.DataLength}");
            string curData;
            if (e.DataFlags == ChannelFlags.First || e.DataFlags == ChannelFlags.Only)
            {
                data = "";
            }
            if (e.Data == null)
            {
                return;
            }

            curData = Encoding.UTF8.GetString(e.Data, 0, e.DataLength);
            data = data + curData;

            if (e.DataFlags == ChannelFlags.Last || e.DataFlags == ChannelFlags.Only)
            {
                var url = data;
                Process.Start("explorer", url);

                var response = data;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                try
                {
                    client.VirtualChannelWrite(responseBytes);
                }
                catch (VirtualChannelException vce)
                {
                    //If we don't write the response, the client
                    //probably fell asleep. This isn't a fatal error.
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }
    }
}