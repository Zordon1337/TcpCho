using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpCho.Classes;
using TcpCho.Services;

namespace TcpCho.Utils
{
    public class Matches
    {
        static PacketsUtil Packet = new PacketsUtil();
        public static bMatch FindById(int id)
        {
            foreach (bMatch m in Storage.matches)
            {
                if (m.matchId == id)
                    return m;

            }
            return null;
        }
        public static void SendMatchJoinFail(User user)
        {
            MemoryStream ms = new MemoryStream();
            Writer writer = new Writer(ms);
            writer.Write(0);
            writer.Flush();

            Packet.Write(user.Client, RequestType.Bancho_MatchJoinFail, false, ms);
        }
        public static void SendMatchJoinSucess(User user, bMatch match)
        {
            MemoryStream ms = new MemoryStream();
            Writer writer = new Writer(ms);
            match.WriteToStream(writer);
            Packet.Write(user.Client, RequestType.Bancho_MatchJoinSuccess, false, ms);
        }
    }
    
}
