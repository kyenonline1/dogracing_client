using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.EVN
{
    public class DailyMission
    {
        public int MissionId { get; set; }
        public string GameId { get; set; }
        public int Amount { get; set; }
        public int BonusCoin { get; set; }
        public int Process { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }// 0 : Tham gia, 1: Nhận thưởng, 2 : Đã nhận
        public int Blind { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.DailyMission; }
        }
        public static byte[] Serialize(object data)
        {
            DailyMission admob = (DailyMission)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(admob.MissionId);
                    writer.Write(admob.GameId);
                    writer.Write(admob.Amount);
                    writer.Write(admob.BonusCoin);
                    writer.Write(admob.Process);
                    writer.Write(admob.Description);
                    writer.Write(admob.Status);
                    writer.Write(admob.Blind);
                }
                return m.ToArray();
            }
        }

        public static DailyMission Desserialize(byte[] data)
        {
            DailyMission result = new DailyMission();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.MissionId = reader.ReadInt32();
                    result.GameId = reader.ReadString();
                    result.Amount = reader.ReadInt32();
                    result.BonusCoin = reader.ReadInt32();
                    result.Process = reader.ReadInt32();
                    result.Description = reader.ReadString();
                    result.Status = reader.ReadByte();
                    result.Blind = reader.ReadInt32();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{MissionId: \"").Append(MissionId).Append("\", ");
            log.Append("GameId: \"").Append(GameId).Append("\", ");
            log.Append("Amount: \"").Append(Amount).Append("\", ");
            log.Append("BonusCoin: \"").Append(BonusCoin).Append("\", ");
            log.Append("Process: \"").Append(Process).Append("\", ");
            log.Append("Description: \"").Append(Description).Append("\", ");
            log.Append("Status: \"").Append(Status).Append("\", ");
            log.Append("Blind: \"").Append(Blind).Append("\"}").Append("\"}");

            return log.ToString();
        }

    }
}
