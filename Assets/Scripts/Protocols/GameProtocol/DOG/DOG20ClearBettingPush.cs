using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG20ClearBettingPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG20";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.Nickname, IsOptional = false)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.CurrentCash, IsOptional = false)]
        public long CurrentCash { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.TotalBets, IsOptional = true)]
        public DogSlot[] TotalBets { get; set; }

        public DOG20ClearBettingPush()
        {
            Flag = 0;
        }

        public DOG20ClearBettingPush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
