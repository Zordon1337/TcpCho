using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpCho.Classes;
using TcpCho.Services;

namespace TcpCho.Handlers
{
    public class HandleLogin
    {
        static Parsing parsing = new Parsing();
        static PacketsUtil Packet = new PacketsUtil();
        public static int Handle(string userdata, TcpClient client)
        {
            
            string username = parsing.ParseUsername(userdata);
            string password = parsing.ParsePassword(userdata);
            string version = parsing.ParseVersion(userdata);
            int userid = new Random().Next(1, 2500000);
            if (version != "b504")
            {
                
                client.Close();
                return LoginErrors.ERR_VER_MISSMATCH;
            }
            else
            {
                Packet.SendLoginReply(userid, client);
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
                User user = new User(username, password, stats, client, client.GetStream());
                stats.completeness = Completeness.Statistics;
                Packet.WriteUserStats(client, stats);
                Bancho.users.Add(user);
                Packet.WriteChannelJoinSuccess(user, "#osu");
                Packet.WriteChannelJoinSuccess(user, "#global");
                Packet.WriteChannelJoinSuccess(user, $"#{user.UserStats.username}-r"); // not sure if required
                Packet.Annouce(user, $"Welcome {user.Username} to TcpCho");
                new Thread(() => Bancho.PlayerThread(user)).Start();
                Console.WriteLine($"User {user.UserStats.username} just logged in");
                return userid;
            }
        }
    }
}
