using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho
{
    public class bUserStats
    {
        public bUserStats(int userId, string username, long rankedScore, float accuracy, int playcount, long totalScore, int rank, string avatarFilename, bStatusUpdate status, int timezone, string location, Permissions permission)
        {
            this.userId = userId;
            this.username = username;
            this.rankedScore = rankedScore;
            this.accuracy = accuracy;
            this.playcount = playcount;
            this.totalScore = totalScore;
            this.rank = rank;
            this.avatarFilename = avatarFilename;
            this.status = status;
            this.timezone = timezone;
            this.location = location;
            this.permission = permission;
        }

        public bUserStats(Stream s) : this(s, false)
        {
            
        }
        

        public bUserStats(Stream s, bool forceFull)
        {
            BinaryReader sr = new BinaryReader(s);
            this.userId = sr.ReadInt32();
            Completeness comp = (Completeness)sr.ReadByte();
            this.completeness = (forceFull ? Completeness.Full : comp);
            this.status = new bStatusUpdate(s);
            if (this.completeness > Completeness.StatusOnly)
            {
                this.rankedScore = sr.ReadInt64();
                this.accuracy = sr.ReadSingle();
                this.playcount = sr.ReadInt32();
                this.totalScore = sr.ReadInt64();
                this.rank = (int)sr.ReadUInt16();
            }
            if (this.completeness != Completeness.Full)
            {
                return;
            }
            this.username = sr.ReadString();
            this.avatarFilename = sr.ReadString();
            this.timezone = (int)(sr.ReadByte() - 24);
            this.location = sr.ReadString();
            this.permission = (Permissions)sr.ReadByte();
        }


        public float accuracy;

        public string avatarFilename;

        public Completeness completeness;

        public int level;

        public string location;

        private readonly Permissions permission;

        public int playcount;

        public int rank;

        public long rankedScore;

        public bStatusUpdate status;

        public int timezone;

        public long totalScore;

        public int userId;

        public string username;
    }
}
