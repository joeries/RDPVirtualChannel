using FieldEffect.VCL.Client.WtsApi32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RDPVirtualChannel.Client.Test
{
    internal class Program
    {
        [DllImport("_RDPVirtualChannel.Client.dll")]
        public static extern bool VirtualChannelEntry(ref ChannelEntryPoints entry);

        static void Main(string[] args)
        {
            ChannelEntryPoints entry = new ChannelEntryPoints();
            VirtualChannelEntry(ref entry);
        }
    }
}
