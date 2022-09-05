
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PAY
{
    public class PAY3_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY3";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }

        public PAY3_Response(Dictionary<byte, object> data) : base(data)
        {

        }

    }
}
