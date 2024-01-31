using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TcpCho
{
    public enum PlayModes
    {
        OsuStandard,
        Taiko,
        CatchTheBeat
    }
    public class bStatusUpdate
    {
        public bStatusUpdate(bStatus status, bool beatmapUpdate, string statusText, string songChecksum, int beatmapId, Mods mods, PlayModes playMode)
        {
            this.status = status;
            this.beatmapUpdate = beatmapUpdate;
            this.beatmapChecksum = songChecksum;
            this.statusText = statusText;
            this.currentMods = mods;
            this.playMode = playMode;
            this.beatmapId = beatmapId;
        }

        public bStatusUpdate(Stream s)
        {
            BinaryReader sr = new BinaryReader(s);
            this.status = (bStatus)sr.ReadByte();
            this.beatmapUpdate = sr.ReadBoolean();
            if (!this.beatmapUpdate)
            {
                return;
            }
            this.statusText = sr.ReadString();
            this.beatmapChecksum = sr.ReadString();
            this.currentMods = (Mods)sr.ReadUInt16();
            this.playMode = (PlayModes)sr.ReadByte();
            this.beatmapId = sr.ReadInt32();
        }

        public void ReadFromStream(BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public void WriteToStream(BinaryWriter sw)
        {
            sw.Write((byte)this.status);
            sw.Write(this.beatmapUpdate);
            if (!this.beatmapUpdate)
            {
                return;
            }
            sw.Write(this.statusText);
            sw.Write(this.beatmapChecksum);
            sw.Write((ushort)this.currentMods);
            sw.Write((byte)this.playMode);
            sw.Write(this.beatmapId);
        }

        public string beatmapChecksum;

        public int beatmapId;

        public bool beatmapUpdate;

        public Mods currentMods;

        public PlayModes playMode;

        public bStatus status;

        public string statusText;
    }
}
