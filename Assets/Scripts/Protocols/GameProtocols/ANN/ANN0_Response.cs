
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.ANN
{
    public class ANN0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ANN0";
        }
        [DataMember(Code = (byte)ANN_ParameterCode.Announce, IsOptional = true)]
        public Announce[] data { get; set; }

        public ANN0_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
