using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpCho.Classes;

namespace TcpCho
{
    class IrcMessage
    {
        public bool isprivate
        {
            get
            {
                return this.target.Length == 0 || this.target[0] != '#';
            }
        }

        public IrcMessage(string sender, string target, string message)
        {
            this.sender = sender;
            this.message = message;
            this.target = target;
        }

        public IrcMessage(Stream s)
        {
            Reader sr = new Reader(s);
            this.sender = sr.ReadString();
            this.message = sr.ReadString();
            this.target = sr.ReadString();
        }

        public void ReadFromStream(Reader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(Writer sw)
        {
            sw.Write(this.sender);
            sw.Write(this.message);
            sw.Write(this.target);
        }

        public string sender;

        public string message;

        public string target;
    }
    
    class PacketsUtil
    {
        public void Write(TcpClient client, RequestType PacketID, bool compression, MemoryStream ms)
        {
            try
            {
                if (client.Connected)
                {
                    BinaryWriter bw = new BinaryWriter(client.GetStream());
                    bw.Write((ushort)PacketID);
                    bw.Write(compression);
                    bw.Write((uint)ms.Length);
                    bw.Write(ms.ToArray());
                    bw.Flush();
                }
            } catch
            {

            }
        }
        public void WriteEmptyPacket(TcpClient client, RequestType PacketID)
        {
            if(client.Connected)
            {
                try
                {
                    BinaryWriter bw = new BinaryWriter(client.GetStream());
                    bw.Write((ushort)PacketID);
                    bw.Write(false);
                    bw.Write((uint)0);
                    bw.Flush();
                } catch
                {

                }
            }
        }
        public string ReadStringFromStream(TcpClient client)
        {
            byte[] buffer = new byte[4096];
            var stream = client.GetStream();

            if (stream.DataAvailable && stream.CanRead)
            {
                string receivedData = null;

                Task.Run(() =>
                {
                    int bytesReceived = stream.Read(buffer, 0, buffer.Length);
                    receivedData = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                });

                Thread.Sleep(500);

                return receivedData;
            }

            //Console.WriteLine($"{stream.DataAvailable} && {stream.CanRead}");
            return ReadStringFromStream(client); // :trolley:, but actually it shouldn't happen!!!
        }
        public string ReadBinaryStringFromStream(TcpClient client)
        {
            return new BinaryReader(client.GetStream()).ReadString();
        }
        public void Annouce(User user, string message)
        {
            MemoryStream ms = new MemoryStream();
            Writer writer = new Writer(ms);
            writer.Write(message);
            writer.Flush();
            Write(user.Client, RequestType.Bancho_Announce, false, ms);
        }
        public void GlobalAnnouce(List<User> users, string message)
        {
            foreach(var user in users)
            {
                if(user.Client.Connected)
                {
                    this.Annouce(user, message);
                }
            }
        }
        
        public void WriteChannelJoinSuccess(User user, string channel)
        {
            MemoryStream ms = new MemoryStream();
            Writer writer = new Writer(ms);
            writer.Write(channel);
            writer.Flush();
            Write(user.Client, RequestType.Bancho_ChannelJoinSuccess, false, ms);
        }
        public int GetPacketID(NetworkStream ns)
        {
            BinaryReader br = new BinaryReader(ns);
            Thread.Sleep(100); // avoiding StackOverFlow(not the website)
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
        public void SendPing(User user)
        {
            MemoryStream ms = new MemoryStream();
            Writer writer = new Writer(ms);
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
        
        public void WriteClientUpdate(TcpClient client, bStatusUpdate status, bUserStats stats)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                NetworkStream ns = client.GetStream();
                stats.completeness = Completeness.StatusOnly;
                sw.Write(stats.userId);
                sw.Write((byte)stats.completeness);
                this.WriteStatusUpdate(stats.status, sw);
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
            } catch
            {

            }
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
        

        /*public void SendVerMissmatch(TcpClient client, BinaryWriter bw)
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
        }*/
        
        public void SendLoginReply(int id, TcpClient client)
        {
            MemoryStream ms = new MemoryStream();
            Writer writer = new Writer(ms);

            writer.Write(id);
            writer.Flush();
            this.Write(client, RequestType.Bancho_LoginReply, false, ms);
        }
    }
}
