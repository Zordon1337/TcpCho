using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpCho
{
    class PacketsUtil
    {
        public void Write(TcpClient client, RequestType PacketID, bool compression, MemoryStream ms)
        {
            BinaryWriter bw = new BinaryWriter(client.GetStream());
            bw.Write((ushort)PacketID);
            bw.Write(compression);
            bw.Write((uint)ms.Length);
            bw.Write(ms.ToArray());
            bw.Flush();
        }
        public string ReadStringFromStream(TcpClient client)
        {
            byte[] buffer = new byte[4096];
            var stream = client.GetStream();
            if (stream.DataAvailable && stream.CanRead)
            {
                int bytesreceived = client.GetStream().Read(buffer, 0, buffer.Length);
                
                return Encoding.UTF8.GetString(buffer,0, bytesreceived);
            }
            Console.WriteLine($"{stream.DataAvailable} && {stream.CanRead}");
            return ReadStringFromStream(client); // :trolley:, but actually it shouldn't happen!!!
            
        }
        public int GetPacketID(NetworkStream ns)
        {
            BinaryReader br = new BinaryReader(ns);

            try
            {
                return br.ReadInt16();
            }
            catch (Exception ex)
            {


                //throw(new Exception("why it doesn't work!1111!!!!"));
                return GetPacketID(ns); // there is 100% something wrong with me and especially my brain(that doesn't exist)
            }
        }

        public void WriteUserStats(TcpClient client, bUserStats stats)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            NetworkStream ns = client.GetStream();
            sw.Write(stats.userId);
            sw.Write((byte)stats.completeness);
            this.WriteStatusUpdate(stats.status,sw);
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
            this.Write(client, RequestType.Bancho_HandleOsuUpdate, false, ms);
        }
        public void WriteStatusUpdate(bStatusUpdate status, BinaryWriter bw)
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
        public void SendVerMissmatch(TcpClient client, BinaryWriter bw)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw2 = new BinaryWriter(ms);
            NetworkStream ns = client.GetStream();
            bw2.Write(-2);
            bw2.Flush();
            this.Write(client, RequestType.Bancho_LoginReply, false, ms);
        }
        public void SendBadPass(TcpClient client, BinaryWriter bw)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw2 = new BinaryWriter(ms);
            
            bw2.Write(-1);
            bw2.Flush();
            this.Write(client, RequestType.Bancho_LoginReply, false, ms);
        }
    }
}
