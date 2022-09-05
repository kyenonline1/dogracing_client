using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG8BettingHistoriesResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG8";
        }
        
        public DOG8BettingHistoriesResponse()
        {
            Flag = 0;
        }

        public DOG8BettingHistoriesResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)DOG_ParameterCode.Histories, IsOptional = true)]
        public DogRacingHistoryByUser[] Histories { get; set; }
    }
}
