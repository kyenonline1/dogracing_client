
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using System.IO;

namespace GameProtocol.PAY
{
    public class PAY1_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY1";
        }

        [DataMember(Code = (byte)PAY_ParameterCode.Packages, IsOptional = true)]
        public Package[] Packages { get; set; }

        public PAY1_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
