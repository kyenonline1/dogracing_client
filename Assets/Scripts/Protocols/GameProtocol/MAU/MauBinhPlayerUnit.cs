using GameProtocol.Base;
using System.IO;

namespace GameProtocol.MAU
{
    public class MauBinhPlayerUnit
    {
        public long UserId { get; set; }
        public int[] CardType { get; set; }
        public long[] WinLoseCash { get; set; }
        //public int[] CardsUnit { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.MauBinhPlayerUnit; }
        //}
        public static byte[] Serialize(object data)
        {
            MauBinhPlayerUnit ene = (MauBinhPlayerUnit)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.UserId);
                    writer.Write(ene.CardType.Length);
                    foreach (var c in ene.CardType)
                    {
                        writer.Write(c);
                    }

                    writer.Write(ene.WinLoseCash.Length);
                    foreach (var c in ene.WinLoseCash)
                    {
                        writer.Write(c);
                    }

                    //writer.Write(ene.CardsUnit.Length);
                    //foreach (var c in ene.CardsUnit)
                    //{
                    //    writer.Write(c);
                    //}
                }
                return m.ToArray();
            }
        }

        public static MauBinhPlayerUnit Desserialize(byte[] data)
        {
            MauBinhPlayerUnit result = new MauBinhPlayerUnit();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    int length = reader.ReadInt32();
                    result.CardType = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.CardType[i] = reader.ReadInt32();
                    }

                    length = reader.ReadInt32();
                    result.WinLoseCash = new long[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.WinLoseCash[i] = reader.ReadInt64();
                    }

                    //length = reader.ReadInt32();
                    //result.CardsUnit = new int[length];
                    //for (int i = 0; i < length; i++)
                    //{
                    //    result.CardsUnit[i] = reader.ReadInt32();
                    //}
                }
            }
            return result;
        }
    }
}
