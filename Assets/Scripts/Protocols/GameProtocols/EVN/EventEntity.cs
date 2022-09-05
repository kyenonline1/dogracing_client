using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.EVN
{
    public class EventEntity
    {
        public string ImageData { get; set; }
        public string ImageBanner { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.EventEntity; }
        }

        public static byte[] Serialize(object data)
        {
            EventEntity obj = (EventEntity)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.ImageData);
                    writer.Write(obj.ImageBanner);
                }
                return m.ToArray();
            }
        }

        public static EventEntity Desserialize(byte[] data)
        {
            EventEntity obj = new EventEntity();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    obj.ImageData = reader.ReadString();
                    obj.ImageBanner = reader.ReadString();
                }
            }
            return obj;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{ImageData: \"").Append(ImageData).Append("\", ");
            log.Append("ImageBanner: \"").Append(ImageBanner).Append("\"}").Append("\"}");

            return log.ToString();
        }

    }
}