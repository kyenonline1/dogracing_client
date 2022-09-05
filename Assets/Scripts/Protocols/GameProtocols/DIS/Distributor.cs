using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.DIS
{
    public class Distributor
    {
        public string Nickname { get; set; }
        public string DistributorName { get; set; }
        public string Address { get; set; }
        public string FacebookUrl { get; set; }
        public string Phone { get; set; }
        public string Zalo { get; set; }
        public string Telegram { get; set; }
        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.Distributor; }
        //}
        public static byte[] Serialize(object data)
        {
            Distributor obj = (Distributor)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.Nickname);
                    writer.Write(obj.DistributorName);
                    writer.Write(obj.Address);
                    writer.Write(obj.FacebookUrl);
                    writer.Write(obj.Phone);
                    writer.Write(obj.Zalo);
                    writer.Write(obj.Telegram);
                }
                return m.ToArray();
            }
        }

        public static Distributor Desserialize(byte[] data)
        {
            Distributor obj = new Distributor();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    obj.Nickname = reader.ReadString();
                    obj.DistributorName = reader.ReadString();
                    obj.Address = reader.ReadString();
                    obj.FacebookUrl = reader.ReadString();
                    obj.Phone = reader.ReadString();
                    obj.Zalo = reader.ReadString();
                    obj.Telegram = reader.ReadString();
                }
            }
            return obj;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Nickname: \"").Append(Nickname).Append("\", ");
            log.Append("DistributorName: \"").Append(DistributorName).Append("\", ");
            log.Append("Address: \"").Append(Address).Append("\", ");
            log.Append("FacebookUrl: \"").Append(FacebookUrl).Append("\", ");
            log.Append("Phone: \"").Append(Phone).Append("\", ");
            log.Append("Zalo: \"").Append(Zalo).Append("\", ");
            log.Append("Telegram: \"").Append(Telegram).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
