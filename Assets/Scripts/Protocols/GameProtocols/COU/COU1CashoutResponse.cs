using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.COU
{
    public class COU1CashoutResponse : ResponseBase
    {
        public COU1CashoutResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "COU1";
        }
        [DataMember(Code = (byte)COU_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }
    }
}
