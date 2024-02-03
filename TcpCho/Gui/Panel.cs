using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpCho.Services;

namespace TcpCho.Gui
{
    public partial class Panel : Form
    {
        PacketsUtil packet = new PacketsUtil();
        public Panel()
        {
            test();
            InitializeComponent();
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Panel_Load(object sender, EventArgs e)
        {
            
        }
        async Task test()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    string ids = "Connected Users ID: ";
                    foreach (var user in Storage.users)
                    {
                        ids += $"{user.UserStats.userId}({user.UserStats.username}), ";
                    }


                    CUI.Invoke((MethodInvoker)delegate
                    {
                        CUI.Text = ids;
                    });

                    CU.Invoke((MethodInvoker)delegate
                    {
                        CU.Text = $"Connected Users: {Storage.users.Count}";
                    });
                }
            });
        }


        private void CU_Click(object sender, EventArgs e)
        {

        }

        private void SEP2_Click(object sender, EventArgs e)
        {
            RequestType Packet = (RequestType)int.Parse(PacketID3.Text);
            int userid = int.Parse(Target2.Text);
            foreach(var user in Storage.users)
            {
                if(user.UserStats.userId == userid)
                {
                    packet.WriteEmptyPacket(user.Client, Packet);
                }
            }
        }

        private void SSP2_Click(object sender, EventArgs e)
        {
            RequestType Packet = (RequestType)int.Parse(PacketID2.Text);
            int userid = int.Parse(Target1.Text);
            string data = STD2.Text;
            foreach (var user in Storage.users)
            {
                if (user.UserStats.userId == userid)
                {
                    MemoryStream ms = new MemoryStream();
                    Writer w = new Writer(ms);
                    w.Write(data);
                    w.Flush();
                    packet.Write(user.Client, Packet, false, ms);
                }
            }
        }

        private void SEP1_Click(object sender, EventArgs e)
        {
            RequestType Packet = (RequestType)int.Parse(textBox3.Text);
            foreach (var user in Storage.users)
            {
                packet.WriteEmptyPacket(user.Client, Packet);
            }
        }

        private void SSP1_Click(object sender, EventArgs e)
        {
            RequestType Packet = (RequestType)int.Parse(PacketID2.Text);
            string data = STD1.Text;
            foreach (var user in Storage.users)
            {
                MemoryStream ms = new MemoryStream();
                Writer w = new Writer(ms);
                w.Write(data);
                w.Flush();
                packet.Write(user.Client, Packet, false, ms);
            }
        }
    }
}
