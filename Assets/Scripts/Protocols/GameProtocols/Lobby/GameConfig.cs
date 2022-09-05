using GameProtocol.Base;
using System;
using System.IO;

namespace GameProtocol.ATH
{

    public class GameConfig
    {
        public string GameId { get; set; }
        public bool Active { get; set; }
        public int[] Blind { get; set; }
        public string Gamename { get; set; }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.GameConfig; }
        }
        public static byte[] Serialize(object data)
        {
            GameConfig ene = (GameConfig)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.GameId);
                    writer.Write(ene.Active);
                    writer.Write(ene.Blind.Length);
                    foreach (var j in ene.Blind) writer.Write(j);
                    writer.Write(ene.Gamename);
                }
                return m.ToArray();
            }
        }

        public static GameConfig Desserialize(byte[] data)
        {
            GameConfig result = new GameConfig();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.GameId = reader.ReadString();
                    result.Active = reader.ReadBoolean();
                    int lengthBlinds = reader.ReadInt32();
                    result.Blind = new int[lengthBlinds];
                    for (int i = 0; i < lengthBlinds; i++)
                        result.Blind[i] = reader.ReadInt32();
                    result.Gamename = reader.ReadString();
                }
            }
            return result;
        }
    }
}
