using GameProtocol.Base;
using System;
using System.IO;

namespace GameProtocol.XIT
{
    public class XitoPlayerCard
    {
        public long UserId { get; set; }
        public int[] HandCards { get; set; }
        public int Rank { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.XitoPlayerCard; }
        //}
        public static byte[] Serialize(object data)
        {
            XitoPlayerCard obj = (XitoPlayerCard)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.UserId);
                    writer.Write(obj.HandCards.Length);
                    foreach (var c in obj.HandCards)
                    {
                        writer.Write(c);
                    }
                    writer.Write(obj.Rank);
                }
                return m.ToArray();
            }
        }

        public static XitoPlayerCard Desserialize(byte[] data)
        {
            XitoPlayerCard result = new XitoPlayerCard();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    int length = reader.ReadInt32();
                    result.HandCards = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.HandCards[i] = reader.ReadInt32();
                    }
                    result.Rank = reader.ReadInt32();
                }
            }
            return result;
        }
    }
}
