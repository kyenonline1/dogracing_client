
using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITORegisterLeaveTablePush : PushBase
    {
        public XITORegisterLeaveTablePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT7";
        }

        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.IsRegisterLeaveTable, IsOptional = true)]
        public bool IsRegisterLeaveTable { get; set; }
    }
}
