using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUSortCardPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU12";
        }

        public MAUSortCardPush()
        {

        }

        public MAUSortCardPush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Sortable, IsOptional = true)]
        public bool Sortable { get; set; }

    }
}
