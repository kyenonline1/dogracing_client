using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG5ChangeGameStatePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG5";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.RemainTime, IsOptional = true)]
        public long RemainTime { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.GameState, IsOptional = true)]
        public byte GameState { get; set; }

        public DOG5ChangeGameStatePush()
        {
            Flag = 0;
        }

        public DOG5ChangeGameStatePush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
