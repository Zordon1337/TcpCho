using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpCho.Classes;
using TcpCho.Services;

namespace TcpCho.Handlers
{
    public class HandlePacket
    {
        static PacketsUtil Packet = new PacketsUtil();
        public static void Handle(User user, RequestType packet, Reader sr)
        {
            switch (packet)
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
                        foreach (var client in Bancho.users)
                        {
                            MemoryStream ms = new MemoryStream();
                            Writer w = new Writer(ms);
                            stats.WriteToStream(w);
                            Packet.Write(client.Client, RequestType.Bancho_HandleOsuUpdate, false, ms);
                        }
                        break;
                    }
                case RequestType.Osu_SendIrcMessage:
                    {
                        /*IrcMessage msg = new IrcMessage(user.Stream);


                        Console.WriteLine(msg.sender);
                        Console.WriteLine(msg.message);
                        Console.WriteLine(msg.target);*/
                        break;
                    }
                case RequestType.Osu_MatchCreate:
                    {
                        MemoryStream ms = new MemoryStream();
                        Writer writer = new Writer(ms);
                        bMatch match = new bMatch(user.Stream);
                        Console.WriteLine(match.gameName);
                        Console.WriteLine(match.hostId);
                        Console.WriteLine(match.beatmapName);
                        //match.WriteToStream(writer);
                        Packet.Write(user.Client, RequestType.Bancho_MatchJoinSuccess, false, ms);
                        break;
                    }
                case RequestType.Osu_LobbyPart:
                    {
                        MemoryStream ms = new MemoryStream();
                        Writer writer = new Writer(ms);
                        writer.Write(user.UserStats.userId);
                        writer.Flush();
                        Packet.Write(user.Client, RequestType.Bancho_LobbyPart, false, ms);
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
        }
    }
}
