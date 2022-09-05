using GameProtocol.Base;
using System.IO;

namespace GameProtocol.MAU
{
    public class MauBinhPlayerCompare
    {
        public long UserId { get; set; }
        public int CardType { get; set; }
        public string CardName { get; set; }
        public long WinLoseCash { get; set; }
        public int[] CardsUnit { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.MauBinhPlayerCompare; }
        //}
        public static byte[] Serialize(object data)
        {
            MauBinhPlayerCompare ene = (MauBinhPlayerCompare)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.UserId);
                    writer.Write(ene.CardType);
                    writer.Write(ene.CardName);
                    writer.Write(ene.WinLoseCash);
                    if (ene.CardsUnit == null) ene.CardsUnit = new int[0];
                    writer.Write(ene.CardsUnit.Length);
                    foreach (var c in ene.CardsUnit)
                    {
                        writer.Write(c);
                    }
                    
                }
                return m.ToArray();
            }
        }

        public static MauBinhPlayerCompare Desserialize(byte[] data)
        {
            MauBinhPlayerCompare result = new MauBinhPlayerCompare();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    result.CardType = reader.ReadInt32();
                    result.CardName = reader.ReadString();
                    result.WinLoseCash = reader.ReadInt64();
                    int length = reader.ReadInt32();
                    result.CardsUnit = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.CardsUnit[i] = reader.ReadInt32();
                    }
                }
            }
            return result;
        }
    }
}
