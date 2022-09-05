using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKREndGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR10";
        }

        public PKREndGamePush()
        {
        }

        public PKREndGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Winners, IsOptional = true)]
        public PokerWinner[] Winners { get; set; }


        [DataMember(Code = (byte)PKR_ParameterCode.PlayerAntes, IsOptional = true)]
        public PokerPlayerWinCash[] PlayerAntes { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Players, IsOptional = true)]
        public PokerPlayerWinCash[] Players { get; set; }

    }
}
