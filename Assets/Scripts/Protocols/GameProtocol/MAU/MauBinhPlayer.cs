using GameProtocol.Base;
using System.IO;

namespace GameProtocol.MAU
{
    public class MauBinhPlayer
    {
        public string NickName { get; set; }
        public long Cash { get; set; }
        public string Avatar { get; set; }
        public int[] HandCards { get; set; }
        public int Position { get; set; }
        public bool IsReady { get; set; }
        public long WinLoseCash { get; set; }
        public bool CompeletedSort { get; set; }
        public bool IsMauBinh { get; set; }
        public string Rank { get; set; }
        public long UserId { get; set; }
        public long HoldCash { get; set; }
        //public static byte RegisterType
        //{
        //    get { return (byte)RegisterTypes.MauBinhPlayer; }
        //}
        public static byte[] Serialize(object data)
        {
            MauBinhPlayer ene = (MauBinhPlayer)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(ene.NickName);
                    writer.Write(ene.Cash);
                    writer.Write(ene.Avatar);
                    if (ene.HandCards == null) ene.HandCards = new int[0];
                    writer.Write(ene.HandCards.Length);
                    foreach (var c in ene.HandCards)
                    {
                        writer.Write(c);
                    }

                    writer.Write(ene.Position);
                    writer.Write(ene.IsReady);
                    writer.Write(ene.WinLoseCash);

                    writer.Write(ene.CompeletedSort);
                    writer.Write(ene.IsMauBinh);
                    writer.Write(ene.Rank);
                    writer.Write(ene.UserId);
                    writer.Write(ene.HoldCash);
                }
                return m.ToArray();
            }
        }

        public static MauBinhPlayer Desserialize(byte[] data)
        {
            MauBinhPlayer result = new MauBinhPlayer();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.NickName = reader.ReadString();
                    result.Cash = reader.ReadInt64();
                    result.Avatar = reader.ReadString();
                    int length = reader.ReadInt32();
                    result.HandCards = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        result.HandCards[i] = reader.ReadInt32();
                    }

                    result.Position = reader.ReadInt32();
                    result.IsReady = reader.ReadBoolean();
                    result.WinLoseCash = reader.ReadInt64();

                    result.CompeletedSort = reader.ReadBoolean();
                    result.IsMauBinh = reader.ReadBoolean();
                    result.Rank = reader.ReadString();
                    result.UserId = reader.ReadInt64();
                    result.HoldCash = reader.ReadInt64();
                }
            }
            return result;
        }
    }
}
