using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN4_DailyMissionResponse : ResponseBase
    {
        public EVN4_DailyMissionResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "EVN4";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Mission, IsOptional = false)]
        public DailyMission[] Missions { get; set; }
    }
}
