using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.COU
{
    public class COU2HistoriesCashoutResponse : ResponseBase
    {
        public COU2HistoriesCashoutResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "COU2";
        }

        [DataMember(Code = (byte)COU_ParameterCode.Histories, IsOptional = true)]
        public CashoutHistory[] Histories { get; set; }

    }
    
}
