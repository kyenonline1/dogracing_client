using GameProtocol.Base;
using System.IO;

namespace GameProtocol.EVN
{
    public class FirstCharging
    {
        public int Id { get; set; }
        public string Cate { get; set; }
        public float Rate { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }// 0 : Tham gia, 1: Nhận thưởng, 2 : Đã nhận
        
        public int BonusCash { get; set; }
        public string CreateTime { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.FirstCharging; }
        }
        public static byte[] Serialize(object data)
        {
            FirstCharging charging = (FirstCharging)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(charging.Id);
                    writer.Write(charging.Cate);
                    writer.Write(charging.Rate);
                    writer.Write(charging.Amount);
                    writer.Write(charging.Description);
                    writer.Write(charging.Status);
                    writer.Write(charging.BonusCash);
                    writer.Write(charging.CreateTime);
                }
                return m.ToArray();
            }
        }

        public static FirstCharging Desserialize(byte[] data)
        {
            FirstCharging result = new FirstCharging();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Id = reader.ReadInt32();
                    result.Cate = reader.ReadString();
                    result.Rate = reader.ReadSingle();
                    result.Amount = reader.ReadInt32();
                    result.Description = reader.ReadString();
                    result.Status = reader.ReadByte();
                    result.BonusCash = reader.ReadInt32();
                    result.CreateTime = reader.ReadString();
                }
            }
            return result;
        }
        
    }
}
