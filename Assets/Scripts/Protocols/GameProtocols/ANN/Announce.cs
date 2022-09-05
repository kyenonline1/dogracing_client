using GameProtocol.Base;
using System;
using System.IO;
using System.Text;

namespace GameProtocol.ANN
{
    public class Announce
    {
        public int AnnouneId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public short Type { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        /// <summary>
        /// 0: chưa đọc, 1 đã đọc, thư hệ thống thì mặc định là 0. k cho đổi state
        /// </summary>
        public byte State { get; set; } 

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.Announce; }
        }
        public static byte[] Serialize(object data)
        {
            Announce obj = (Announce)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.AnnouneId);
                    writer.Write(obj.Title);
                    writer.Write(obj.Content);
                    writer.Write(obj.Type);
                    writer.Write(obj.StartTime);
                    writer.Write(obj.EndTime);
                    writer.Write(obj.State);
                }
                return m.ToArray();
            }
        }

        public static Announce Desserialize(byte[] data)
        {
            Announce result = new Announce();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.AnnouneId = reader.ReadInt32();
                    result.Title = reader.ReadString();
                    result.Content = reader.ReadString();
                    result.Type = reader.ReadInt16();
                    result.StartTime = reader.ReadString();
                    result.EndTime = reader.ReadString();
                    result.State = reader.ReadByte();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{AnnouneId: \"").Append(AnnouneId).Append("\", ");
            log.Append("Title: \"").Append(Title).Append("\", ");
            log.Append("Content: \"").Append(Content).Append("\", ");
            log.Append("Type: \"").Append(Type).Append("\", ");
            log.Append("StartTime: \"").Append(StartTime).Append("\", ");
            log.Append("EndTime: \"").Append(EndTime).Append("\", ");
            log.Append("State: \"").Append(State).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
