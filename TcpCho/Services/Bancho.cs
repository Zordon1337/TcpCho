using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpCho.Classes;

namespace TcpCho.Services
{
    internal class Bancho
    {
        private static PacketsUtil Packet = new PacketsUtil();
        private static Parsing Parsing = new Parsing();
        public static List<User> users = new List<User>();
        const int PORT = 13381;
        const string IP = "127.0.0.1";
        public static void Init(string[] args)
        {

            TcpListener tcp = new TcpListener(IPAddress.Parse(IP), PORT);
            tcp.Start();
            NetworkStream stream;
            new Thread(CheckIfConnectionAlive).Start();
            new Thread(PingThread).Start();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TcpCho started listening at port " + PORT);

#if DEBUG
            Thread guiThread = new Thread(() =>
            {
                // Set the apartment state to STA
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Gui.Panel());
            });
            guiThread.SetApartmentState(ApartmentState.STA); // Set the apartment state to STA
            guiThread.Start(); // Start the thread
#else
            if (args.Contains("--GUI"))
            {
                Thread guiThread = new Thread(() =>
                {
                    // Set the apartment state to STA
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Gui.Panel());
                });

                guiThread.SetApartmentState(ApartmentState.STA); // Set the apartment state to STA
                guiThread.Start(); // Start the thread
            }
#endif
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

                if (Parsing.IsLoginPacket(userdata))
                {

                    Handlers.HandleLogin.Handle(userdata, client);
                }
                else
                {
                    Console.WriteLine("Got connection but its not an login packet");

                }



            }
        }
        public static void PlayerThread(User user)
        {
            while (user.Client.Connected)
            {
                if (user.Stream.DataAvailable)
                {
                    NetworkStream ns = user.Stream;
                    if (!ns.CanRead)
                    {
                        Thread.Sleep(100);
                    }
                    int PacketID = Packet.GetPacketID(ns);
                    Reader sr = new Reader(ns);
                    //Console.WriteLine((RequestType)PacketID);
                    if (PacketID <= 76)
                    {
                        string username = user.Username;
                        if (PacketID < 0)
                        {
                            Console.WriteLine(sr.Read());
                        }
                        else
                        {
                            Console.WriteLine($"Received packet {(RequestType)PacketID} sent by {username}");
                            Handlers.HandlePacket.Handle(user, (RequestType)PacketID, sr);
                        }

                    }
                    else
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
