using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG9_ChatPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "AIG9";
        }

        [DataMember(Code = (byte)AIG_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }

        [DataMember(Code = (byte)AIG_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        public DOG9_ChatPush()
        {
        }

        public DOG9_ChatPush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
