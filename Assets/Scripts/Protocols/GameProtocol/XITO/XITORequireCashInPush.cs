using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITORequireCashInPush : PushBase
    {
        public XITORequireCashInPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT11";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Cash, IsOptional = true)]
        public long Cash { get; set; }
    }
}
