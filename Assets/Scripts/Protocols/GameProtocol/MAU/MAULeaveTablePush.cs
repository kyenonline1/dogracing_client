using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAULeaveTablePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU6";
        }

        public MAULeaveTablePush()
        {

        }

        public MAULeaveTablePush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Cash, IsOptional = true)]
        public long Cash { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.LeaveType, IsOptional = true)]
        public int LeaveType { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }
    }
}
