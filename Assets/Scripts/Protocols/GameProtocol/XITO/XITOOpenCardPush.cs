using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOOpenCardPush : PushBase
    {
        public XITOOpenCardPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT6";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Cards, IsOptional = true)]
        public int[] Cards { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Rank, IsOptional = true)]
        public int Rank { get; set; }
    }
}
