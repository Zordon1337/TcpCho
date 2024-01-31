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
        public void WriteUserStats(NetworkStream ns, bUserStats stats)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            sw.Write(stats.userId);
            sw.Write((byte)stats.completeness);
            this.WriteStatusUpdate(ns, stats.status, sw);
            if (stats.completeness > Completeness.StatusOnly)
            {
                sw.Write(stats.rankedScore);
                sw.Write(stats.accuracy);
                sw.Write(stats.playcount);
                sw.Write(stats.totalScore);
                sw.Write((ushort)stats.rank);
            }
            if (stats.completeness == Completeness.Full)
            {
                sw.Write(stats.username);
                sw.Write(stats.avatarFilename);
                sw.Write((byte)(stats.timezone + 24));
                sw.Write(stats.location);
                sw.Write((byte)2);
            }
            sw.Flush();
            this.Write(ns, RequestType.Bancho_HandleOsuUpdate, false, ms);
        }
        public void WriteStatusUpdate(NetworkStream ns, bStatusUpdate status, BinaryWriter bw)
        {
            bw.Write((byte)status.status);
            bw.Write(status.beatmapUpdate);
            if (!status.beatmapUpdate)
            {
                return;
            }
            bw.Write(status.statusText);
            bw.Write(status.beatmapChecksum);
            bw.Write((ushort)status.currentMods);
            bw.Write((byte)status.playMode);
            bw.Write(status.beatmapId);
            bw.Flush();
        }
        public void SendVerMissmatch(BinaryWriter bw, NetworkStream ns)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw2 = new BinaryWriter(ms);
            bw2.Write(-2);
            bw2.Flush();
            this.Write(ns, RequestType.Bancho_LoginReply, false, ms);
        }
        public void SendBadPass(BinaryWriter bw, NetworkStream ns)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw2 = new BinaryWriter(ms);
            bw2.Write(-1);
            bw2.Flush();
            this.Write(ns, RequestType.Bancho_LoginReply, false, ms);
        }
    }
}
