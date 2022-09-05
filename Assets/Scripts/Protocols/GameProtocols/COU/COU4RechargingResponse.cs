using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.COU
{
    public class COU4RechargingResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU4";
        }

        [DataMember(Code = (byte)COU_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }

        public COU4RechargingResponse(Dictionary<byte, object> data) : base(data)
        {

        }

        public COU4RechargingResponse()
        {
        }
    }
    
}
