using GameProtocol.Base;
using System.IO;

namespace GameProtocol.LKC
{
    public class LuckyCardHits
    {
        /// <summary>
        /// Số hit để nhận thưởng
        /// </summary>
        public byte hit { get; set; }
        
        /// <summary>
        /// Loại hit, 0: xBB 1: %Pool;
        /// </summary>
        public byte hitType { get; set; }

        /// <summary>
        /// Hit Value
        /// Ví dụ 1: hit = 0, hitValue = 5 nghĩa là giá trị thưởng = x5 lần BB
        /// Ví dụ 2: hit = 1, hitValue = 5 nghĩa là giá trị thưởng = 5% giá trị Pool
        /// </summary>
        public byte hitValue { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.LuckyCardHit; }
        //}
        public static byte[] Serialize(object data)
        {
            var obj = (LuckyCardHits)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.hit);
                    writer.Write(obj.hitType);
                    writer.Write(obj.hitValue);
                }
                return m.ToArray();
            }
        }

        public static LuckyCardHits Desserialize(byte[] data)
        {
            var result = new LuckyCardHits();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.hit = reader.ReadByte();
                    result.hitType = reader.ReadByte();
                    result.hitValue = reader.ReadByte();
                }
            }
            return result;
        }
    }
}
