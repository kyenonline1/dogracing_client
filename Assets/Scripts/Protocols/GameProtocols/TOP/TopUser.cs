using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.TOP
{
    public class TopEvent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string Avatar { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.TopEvent; }
        }
        public static byte[] Serialize(object data)
        {
            TopEvent obj = (TopEvent)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.Id);
                    writer.Write(obj.Name);
                    writer.Write(obj.Score);
                    writer.Write(obj.Avatar);
                }
                return m.ToArray();
            }
        }

        public static TopEvent Desserialize(byte[] data)
        {
            TopEvent result = new TopEvent();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Id = reader.ReadInt64();
                    result.Name = reader.ReadString();
                    result.Score = reader.ReadInt32();
                    result.Avatar = reader.ReadString();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Id: \"").Append(Id).Append("\", ");
            log.Append("Name: \"").Append(Name).Append("\", ");
            log.Append("Score: \"").Append(Score).Append("\", ");
            log.Append("Avatar: \"").Append(Avatar).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
