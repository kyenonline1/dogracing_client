using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.DOG
{
    public class PlayerDog
    {
        public string Nickname { get; set; }
        public long Cash { get; set; }
        public string Avatar { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.PlayerDog; }
        }
        public static byte[] Serialize(object data)
        {
            PlayerDog ene = (PlayerDog)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.Nickname);
                    writer.Write(ene.Cash);
                    writer.Write(ene.Avatar);
                }
                return m.ToArray();
            }
        }

        public static PlayerDog Desserialize(byte[] data)
        {
            PlayerDog result = new PlayerDog();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Nickname = reader.ReadString();
                    result.Cash = reader.ReadInt64();
                    result.Avatar = reader.ReadString();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("Nickname: \"").Append(Nickname).Append("\", ");
            log.Append("Cash: \"").Append(Cash).Append("\", ");
            log.Append("Avatar: \"").Append(Avatar).Append("\"}").Append("\"}");

            return log.ToString();
        }

    }
}
