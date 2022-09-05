using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH6_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH6";
        }

        public ATH6_Response(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)ATH_ParameterCode.Url, IsOptional = true)]
        public string Url { get; set; }
    }
}
