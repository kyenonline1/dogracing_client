using GameProtocol.Base;
using System.IO;
using System.Linq;

namespace GameProtocol.TOP
{
    public class TopCate
    {
        public byte CateId { get; set; }
        public string CateName { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.TopCate; }
        }
        public static byte[] Serialize(object data)
        {
            TopCate obj = (TopCate)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.CateId);
                    writer.Write(obj.CateName);
                }
                return m.ToArray();
            }
        }

        public static TopCate Desserialize(byte[] data)
        {
            TopCate result = new TopCate();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.CateId = reader.ReadByte();
                    result.CateName = reader.ReadString();
                }
            }
            return result;
        }
    }
}
