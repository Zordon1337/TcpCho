using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpCho
{
    public class User
    {
        public User(string Username, string Password, bUserStats userstats, TcpClient client, NetworkStream stream)
        { 
            this.Username = Username;
            this.Password = Password;
            this.UserStats = userstats;
            this.Client = client;
            this.Stream = stream;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public bUserStats UserStats { get; set; }
        public TcpClient Client { get; set;}
        public NetworkStream Stream { get; set;}
    }
    
    internal class Bancho
    {
        private static PacketsUtil Packet = new PacketsUtil();
        private static Parsing Parsing = new Parsing();
        private static List<User> users = new List<User>();
        static void Main(string[] args)
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse("127.0.0.1"), 13381);
            tcp.Start();
            NetworkStream stream;
            new Thread(CheckIfConnectionAlive).Start();
            while (true)
            {
                
                TcpClient client = tcp.AcceptTcpClient();
                stream = client.GetStream();
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                string userdata = "";
                userdata = Packet.ReadStringFromStream(client);
                Thread.Sleep(500);
                //Console.WriteLine($"Got Packet {(RequestType)Packet.GetPacketID(stream, client)}");
                Console.Write(userdata);
                if(Parsing.IsLoginPacket(userdata)) 
                {
                    string username = Parsing.ParseUsername(userdata);
                    string password = Parsing.ParsePassword(userdata); 
                    string version = Parsing.ParseVersion(userdata);
                    int userid = new Random().Next(1, 2500000);
                    if(version != "b504")
                    {
                        Packet.SendVerMissmatch(client, sw);
                        client.Close();
                        continue;
                    } else
                    {
                        sw.Write(userid);
                        sw.Flush();
                        Packet.Write(client, RequestType.Bancho_LoginReply, false, ms);
                        bStatusUpdate s = new bStatusUpdate(
                            bStatus.Idle, // Player status
                            false, // ???
                            "Idle", // Player status in string
                            "no", // Beatmap checksum
                            1, // Id of beatmap currently played by user
                            Mods.None, // Current Mods
                            PlayModes.OsuStandard // Current Mode played by player
                            );
                        bUserStats stats = new bUserStats(
                            userid, // user id 
                            "ZRD", // Player username(i don't think its used by client)
                            0, // Ranked Score(only ranked)
                            1f, // player accuracy(0.01f is 1% etc.)
                            0, // play count
                            0, // Total Score(not ranked score and ranked score combined)
                            1, // player rank(for ex. #1)
                            "1.png", // player pfp filename
                            s, // player status update
                            24, // player timezone
                            "PL", // player location
                            Permissions.BAT // name says itself
                            );

                        User user = new User(username, password, stats, client,stream);

                        stats.completeness = Completeness.Statistics;

                        Packet.WriteUserStats(client, stats);
                        users.Add(user);
                        new Thread(() => PlayerThread(user)).Start();
                    }
                    
                } else
                {
                    Console.WriteLine("Got connection but its not an login packet");
                    
                }
                

                
            }
        }
        static void PlayerThread(User user)
        {
            while(user.Client.Connected)
            {
                if(user.Stream.DataAvailable)
                {
                    NetworkStream ns = user.Stream;
                    if(!ns.CanRead)
                    {
                        Thread.Sleep(100);
                    }
                    RequestType PacketID = (RequestType)Packet.GetPacketID(ns);
                    Console.WriteLine(PacketID);
                    switch(PacketID)
                    {
                        case RequestType.Osu_SendUserStatus:
                            {
                                break;
                            }
                        
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }
        private static void CheckIfConnectionAlive()
        {
            while (true)
            {
                Thread.Sleep(500); // we don't want our CPU to explode

                for (int i = users.Count - 1; i >= 0; i--)
                {
                    var client = users[i];
                    if (!client.Client.Connected)
                    {
                        users.RemoveAt(i);
                    }
                }
            }
        }

    }
}
