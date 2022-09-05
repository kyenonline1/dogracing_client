using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKROpenCardPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR6";
        }

        public PKROpenCardPush()
        {
        }

        public PKROpenCardPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Cards, IsOptional = true)]
        public int[] Cards { get; set; }
    }
}
