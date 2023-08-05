using FieldEffect.VCL.Server;
using FieldEffect.VCL.Server.Interfaces;
using System;
using System.Net;
using System.Runtime.InteropServices;

namespace RDPVirtualChannel.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No args");
                return;
            }
            var url = args[0];
            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("No url");
                return;
            }

            try
            {
                IRdpServerVirtualChannel server = new RdpServerVirtualChannel("RDPVC");
                server.OpenChannel();
                server.WriteChannel(url);
                url = server.ReadChannel();
                server.CloseChannel();
            }
            catch (Exception ex)
            {
                var wc = new WebClient();
                wc.UploadString($"http://test.uu163yun.com/Home/Push?url={url}", "");
                //Console.WriteLine(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                //return;
            }
        }
    }
}