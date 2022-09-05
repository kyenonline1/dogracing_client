using GameProtocol.Base;
using System;
using System.IO;
using System.Text;

namespace GameProtocol.ATH
{

    public class BlindInfo
    {
        public int Blind { get; set; }
        public string Region { get; set; }
        public int MinCashIn { get; set; }
        public bool Active { get; set; }
        public int UsersOnline { get; set; }
        public byte Type { get; set; }
        public int isAnte { get; set; }
        public bool isStraddle { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.BlindInfo; }
        //}
        public static byte[] Serialize(object data)
        {
            BlindInfo ene = (BlindInfo)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.Blind);
                    writer.Write(ene.Region);
                    writer.Write(ene.MinCashIn);
                    writer.Write(ene.Active);
                    writer.Write(ene.UsersOnline);
                    writer.Write(ene.Type);
                    writer.Write(ene.isAnte);
                    writer.Write(ene.isStraddle);
                }
                return m.ToArray();
            }
        }

        public static BlindInfo Desserialize(byte[] data)
        {
            BlindInfo result = new BlindInfo();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Blind = reader.ReadInt32();
                    result.Region = reader.ReadString();
                    result.MinCashIn = reader.ReadInt32();
                    result.Active = reader.ReadBoolean();
                    result.UsersOnline = reader.ReadInt32();
                    result.Type = reader.ReadByte();
                    result.isAnte = reader.ReadInt32();
                    result.isStraddle = reader.ReadBoolean();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Blind: \"").Append(Blind).Append("\", ");
            log.Append("Region: \"").Append(Region).Append("\", ");
            log.Append("MinCashIn: \"").Append(MinCashIn).Append("\", ");
            log.Append("Active: \"").Append(Active).Append("\", ");
            log.Append("UsersOnline: \"").Append(UsersOnline).Append("\", ");
            log.Append("Type: \"").Append(Type).Append("\", ");
            log.Append("isAnte: \"").Append(isAnte).Append("\", ");
            log.Append("isStraddle: \"").Append(isStraddle).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
