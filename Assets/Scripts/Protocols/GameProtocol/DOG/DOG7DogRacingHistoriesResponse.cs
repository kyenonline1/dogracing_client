using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG7DogRacingHistoriesResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG7";
        }

        public DOG7DogRacingHistoriesResponse()
        {
            Flag = 0;
        }

        public DOG7DogRacingHistoriesResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)DOG_ParameterCode.Histories, IsOptional = true)]
        public DogRacingHistory[] Histories { get; set; }
    }
}
