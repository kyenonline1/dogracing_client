using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRHandCardPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR4";
        }

        public PKRHandCardPush()
        {
        }

        public PKRHandCardPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.RoundName, IsOptional = true)]
        public string RoundName { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.NextPlayer, IsOptional = true)]
        public long NextPlayer { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Players, IsOptional = true)]
        public PokerPlayerHandCard[] Players { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Gamesession, IsOptional = true)]
        public long Gamesession { get; set; }

    }
    
}
