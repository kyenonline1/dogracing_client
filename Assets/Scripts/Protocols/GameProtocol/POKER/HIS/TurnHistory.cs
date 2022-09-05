using GameProtocol.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProtocol.HIS
{
    public class TurnHistory
    {
        public string Code { get; set; }
        public int ThinkingTime { get; set; }

        public string Data { get; set; }
        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.TurnHistory; }
        //}
        public static byte[] Serialize(object data)
        {
            TurnHistory obj = (TurnHistory)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.Code);
                    writer.Write(obj.ThinkingTime);
                    writer.Write(obj.Data);
                }
                return m.ToArray();
            }
        }

        public static TurnHistory Desserialize(byte[] data)
        {
            TurnHistory result = new TurnHistory();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Code = reader.ReadString();
                    result.ThinkingTime = reader.ReadInt32();
                    result.Data = reader.ReadString();

                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Code: \"").Append(Code).Append("\", ");
            log.Append("ThinkingTime: \"").Append(ThinkingTime).Append("\", ");
            log.Append("Data: \"").Append(Data).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
