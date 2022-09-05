using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.DOG
{
    public class DogRacingHistoryByUser
    {
        public long GameSession { get; set; }
        public byte[] Result { get; set; }
        public long WinCash { get; set; }
        public string CashBets { get; set; }
        public string CreateTime { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.DogRacingHistoryByUser; }
        }
        public static byte[] Serialize(object data)
        {
            DogRacingHistoryByUser ene = (DogRacingHistoryByUser)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.GameSession);
                    int len = ene.Result.Length;
                    writer.Write(len);
                    foreach (var p in ene.Result)
                    {
                        writer.Write(p);
                    }

                    writer.Write(ene.WinCash);
                    writer.Write(ene.CashBets);
                    writer.Write(ene.CreateTime);
                }
                return m.ToArray();
            }
        }
        public static DogRacingHistoryByUser Desserialize(byte[] data)
        {
            DogRacingHistoryByUser result = new DogRacingHistoryByUser();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.GameSession = reader.ReadInt64();
                    int len = reader.ReadInt32();
                    result.Result = new byte[len];
                    for (int i = 0; i < len; i++)
                    {
                        result.Result[i] = reader.ReadByte();
                    }
                    result.WinCash = reader.ReadInt64();
                    result.CashBets = reader.ReadString();
                    result.CreateTime = reader.ReadString();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("GameSession: \"").Append(GameSession).Append("\", ");
            log.Append("Result: \"").Append(Result).Append("\", ");
            log.Append("WinCash: \"").Append(WinCash).Append("\", ");
            log.Append("CashBets: \"").Append(CashBets).Append("\", ");
            log.Append("CreateTime: \"").Append(CreateTime).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
