using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN6FirstChargingResponse : ResponseBase
    {
        public EVN6FirstChargingResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "EVN6";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Events, IsOptional = false)]
        public FirstCharging[] Events { get; set; }
    }
}
