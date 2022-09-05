using GameProtocol.Base;
using System;
using System.IO;
using System.Text;

namespace GameProtocol.PKR
{
    public class PokerPlayer
    {
        public string Nickname { get; set; }
        public int Position { get; set; }
        public long CashIn { get; set; }
        public int[] HandCards { get; set; }
        public bool IsReady { get; set; }
        public long CashBet { get; set; }
        public short ActionId { get; set; }
        public string Actionname { get; set; }
        public long UserId { get; set; }
        public byte VipType { get; set; }
        public bool IsSittingOut { get; set; }
        public string Avatar { get; set; }
        public long FreePot { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.PokerPlayer; }
        //}
        public static byte[] Serialize(object data)
        {
            PokerPlayer obj = (PokerPlayer)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.Nickname);
                    writer.Write(obj.Position);
                    writer.Write(obj.CashIn);
                    if (obj.HandCards == null) obj.HandCards = new int[0];
                    writer.Write(obj.HandCards.Length);
                    foreach (var c in obj.HandCards)
                    {
                        writer.Write(c);
                    }
                    writer.Write(obj.IsReady);
                    writer.Write(obj.CashBet);

                    writer.Write(obj.ActionId);
                    if (obj.Actionname == null) obj.Actionname = "";
                    writer.Write(obj.Actionname);
                    writer.Write(obj.UserId);
                    writer.Write(obj.VipType);
                    writer.Write(obj.IsSittingOut);
                    writer.Write(obj.Avatar);
                    writer.Write(obj.FreePot);
                }
                return m.ToArray();
            }
        }

        public static PokerPlayer Desserialize(byte[] data)
        {
            PokerPlayer result = new PokerPlayer();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.Nickname = reader.ReadString();
                    result.Position = reader.ReadInt32();
                    result.CashIn = reader.ReadInt64();
                    int length = reader.ReadInt32();
                    result.HandCards = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.HandCards[i] = reader.ReadInt32();
                    }
                    result.IsReady = reader.ReadBoolean();
                    result.CashBet = reader.ReadInt64();

                    result.ActionId = reader.ReadInt16();
                    result.Actionname = reader.ReadString();

                    result.UserId = reader.ReadInt64();
                    result.VipType = reader.ReadByte();
                    result.IsSittingOut = reader.ReadBoolean();
                    result.Avatar = reader.ReadString();
                    result.FreePot = reader.ReadInt64();
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{Nickname: \"").Append(Nickname).Append("\", ");
            log.Append("Position: \"").Append(Position).Append("\", ");
            log.Append("CashIn: \"").Append(CashIn).Append("\", ");
            log.Append("HandCards: \"").Append(HandCards).Append("\", ");
            log.Append("IsReady: \"").Append(IsReady).Append("\", ");
            log.Append("CashBet: \"").Append(CashBet).Append("\", ");
            log.Append("ActionId: \"").Append(ActionId).Append("\", ");
            log.Append("Actionname: \"").Append(Actionname).Append("\", ");
            log.Append("UserId: \"").Append(UserId).Append("\", ");
            log.Append("VipType: \"").Append(VipType).Append("\", ");
            log.Append("IsSittingOut: \"").Append(IsSittingOut).Append("\", ");
            log.Append("Avatar: \"").Append(Avatar).Append("\", ");
            log.Append("FreePot: \"").Append(FreePot).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
