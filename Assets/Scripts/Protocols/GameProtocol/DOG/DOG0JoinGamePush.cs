using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG0JoinGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG0";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.Players, IsOptional = true)]
        public PlayerDog Player { get; set; }

        public DOG0JoinGamePush()
        {
            Flag = 0;
        }

        public DOG0JoinGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
