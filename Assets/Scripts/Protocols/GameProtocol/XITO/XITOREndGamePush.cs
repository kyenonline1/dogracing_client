using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOREndGamePush : PushBase
    {
        public XITOREndGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT10";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Winners, IsOptional = true)]
        public XitoWinner[] Winners { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Players, IsOptional = true)]
        public XitoPlayerWinCash[] Players { get; set; }
    }
}
