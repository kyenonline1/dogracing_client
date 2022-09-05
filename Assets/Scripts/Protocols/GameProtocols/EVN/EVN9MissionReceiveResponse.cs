using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN9MissionReceiveResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN9";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Gold, IsOptional = false)]
        public long Gold { get; set; }

        public EVN9MissionReceiveResponse()
        {
        }
        public EVN9MissionReceiveResponse(Dictionary<byte, object> data) : base(data)
        {
        }
    }
}
