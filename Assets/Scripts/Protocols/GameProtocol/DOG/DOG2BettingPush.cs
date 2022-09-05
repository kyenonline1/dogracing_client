using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG2BettingPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG2";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.Nickname, IsOptional = false)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.CurrentCash, IsOptional = false)]
        public long CurrentCash { get; set; }
        
        [DataMember(Code = (byte)DOG_ParameterCode.CashBet, IsOptional = false)]
        public long CashBet { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.TotalBet, IsOptional = false)]
        public long TotalBet { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.SlotId, IsOptional = false)]
        public int DogId { get; set; }

        public DOG2BettingPush()
        {
            Flag = 0;
        }

        public DOG2BettingPush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
