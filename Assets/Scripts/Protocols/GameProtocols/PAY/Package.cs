using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.PAY
{
    public class Package
    {
        public string ProductId { get; set; }
        public string PackageName { get; set; }
        public short Type { get; set; }
        public float Money { get; set; }
        public long Gold { get; set; }
        public int Rate { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.Package; }
        }
        public static byte[] Serialize(object data)
        {
            Package obj = (Package)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.ProductId);
                    writer.Write(obj.PackageName);
                    writer.Write(obj.Type);
                    writer.Write(obj.Money);
                    writer.Write(obj.Gold);
                    writer.Write(obj.Rate);
                }
                return m.ToArray();
            }
        }

        public static Package Desserialize(byte[] data)
        {
            Package result = new Package();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.ProductId = reader.ReadString();
                    result.PackageName = reader.ReadString();
                    result.Type = reader.ReadInt16();
                    result.Money = reader.ReadSingle();
                    result.Gold = reader.ReadInt64();
                    result.Rate = reader.ReadInt32();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{ProductId: \"").Append(ProductId).Append("\", ");
            log.Append("PackageName: \"").Append(PackageName).Append("\", ");
            log.Append("Type: \"").Append(Type).Append("\", ");
            log.Append("Money: \"").Append(Money).Append("\", ");
            log.Append("Gold: \"").Append(Gold).Append("\", ");
            log.Append("Rate: \"").Append(Rate).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
