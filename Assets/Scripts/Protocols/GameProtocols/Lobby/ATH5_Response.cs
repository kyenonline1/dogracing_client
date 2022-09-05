using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    
    public class ATH5_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH5";
        }

        public ATH5_Response(Dictionary<byte, object> data): base(data)
        {

        }

        [DataMember(Code = (byte)ATH_ParameterCode.Username, IsOptional = true)]
        public string Username { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Password, IsOptional = true)]
        public string Password { get; set; }
    }
}
