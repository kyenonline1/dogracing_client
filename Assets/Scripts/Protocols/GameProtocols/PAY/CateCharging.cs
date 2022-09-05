using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.PAY
{
    public class CateCharging
    {
        public short Type { get; set; }
        public string Name { get; set; }
        public int[] Amounts { get; set; }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.CateCharging; }
        }
        public static byte[] Serialize(object data)
        {
            CateCharging obj = (CateCharging)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.Type);
                    writer.Write(obj.Name);
                    if (obj.Amounts == null) obj.Amounts = new int[0];
                    int length = obj.Amounts.Length;
                    writer.Write(length);
                    foreach (var amount in obj.Amounts)
                    {
                        writer.Write(amount);
                    }
                }
                return m.ToArray();
            }
        }

        public static CateCharging Desserialize(byte[] data)
        {
            CateCharging result = new CateCharging();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Type = reader.ReadInt16();
                    result.Name = reader.ReadString();
                    int length = reader.ReadInt32();
                    result.Amounts = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.Amounts[i] = reader.ReadInt32();
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Type: \"").Append(Type).Append("\", ");
            log.Append("Name: \"").Append(Name).Append("\", ");
            log.Append("Amounts: \"").Append(Amounts).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
