using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho
{
    internal class Bancho
    {
        private static PacketsUtil Packet = new PacketsUtil();
        private static Parsing Parsing = new Parsing();
        static void Main(string[] args)
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse("127.0.0.1"), 13381);
            tcp.Start();
            NetworkStream stream;
            while (true)
            {
                TcpClient client = tcp.AcceptTcpClient();
                
                stream = client.GetStream();
                MemoryStream ms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(ms);
                BinaryReader sr = new BinaryReader(stream);
                string userdata = Packet.ReadStringFromStream(stream,client);
                if(Parsing.IsLoginPacket(userdata))
                {
                    string username = Parsing.ParseUsername(userdata);
                    string password = Parsing.ParsePassword(userdata); 
                    string version = Parsing.ParseVersion(userdata);
                    if(version != "b504")
                    {
                        Packet.SendVerMissmatch(bw,stream);
                    }
                    bw.Write(1);
                    bw.Flush();
                    Packet.Write(stream, RequestType.Bancho_LoginReply, false, ms);
                    bw.BaseStream.Position = 0;
                    bStatusUpdate s = new bStatusUpdate(bStatus.Idle, false, "Idle", "no", 1, Mods.None, PlayModes.OsuStandard);
                    bUserStats stats = new bUserStats(1, "ZRD", 1337, 1f, 50, 1337, 1337, "1.png", s, 24, "PL", Permissions.Normal);
                    stats.completeness = Completeness.Statistics;
                    Packet.WriteUserStats(stream, stats, bw);
                    bw.Flush();
                } else
                {
                    Console.WriteLine("Got connection but its not an login packet");
                    // parse other packets, for now we don't care
                }
                

                
            }
        }
    }
}
