using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRChooseDealerPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR1";
        }

        public PKRChooseDealerPush()
        {
        }

        public PKRChooseDealerPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Players, IsOptional = true)]
        public PokerPlayerHandCard[] Players { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Dealer, IsOptional = true)]
        public long Dealer { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CardDealer, IsOptional = true)]
        public int CardDealer { get; set; }
    }
}
