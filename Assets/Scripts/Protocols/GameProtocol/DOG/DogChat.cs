using GameProtocol.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProtocol.DOG
{
    public class DogChat
    {
        public string Nickname { get; set; }
        public string Message { get; set; }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.DogChat; }
        }
        public static byte[] Serialize(object data)
        {
            DogChat ene = (DogChat)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.Nickname);
                    writer.Write(ene.Message);
                }
                return m.ToArray();
            }
        }

        public static DogChat Desserialize(byte[] data)
        {
            DogChat result = new DogChat();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Nickname = reader.ReadString();
                    result.Message = reader.ReadString();
                }
            }
            return result;
        }
    }
}
