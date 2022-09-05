using GameProtocol.Base;
using System.IO;

namespace GameProtocol.COU
{
    public class TelcoDetail
    {
        public string TelcoId { get; set; }
        public string TelcoName { get; set; }
        public int[] Items { get; set; }


        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.TelcoDetail; }
        }
        public static byte[] Serialize(object data)
        {
            TelcoDetail obj = (TelcoDetail)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.TelcoId);
                    writer.Write(obj.TelcoName);
                    writer.Write(obj.Items.Length);
                    foreach (var c in obj.Items)
                    {
                        writer.Write(c);
                    }

                }
                return m.ToArray();
            }
        }

        public static TelcoDetail Desserialize(byte[] data)
        {
            TelcoDetail result = new TelcoDetail();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.TelcoId = reader.ReadString();
                    result.TelcoName = reader.ReadString();
                    int length = reader.ReadInt32();
                    result.Items = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.Items[i] = reader.ReadInt32();
                    }
                }
            }
            return result;
        }

    }
}
