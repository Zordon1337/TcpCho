using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpCho.Classes;

namespace TcpCho
{
    // https://github.com/Beyley/osu-b497-server/blob/master/osu-server/BanchoSerializer.cs#L41
    public class Writer : BinaryWriter
    {
        public Writer(Stream s) : base(s) { }
        public override void Write(string value)
        {
            if (value.Length == 0)
            {
                this.Write(new byte[] { 0x00 });
                return;
            }

            this.Write((byte)11);
            base.Write(value);
        }
    }
    // https://github.com/Beyley/osu-b497-server/blob/master/osu-server/BanchoSerializer.cs#L6
    public class Reader : BinaryReader
    {
        public Reader(Stream stream) : base(stream) { }
        public override string ReadString()
        {
            byte type = this.ReadByte();
            return type == 11
                ? base.ReadString()
                : null;
        }
    }
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
        public static List<User> users = new List<User>();
        static void Main(string[] args)
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse("127.0.0.1"), 13381);
            tcp.Start();
            NetworkStream stream;
            new Thread(CheckIfConnectionAlive).Start();
            new Thread(PingThread).Start();
            new Thread(Webserver.Init).Start();
            Console.ForegroundColor = ConsoleColor.Green; 
            while (true)
            {
                
                TcpClient client = tcp.AcceptTcpClient();
                stream = client.GetStream();
                MemoryStream ms = new MemoryStream();
                Writer sw = new Writer(ms);
                BinaryReader sr = new BinaryReader(stream);
                string userdata = "";
                userdata = Packet.ReadStringFromStream(client);
                
                //Console.WriteLine($"Got Packet {(RequestType)Packet.GetPacketID(stream, client)}");
                //Console.Write(userdata);
                
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
                        Packet.WriteChannelJoinSuccess(user, "#osu");
                        Packet.Annouce(user, $"Welcome {user.Username} to TcpCho");
                        new Thread(() => PlayerThread(user)).Start();
                        Console.WriteLine($"User {username} just logged in");
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
                    int PacketID = Packet.GetPacketID(ns);
                    Reader sr = new Reader(ns);
                    //Console.WriteLine((RequestType)PacketID);
                    if( PacketID <= 76 )
                    {
                        string username = user.Username;
                        Console.WriteLine($"Received packet {(RequestType)PacketID} sent by {username}");
                        switch ((RequestType)PacketID)
                        {
                            /*case RequestType.Osu_Exit:
                                {
                                    Console.WriteLine($"User {username} logged out");
                                    user.Client.Close();
                                    users.Remove(user);
                                    
                                    break;
                                }*/ // this causes problems for some reason, gonna check it some day
                            case RequestType.Osu_RequestStatusUpdate:
                                {

                                    break;
                                }
                            case RequestType.Osu_SendUserStatus:
                                {
                                    bStatusUpdate stats = new bStatusUpdate(user.Stream);
                                    Console.WriteLine(stats.beatmapChecksum);
                                    Console.WriteLine(stats.currentMods);
                                    Console.WriteLine(stats.playMode);
                                    Console.WriteLine(stats.currentMods);
                                    Console.WriteLine(stats.status);
                                    Console.WriteLine(stats.statusText);
                                    break;
                                }
                            case RequestType.Osu_SendIrcMessage:
                                {
                                    IrcMessage msg = new IrcMessage();
                                    msg.ReadFromStream(user);

                                    Console.WriteLine(msg.author);
                                    Console.WriteLine(msg.message);
                                    Console.WriteLine(msg.receiver);
                                    break;
                                }
                            case RequestType.Osu_MatchCreate:
                                {
                                    MemoryStream ms = new MemoryStream();
                                    Writer writer = new Writer(ms);
                                    bMatch match = new bMatch(ns);
                                    Console.WriteLine(match.ToString());
                                    match.WriteToStream(writer);
                                    Packet.Write(user.Client, RequestType.Bancho_MatchNew, false, ms);
                                    break;
                                }
                            case RequestType.Osu_ErrorReport:
                                {
                                    Console.WriteLine($"osu reported bug: {sr.ReadString()}");
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    } else
                    {
                       // Console.WriteLine(Packet.ReadStringFromStream(user.Client));
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
        private static void PingThread()
        {
            while (true)
            {
                Thread.Sleep(5000);
                for (int i = users.Count - 1; i >= 0; i--)
                {
                    var client = users[i];
                    if (client.Client.Connected)
                    {
                        Packet.WriteEmptyPacket(client.Client, RequestType.Bancho_Ping);
                    }
                }
            }
        }

    }
}
