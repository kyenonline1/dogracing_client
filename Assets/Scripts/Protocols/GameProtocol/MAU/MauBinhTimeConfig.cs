using GameProtocol.Base;
using System.IO;

namespace GameProtocol.MAU
{
    public class MauBinhTimeConfig
    {
        public int TimePlay { get; set; }
        public int TimeCountDownToStart { get; set; }
        public int TimeOverForSubmit { get; set; }
        public int PhaseMaubinh { get; set; }
        public int PhaseCompare { get; set; }
        public int PhaseSumary { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.MauBinhTimeConfig; }
        //}
        public static byte[] Serialize(object data)
        {
            MauBinhTimeConfig ene = (MauBinhTimeConfig)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.TimePlay);
                    writer.Write(ene.TimeCountDownToStart);
                    writer.Write(ene.TimeOverForSubmit);
                    writer.Write(ene.PhaseMaubinh);
                    writer.Write(ene.PhaseCompare);
                    writer.Write(ene.PhaseSumary);
                }
                return m.ToArray();
            }
        }

        public static MauBinhTimeConfig Desserialize(byte[] data)
        {
            MauBinhTimeConfig result = new MauBinhTimeConfig();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.TimePlay = reader.ReadInt32();
                    result.TimeCountDownToStart = reader.ReadInt32();
                    result.TimeOverForSubmit = reader.ReadInt32();
                    result.PhaseMaubinh = reader.ReadInt32();
                    result.PhaseCompare = reader.ReadInt32();
                    result.PhaseSumary = reader.ReadInt32();
                }
            }
            return result;
        }
    }
}
