using GameProtocol.Base;
using System.IO;

namespace GameProtocol.COU
{
    public class CashoutHistory
    {
        public int TransId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TimeCashout { get; set; }
        public byte Status { get; set; } //0: Chưa duyệt, : 1 đã duyệt, 2 : Đã hủy, 3 : Đã nhận thẻ, 4 : Đã Nạp lại
        public int Amount { get; set; }
        public static byte RegisterType
        {
            get { return (byte)RegisterTypes.CashoutHistory; }
        }
        public static byte[] Serialize(object data)
        {
            CashoutHistory obj = (CashoutHistory)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.TransId);
                    writer.Write(obj.Email);
                    writer.Write(obj.Firstname);
                    writer.Write(obj.Lastname);
                    writer.Write(obj.TimeCashout);
                    writer.Write(obj.Status);
                    writer.Write(obj.Amount);
                }
                return m.ToArray();
            }
        }

        public static CashoutHistory Desserialize(byte[] data)
        {
            CashoutHistory result = new CashoutHistory();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.TransId = reader.ReadInt32();
                    result.Email = reader.ReadString();
                    result.Firstname = reader.ReadString();
                    result.Lastname = reader.ReadString();
                    result.TimeCashout = reader.ReadString();
                    result.Status = reader.ReadByte();
                    result.Amount = reader.ReadInt32();
                }
            }
            return result;
        }
    }
}
