using GameProtocol.Base;
using System.IO;
using System.Text;

namespace GameProtocol.DIS
{
    public class TranferHistory
    {
        public string CreateTime { get; set; }
        public string UserTranfer { get; set; }
        public string UserReceived { get; set; }
        public int Amount { get; set; }
        public string Content { get; set; }

        //public static byte RegisterType
        //{
        //    //get { return (byte)RegisterTypes.TranferHistory; }
        //}
        public static byte[] Serialize(object data)
        {
            TranferHistory obj = (TranferHistory)data;
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(obj.CreateTime);
                    writer.Write(obj.UserTranfer);
                    writer.Write(obj.UserReceived);
                    writer.Write(obj.Amount);
                    writer.Write(obj.Content);

                }
                return m.ToArray();
            }
        }

        public static TranferHistory Desserialize(byte[] data)
        {
            TranferHistory result = new TranferHistory();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    result.CreateTime = reader.ReadString();
                    result.UserTranfer = reader.ReadString();

                    result.UserReceived = reader.ReadString();
                    result.Amount = reader.ReadInt32();
                    result.Content = reader.ReadString();

                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.Append("{CreateTime: \"").Append(CreateTime).Append("\", ");
            log.Append("UserTranfer: \"").Append(UserTranfer).Append("\", ");
            log.Append("UserReceived: \"").Append(UserReceived).Append("\", ");
            log.Append("Amount: \"").Append(Amount).Append("\", ");
            log.Append("Content: \"").Append(Content).Append("\"}").Append("\"}");

            return log.ToString();
        }
    }
}
