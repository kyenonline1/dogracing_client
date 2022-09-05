using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.DOG
{
    public class DogRacingHistory
    {
        //game_session, result, create_time
        public long GameSession { get; set; }
        public byte[] Result { get; set; }
        public string CreateTime { get; set; }

        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.DogRacingHistory; }
        }
        public static byte[] Serialize(object data)
        {
            DogRacingHistory ene = (DogRacingHistory)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.GameSession);
                    int len_factor = ene.Result.Length;
                    writer.Write(len_factor);
                    foreach (var p in ene.Result)
                    {
                        writer.Write(p);
                    }

                    writer.Write(ene.CreateTime);
                }
                return m.ToArray();
            }
        }

        public static DogRacingHistory Desserialize(byte[] data)
        {
            DogRacingHistory result = new DogRacingHistory();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.GameSession = reader.ReadInt64();
                    int len_factor = reader.ReadInt32();
                    result.Result = new byte[len_factor];
                    for (int i = 0; i < len_factor; i++)
                    {
                        result.Result[i] = reader.ReadByte();
                    }
                    result.CreateTime = reader.ReadString();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("GameSession: \"").Append(GameSession).Append("\", ");
            log.Append("Result: \"").Append(Result).Append("\", ");
            log.Append("CreateTime: \"").Append(CreateTime).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }

}
