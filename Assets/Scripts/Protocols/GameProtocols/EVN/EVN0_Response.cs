using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN0_Response : ResponseBase
    {
        public EVN0_Response(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "EVN0";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.EventEntity, IsOptional = true)]
        public EventEntity[] Data { get; set; }
        
    }
}
