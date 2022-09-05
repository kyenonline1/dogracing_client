using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN8MissionsResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN8";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Mission, IsOptional = false)]
        public Mission[] Missions { get; set; }

        public EVN8MissionsResponse()
        {
        }
        public EVN8MissionsResponse(Dictionary<byte, object> data) : base(data)
        {
        }
    }
}
