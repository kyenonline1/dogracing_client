

namespace GameProtocol.Protocol
{
    public abstract class MessageParameter
    {
        public string Language = "vn";

        public MessageParameter ShallowCopy()
        {
            return (MessageParameter)this.MemberwiseClone();
        }
    }
}
