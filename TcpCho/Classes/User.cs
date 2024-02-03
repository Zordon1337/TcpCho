using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho.Classes
{
    public class User
    {
        public User(string Username, string Password, bUserStats userstats, TcpClient client, NetworkStream stream, bMatch curmatch)
        {
            this.Username = Username;
            this.Password = Password;
            this.UserStats = userstats;
            this.Client = client;
            this.Stream = stream;
            this.curmatch = curmatch;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public bUserStats UserStats { get; set; }
        public TcpClient Client { get; set; }
        public NetworkStream Stream { get; set; }
        public bMatch curmatch { get; set; }
    }
}
