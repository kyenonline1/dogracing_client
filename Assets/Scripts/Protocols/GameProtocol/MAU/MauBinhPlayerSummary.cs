using GameProtocol.Base;
using System.IO;

namespace GameProtocol.MAU
{
    public class MauBinhPlayerSummary
    {
        public long UserId { get; set; }
        public long WinLoseCash { get; set; }
        public long Cash { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.MauBinhPlayerSummary; }
        //}
        public static byte[] Serialize(object data)
        {
            MauBinhPlayerSummary ene = (MauBinhPlayerSummary)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.UserId);
                    writer.Write(ene.WinLoseCash);
                    writer.Write(ene.Cash);
                }
                return m.ToArray();
            }
        }

        public static MauBinhPlayerSummary Desserialize(byte[] data)
        {
            MauBinhPlayerSummary result = new MauBinhPlayerSummary();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    result.WinLoseCash = reader.ReadInt64();
                    result.Cash = reader.ReadInt64();
                }
            }
            return result;
        }
    }
}
