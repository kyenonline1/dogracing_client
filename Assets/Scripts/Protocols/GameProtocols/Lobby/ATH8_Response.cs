using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH8_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH8";
        }

        public ATH8_Response(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)ATH_ParameterCode.Games, IsOptional = true)]
        public GameConfig[] Games { get; set; }
    }
}
