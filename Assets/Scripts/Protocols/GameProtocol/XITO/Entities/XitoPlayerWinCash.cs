using GameProtocol.Base;
using System;
using System.IO;

namespace GameProtocol.XIT
{
    public class XitoPlayerWinCash
    {
        public long UserId { get; set; }
        public long WinCash { get; set; }
        public long Cash { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.XitoPlayerWinCash; }
        //}
        public static byte[] Serialize(object data)
        {
            XitoPlayerWinCash obj = (XitoPlayerWinCash)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.UserId);
                    writer.Write(obj.WinCash);
                    writer.Write(obj.Cash);
                }
                return m.ToArray();
            }
        }

        public static XitoPlayerWinCash Desserialize(byte[] data)
        {
            XitoPlayerWinCash result = new XitoPlayerWinCash();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    result.WinCash = reader.ReadInt64();
                    result.Cash = reader.ReadInt64();
                }
            }
            return result;
        }
    }
}
