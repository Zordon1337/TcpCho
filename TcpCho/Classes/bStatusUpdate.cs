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
            if(s.CanRead)
            {
                try
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
                } catch (Exception e)
                {

                }
            }
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
