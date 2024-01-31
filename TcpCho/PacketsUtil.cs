using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho
{
    class PacketsUtil
    {
        public void Write(NetworkStream ns, RequestType PacketID, bool compression, MemoryStream ms)
        {
            BinaryWriter bw = new BinaryWriter(ns);
            bw.Write((ushort)PacketID);
            bw.Write(compression);
            bw.Write((uint)ms.Length);
            bw.Write(ms.ToArray());
            bw.Flush();
        }
        public string ReadStringFromStream(NetworkStream ns, TcpClient client)
        {
            byte[] buffer = new byte[4092];
            while(client.Connected)
            {
                int data = ns.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, data);
            }
            return "notconnected"; // shouldn't happen!!!
        }
        public void WriteUserStats(NetworkStream ns, bUserStats stats, BinaryWriter bw)
        {
            MemoryStream ms = new MemoryStream();
            bw.BaseStream.Position = 0;
            bw.Write(stats.userId);
            bw.Write((byte)stats.completeness);
            stats.status.WriteToStream(bw);
            if (stats.completeness > Completeness.StatusOnly)
            {
                bw.Write(stats.rankedScore);
                bw.Write(stats.accuracy);
                bw.Write(stats.playcount);
                bw.Write(stats.totalScore);
                bw.Write((ushort)stats.rank);
            }
            if (stats.completeness == Completeness.Full)
            {
                bw.Write(stats.username);
                bw.Write(stats.avatarFilename);
                bw.Write((byte)(stats.timezone + 24));
                bw.Write(stats.location);
                bw.Write((byte)2);
            }
            bw.Flush();
            this.Write(ns, RequestType.Bancho_HandleOsuUpdate, false, ms);
        }
        public void SendVerMissmatch(BinaryWriter bw, NetworkStream ns)
        {
            MemoryStream ms = new MemoryStream();
            bw.BaseStream.Position = 0;
            bw.Write(-2);
            bw.Flush();
            this.Write(ns, RequestType.Bancho_LoginReply, false, ms);
        }
        public void SendBadPass(BinaryWriter bw, NetworkStream ns)
        {
            MemoryStream ms = new MemoryStream();
            bw.BaseStream.Position = 0;
            bw.Write(-1);
            bw.Flush();
            this.Write(ns, RequestType.Bancho_LoginReply, false, ms);
        }
    }
}
