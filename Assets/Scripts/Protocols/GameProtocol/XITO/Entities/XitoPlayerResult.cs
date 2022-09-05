using GameProtocol.Base;
using System.IO;

namespace GameProtocol.XIT
{
    public class XitoPlayerResult
    {
        public long UserId { get; set; }
        public long WinCash { get; set; }
        public int[] HighestCards { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.XitoPlayerResult; }
        //}
        public static byte[] Serialize(object data)
        {
            XitoPlayerResult obj = (XitoPlayerResult)data;
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

        public static XitoPlayerResult Desserialize(byte[] data)
        {
            XitoPlayerResult result = new XitoPlayerResult();
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
    }
    
    public class XitoWinner
    {
        public int HighestRank { get; set; }
        public long CenterCash { get; set; }
        public XitoPlayerResult[] PlayerResults { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.XitoWinner; }
        //}
        public static byte[] Serialize(object data)
        {
            XitoWinner obj = (XitoWinner)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.HighestRank);
                    writer.Write(obj.CenterCash);
                    writer.Write(obj.PlayerResults.Length);
                    foreach (var c in obj.PlayerResults)
                    {
                        var arr = XitoPlayerResult.Serialize(c);
                        writer.Write(arr.Length);
                        writer.Write(arr);
                    }
                }
                return m.ToArray();
            }
        }

        public static XitoWinner Desserialize(byte[] data)
        {
            XitoWinner result = new XitoWinner();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.HighestRank = reader.ReadInt32();
                    
                    result.CenterCash = reader.ReadInt64();
                    int plength = reader.ReadInt32();
                    result.PlayerResults = new XitoPlayerResult[plength];
                    for (int i = 0; i < plength; i++)
                    {
                        int _len = reader.ReadInt32();
                        result.PlayerResults[i] = XitoPlayerResult.Desserialize(reader.ReadBytes(_len));
                    }
                }
            }
            return result;
        }
    }
}
