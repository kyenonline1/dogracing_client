using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PAY
{
    public class PAY5_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY5";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.Histories, IsOptional = true)]
        public ChargingHistory[] Histories { get; set; }

        public PAY5_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
