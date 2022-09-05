using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRShowHandPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR8";
        }

        public PKRShowHandPush()
        {
        }

        public PKRShowHandPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Players, IsOptional = true)]
        public PokerPlayerHandCard[] Players { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Cards, IsOptional = true)]
        public int[] CenterCards { get; set; }
    }
}
