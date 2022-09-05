using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG90ChatHistoriesResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "AIG90";
        }

        public DOG90ChatHistoriesResponse()
        {
            Flag = 0;
        }

        public DOG90ChatHistoriesResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)DOG_ParameterCode.Histories, IsOptional = true)]
        public DogChat[] Histories { get; set; }
    }
}
