using GameProtocol.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProtocol.HIS
{
    public class PokerHistory
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public int[] HandCard { get; set; }
        public string StartTime { get; set; }
        public int Blind { get; set; }
        public int Ante { get; set; }
        public int WinLoseCash { get; set; }
        public bool IsSave { get; set; }
        public int[] CenterCard { get; set; }
        public int[] HighestCard { get; set; }
        public byte Rank { get; set; }
        public int TableId { get; set; }
        public int GameSeasion { get; set; }
        public string PlayerType { get; set; }//Dealer, Small, Big
        public string Action { get; set; }
        /// <summary>
        /// -1: Sảnh chính, 0: SpinUp, 1: Tournament, 
        /// </summary>
        public string GameId { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.PokerHistory; }
        //}
        public static byte[] Serialize(object data)
        {
            PokerHistory obj = (PokerHistory)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.UserId);
                    writer.Write(obj.Nickname);
                    writer.Write(obj.HandCard.Length);
                    foreach (var c in obj.HandCard)
                    {
                        writer.Write(c);
                    }
                    writer.Write(obj.StartTime);
                    writer.Write(obj.Blind);
                    writer.Write(obj.Ante);
                    writer.Write(obj.WinLoseCash);
                    writer.Write(obj.IsSave);

                    writer.Write(obj.CenterCard.Length);
                    foreach (var c in obj.CenterCard)
                    {
                        writer.Write(c);
                    }

                    writer.Write(obj.HighestCard.Length);
                    foreach (var c in obj.HighestCard)
                    {
                        writer.Write(c);
                    }

                    writer.Write(obj.Rank);

                    writer.Write(obj.TableId);
                    writer.Write(obj.GameSeasion);
                    writer.Write(obj.PlayerType);
                    writer.Write(obj.Action);
                    writer.Write(obj.GameId);
                }
                return m.ToArray();
            }
        }

        public static PokerHistory Desserialize(byte[] data)
        {
            PokerHistory result = new PokerHistory();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
                    result.Nickname = reader.ReadString();
                    int length = reader.ReadInt32();
                    result.HandCard = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.HandCard[i] = reader.ReadInt32();
                    }
                    result.StartTime = reader.ReadString();
                    result.Blind = reader.ReadInt32();
                    result.Ante = reader.ReadInt32();
                    result.WinLoseCash = reader.ReadInt32();
                    result.IsSave = reader.ReadBoolean();

                    length = reader.ReadInt32();
                    result.CenterCard = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.CenterCard[i] = reader.ReadInt32();
                    }

                    length = reader.ReadInt32();
                    result.HighestCard = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.HighestCard[i] = reader.ReadInt32();
                    }

                    result.Rank = reader.ReadByte();

                    result.TableId = reader.ReadInt32();
                    result.GameSeasion = reader.ReadInt32();
                    result.PlayerType = reader.ReadString();
                    result.Action = reader.ReadString();
                    result.GameId = reader.ReadString();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{UserId: \"").Append(UserId).Append("\", ");
            log.Append("Nickname: \"").Append(Nickname).Append("\", ");
            log.Append("HandCard: \"").Append(HandCard).Append("\", ");
            log.Append("StartTime: \"").Append(StartTime).Append("\", ");
            log.Append("Blind: \"").Append(Blind).Append("\", ");
            log.Append("Ante: \"").Append(Ante).Append("\", ");
            log.Append("WinLoseCash: \"").Append(WinLoseCash).Append("\", ");
            log.Append("IsSave: \"").Append(IsSave).Append("\", ");
            log.Append("CenterCard: \"").Append(CenterCard).Append("\", ");
            log.Append("HighestCard: \"").Append(HighestCard).Append("\", ");
            log.Append("Rank: \"").Append(Rank).Append("\", ");
            log.Append("TableId: \"").Append(TableId).Append("\", ");
            log.Append("GameSeasion: \"").Append(GameSeasion).Append("\", ");
            log.Append("PlayerType: \"").Append(PlayerType).Append("\", ");
            log.Append("Action: \"").Append(Action).Append("\", ");
            log.Append("GameId: \"").Append(GameId).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
