using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAULeaveTableRegistPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU5";
        }

        public MAULeaveTableRegistPush()
        {

        }

        public MAULeaveTableRegistPush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.LeaveType, IsOptional = true)]
        public bool IsLeave { get; set; }
    }
}
