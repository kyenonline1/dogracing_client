using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUCancelSortCardPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU15";
        }

        public MAUCancelSortCardPush()
        {

        }

        public MAUCancelSortCardPush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }
    }
}
