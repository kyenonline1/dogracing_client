using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUJoinGameForOtherPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU3";
        }

        public MAUJoinGameForOtherPush()
        {

        }

        public MAUJoinGameForOtherPush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.NickName, IsOptional = true)]
        public string NickName { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Position, IsOptional = true)]
        public int Position { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Cash, IsOptional = true)]
        public long Cash { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }
    }
}
