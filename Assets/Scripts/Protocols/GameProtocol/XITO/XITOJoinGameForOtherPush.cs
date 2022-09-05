using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOJoinGameForOtherPush : PushBase
    {
        public XITOJoinGameForOtherPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT0";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Position, IsOptional = true)]
        public int Position { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.NickName, IsOptional = true)]
        public string NickName { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }
    }
}
