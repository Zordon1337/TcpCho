using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpCho.Classes;
using TcpCho.Services;

namespace TcpCho
{
    // https://github.com/Beyley/osu-b497-server/blob/master/osu-server/BanchoSerializer.cs#L41
    public class Writer : BinaryWriter
    {
        public Writer(Stream s) : base(s) { }
        public override void Write(string value)
        {
            if(value != null)
            {
                if (value.Length == 0)
                {
                    this.Write(new byte[] { 0x00 });
                    return;
                }
            }
            

            try
            {
                this.Write((byte)11);
                base.Write(value);
            }
            catch
            {

            }
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
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Bancho.Init(args);
        }
    }
}
