using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.PKR
{
    public class PokerPlayerWinCash
    {
        public long UserId { get; set; }
        public long WinCash { get; set; }
        public long Cash { get; set; }
        public int[] HighestCards { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.PokerPlayerWinCash; }
        //}
        public static byte[] Serialize(object data)
        {
            PokerPlayerWinCash obj = (PokerPlayerWinCash)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.UserId);
                    writer.Write(obj.WinCash);
                    writer.Write(obj.Cash);
                    writer.Write(obj.HighestCards.Length);
                    foreach (var c in obj.HighestCards)
                    {
                        writer.Write(c);
                    }
                }
                return m.ToArray();
            }
        }

        public static PokerPlayerWinCash Desserialize(byte[] data)
        {
            PokerPlayerWinCash result = new PokerPlayerWinCash();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    result.WinCash = reader.ReadInt64();
                    result.Cash = reader.ReadInt64();
                    int length = reader.ReadInt32();
                    result.HighestCards = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.HighestCards[i] = reader.ReadInt32();
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{UserId: \"").Append(UserId).Append("\", ");
            log.Append("WinCash: \"").Append(WinCash).Append("\", ");
            log.Append("Cash: \"").Append(Cash).Append("\", ");
            log.Append("HighestCards: \"").Append(HighestCards).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
