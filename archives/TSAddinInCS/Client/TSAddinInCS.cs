using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32.WtsApi32;

namespace TSAddinInCS
{
    public class TSAddinInCS
    {
        static IntPtr Channel;
        static ChannelEntryPoints EntryPoints;
        static int OpenChannel = 0;
        //ATTENTION: name should have 7 or less signs
        const string ChannelName = "TSCS";
        static Unpacker unpacker = null;
        static ChannelInitEventDelegate channelInitEventDelegate = new ChannelInitEventDelegate(VirtualChannelInitEventProc);
        static ChannelOpenEventDelegate channelOpenEventDelegate = new ChannelOpenEventDelegate(VirtualChannelOpenEvent);
        static frmMain main = null;

        [ExportDllAttribute.ExportDll("VirtualChannelEntry", System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static bool VirtualChannelEntry(ref ChannelEntryPoints entry)
        {
            ChannelDef[] cd = new ChannelDef[1];
            cd[0] = new ChannelDef();
            EntryPoints = entry;
            cd[0].name = ChannelName;
            ChannelReturnCodes ret = EntryPoints.VirtualChannelInit(ref Channel, cd, 1, 1, channelInitEventDelegate);
            if (ret != ChannelReturnCodes.Ok)
            {
                MessageBox.Show("TSAddinInCS: RDP Virtual channel Init Failed.\n" + ret.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static void VirtualChannelInitEventProc(IntPtr initHandle, ChannelEvents Event, byte[] data, int dataLength)
        {
            switch (Event)
            {
                case ChannelEvents.Initialized:
                    break;
                case ChannelEvents.Connected:
                    ChannelReturnCodes ret = EntryPoints.VirtualChannelOpen(initHandle, ref OpenChannel, ChannelName, channelOpenEventDelegate);
                    if (ret != ChannelReturnCodes.Ok)
                        MessageBox.Show("TSAddinInCS: Open of RDP virtual channel failed.\n" + ret.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        main = new frmMain(EntryPoints, OpenChannel);
                        main.Show();
                        main.Hide();
                        string servername = System.Text.Encoding.Unicode.GetString(data);
                        servername = servername.Substring(0, servername.IndexOf('\0'));
                        main.Text = "TS addin in C#: " + servername;
                    }
                    break;
                case ChannelEvents.V1Connected:
                    MessageBox.Show("TSAddinInCS: Connecting to a non Windows 2000 Terminal Server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ChannelEvents.Disconnected:
                    main.RealClosing = true;
                    main.Invoke(new EventHandler(delegate(object sender, EventArgs e) { main.Close(); }));
                    GC.KeepAlive(main);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    break;
                case ChannelEvents.Terminated:
                    GC.KeepAlive(channelInitEventDelegate);
                    GC.KeepAlive(channelOpenEventDelegate);
                    GC.KeepAlive(EntryPoints);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    break;
            }
        }

        public static void VirtualChannelOpenEvent(int openHandle, ChannelEvents Event, byte[] data, int dataLength, uint totalLength, ChannelFlags dataFlags)
        {
            switch (Event)
            {
                case ChannelEvents.DataRecived:
                    switch (dataFlags & ChannelFlags.Only)
                    {
                        case ChannelFlags.Only:
                            Unpacker unpack = new Unpacker(totalLength);
                            unpack.DataRecived(data);
                            unpack.Unpack();
                            main.pbMain.Maximum = (int)totalLength;
                            main.pbMain.Value = (int)dataLength;
                            break;
                        case ChannelFlags.First:
                            unpacker = new Unpacker(totalLength);
                            unpacker.DataRecived(data);
                            main.pbMain.Maximum = (int)totalLength;
                            main.pbMain.Value = 0;
                            main.pbMain.Value += dataLength;
                            break;
                        case ChannelFlags.Middle:
                            if (unpacker != null)
                            {
                                unpacker.DataRecived(data);
                                main.pbMain.Value += dataLength;
                            }
                            break;
                        case ChannelFlags.Last:
                            if (unpacker != null)
                            {
                                unpacker.DataRecived(data);
                                unpacker.Unpack();
                                unpacker = null;
                                main.pbMain.Value += dataLength;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
