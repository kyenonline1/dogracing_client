using GameProtocol.Base;
using System;
using System.IO;

namespace GameProtocol.XIT
{
    public class XitoPlayerInfo
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public int Position { get; set; }
        public long CashIn { get; set; }
        public int[] HandCards { get; set; }
        public bool IsReady { get; set; }
        public long CashBet { get; set; }
        public short ActionId { get; set; }
        public string Avatar { get; set; }

        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.XitoPlayer; }
        //}
        public static byte[] Serialize(object data)
        {
            XitoPlayerInfo obj = (XitoPlayerInfo)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.UserId);
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
                    writer.Write(obj.Avatar);

                }
                return m.ToArray();
            }
        }

        public static XitoPlayerInfo Desserialize(byte[] data)
        {
            XitoPlayerInfo result = new XitoPlayerInfo();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.UserId = reader.ReadInt64();
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
                    result.Avatar = reader.ReadString();

                }
            }
            return result;
        }
    }
}
