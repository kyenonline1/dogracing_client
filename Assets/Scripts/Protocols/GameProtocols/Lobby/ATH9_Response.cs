using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH9_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH9";
        }

        public ATH9_Response(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)ATH_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }

    }
}
