using GameProtocol.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProtocol.DOG
{
    public class DogSlot
    {
        public int SlotId { get; set; }
        public float Factor { get; set; }
        public long TotalBeting { get; set; }
        public short State { get; set; }// -1 : down, 0 : normal, 1 : up
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.DogSlot; }
        }
        public static byte[] Serialize(object data)
        {
            DogSlot ene = (DogSlot)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.SlotId);
                    writer.Write(ene.Factor);
                    writer.Write(ene.TotalBeting);
                    writer.Write(ene.State);
                }
                return m.ToArray();
            }
        }

        public static DogSlot Desserialize(byte[] data)
        {
            DogSlot result = new DogSlot();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.SlotId = reader.ReadInt32();
                    result.Factor = reader.ReadSingle();
                    result.TotalBeting = reader.ReadInt64();
                    result.State = reader.ReadInt16();
                }
            }
            return result;
        }
        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("SlotId: \"").Append(SlotId).Append("\", ");
            log.Append("Factor: \"").Append(Factor).Append("\", ");
            log.Append("TotalBeting: \"").Append(TotalBeting).Append("\", ");
            log.Append("State: \"").Append(State).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
