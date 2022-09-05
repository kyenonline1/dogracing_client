using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.PAY
{
    public class ChargingHistory
    {
        public int Amount { get; set; }
        public float Price { get; set; }
        public string OrderId { get; set; }
        public string PackageId { get; set; }
        public string Telco { get; set; }
        public string Status { get; set; }
        public string CreateTime { get; set; }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.ChargingHistories; }
        }
        public static byte[] Serialize(object data)
        {
            ChargingHistory obj = (ChargingHistory)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.Amount);
                    writer.Write(obj.Price);

                    writer.Write(obj.OrderId);
                    writer.Write(obj.PackageId);

                    writer.Write(obj.Telco);
                    writer.Write(obj.Status);
                    writer.Write(obj.CreateTime);
                }
                return m.ToArray();
            }
        }

        public static ChargingHistory Desserialize(byte[] data)
        {
            ChargingHistory result = new ChargingHistory();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Amount = reader.ReadInt32();
                    result.Price = reader.ReadSingle();

                    result.OrderId = reader.ReadString();
                    result.PackageId = reader.ReadString();

                    result.Telco = reader.ReadString();
                    result.Status = reader.ReadString();
                    result.CreateTime = reader.ReadString();
                }
            }
            return result;
        }



        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Amount: \"").Append(Amount).Append("\", ");
            log.Append("Price: \"").Append(Price).Append("\", ");
            log.Append("OrderId: \"").Append(OrderId).Append("\", ");
            log.Append("PackageId: \"").Append(PackageId).Append("\", ");
            log.Append("Telco: \"").Append(Telco).Append("\", ");
            log.Append("Status: \"").Append(Status).Append("\", ");
            log.Append("CreateTime: \"").Append(CreateTime).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}

