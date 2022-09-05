using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRJoinGameForOtherPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR0";
        }

        public PKRJoinGameForOtherPush()
        {
        }

        public PKRJoinGameForOtherPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Position, IsOptional = true)]
        public int Position { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }


        [DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }
    }
}
