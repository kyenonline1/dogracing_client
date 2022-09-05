
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PAY
{
    public class PAY2_Response : ResponseBase
    {

        public override void SetCodeRun()
        {
            CodeRun = "PAY2";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }

        public PAY2_Response(Dictionary<byte, object> data) : base(data)
        {

        }

    }
}
