using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH4GetBlindsInfoResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH4";
        }

        public ATH4GetBlindsInfoResponse(Dictionary<byte, object> dict) : base(dict)
        {

        }

        [DataMember(Code = (byte)ATH_ParameterCode.BlindsInfo, IsOptional = true)]
        public BlindInfo[] BlindsInfo { get; set; }
    }
}
