using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.PKR
{
    public class PokerPlayerHandCard
    {
        public long UserId { get; set; }
        public int[] HandCards { get; set; }
        public int Rank { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.PokerPlayerHandCard; }
        //}
        public static byte[] Serialize(object data)
        {
            PokerPlayerHandCard obj = (PokerPlayerHandCard)data;
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

        public static PokerPlayerHandCard Desserialize(byte[] data)
        {
            PokerPlayerHandCard result = new PokerPlayerHandCard();
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

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{UserId: \"").Append(UserId).Append("\", ");
            log.Append("HandCards: \"").Append(HandCards).Append("\", ");
            log.Append("Rank: \"").Append(Rank).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
