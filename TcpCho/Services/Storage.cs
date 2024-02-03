using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TcpCho.Classes;

namespace TcpCho.Services
{
    /*
     * Temporary storage for:
     * - Multiplayer rooms
     * - Users
     * - Clients Lobby
     */
    public class Storage
    {
        public static List<User> users = new List<User>();
        public static List<bMatch> matches = new List<bMatch>();
        public static List<User> Lobby = new List<User>();
    }
}
