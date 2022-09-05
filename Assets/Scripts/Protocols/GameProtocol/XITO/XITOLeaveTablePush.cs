using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOLeaveTablePush : PushBase
    {
        public XITOLeaveTablePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT6";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }
    }
}
