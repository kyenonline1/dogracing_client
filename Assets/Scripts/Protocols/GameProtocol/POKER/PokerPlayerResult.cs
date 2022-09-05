using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.PKR
{
    public class PokerPlayerResult
    {
        public long UserId { get; set; }
        public long WinCash { get; set; }
        public int[] HighestCards { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.PokerPlayerResult; }
        //}
        public static byte[] Serialize(object data)
        {
            PokerPlayerResult obj = (PokerPlayerResult)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.UserId);
                    writer.Write(obj.WinCash);
                    writer.Write(obj.HighestCards.Length);
                    foreach (var c in obj.HighestCards)
                    {
                        writer.Write(c);
                    }
                }
                return m.ToArray();
            }
        }

        public static PokerPlayerResult Desserialize(byte[] data)
        {
            PokerPlayerResult result = new PokerPlayerResult();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    result.WinCash = reader.ReadInt64();
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
            log.Append("HighestCards: \"").Append(HighestCards).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
    
    public class PokerWinner
    {
        public int HighestRank { get; set; }
        public long[] CenterCash { get; set; }
        public PokerPlayerResult[] PlayerResults { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.PokerWinner; }
        //}
        public static byte[] Serialize(object data)
        {
            PokerWinner obj = (PokerWinner)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.HighestRank);
                    writer.Write(obj.CenterCash.Length);
                    foreach (var c in obj.CenterCash)
                    {
                        writer.Write(c);
                    }

                    writer.Write(obj.PlayerResults.Length);
                    foreach (var c in obj.PlayerResults)
                    {
                        var arr = PokerPlayerResult.Serialize(c);
                        writer.Write(arr.Length);
                        writer.Write(arr);
                    }
                }
                return m.ToArray();
            }
        }

        public static PokerWinner Desserialize(byte[] data)
        {
            PokerWinner result = new PokerWinner();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.HighestRank = reader.ReadInt32();
                    
                    int length = reader.ReadInt32();
                    result.CenterCash = new long[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.CenterCash[i] = reader.ReadInt64();
                    }

                    int plength = reader.ReadInt32();
                    result.PlayerResults = new PokerPlayerResult[plength];
                    for (int i = 0; i < plength; i++)
                    {
                        int _len = reader.ReadInt32();
                        result.PlayerResults[i] = PokerPlayerResult.Desserialize(reader.ReadBytes(_len));
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{HighestRank: \"").Append(HighestRank).Append("\", ");
            log.Append("CenterCash: \"").Append(CenterCash).Append("\", ");
            log.Append("PlayerResults: \"").Append(PlayerResults).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
