using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOOpenFirstCardPush : PushBase
    {
        public XITOOpenFirstCardPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT1";
        }

        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Cards, IsOptional = true)]
        public int[] HandCards { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Rank, IsOptional = true)]
        public int Rank { get; set; }
    }
}
