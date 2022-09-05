using GameProtocol.Base;
using System.IO;

namespace GameProtocol.SLC
{
    public class TopGame
    {
        public long GameSession { get; set; }
        public string Nickname { get; set; }
        public long Blind { get; set; }
        public long WinCash { get; set; }
        public string Description { get; set; }
        public string CreateTime { get; set; }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.TopGame; }
        }
        public static byte[] Serialize(object data)
        {
            TopGame ene = (TopGame)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.GameSession);
                    writer.Write(ene.Nickname);
                    writer.Write(ene.Blind);
                    writer.Write(ene.WinCash);
                    writer.Write(ene.Description);
                    writer.Write(ene.CreateTime);
                }
                return m.ToArray();
            }
        }

        public static TopGame Desserialize(byte[] data)
        {
            TopGame result = new TopGame();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.GameSession = reader.ReadInt64();
                    result.Nickname = reader.ReadString();
                    result.Blind = reader.ReadInt64();
                    result.WinCash = reader.ReadInt64();
                    result.Description = reader.ReadString();
                    result.CreateTime = reader.ReadString();
                }
            }
            return result;
        }
    }
}
