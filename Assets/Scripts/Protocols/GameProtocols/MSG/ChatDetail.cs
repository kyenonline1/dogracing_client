using GameProtocol.Base;
using System.IO;

namespace GameProtocol.MSG
{
    public class ChatDetail
    {
        public string Nickname { get; set; }
        public string Message { get; set; }
        public string TimeChat { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.ChatDetail; }
        }

        public static byte[] Serialize(object data)
        {
            ChatDetail ene = (ChatDetail)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.Nickname);
                    writer.Write(ene.Message);
                    writer.Write(ene.TimeChat);
                }
                return m.ToArray();
            }
        }

        public static ChatDetail Desserialize(byte[] data)
        {
            ChatDetail result = new ChatDetail();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Nickname = reader.ReadString();
                    result.Message = reader.ReadString();
                    result.TimeChat = reader.ReadString();
                }
            }
            return result;
        }
    }
}
