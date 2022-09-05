using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.EVN
{
    public class Mission
    {
        public int MissionId { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public int Amount { get; set; }
        public int BonusCoin { get; set; }
        public int Process { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }// 0 : Tham gia, 1: Nhận thưởng, 2 : Đã nhận

        public byte MissionType { get; set; } //0: nạp thẻ, 1: Poker thường, 2: SpinUp, 3: MTT

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.Mission; }
        }

        public static byte[] Serialize(object data)
        {
            Mission admob = (Mission)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(admob.MissionId);
                    writer.Write(admob.Name);
                    writer.Write(admob.Action);
                    writer.Write(admob.Amount);
                    writer.Write(admob.BonusCoin);
                    writer.Write(admob.Process);
                    writer.Write(admob.Description);
                    writer.Write(admob.Status);
                    writer.Write(admob.MissionType);
                }
                return m.ToArray();
            }
        }

        public static Mission Desserialize(byte[] data)
        {
            Mission result = new Mission();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.MissionId = reader.ReadInt32();
                    result.Name = reader.ReadString();
                    result.Action = reader.ReadString();
                    result.Amount = reader.ReadInt32();
                    result.BonusCoin = reader.ReadInt32();
                    result.Process = reader.ReadInt32();
                    result.Description = reader.ReadString();
                    result.Status = reader.ReadByte();
                    result.MissionType = reader.ReadByte();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{MissionId: \"").Append(MissionId).Append("\", ");
            log.Append("Name: \"").Append(Name).Append("\", ");
            log.Append("Action: \"").Append(Action).Append("\", ");
            log.Append("Amount: \"").Append(Amount).Append("\", ");
            log.Append("BonusCoin: \"").Append(BonusCoin).Append("\", ");
            log.Append("Process: \"").Append(Process).Append("\", ");
            log.Append("Description: \"").Append(Description).Append("\", ");
            log.Append("Status: \"").Append(Status).Append("\", ");
            log.Append("MissionType: \"").Append(MissionType).Append("\"}").Append("\"}");

            return log.ToString();
        }

    }
}
